﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPG.EventSourcing.Domain;
using SPG.EventSourcing.Exception;
using SPG.EventSourcing.Repository;

namespace SPG.EventSourcing.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Guid, AggregateRoot> _trackedAggregates;

        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
            _trackedAggregates = new Dictionary<Guid, AggregateRoot>();
        }

        public void Add<T>(T aggregate) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Id))
                _trackedAggregates.Add(aggregate.Id, aggregate);
            else if (_trackedAggregates[aggregate.Id] != aggregate)
                throw new ConcurrencyException($"Aggregate can't be added because it's already tracked.");
        }

        public async Task<T> GetAsync<T>(Guid id, int? expectedVersion = null) where T : AggregateRoot
        {

            T aggregate = null;
            bool mustbeAdded = false;

            if (IsTracked(id))
            {
                aggregate = (T)_trackedAggregates[id];
            }
            else
            {
                aggregate = await _repository.GetByIdAsync<T>(id);
                mustbeAdded = true;
            }

            if (expectedVersion != null && aggregate.CurrentVersion != expectedVersion)
                throw new ConcurrencyException(
                    $"The aggregate version ({aggregate.CurrentVersion}) doesn't match the expected version ({expectedVersion})");

            if (mustbeAdded)
                Add(aggregate);

            return aggregate;
        }

        private bool IsTracked(Guid id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public async Task CommitAsync()
        {
            foreach (var aggregate in _trackedAggregates.Values)
            {
                await _repository.SaveAsync(aggregate);
            }
            _trackedAggregates.Clear();
        }

    }
}
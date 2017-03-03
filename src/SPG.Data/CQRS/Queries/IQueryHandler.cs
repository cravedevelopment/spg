﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SPG.Data.EF;
using SPG.Model;

namespace SPG.WebAPI
{
    public class Seeder
    {
        public static void Seedit(string jsonData, IServiceProvider serviceProvider)
        {
            var events = JsonConvert.DeserializeObject<List<User>>(jsonData);
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                if (context.User.Any()) return;
                context.AddRange(events);
                context.SaveChanges();
            }
        }
    }
}
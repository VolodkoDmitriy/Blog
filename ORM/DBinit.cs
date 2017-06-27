using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ORM
{
    class DBinit
    {
        public class SchoolInitializer : CreateDatabaseIfNotExists<ORM.BlogModel>
        {
            protected override void Seed(BlogModel context)
            {
                var roles = new List<Roles>
            {
                new Roles { RoleId=1, Name="admin"},
                new Roles { RoleId=1, Name="user"}
                
            };
                roles.ForEach(s => context.Roles.Add(s));
                context.SaveChanges();
            }
        }
    }
}

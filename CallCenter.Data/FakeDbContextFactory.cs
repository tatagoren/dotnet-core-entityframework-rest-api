using CallCenter.Data.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Data
{
    public class FakeDbContextFactory : IDbContextFactory<CallCenterContext>
    {

        public CallCenterContext Create(DbContextFactoryOptions options)
        {
            IOptions<DatabaseSettings> opt = null;
            var builder = new DbContextOptionsBuilder<CallCenterContext>();
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CCDB_;Trusted_Connection=true;MultipleActiveResultSets=true;");
            return new CallCenterContext(opt,builder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using SYN.DBModel;
using SYN.Log.EFLog;

namespace SYN.Repository.DBContexts
{
    public class SynDbContext : DbContext
    {
        public SynDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<SystemDictionaryDBModel> SystemDictionary { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Secret_Message_Viewer.Models;

namespace Secret_Message_Viewer.Data
{
    public class AppSettingsDbContext : DbContext
    {
        public DbSet<Message> Messages {get; set;}
        public DbSet<MsgAttribute> MessageAttributes { get; set; }

        public AppSettingsDbContext(DbContextOptions<AppSettingsDbContext> options) : base(options) { }
    }
}

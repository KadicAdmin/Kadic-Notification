using KadicNotificationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.DbContexts;

public class KadicNotificationDbContext : DbContext
{
    public KadicNotificationDbContext(DbContextOptions<KadicNotificationDbContext> options) : base(options) { }
    
    public DbSet<EmailTemplateEntity> EmailTemplates { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplateEntity>().ToTable("EmailTemplates");           
    }
}
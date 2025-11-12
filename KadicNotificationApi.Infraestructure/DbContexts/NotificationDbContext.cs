using KadicNotificationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.DbContexts;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
    
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplate>()
            .HasOne(t => t.TemplateType)
            .WithMany()
            .HasForeignKey(t => t.TemplateTypeId);           
    }
}
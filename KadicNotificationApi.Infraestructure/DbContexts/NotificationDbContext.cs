using KadicNotificationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadicNotificationApi.Infraestructure.DbContexts;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
    
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<EmailTemplatesType> EmailTemplatesType { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Tablas

        modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplates");
        modelBuilder.Entity<EmailTemplatesType>().ToTable("EmailTemplateTypes");

        modelBuilder.Entity<EmailTemplate>()
      .HasOne(e => e.Type)
      .WithMany()
      .HasForeignKey(e => e.EmailTemplatesTypeId);
    }


}

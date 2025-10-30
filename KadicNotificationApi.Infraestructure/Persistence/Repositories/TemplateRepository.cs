using KadicNotificationApi.Infraestructure.Persistence.DbContexts;
using KadicNotificationApi.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace KadicNotificationApi.Infraestructure.Persistence.Repositories;

public class TemplateRepository: ITemplateRepository
{
    private readonly KadicNotificationDbContext _db;
    public TemplateRepository(KadicNotificationDbContext db)
    {
        _db = db; 
    }


    public async Task<EmailTemplateEntity> GetByIdAsync(int templateId)
    {
        var row = await _db.EmailTemplates
            .Where(t => t.Id == templateId)
            .FirstOrDefaultAsync();

       return row;

    }
}


//flujo : el repositorio de templates permite obtener una plantilla de email por su id,
//utilizando Entity Framework Core para acceder a la base de datos a través del contexto KadicNotificationDbContext.
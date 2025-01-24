using ProductTracker.Core.DTO.Response;
using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<IReadOnlyList<TemplateResponseDTOs>> GetAllTemplates();
        Task<TempFormat> GetTemplatesById(int id);
        Task<string> SaveTemplateFormat(int Id, string CreatedBy, TempFormat entity);
    }
}

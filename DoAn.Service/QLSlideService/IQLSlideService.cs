using DoAn.Domain.Entities;
using DoAn.Service.Core;
using DoAn.Service.Dtos.FileManagerDto;
using DoAn.Service.Dtos.QLSlideDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.QLSlideService
{
    public interface IQLSlideService : IService<QLSlide>
    {
        Task<List<QLSlideDto>> GetAllFParent();

        Task<List<QLSlideDto>> GetFileByParent(Guid idParent);

        Task<List<QLSlide>> DeleteSoftFile(Guid IdItem);

        Task<List<QLSlide>> DeleteFile(Guid IdItem);
        List<QLSlide> GetFileById(Guid idParent);
        List<QLSlideDto> GetByType(string Type);
    }
}

using DoAn.Domain.Entities;
using DoAn.Service.Core;
using DoAn.Service.Dtos.FileManagerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.FileManagerService
{
    public interface IFileManagerService : IService<FileManager>
    {
        Task<List<FileManagerDto>> GetAllFParent();

        Task<List<FileManagerDto>> GetFileByParent(Guid idParent);

        Task<List<FileManager>> DeleteSoftFile(Guid IdItem);

        Task<List<FileManager>> DeleteFile(Guid IdItem);
        List<FileManager> GetFileById(Guid idParent);
        Task<List<FileManagerDto>> GetAllFile();
    }
}

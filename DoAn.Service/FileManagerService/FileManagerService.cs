using DoAn.Domain.Entities;
using DoAn.Repository.FileManagerRepository;
using DoAn.Service.Core;
using DoAn.Service.Dtos.FileManagerDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.FileManagerService
{
    public class FileManagerService : Service<FileManager>, IFileManagerService
    {
        private readonly IFileManagerRepository _fileManagerRepository;

        public FileManagerService(IFileManagerRepository fileManagerRepository) : base(fileManagerRepository) 
        {
            _fileManagerRepository = fileManagerRepository;
        }

        public async Task<List<FileManager>> DeleteFile(Guid IdItem)
        {
            var listDelteFile = new List<FileManager>();
            var query = await (from q in _fileManagerRepository.GetQueryable()
                               where q.Id == IdItem
                               select q).FirstOrDefaultAsync();

            if (query != null)
            {

                listDelteFile.Add(query);
            }
            return listDelteFile;
        }

        public async Task<List<FileManager>> DeleteSoftFile(Guid IdItem)
        {
            var listDelteFile = new List<FileManager>();
            var query = await (from q in _fileManagerRepository.GetQueryable()
                               where q.Id == IdItem
                               select q).FirstOrDefaultAsync();

            if (query != null)
            {
           
                listDelteFile.Add(query);
                
            }
            return listDelteFile;
        }

        public Task<List<FileManagerDto>> GetAllFile()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FileManagerDto>> GetAllFParent()
        {
            var query = from q in _fileManagerRepository.GetQueryable()
                         where q.ParentId == null
                         select new FileManagerDto
                         {
                             Id = q.Id,
                             ParentId = q.ParentId,
                             Name = q.Name,
                             Mine = q.Mine,
                             Path = q.Path,
                         };
            return await query.ToListAsync();
        }

        public List<FileManager> GetFileById(Guid idParent)
        {
            var query = _fileManagerRepository.GetQueryable()
                        .Where(q => q.ParentId == idParent)
                        .ToList();
            return query;
        }

        public async Task<List<FileManagerDto>> GetFileByParent(Guid idParent)
        {
            var query = from q in _fileManagerRepository.GetQueryable()
                         where q.ParentId == idParent 
                         select new FileManagerDto
                         {
                             Id = q.Id,

                             ParentId = q.ParentId,
                             Name = q.Name,
                             Mine = q.Mine,
                             Path = q.Path,
                         };
            return await query.ToListAsync();

        }
    }
}

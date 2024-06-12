using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.FileManagerRepository;
using DoAn.Repository.QLSlideRepository;
using DoAn.Service.Core;
using DoAn.Service.Dtos.FileManagerDto;
using DoAn.Service.Dtos.QLSlideDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.QLSlideService
{
    public class QLSlideService : Service<QLSlide>, IQLSlideService
    {
        private readonly IQLSlideRepository _qlSlideRepository;

        public QLSlideService(IQLSlideRepository qLSlideRepository) : base(qLSlideRepository)
        {
            _qlSlideRepository = qLSlideRepository;
        }

        public async Task<List<QLSlide>> DeleteFile(Guid IdItem)
        {
            var listDelteFile = new List<QLSlide>();
            var query = await(from q in _qlSlideRepository.GetQueryable()
                              where q.Id == IdItem
                              select q).FirstOrDefaultAsync();

            if (query != null)
            {

                listDelteFile.Add(query);
            }
            return listDelteFile;
        }

        public async Task<List<QLSlide>> DeleteSoftFile(Guid IdItem)
        {
            var listDelteFile = new List<QLSlide>();
            var query = await(from q in _qlSlideRepository.GetQueryable()
                              where q.Id == IdItem
                              select q).FirstOrDefaultAsync();

            if (query != null)
            {

                listDelteFile.Add(query);

            }
            return listDelteFile;
        }

        public async Task<List<QLSlideDto>> GetAllFParent()
        {
            var query = from q in _qlSlideRepository.GetQueryable()
                        where q.ParentId == null
                        select new QLSlideDto
                        {
                            Id = q.Id,
                            ParentId = q.ParentId,
                            Name = q.Name,
                            Mine = q.Mine,
                            Path = q.Path,
                            Type = q.Type,
                            TenBanner = q.TenBanner,
                            
                        };
            return await query.ToListAsync();
        }

        public List<QLSlideDto> GetByType(string Type)
        {
            var query = from QLSlidebtl in _qlSlideRepository.GetQueryable()
                        where QLSlidebtl.Type == Type
                        select new QLSlideDto
                        {
                            Id = QLSlidebtl.Id,

                            ParentId = QLSlidebtl.ParentId,
                            Name = QLSlidebtl.Name,
                            Mine = QLSlidebtl.Mine,
                            Path = QLSlidebtl.Path,
                            Type = QLSlidebtl.Type,
                            TenBanner = QLSlidebtl.TenBanner,   
                        };
            return query.ToList();
        }

        public List<QLSlide> GetFileById(Guid idParent)
        {
            var query = _qlSlideRepository.GetQueryable()
                        .Where(q => q.ParentId == idParent)
                        .ToList();
            return query;
        }

        public async Task<List<QLSlideDto>> GetFileByParent(Guid idParent)
        {

            var query = from q in _qlSlideRepository.GetQueryable()
                        where q.ParentId == idParent
                        select new QLSlideDto
                        {
                            Id = q.Id,

                            ParentId = q.ParentId,
                            Name = q.Name,
                            Mine = q.Mine,
                            Path = q.Path,
                            Type = q.Type,
                            TenBanner = q.TenBanner,
                        };
            return await query.ToListAsync();
        }
    }

        
    
}

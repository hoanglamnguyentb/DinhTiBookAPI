using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.DanhMucRepository;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.DanhMucDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.DanhMucService
{
    public class DanhMucService : Service<DanhMuc>, IDanhMucService
    {
        private readonly IDanhMucRepository _danhmucRepository;

        public DanhMucService(IDanhMucRepository danhMucRepository) : base(danhMucRepository)
        {
            _danhmucRepository = danhMucRepository;
        }
        public ResponseWithDataDto<DanhMuc> AddDanhMuc(DanhMucDto danhmuc)
        {
            try
            {
                var data = _danhmucRepository.Add(danhmuc);
                return data != null ? new ResponseWithDataDto<DanhMuc>()
                {
                    Data = data,
                    Message = "Thêm danh mục thành công",
                    Status = "Success",

                } : new ResponseWithDataDto<DanhMuc>()
                {
                    Data = null,
                    Message = "Thêm danh mục lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<DanhMuc>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "Thêm danh mục không hợp lệ"
                };
            }
        }
        public ResponseWithMessageDto DeleteDanhMuc(Guid id)
        {
            try
            {
                var danhmuc = _danhmucRepository.GetById(id);
                if(danhmuc == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại danh mục",
                        Status = "Error"
                    };
                }
                var data = _danhmucRepository.Delete(danhmuc);
                return new ResponseWithMessageDto()
                {
                    Message = "Xóa Danh mục thành công",
                    Status = "Success"
                };
            }
            catch(Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = "Error"
                };
            }
        }

        public ResponseWithDataDto<DanhMucDto> FindById(Guid id)
        {
            try
            {
                var data = from tblDanhMuc in _danhmucRepository.GetQueryable()
                           where tblDanhMuc.Id == id
                           select new DanhMucDto
                           {
                               Id = tblDanhMuc.Id,
                               MaCategory = tblDanhMuc.MaCategory,
                               CategoryName = tblDanhMuc.CategoryName,
                               MoTa = tblDanhMuc.MoTa,  
                           };
                return new ResponseWithDataDto<DanhMucDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = "Success",
                    Message = "Lấy danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<DanhMucDto>()
                {
                    Data = null,
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }
        public ResponseWithDataDto<PagedList<DanhMucDto>> GetDataByPage(DanhMucSearchDto searchDto)
        {
            try
            {
                var query = from tblDanhMuc in _danhmucRepository.GetQueryable()
                            select new DanhMucDto
                            {
                                Id = tblDanhMuc.Id,
                                MaCategory = tblDanhMuc. MaCategory,
                                CategoryName = tblDanhMuc.CategoryName,
                                MoTa = tblDanhMuc.MoTa,
                            };

                if(searchDto != null)
                {
                    if (!string.IsNullOrEmpty(searchDto.CategoryNameFilter))
                    {
                        query = query.Where(record => record.CategoryName.Trim().ToLower().Contains(searchDto.CategoryNameFilter.Trim().ToLower()));
                    }
                }
                var result = PagedList<DanhMucDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<DanhMucDto>>()
                {
                    Data = result,
                    Status = "Success",
                    Message = "Lấy danh mục thành công"
                };
            }
            catch(Exception ex)
            {
                return new ResponseWithDataDto<PagedList<DanhMucDto>>()
                {
                    Data = null,
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto UpdateDanhMuc(Guid id, DanhMucDto danhmuc)
        {
            try
            {
                var danhmucnew = _danhmucRepository.GetById(id);
                if (danhmucnew == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy danh mục",
                        Status = "Error",
                    };
                }
                else
                {
                    danhmucnew.CategoryName = danhmuc.CategoryName;
                    danhmucnew.MaCategory = danhmuc.MaCategory;
                    _danhmucRepository.Edit(danhmucnew);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật danh mục thành công",
                        Status = "Success",
                    };
                }

            }  
            catch (Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = "Error"
                };
            }
        }
    }
}

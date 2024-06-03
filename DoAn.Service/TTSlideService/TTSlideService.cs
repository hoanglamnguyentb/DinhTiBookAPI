using DoAn.Domain.Entities;
using DoAn.Repository.SanPhamRepository;
using DoAn.Repository.TTSlideRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos.TTSlideDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.TTSlideService
{
    public class TTSlideService : Service<TTSlide>, ITTSlideService
    {
        private readonly ITTSlideRepository _tTSlideRepository;

        public TTSlideService(ITTSlideRepository tTSlideRepository) : base(tTSlideRepository) 
        {
            _tTSlideRepository = tTSlideRepository;
        }

        public ResponseWithDataDto<TTSlide> Add(TTSlideDto ttSlide)
        {
            try
            {
                var data = _tTSlideRepository.Add(ttSlide);
                return data != null ? new ResponseWithDataDto<TTSlide>()
                {
                    Data = data,
                    Message = "Thêm sản phẩm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<TTSlide>()
                {
                    Data = null,
                    Message = "Thêm sản phẩm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<TTSlide>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithMessageDto Delete(Guid id)
        {
            try
            {
                var sanpham = _tTSlideRepository.GetById(id);
                if (sanpham == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại sản phẩm",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _tTSlideRepository.Delete(sanpham);
                return new ResponseWithMessageDto()
                {
                    Message = "Xóa sản phẩm thành công",
                    Status = StatusConstant.SUCCESS
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithDataDto<TTSlideDto> FindById(Guid id)
        {
            try
            {
                var data = from tblTTSlide in _tTSlideRepository.GetQueryable()
                           where tblTTSlide.Id == id
                           select new TTSlideDto
                           {
                               Id = tblTTSlide.Id,
                               ThongTin1 = tblTTSlide.ThongTin1,
                               ThongTin2 = tblTTSlide.ThongTin2,
                               ThongTin3 = tblTTSlide.ThongTin3,
                               Type = tblTTSlide.Type,
                               CreatedBy = tblTTSlide.CreatedBy,
                               CreatedDate= tblTTSlide.CreatedDate, 
                               CreatedID = tblTTSlide.CreatedID,
                               UpdatedBy = tblTTSlide.UpdatedBy,
                               UpdatedDate= tblTTSlide.UpdatedDate,
                               UpdatedID = tblTTSlide.UpdatedID,    
                               IsDelete = tblTTSlide.IsDelete,
                               DeleteTime = tblTTSlide.DeleteTime,
                               DeleteId = tblTTSlide.DeleteId,
                               DeleteBy = tblTTSlide.DeleteBy
                           };
                return new ResponseWithDataDto<TTSlideDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy sản phẩm thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<TTSlideDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public TTSlide GetById(Guid id)
        {
            return _tTSlideRepository.GetById(id);
        }

        public ResponseWithDataDto<PagedList<TTSlideDto>> GetDataByPage(TTSlideSearchDto searchDto)
        {
            try
            {
                var query = from tblTTSlide in _tTSlideRepository.GetQueryable()

                            select new TTSlideDto
                            {
                                Id = tblTTSlide.Id,
                                ThongTin1 = tblTTSlide.ThongTin1,
                                ThongTin2 = tblTTSlide.ThongTin2,
                                ThongTin3 = tblTTSlide.ThongTin3,
                                Type = tblTTSlide.Type,
                                CreatedBy = tblTTSlide.CreatedBy,
                                CreatedDate = tblTTSlide.CreatedDate,
                                CreatedID = tblTTSlide.CreatedID,
                                UpdatedBy = tblTTSlide.UpdatedBy,
                                UpdatedDate = tblTTSlide.UpdatedDate,
                                UpdatedID = tblTTSlide.UpdatedID,
                                IsDelete = tblTTSlide.IsDelete,
                                DeleteTime = tblTTSlide.DeleteTime,
                                DeleteId = tblTTSlide.DeleteId,
                                DeleteBy = tblTTSlide.DeleteBy
                            };

                if (searchDto != null)
                {
                    if (!string.IsNullOrEmpty(searchDto.TenFilter))
                    {
                        var TenSearch = searchDto.TenFilter.RemoveAccentsUnicode();
                    }
                }
                var result = PagedList<TTSlideDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<TTSlideDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<TTSlideDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto Update(Guid id, TTSlideDto ttSlide)
        {
            try
            {
                var ttNew = _tTSlideRepository.GetById(id);
                if (ttNew == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy sản phẩm",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {
                    ttNew.Type = ttNew.Type;
                    ttNew.ThongTin1 = ttNew.ThongTin1;
                    ttNew.ThongTin2 = ttNew.ThongTin2;
                    ttNew.ThongTin3 = ttNew.ThongTin3;
                  
                    _tTSlideRepository.Edit(ttNew);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật danh mục thành công",
                        Status = StatusConstant.SUCCESS,
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

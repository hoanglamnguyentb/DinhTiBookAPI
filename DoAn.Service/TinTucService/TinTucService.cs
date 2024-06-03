using DoAn.Domain.Entities;
using DoAn.Repository.NhomDoTuoiRepository;
using DoAn.Repository.SanPhamRepository;
using DoAn.Repository.TinTucRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhomDoTuoiDto;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos.TinTucDto;
using DoAn.Service.SanPhamService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.TinTucService
{
    public class TinTucService : Service<TinTuc>, ITinTucService
    {
        private readonly ITinTucRepository _tinTucRepository;

        public TinTucService
            (
        ITinTucRepository tinTucRepository
            ) : base(tinTucRepository)
        {
            _tinTucRepository = tinTucRepository;
        }

        public ResponseWithMessageDto Delete(Guid id)
        {
            try
            {
                var tintuc = _tinTucRepository.GetById(id);
                if (tintuc == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại thông tin",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _tinTucRepository.Delete(tintuc);
                return new ResponseWithMessageDto()
                {
                    Message = "Xóa thành công",
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

        public ResponseWithDataDto<TinTucDto> FindById(Guid id)
        {
            try
            {
                var data = from tblTinTuc in _tinTucRepository.GetQueryable()
                           where tblTinTuc.Id == id
                           select new TinTucDto
                           {   
                               Id = tblTinTuc.Id,
                               TieuDe = tblTinTuc.TieuDe,
                               DanhMuc = tblTinTuc.DanhMuc,
                               MoTa = tblTinTuc.MoTa,
                               NoiDung = tblTinTuc.NoiDung,
                               HinhAnh = tblTinTuc.HinhAnh,
                               isNoiBat = tblTinTuc.isNoiBat,
                               LuotXem = tblTinTuc.LuotXem,
                               
                           };
                return new ResponseWithDataDto<TinTucDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<TinTucDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<TinTucDto>> GetDataByPage(TinTucSearchDto searchDto)
        {
            try
            {
                var query = from tblTinTuc in _tinTucRepository.GetQueryable()
                            select new TinTucDto
                            {
                                Id = tblTinTuc.Id,
                                TieuDe = tblTinTuc.TieuDe,
                                DanhMuc = tblTinTuc.DanhMuc,
                                MoTa = tblTinTuc.MoTa,
                                NoiDung = tblTinTuc.NoiDung,
                                HinhAnh = tblTinTuc.HinhAnh,
                                isNoiBat = tblTinTuc.isNoiBat,
                                NgayTao = tblTinTuc.CreatedDate,
                                LuotXem = tblTinTuc.LuotXem,
                            };

                if (searchDto != null)
                {
                    if (searchDto.TieuDeFilter != null)
                    {
                        query = query.Where(record => record.TieuDe.Trim().ToLower().Contains(searchDto.TieuDeFilter.Trim().ToLower()));
                    }
                    if (searchDto.DanhMucFilter != null)
                    {
                        query = query.Where(record => record.DanhMuc.Trim().ToLower().Contains(searchDto.DanhMucFilter.Trim().ToLower()));
                    }
                }
                var result = PagedList<TinTucDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<TinTucDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<TinTucDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto Update(Guid id, TinTucDto tintuc)
        {
            try
            {
                var DataTinTuc = _tinTucRepository.GetById(id);
                if (DataTinTuc == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {

                    DataTinTuc.TieuDe = tintuc.TieuDe;
                    DataTinTuc.DanhMuc = tintuc.DanhMuc;
                    DataTinTuc.MoTa = tintuc.MoTa;
                    DataTinTuc.NoiDung = tintuc.NoiDung;
                    DataTinTuc.HinhAnh = tintuc.HinhAnh;
                    DataTinTuc.isNoiBat = tintuc.isNoiBat;
                    DataTinTuc.LuotXem = tintuc.LuotXem;
                    _tinTucRepository.Edit(DataTinTuc);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật thành công",
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

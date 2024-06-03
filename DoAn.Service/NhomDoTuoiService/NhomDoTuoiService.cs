using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Repository.NhomDoTuoiRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos.NhomDoTuoiDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.NhomDoTuoiService
{
    public class NhomDoTuoiService : Service<NhomDoTuoi>, INhomDoTuoiService
    {
        private readonly INhomDoTuoiRepository _nhomDoTuoiRepository;

        public NhomDoTuoiService(INhomDoTuoiRepository nhomDoTuoiRepository) : base(nhomDoTuoiRepository)
        {
            _nhomDoTuoiRepository = nhomDoTuoiRepository;
        }

        public ResponseWithDataDto<NhomDoTuoi> Add(NhomDoTuoiDto nhomDoTuoi)
        {
            try
            {
                var data = _nhomDoTuoiRepository.Add(nhomDoTuoi);
                return data != null ? new ResponseWithDataDto<NhomDoTuoi>()
                {
                    Data = data,
                    Message = "Thêm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<NhomDoTuoi>()
                {
                    Data = null,
                    Message = "Thêm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<NhomDoTuoi>()
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
                var nhomdotuoi = _nhomDoTuoiRepository.GetById(id);
                if (nhomdotuoi == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _nhomDoTuoiRepository.Delete(nhomdotuoi);
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

        public ResponseWithDataDto<NhomDoTuoiDto> FindById(Guid id)
        {
            try
            {
                var data = from tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                           where tblNhomDoTuoi.Id == id
                           select new NhomDoTuoiDto
                           {
                               Id = tblNhomDoTuoi.Id,
                               MaNhomDoTuoi = tblNhomDoTuoi.MaNhomDoTuoi,
                               TenNhom = tblNhomDoTuoi.TenNhom,
                               MoTaDoTuoi = tblNhomDoTuoi.MoTaDoTuoi
                         
                           };
                return new ResponseWithDataDto<NhomDoTuoiDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<NhomDoTuoiDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<NhomDoTuoiDto>> GetDataByPage(NhomDoTuoiSearchDto searchDto)
        {
            try
            {
                var query = from tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                            select new NhomDoTuoiDto
                            {
                                Id = tblNhomDoTuoi.Id,
                                MaNhomDoTuoi = tblNhomDoTuoi.MaNhomDoTuoi,
                                TenNhom = tblNhomDoTuoi.TenNhom,
                                MoTaDoTuoi = tblNhomDoTuoi.MoTaDoTuoi
                            };

                if (searchDto != null)
                {
                    if (searchDto.TenNhomFilter != null)
                    {
                        query = query.Where(record => record.TenNhom.Trim().ToLower().Contains(searchDto.TenNhomFilter.Trim().ToLower()));
                    }
                    if (searchDto.MaNhomFilter != null)
                    {
                        query = query.Where(record => record.MaNhomDoTuoi.Trim().ToLower().Contains(searchDto.MaNhomFilter.Trim().ToLower()));
                    }
                }
                var result = PagedList<NhomDoTuoiDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<NhomDoTuoiDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<NhomDoTuoiDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto Update(Guid id, NhomDoTuoiDto nhomDoTuoi)
        {
            try
            {
                var DatanhomDoTuoi = _nhomDoTuoiRepository.GetById(id);
                if (DatanhomDoTuoi == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {
                    
                    DatanhomDoTuoi.TenNhom = nhomDoTuoi.TenNhom;
                    DatanhomDoTuoi.MaNhomDoTuoi = nhomDoTuoi.MaNhomDoTuoi;
                    DatanhomDoTuoi.MoTaDoTuoi = nhomDoTuoi.MoTaDoTuoi;
                    _nhomDoTuoiRepository.Edit(DatanhomDoTuoi);
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

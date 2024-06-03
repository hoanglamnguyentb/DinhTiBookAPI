using DoAn.Domain.Entities;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.NhaXuatBanDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.NhaXuatBanService
{
    public class NhaXuatBanService : Service<NhaXuatBan>, INhaXuatBanService
    {
        private readonly INhaXuatBanRepository _nhaXuatBanRepository;

        public NhaXuatBanService(INhaXuatBanRepository nhaXuatBanRepository) : base(nhaXuatBanRepository) 
        {
            _nhaXuatBanRepository = nhaXuatBanRepository;
        }

        public ResponseWithDataDto<NhaXuatBan> AddNhaXuatBan(NhaXuatBanDto nhaXuatBan)
        {
            try
            {
                var data = _nhaXuatBanRepository.Add(nhaXuatBan);
                return data != null ? new ResponseWithDataDto<NhaXuatBan>()
                {
                    Data = data,
                    Message = "Thêm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<NhaXuatBan>()
                {
                    Data = null,
                    Message = "Thêm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<NhaXuatBan>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithMessageDto DeleteNhaXuatBan(Guid id)
        {
            try
            {
                var nhaxuatban = _nhaXuatBanRepository.GetById(id);
                if (nhaxuatban == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _nhaXuatBanRepository.Delete(nhaxuatban);
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

        public ResponseWithDataDto<NhaXuatBanDto> FindById(Guid id)
        {
            try
            {
                var data = from tblNXB in _nhaXuatBanRepository.GetQueryable()
                           where tblNXB.Id == id
                           select new NhaXuatBanDto
                           {
                               Id = tblNXB.Id,
                               TenNXB = tblNXB.TenNXB,
                               MaNXB = tblNXB.MaNXB,
                           };
                return new ResponseWithDataDto<NhaXuatBanDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<NhaXuatBanDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<NhaXuatBanDto>> GetDataByPage(NhaXuatBanSearchDto searchDto)
        {
            try
            {
                var query = from tblNXB in _nhaXuatBanRepository.GetQueryable()
                            select new NhaXuatBanDto
                            {
                                Id = tblNXB.Id,
                                TenNXB = tblNXB.TenNXB,
                                MaNXB = tblNXB.MaNXB
                            };

                if (searchDto != null)
                {
                    if (searchDto.TenNXBFilter != null)
                    {
                        query = query.Where(record => record.TenNXB.Trim().ToLower().Contains(searchDto.TenNXBFilter.Trim().ToLower()));
                    }
                }
                var result = PagedList<NhaXuatBanDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<NhaXuatBanDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<NhaXuatBanDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto UpdateNhaXuatBan(Guid id, NhaXuatBanDto nhaXuatBan)
        {
            try
            {
                var nxbNew = _nhaXuatBanRepository.GetById(id);
                if (nxbNew == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy NXB",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {
                    nxbNew.TenNXB = nhaXuatBan.TenNXB;
                    nxbNew.MaNXB = nhaXuatBan.MaNXB;
                    _nhaXuatBanRepository.Edit(nxbNew);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật NXB thành công",
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

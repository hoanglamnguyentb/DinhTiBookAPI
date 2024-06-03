using DoAn.Domain.Entities;
using DoAn.Repository.DanhMucTinTucRepository;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.DanhMucTinTucDto;
using DoAn.Service.Dtos.NhaXuatBanDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.DanhMucTinTucService
{
    public class DanhMucTinTucService : Service<DanhMucTinTuc> , IDanhMucTinTucService
    {
        private readonly IDanhMucTinTucRepository _danhMucTinTucRepository;

        public DanhMucTinTucService(IDanhMucTinTucRepository danhMucTinTucRepository) : base(danhMucTinTucRepository)
        {
            _danhMucTinTucRepository = danhMucTinTucRepository;
        }

        public ResponseWithMessageDto Delete(Guid id)
        {
            try
            {
                var danhmuctt = _danhMucTinTucRepository.GetById(id);
                if (danhmuctt == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _danhMucTinTucRepository.Delete(danhmuctt);
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

        public ResponseWithDataDto<DanhMucTinTucDto> FindById(Guid id)
        {
            try
            {
                var data = from tblDanhMuctt in _danhMucTinTucRepository.GetQueryable()
                           where tblDanhMuctt.Id == id
                           select new DanhMucTinTucDto
                           {
                               Id = tblDanhMuctt.Id,
                               MaDanhMuc = tblDanhMuctt.MaDanhMuc,
                               TenDanhMuc = tblDanhMuctt.TenDanhMuc,
                               isHienThi = tblDanhMuctt.isHienThi,
                           };
                return new ResponseWithDataDto<DanhMucTinTucDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<DanhMucTinTucDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<DanhMucTinTucDto>> GetDataByPage(DanhMucTinTucSearchDto searchDto)
        {
            try
            {
                var query = from tblDanhMuctt in _danhMucTinTucRepository.GetQueryable()
                            select new DanhMucTinTucDto
                            {
                                Id = tblDanhMuctt.Id,
                                MaDanhMuc = tblDanhMuctt.MaDanhMuc,
                                TenDanhMuc = tblDanhMuctt.TenDanhMuc,
                                isHienThi = tblDanhMuctt.isHienThi
                            };

                if (searchDto != null)
                {
                    if (searchDto.TenDanhMucFilter != null)
                    {
                        query = query.Where(record => record.TenDanhMuc.Trim().ToLower().Contains(searchDto.TenDanhMucFilter.Trim().ToLower()));
                    }
                    if (searchDto.MaDanhMucFilter != null)
                    {
                        query = query.Where(record => record.MaDanhMuc.Trim().ToLower().Contains(searchDto.MaDanhMucFilter.Trim().ToLower()));
                    }
                }
                var result = PagedList<DanhMucTinTucDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<DanhMucTinTucDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<DanhMucTinTucDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto Update(Guid id, DanhMucTinTucDto tintuc)
        {
            try
            {
                var danhmucttNew = _danhMucTinTucRepository.GetById(id);
                if (danhmucttNew == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {
                    danhmucttNew.MaDanhMuc = tintuc.MaDanhMuc;
                    danhmucttNew.TenDanhMuc = tintuc.TenDanhMuc;
                    danhmucttNew.isHienThi = tintuc.isHienThi;
                    _danhMucTinTucRepository.Edit(danhmucttNew);
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

using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.TTSlideDto;

namespace DoAn.Service.TTSlideService
{
    public interface ITTSlideService : IService<TTSlide> 
    {
        ResponseWithDataDto<PagedList<TTSlideDto>> GetDataByPage(TTSlideSearchDto searchDto);
        ResponseWithDataDto<TTSlide> Add(TTSlideDto ttSlide);
        ResponseWithMessageDto Delete(Guid id);
        ResponseWithMessageDto Update(Guid id, TTSlideDto ttSlide);
        ResponseWithDataDto<TTSlideDto> FindById(Guid id);
        TTSlide GetById(Guid id);
    }
}

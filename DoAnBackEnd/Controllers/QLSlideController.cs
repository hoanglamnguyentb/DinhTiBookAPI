using DoAn.Service.Constants;

using DoAn.Service.Dtos;
using DoAn.Service.QLSlideService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAn.Service.Dtos.QLSlideDto;
using DoAn.Domain.Entities;
using DoAnBackEnd.Model.FileManagerVM;
using System.Reflection;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QLSlideController : ControllerBase
    {
        private readonly IQLSlideService _qlSlideService;
        private readonly string[] AcceptMine = ["image/png", "image/jpg", "image/jpeg", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/pdf", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "audio/mpeg", "video/mp4", "video/mpeg", "image/webp"];
        private readonly string BasePath = "Uploads/FileManager/Slide";
        private readonly string[] AcceptType = [".png", ".jpg", ".jpeg", ".doc", ".docx", ".xlsx", ".xls", ".pdf", ".mp3", ".mp4", ".mpeg", ".webp"];

        public QLSlideController(IQLSlideService qlSlideService) 
        {
            _qlSlideService = qlSlideService;
        }

        [HttpGet("get")]

        public async Task<IActionResult> GetFile()
        {
            try
            {
                // xử lý logic
                var rs = await _qlSlideService.GetAllFParent();
                if (rs != null)
                {
                    return StatusCode(StatusCodes.Status200OK,
                    new ResponseWithDataDto<List<QLSlideDto>> { Status = StatusConstant.SUCCESS, Message = "Lấy file thành công", Data = rs });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Không tồn tại dữ liệu" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpGet("GetByType")]
        public async Task<IActionResult> GetByType([FromQuery] string? Type)
        {
            try
            {
                // xử lý logic
                var rs =  _qlSlideService.GetByType(Type);
                if (rs != null)
                {
                    return StatusCode(StatusCodes.Status200OK,
                    new ResponseWithDataDto<List<QLSlideDto>> { Status = StatusConstant.SUCCESS, Message = "Lấy file thành công", Data = rs });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Không tồn tại dữ liệu" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }


        /// <summary>
        /// Lấy ra file ad folder theo idParent
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        [HttpGet("getbyParentID")]

        public async Task<IActionResult> GetFileByParaent([FromQuery] Guid parentid)
        {
            try
            {
                var data = await _qlSlideService.GetFileByParent(parentid);
                return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<List<QLSlideDto>> { Status = StatusConstant.SUCCESS, Data = data, Message = "Lấy file và folder thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        [HttpPost("Upload")]
/*        [Authorize]*/
        public async Task<IActionResult> UploadsFile([FromForm] UploadVM file, [FromQuery] Guid? ParentID,  string? Type, string? TenBanner)
        {
            try
            {
                if (file.Files != null && file.Files.Count() > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FileManager", "Slide");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    foreach (var item in file.Files)
                    {
                        var fileName = item.FileName;
                        string fileExtension = Path.GetExtension(fileName);
                        //valid file upload

                        //1. Valid đuôi file
                        if (!AcceptType.Any(x => x.Equals(fileExtension)) || !AcceptMine.Any(x => x.Equals(item.ContentType)))
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tập tin có định dạng không được hỗ trợ" });
                        }

                        //2. Kiểm tra xem tên tệp chứa chuỗi "../" hay không
                        if (fileName.Contains("../"))
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tên tập tin có chứa ký tự không hợp lệ." });
                        }

                        //3.Kiểm tra tên tệp chứa ký tự đặc biệt khác
                        char[] invalidChars = Path.GetInvalidFileNameChars();
                        foreach (char c in invalidChars)
                        {
                            if (fileName.Contains(c))
                            {
                                return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tên tập tin có chứa ký tự không hợp lệ." });
                            }
                        }

                        //Thêm unique file name tránh ghi đè
                        string uniqueFileName = GetUniqueFileName(fileName);
                        string filePath = Path.Combine(folderPath, uniqueFileName);
                        string shortPath = Path.Combine("Uploads/FileManager/Slide", uniqueFileName);
                        //nếu có thư mục cha
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyToAsync(stream);
                        }

                        var fileNew = new QLSlide()
                        {
                            ParentId = ParentID,
                            Name = uniqueFileName,
                            Path = shortPath,
                            Mine = item.ContentType,
                            Type = Type,
                            TenBanner = TenBanner 
                        };
                        await _qlSlideService.Create(fileNew);
                        // Lưu tệp vào thư mục
                        using (var stream = new FileStream(Path.Combine(folderPath, uniqueFileName), FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                        return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Tải tập tin thành công" });
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tải lên tập tin không thành công" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Vui lòng chọn tập tin cần tải lên." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        static string GetUniqueFileName(string fileName)
        {
            string directory = Path.Combine(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);

            // Thêm một hậu tố duy nhất dựa trên thời gian hiện tại
            string uniqueSuffix = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Tạo tên tệp mới
            string uniqueFileName = $"{fileNameWithoutExtension}_{uniqueSuffix}{fileExtension}";

            // Kiểm tra xem tên tệp đã tồn tại hay chưa, nếu có thì thử lại với một hậu tố khác
            while (System.IO.File.Exists(Path.Combine(directory, uniqueFileName)))
            {
                uniqueSuffix = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                uniqueFileName = $"{fileNameWithoutExtension}_{uniqueSuffix}{fileExtension}";
            }

            return uniqueFileName;
        }

        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            string filePath = Path.Combine(BasePath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FileManager", "Slide");
            string contentType = GetContentType(fileName);
            string rootPath = Path.Combine(folderPath, fileName);
            return PhysicalFile(rootPath, contentType);
        }

        private string GetContentType(string fileName)
        {
            // Xác định loại MIME dựa trên phần mở rộng của tên tệp
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (fileExtension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".doc":
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".webp":
                    return "image/webp"; // Loại MIME cho tệp .webp
                default:
                    return "application/octet-stream"; // Loại MIME mặc định nếu không phát hiện được
            }
        }
        [HttpDelete("deleteSoft")]
        [Authorize]
        public async Task<IActionResult> DeleteSoft([FromBody] Guid IDItem)
        {
            try
            {
                var fileorFolder = await _qlSlideService.DeleteSoftFile(IDItem);
                if (fileorFolder == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Không tồn tại file" });
                }
                else
                {
                    await _qlSlideService.Update(fileorFolder);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Xóa file thành công" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }


        /// <summary>
        /// Xóa cứng
        /// </summary>
        /// <param name="IDItem"></param>
        /// <returns></returns>
		[HttpDelete("delete")]

        public async Task<IActionResult> DeleteFile(Guid IDItem)
        {
            try
            {
                var fileorFolder = await _qlSlideService.DeleteFile(IDItem);
                if (fileorFolder == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Không tồn tại file hoặc folder" });
                }
                else
                {
                    foreach (var item in fileorFolder)
                    {
                        await _qlSlideService.Delete(item);
                    }
                    //await _fileManagerService.Delete(fileorFolder);
                    return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Xóa file Hoặc folder thành công" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }
    }
}

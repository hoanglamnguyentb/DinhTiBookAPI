using DoAn.Domain.Entities;
using DoAn.Service.Constants;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.FileManagerDto;
using DoAn.Service.FileManagerService;
using DoAnBackEnd.Model.FileManagerVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly IFileManagerService _fileManagerService;
        private readonly string[] AcceptMine = ["image/png", "image/jpg", "image/jpeg", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/pdf", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "audio/mpeg", "video/mp4", "video/mpeg", "image/webp"];
        private readonly string BasePath = "Uploads/FileManager";
        private readonly string[] AcceptType = [".png", ".jpg", ".jpeg", ".doc", ".docx", ".xlsx", ".xls", ".pdf", ".mp3", ".mp4", ".mpeg", ".webp"];

        public FileManagerController(IFileManagerService fileManagerService) 
        {
            _fileManagerService = fileManagerService;
        }

        /// <summary>
        /// Lấy ra tất file
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetFile()
        {
            try
            {
                // xử lý logic
                var rs = await _fileManagerService.GetAllFParent();
                if (rs != null)
                {
                    return StatusCode(StatusCodes.Status200OK,
                    new ResponseWithDataDto<List<FileManagerDto>> { Status = StatusConstant.SUCCESS, Message = "Lấy file thành công", Data = rs });
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

        [HttpGet("GetAll")]
     
        public async Task<IActionResult> GetAllFile()
        {
            try
            {
                // xử lý logic
                var rs = await _fileManagerService.GetAllFile();
                if (rs != null)
                {
                    return StatusCode(StatusCodes.Status200OK,
                    new ResponseWithDataDto<List<FileManagerDto>> { Status = StatusConstant.SUCCESS, Message = "Lấy file thành công", Data = rs });
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
/*        [Authorize]*/
        public async Task<IActionResult> GetFileByParaent([FromQuery] Guid parentid)
        {
            try
            {
                var data = await _fileManagerService.GetFileByParent(parentid);
                return StatusCode(StatusCodes.Status200OK, new ResponseWithDataDto<List<FileManagerDto>> { Status = StatusConstant.SUCCESS, Data = data, Message = "Lấy file và folder thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = ex.Message });
            }
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <returns></returns>
        [HttpPost("Upload")]
        [Authorize]
        public async Task<IActionResult> UploadsFile([FromForm] UploadVM file, [FromQuery] Guid? ParentID)
        {
            try
            {
                if (file.Files != null && file.Files.Count() > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FileManager");
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
                        string shortPath = Path.Combine("Uploads/FileManager", uniqueFileName);
                        //nếu có thư mục cha
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyToAsync(stream);
                        }

                        var fileNew = new FileManager()
                        {
                            ParentId = ParentID,
                            Name = uniqueFileName,
                            Path = shortPath,
                            Mine = item.ContentType
                        };
/*
                        if (ParentID.HasValue && ParentID.Value != Guid.Empty)
                        {
                            var getParrent = _fileManagerService.GetById(ParentID);
                            if (getParrent != null)
                            {
                                fileNew.ParentId = getParrent.ParentId;
                            }   
                        }*/
                        await _fileManagerService.Create(fileNew);
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
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FileManager");
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


        [HttpPost("Upload1")]
        [Authorize]
        public async Task<IActionResult> UploadsFile1([FromForm] UploadVM file, [FromQuery] Guid? ParentID)
        {
            try
            {
                // Kiểm tra xem có tệp nào được chọn không
                if (file.Files == null || file.Files.Count() == 0)
                {
                    // Trả về lỗi nếu không có tệp nào được chọn
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Vui lòng chọn tệp cần tải lên." });
                }

                // Tạo thư mục lưu trữ các tệp tải lên
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FileManager");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                List<string> uploadedFiles = new List<string>();

                // Lặp qua từng tệp được chọn
                foreach (var item in file.Files)
                {
                    var fileName = item.FileName;
                    string fileExtension = Path.GetExtension(fileName);

                    // Kiểm tra định dạng file và tên file hợp lệ
                    if (!AcceptType.Any(x => x.Equals(fileExtension)) || !AcceptMine.Any(x => x.Equals(item.ContentType)) || fileName.Contains("../") || Path.GetInvalidFileNameChars().Any(c => fileName.Contains(c)))
                    {
                        // Trả về lỗi nếu tệp không hợp lệ
                        return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Tệp tin không hợp lệ." });
                    }

                    // Tạo tên file duy nhất và thêm vào danh sách tệp đã tải lên
                    string uniqueFileName = GetUniqueFileName(fileName);
                    uploadedFiles.Add(uniqueFileName);
                    string filePath = Path.Combine(folderPath, uniqueFileName);
                    string shortPath = Path.Combine("Uploads/FileManager", uniqueFileName);
                    //nếu có thư mục cha
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyToAsync(stream);
                    }

                    var fileNew = new FileManager()
                    {
                        ParentId = ParentID,
                        Name = uniqueFileName,
                        Path = shortPath,
                        Mine = item.ContentType
                    };
                    /*
                                            if (ParentID.HasValue && ParentID.Value != Guid.Empty)
                                            {
                                                var getParrent = _fileManagerService.GetById(ParentID);
                                                if (getParrent != null)
                                                {
                                                    fileNew.ParentId = getParrent.ParentId;
                                                }   
                                            }*/
                    await _fileManagerService.Create(fileNew);

                    // Lưu tệp vào thư mục
                    using (var stream = new FileStream(Path.Combine(folderPath, uniqueFileName), FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                }

                // Xử lý thành công và trả về danh sách các tệp đã tải lên
                return StatusCode(StatusCodes.Status200OK, new ResponseWithMessageDto { Status = StatusConstant.SUCCESS, Message = "Tải tệp lên thành công" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Đã xảy ra lỗi khi tải tệp lên: " + ex.Message });
            }
        }


        [HttpDelete("deleteSoft")]
        [Authorize]
        public async Task<IActionResult> DeleteSoft([FromBody] Guid IDItem)
        {
            try
            {
                var fileorFolder = await _fileManagerService.DeleteSoftFile(IDItem);
                if (fileorFolder == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Không tồn tại file" });
                }
                else
                {
                    await _fileManagerService.Update(fileorFolder);
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
   /*     [Authorize]*/
        public async Task<IActionResult> DeleteFile( Guid IDItem)
        {
            try
            {
                var fileorFolder = await _fileManagerService.DeleteFile(IDItem);
                if (fileorFolder == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseWithMessageDto { Status = StatusConstant.ERROR, Message = "Không tồn tại file hoặc folder" });
                }
                else
                {
                    foreach(var item in fileorFolder)
                    {
                        await _fileManagerService.Delete(item);
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

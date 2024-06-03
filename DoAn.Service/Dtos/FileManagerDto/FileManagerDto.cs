using DoAn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.FileManagerDto
{
    public class FileManagerDto : FileManager
    {
        public Guid? ParentId { get; set; }
    }
}

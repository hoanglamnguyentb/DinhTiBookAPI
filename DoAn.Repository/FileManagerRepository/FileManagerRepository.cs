using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.FileManagerRepository
{
    internal class FileManagerRepository : Repository<FileManager>, IFileManagerRepository
    {
        public FileManagerRepository(DoAnContext context) : base(context) { }
    }
}

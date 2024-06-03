using DoAn.Domain.Entities;
using DoAn.Domain;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.NhomDoTuoiRepository
{
    internal class NhomDoTuoiRepository : Repository<NhomDoTuoi>, INhomDoTuoiRepository
    {
        public NhomDoTuoiRepository(DoAnContext context) : base(context) { }
    }
}

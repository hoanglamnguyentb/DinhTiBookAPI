﻿using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.DanhMucDto
{
    public class DanhMucSearchDto : SearchBase
    {
        public string? CategoryNameFilter { get; set; }
    }
}

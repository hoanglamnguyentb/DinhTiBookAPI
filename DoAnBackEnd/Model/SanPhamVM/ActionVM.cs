﻿using DoAn.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DoAnBackEnd.Model.SanPhamVM
{
    public class ActionVM 
    {
        public string? TenSach { get; set; }
        public string? TenTacGia { get; set; }
        public string? IdNhaXuatBan { get; set; }
        public string? NamXuatBan { get; set; }
        public string? IdDanhMuc { get; set; }
        public double GiaTien { get; set; }
        public int SoLuongTon { get; set; }
        public string? MoTaSach { get; set; }
        public string? IdNhomDoTuoi { get; set; }
        public bool NoiBat { get; set; }
        public int GiamGia { get; set; }
        public bool SachKhuyenDoc { get; set; }
        public int LuotXem {  get; set; }
        public int SoLuongDaBan { get; set; }
    }
}

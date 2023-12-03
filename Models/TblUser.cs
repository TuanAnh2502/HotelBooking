using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblUser
{
    public int IdUser { get; set; }

    public string? SEmail { get; set; }

    public string? SMatkhau { get; set; }

    public string? STendaydu { get; set; }

    public string? SSdt { get; set; }

    public string? SLoaiuser { get; set; }

    public virtual ICollection<TblDanhGium> TblDanhGia { get; set; } = new List<TblDanhGium>();

    public virtual ICollection<TblDatphong> TblDatphongs { get; set; } = new List<TblDatphong>();
    public virtual ICollection<TblKhachSan> TblKhachSans { get; set; } = new List<TblKhachSan>();

}

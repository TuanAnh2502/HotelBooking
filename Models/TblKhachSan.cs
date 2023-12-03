using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblKhachSan
{
    public int IdKhachsan { get; set; }

    public string? STenkhachsan { get; set; }

    public string? SDiachi { get; set; }

    public string? SMotakhachsan { get; set; }

    public string? SAnhkhachsan { get; set; }

    public decimal? SDanhgia { get; set; }
    public int? IdUser { get; set; }

    public virtual TblUser? IdUserNavigation { get; set; }
    public virtual ICollection<TblDanhGium> TblDanhGia { get; set; } = new List<TblDanhGium>();

    public virtual ICollection<TblPhong> TblPhongs { get; set; } = new List<TblPhong>();
}

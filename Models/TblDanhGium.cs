using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblDanhGium
{
    public int IdDanhgia { get; set; }

    public int? IdKhachsan { get; set; }

    public int? IdKhachhang { get; set; }

    public int? ISosaodanhgia { get; set; }

    public string? SNoidung { get; set; }

    public virtual TblUser? IdKhachhangNavigation { get; set; }

    public virtual TblKhachSan? IdKhachsanNavigation { get; set; }
}

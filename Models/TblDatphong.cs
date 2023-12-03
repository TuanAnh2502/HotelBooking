using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblDatphong
{
    public int IdMadatphong { get; set; }

    public int? IdUser { get; set; }

    public int? IdPhong { get; set; }

    public DateOnly? DNgaycheckin { get; set; }

    public DateOnly? DNgaycheckout { get; set; }

    public string? SHinhthucthanhtoan { get; set; }

    public virtual TblPhong? IdPhongNavigation { get; set; }

    public virtual TblUser? IdUserNavigation { get; set; }

    public virtual ICollection<TblChiTietDatPhong> TblChiTietDatPhongs { get; set; } = new List<TblChiTietDatPhong>();
}

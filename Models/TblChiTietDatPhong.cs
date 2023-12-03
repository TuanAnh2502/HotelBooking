using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblChiTietDatPhong
{
    public int IdChitietdatphong { get; set; }

    public int? IdMadatphong { get; set; }

    public string? STenuser { get; set; }

    public string? SEmailkhachhang { get; set; }

    public string? SSdtkhachhang { get; set; }

    public string? SChuthich { get; set; }

    public virtual TblDatphong? IdMadatphongNavigation { get; set; }
}

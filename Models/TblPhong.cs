using System;
using System.Collections.Generic;

namespace HotelBooking.Models;

public partial class TblPhong
{
    public int IdPhong { get; set; }

    public int? IdKhachsan { get; set; }

    public string? SKieuPhong { get; set; }

    public double? FGiaphong { get; set; }

    public int? SPhongcontrong { get; set; }

    public string? SMotaphong { get; set; }

    public string? STiennghi { get; set; }

    public string? SAnhphong { get; set; }

    public virtual TblKhachSan? IdKhachsanNavigation { get; set; }

    public virtual ICollection<TblDatphong> TblDatphongs { get; set; } = new List<TblDatphong>();

    internal object? ToListAsync()
    {
        throw new NotImplementedException();
    }
}

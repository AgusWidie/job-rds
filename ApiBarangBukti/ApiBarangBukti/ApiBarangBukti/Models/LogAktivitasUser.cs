using System;
using System.Collections.Generic;

namespace ApiBarangBukti.Models;

public partial class LogAktivitasUser
{
    public int Id { get; set; }

    public string? LogId { get; set; }

    public string? DocumentId { get; set; }

    public int? Status { get; set; }

    public string? NomorTransaksi { get; set; }

    public string? Judul { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? CreateBy { get; set; }
}

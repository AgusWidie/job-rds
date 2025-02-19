using System;
using System.Collections.Generic;

namespace ApiBarangBukti.Models;

public partial class HdBarangBukti
{
    public int Id { get; set; }

    public string? IdHdBarangBukti { get; set; }

    public string? Nama { get; set; }

    public string? NoRegistrasi { get; set; }

    public string? NoPerkara { get; set; }

    public DateTime? TanggalWaktuPenyerahan { get; set; }

    public string? Instansi { get; set; }

    public string? File { get; set; }

    public string? DisitaDari { get; set; }

    public string? NoBapPenyitaan { get; set; }

    public string? TempatPenyitaan { get; set; }

    public string? NomorSprintPenyitaan { get; set; }

    public string? Keterangan { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? FileName { get; set; }

    public string? ContentType { get; set; }

    public string? Extension { get; set; }

    public int? FileSize { get; set; }

    public string? Base64File { get; set; }

    public string? FilePath { get; set; }
}

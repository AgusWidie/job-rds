using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiBarangBukti.Models;

public partial class DbsiramContext : DbContext
{
    public DbsiramContext()
    {
    }

    public DbsiramContext(DbContextOptions<DbsiramContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DtBarangBukti> DtBarangBuktis { get; set; }

    public virtual DbSet<DtTransaksi> DtTransaksis { get; set; }

    public virtual DbSet<HdBarangBukti> HdBarangBuktis { get; set; }

    public virtual DbSet<HdTransaksi> HdTransaksis { get; set; }

    public virtual DbSet<LogAktivitasUser> LogAktivitasUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=dbsiram;User Id=postgres;Password=2808Agus!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DtBarangBukti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dt_barang_bukti_pk");

            entity.ToTable("dt_barang_bukti", "barbuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64File).HasColumnName("base64_file");
            entity.Property(e => e.ContentType)
                .HasMaxLength(255)
                .HasColumnName("content_type");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delete_at");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.IdDtBarangBukti)
                .HasMaxLength(50)
                .HasColumnName("id_dt_barang_bukti");
            entity.Property(e => e.IdHdBarangBukti)
                .HasMaxLength(50)
                .HasColumnName("id_hd_barang_bukti");
            entity.Property(e => e.Identitas).HasColumnName("identitas");
            entity.Property(e => e.Jumlah).HasColumnName("jumlah");
            entity.Property(e => e.Kondisi).HasColumnName("kondisi");
            entity.Property(e => e.NamaBarangBukti)
                .HasMaxLength(255)
                .HasColumnName("nama_barang_bukti");
            entity.Property(e => e.StatusAkhir)
                .HasMaxLength(255)
                .HasColumnName("status_akhir");
            entity.Property(e => e.StatusEksekusi)
                .HasMaxLength(255)
                .HasColumnName("status_eksekusi");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<DtTransaksi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dt_transaksi_pk");

            entity.ToTable("dt_transaksi", "barbuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64File).HasColumnName("base64_file");
            entity.Property(e => e.ContentType)
                .HasMaxLength(255)
                .HasColumnName("content_type");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delete_at");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.Harga)
                .HasDefaultValueSql("0")
                .HasColumnName("harga");
            entity.Property(e => e.IdBarangBukti)
                .HasMaxLength(50)
                .HasColumnName("id_barang_bukti");
            entity.Property(e => e.IdTransaksi)
                .HasMaxLength(50)
                .HasColumnName("id_transaksi");
            entity.Property(e => e.JenisTransaksi).HasColumnName("jenis_transaksi");
            entity.Property(e => e.PemenangLelangInstansi)
                .HasMaxLength(255)
                .HasColumnName("pemenang_lelang_instansi");
            entity.Property(e => e.TanggalPenyerahan)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tanggal_penyerahan");
            entity.Property(e => e.TanggalTerjual)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tanggal_terjual");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<HdBarangBukti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hd_barang_bukti_pk");

            entity.ToTable("hd_barang_bukti", "barbuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64File).HasColumnName("base64_file");
            entity.Property(e => e.ContentType)
                .HasMaxLength(255)
                .HasColumnName("content_type");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delete_at");
            entity.Property(e => e.DisitaDari)
                .HasMaxLength(255)
                .HasColumnName("disita_dari");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.IdHdBarangBukti)
                .HasMaxLength(50)
                .HasColumnName("id_hd_barang_bukti");
            entity.Property(e => e.Instansi)
                .HasMaxLength(255)
                .HasColumnName("instansi");
            entity.Property(e => e.Keterangan).HasColumnName("keterangan");
            entity.Property(e => e.Nama)
                .HasMaxLength(255)
                .HasColumnName("nama");
            entity.Property(e => e.NoBapPenyitaan)
                .HasMaxLength(50)
                .HasColumnName("no_bap_penyitaan");
            entity.Property(e => e.NoPerkara)
                .HasMaxLength(50)
                .HasColumnName("no_perkara");
            entity.Property(e => e.NoRegistrasi)
                .HasMaxLength(50)
                .HasColumnName("no_registrasi");
            entity.Property(e => e.NomorSprintPenyitaan)
                .HasMaxLength(50)
                .HasColumnName("nomor_sprint_penyitaan");
            entity.Property(e => e.TanggalWaktuPenyerahan)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tanggal_waktu_penyerahan");
            entity.Property(e => e.TempatPenyitaan)
                .HasMaxLength(255)
                .HasColumnName("tempat_penyitaan");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<HdTransaksi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hd_transaksi_pk");

            entity.ToTable("hd_transaksi", "barbuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64File).HasColumnName("base64_file");
            entity.Property(e => e.ContentType)
                .HasMaxLength(255)
                .HasColumnName("content_type");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delete_at");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .HasColumnName("extension");
            entity.Property(e => e.File).HasColumnName("file");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.IdTransaksi)
                .HasMaxLength(50)
                .HasColumnName("id_transaksi");
            entity.Property(e => e.JenisTransaksi).HasColumnName("jenis_transaksi");
            entity.Property(e => e.JudulTransaksi).HasColumnName("judul_transaksi");
            entity.Property(e => e.NamaTransaksi)
                .HasMaxLength(255)
                .HasColumnName("nama_transaksi");
            entity.Property(e => e.NoPerkara)
                .HasMaxLength(255)
                .HasColumnName("no_perkara");
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("pic");
            entity.Property(e => e.TanggalTransaksi)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tanggal_transaksi");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<LogAktivitasUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("log_aktivitas_user_pk");

            entity.ToTable("log_aktivitas_user", "barbuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .HasColumnName("create_by");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delete_at");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(50)
                .HasColumnName("document_id");
            entity.Property(e => e.Judul).HasColumnName("judul");
            entity.Property(e => e.LogId)
                .HasMaxLength(50)
                .HasColumnName("log_id");
            entity.Property(e => e.NomorTransaksi)
                .HasMaxLength(50)
                .HasColumnName("nomor_transaksi");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

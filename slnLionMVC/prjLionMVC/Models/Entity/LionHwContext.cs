using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjLionMVC.Models.Entity;

public partial class LionHwContext : DbContext
{
    public LionHwContext()
    {
    }

    public LionHwContext(DbContextOptions<LionHwContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLogTable> ErrorLogTables { get; set; }

    public virtual DbSet<MemberTable> MemberTables { get; set; }

    public virtual DbSet<MessageBoardTable> MessageBoardTables { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LionHW;Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True;Multiple Active Result Sets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLogTable>(entity =>
        {
            entity.HasKey(e => e.ErrorId);

            entity.ToTable("ErrorLogTable");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
        });

        modelBuilder.Entity<MemberTable>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("MemberTable");

            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MemberName).HasMaxLength(10);
        });

        modelBuilder.Entity<MessageBoardTable>(entity =>
        {
            entity.HasKey(e => e.MessageBoardId);

            entity.ToTable("MessageBoardTable");

            entity.Property(e => e.MessageTime).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.MessageBoardTables)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_MessageBoardTable_MemberTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

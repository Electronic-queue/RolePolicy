using Microsoft.EntityFrameworkCore;
using RolePolicy.Domain.Entities;
using Action = RolePolicy.Domain.Entities.Action;

namespace RolePolicy.Persistence;

public partial class RolePolicyDbContext : DbContext
{
    public RolePolicyDbContext()
    {
    }

    public RolePolicyDbContext(DbContextOptions<RolePolicyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccess> RoleAccesses { get; set; }

    public virtual DbSet<RoleResourceAction> RoleResourceActions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=178.89.186.221, 1434;initial catalog=aybolat_db;user id=aybolat_user;password=F5u!03hl9;MultipleActiveResultSets=True;application name=EntityFramework;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("aybolat_user");

        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__Actions__FFE3F4B95C310E07");

            entity.ToTable("Actions", "elec_queue");

            entity.HasIndex(e => e.Name, "UQ__Actions__737584F683EA737E").IsUnique();

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Actions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Actions__Created__73501C2F");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Resource__4ED1814F2837EB68");

            entity.ToTable("Resources", "elec_queue");

            entity.HasIndex(e => e.Name, "UQ__Resource__737584F64F03E5E3").IsUnique();

            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Resources)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Resources__Creat__6E8B6712");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AAA788C23");

            entity.ToTable("Roles", "elec_queue");

            entity.HasIndex(e => e.NameKk, "UQ__Roles__33287F95C4ACE5D5").IsUnique();

            entity.HasIndex(e => e.NameRu, "UQ__Roles__3328B6A4BA0FE2C3").IsUnique();

            entity.HasIndex(e => e.NameEn, "UQ__Roles__332920C24B0D5AEA").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(100)
                .HasColumnName("Name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(100)
                .HasColumnName("Name_ru");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Roles__CreatedBy__640DD89F");
        });

        modelBuilder.Entity<RoleAccess>(entity =>
        {
            entity.HasKey(e => e.RoleAccessId).HasName("PK__RoleAcce__C1244FB463327F39");

            entity.ToTable("RoleAccesses", "elec_queue");

            entity.Property(e => e.RoleAccessId).HasColumnName("RoleAccessID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.GivenByNavigation).WithMany(p => p.RoleAccessGivenByNavigations)
                .HasForeignKey(d => d.GivenBy)
                .HasConstraintName("FK__RoleAcces__Given__69C6B1F5");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAcces__RoleI__68D28DBC");

            entity.HasOne(d => d.User).WithMany(p => p.RoleAccessUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAcces__UserI__67DE6983");
        });

        modelBuilder.Entity<RoleResourceAction>(entity =>
        {
            entity.HasKey(e => e.RoleResourceActionId).HasName("PK__RoleReso__53EA627AA4821A9C");

            entity.ToTable("RoleResourceActions", "elec_queue");

            entity.Property(e => e.RoleResourceActionId).HasColumnName("RoleResourceActionID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).HasColumnName("Description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("Description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("Description_ru");
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Action).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__Actio__7908F585");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__RoleResou__Creat__79FD19BE");

            entity.HasOne(d => d.Resource).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__Resou__7814D14C");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleResourceActions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleResou__RoleI__7720AD13");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC25F45487");

            entity.ToTable("Users", "elec_queue");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B7C5DEA66").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Users__CreatedBy__51EF2864");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

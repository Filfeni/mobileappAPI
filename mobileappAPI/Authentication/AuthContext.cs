using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mobileappAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileappAPI.Authentication
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Carro>(entity =>
            {
                entity.HasKey(e => e.Idcarro)
                    .HasName("PK__Carro__D9C60BA6B1494B17");

                entity.ToTable("Carro");

                entity.Property(e => e.Idcarro).HasColumnName("IDCarro");

                entity.Property(e => e.Año)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

                entity.Property(e => e.Idpropietario).HasColumnName("IDPropietario");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Carros)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carro__Placa__4316F928");

                entity.HasOne(d => d.IdmarcaNavigation)
                    .WithMany(p => p.Carros)
                    .HasForeignKey(d => d.Idmarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carro__IDMarca__440B1D61");

                entity.HasOne(d => d.IdpropietarioNavigation)
                    .WithMany(p => p.Carros)
                    .HasForeignKey(d => d.Idpropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carro__IDPropiet__44FF419A");
            });

            builder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__Categori__70E82E28DC5E70ED");

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            builder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.Idmarca)
                    .HasName("PK__Marca__CEC375E7E0BE3AFA");

                entity.ToTable("Marca");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

                entity.Property(e => e.Marca1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Marca");
            });

            builder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Idpost)
                    .HasName("PK__Post__8B0115BDB10C44CE");

                entity.ToTable("Post");

                entity.HasIndex(e => e.Idcarro, "UQ__Post__D9C60BA74690D2BC")
                    .IsUnique();

                entity.Property(e => e.Idpost).HasColumnName("IDPost");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(144)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Idcarro).HasColumnName("IDCarro");

                entity.Property(e => e.Precio).HasColumnType("decimal(11, 2)");

                entity.HasOne(d => d.IdcarroNavigation)
                    .WithOne(p => p.Post)
                    .HasForeignKey<Post>(d => d.Idcarro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__IDCarro__48CFD27E");
            });

            builder.Entity<Reservacion>(entity =>
            {
                entity.HasKey(e => e.Idreservacion)
                    .HasName("PK__Reservac__E970411B49959C30");

                entity.ToTable("Reservacion");

                entity.Property(e => e.Idreservacion).HasColumnName("IDReservacion");

                entity.Property(e => e.FechaEntrega).HasColumnType("date");

                entity.Property(e => e.FechaSalida).HasColumnType("date");

                entity.Property(e => e.Idcarro).HasColumnName("IDCarro");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.IdtipoReservacion).HasColumnName("IDTipoReservacion");

                entity.HasOne(d => d.IdcarroNavigation)
                    .WithMany(p => p.Reservacions)
                    .HasForeignKey(d => d.Idcarro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservaci__Fecha__4D94879B");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Reservacions)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservaci__IDCli__4E88ABD4");

                entity.HasOne(d => d.IdtipoReservacionNavigation)
                    .WithMany(p => p.Reservacions)
                    .HasForeignKey(d => d.IdtipoReservacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservaci__IDTip__4F7CD00D");
            });

            builder.Entity<TipoReservacion>(entity =>
            {
                entity.HasKey(e => e.IdtipoReservacion)
                    .HasName("PK__TipoRese__138344961896CBF2");

                entity.ToTable("TipoReservacion");

                entity.Property(e => e.IdtipoReservacion).HasColumnName("IDTipoReservacion");

                entity.Property(e => e.Alcance)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            builder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__52311169D789E966");

                entity.ToTable("Usuario");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

        }
    }
}

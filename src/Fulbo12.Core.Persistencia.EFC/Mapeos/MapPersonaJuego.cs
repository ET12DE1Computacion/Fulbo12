namespace Fulbo12.Core.Persistencia.EFC.Mapeos;
public class MapPersonaJuego : IEntityTypeConfiguration<PersonaJuego>
{
    public void Configure(EntityTypeBuilder<PersonaJuego> etb)
    {
        etb.ToTable("Persona");

        etb.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .IsRequired()
            .HasMaxLength(30);

        etb.Property(p => p.Apellido)
            .HasColumnName("apellido")
            .IsRequired()
            .HasMaxLength(30);

        etb.HasOne(p => p.Pais)
            .WithMany()
            .HasForeignKey("idPais")
            .IsRequired()
            .HasConstraintName("FK_Persona_Pais");

        etb.HasIndex(p => new { p.Apellido, p.Nombre })
            .IsFullText()
            .HasDatabaseName("FT_Persona_apellido_nombre");
    }
}
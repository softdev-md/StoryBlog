using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence.Mapping
{
    /// <summary>
    /// Represents a project mapping configuration
    /// </summary>
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(project => project.Id);
            
            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Tags).HasMaxLength(1000);

            base.Configure(builder);
        }

        #endregion
    }
}
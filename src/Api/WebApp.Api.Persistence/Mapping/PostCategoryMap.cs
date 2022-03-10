using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence.Mapping
{
    /// <summary>
    /// Represents a post category mapping configuration
    /// </summary>
    public class PostCategoryMap : EntityTypeConfiguration<PostCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.ToTable("PostCategory");
            builder.HasKey(postCategory => postCategory.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Tags).HasMaxLength(500);

            builder.HasOne(d => d.Project)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(d => d.ProjectId);

            base.Configure(builder);
        }

        #endregion
    }
}
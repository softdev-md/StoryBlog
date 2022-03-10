using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence.Mapping
{
    /// <summary>
    /// Represents a post mapping configuration
    /// </summary>
    public class PostMap : EntityTypeConfiguration<Post>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(post => post.Id);

            builder.Property(e => e.Author).HasMaxLength(150);
            builder.Property(e => e.Body).IsRequired();
            builder.Property(e => e.Tags).HasMaxLength(550);

            builder.HasOne(d => d.PostCategory)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostCategoryId);

            builder.HasOne(d => d.Project)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProjectId);

            base.Configure(builder);
        }

        #endregion
    }
}
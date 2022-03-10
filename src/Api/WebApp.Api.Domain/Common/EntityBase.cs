namespace WebApp.Api.Domain.Common
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class EntityBase : IEntity<int>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}

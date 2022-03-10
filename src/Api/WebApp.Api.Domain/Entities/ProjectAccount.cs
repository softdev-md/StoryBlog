using WebApp.Api.Domain.Common;

namespace WebApp.Api.Domain.Entities
{
    public partial class ProjectAccount : EntityBase
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public bool Active { get; set; }
    }
}

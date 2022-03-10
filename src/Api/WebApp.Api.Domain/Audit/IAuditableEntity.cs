using System;

namespace WebApp.Api.Domain.Audit
{
    public interface IAuditableEntity
    {
        DateTime CreatedAtUtc { get; set; }
        DateTime? UpdatedAtUtc { get; set; }
        string LastModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Api.Domain.Common;

namespace WebApp.Api.Domain.Audit
{
    public abstract class AuditableEntity : EntityBase, IAuditableEntity
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(Order = 101)]
        public DateTime CreatedAtUtc { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(Order = 102)]
        public DateTime? UpdatedAtUtc { get; set; }
        [Column(Order = 103)]
        public string LastModifiedBy { get; set; }
        [Column(Order = 104)]
        public string CreatedBy { get; set; }
    }
}

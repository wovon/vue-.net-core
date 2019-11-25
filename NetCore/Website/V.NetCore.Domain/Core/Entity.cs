using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace V.NetCore.Domain.Core
{
    public class Entity
    {
        [Key]
        [DefaultValue("newid()")]
        [DisplayName("标识")]
        [StringLength(50)]
        //[DefaultValue("{siring:guid}")]
        public string Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V.NetCore.Repository.Core
{
    public class Entity
    {
        [Key]
        [DefaultValue("newid()")]
        [DisplayName("标识")]
        [StringLength(50)]
        [Column(Order = 1)]
        //[DefaultValue("{siring:guid}")]
        public string Id { get; set; }
        
        public Entity()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
        }
    }
}

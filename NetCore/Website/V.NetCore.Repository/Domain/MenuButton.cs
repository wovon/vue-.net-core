﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using V.NetCore.Repository.Core;

namespace V.NetCore.Repository.Domain
{
    public class MenuButton:Entity
    {
        public string MenuId { get; set; }
        [NotMapped()]
        public Menu Menu { get; set; }
        public string ButtonId { get; set; }
        [NotMapped()]
        public Button Button { get; set; }
    }
}

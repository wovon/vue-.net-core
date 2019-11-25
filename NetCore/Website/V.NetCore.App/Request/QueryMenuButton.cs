using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.Request
{
    public class QueryMenuButton
    {
        public string Menus { get; set; }

        public List<MenuButton> MenuButtons { get; set; }
    }
}

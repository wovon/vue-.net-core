using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace V.NetCore.Repository.Domain
{
    public class Model1
    {
        public int A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public List<dynamic> D { get; set; }
        public string E { get; set; }
        [NotMapped()]
        public string F { get; set; }
    }
}

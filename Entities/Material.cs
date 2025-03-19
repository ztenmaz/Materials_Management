using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Material
    {
        public string idMaterial { get; set; }
        public string nameMaterial { get; set; }
        public string supplierName { get; set; }
        public int? quantity { get; set; }
        public string unit { get; set; }
        public string status { get; set; }
    }
}

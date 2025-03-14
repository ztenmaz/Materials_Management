using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Officer
    {
        public int idOfficer { get; set; }
        public string nameOfficer { get; set; }
        public bool gender { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int idAccount { get; set; }
        public int idFaculty { get; set; }
        public string address { get; set; }
        public DateTime ngaysinh { get; set; }
    }
}

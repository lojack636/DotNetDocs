using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserEntity : DefaultEntity
    {
        public string UserName { set; get; }
        public string Phone { set; get; }
        public string Maile { set; get; }
        public DateTime? Birthday { set; get; }
        public string Address { set; get; }
    }
}

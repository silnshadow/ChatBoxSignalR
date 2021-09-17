using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBoxClient.Models
{
    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public byte[] Photo { get; set; }
    }
}

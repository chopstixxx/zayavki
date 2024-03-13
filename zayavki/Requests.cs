using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zayavki
{
    public class Requests
    {
       public int Id { get; set; }
        public int Client_id { get; set; }
        public int Satus_id { get; set; }
        public int Master_id { get; set; }
        public string Equipment { get; set; }
        public string Malf_type { get; set; }
        public string Malf_description { get; set; }
        public DateTime Add_date { get; set; }


    }
}

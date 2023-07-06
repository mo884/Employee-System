using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Helper
{
    public class ApiResponse<t1>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public t1 Data { get; set; }

    }
}

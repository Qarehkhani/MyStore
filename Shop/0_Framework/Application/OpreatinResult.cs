using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OpreatinResult
    {
        public bool IsSuccedded { get; set; }
        public string Message { get; set; }

        public OpreatinResult()
        {
            IsSuccedded = false;
        }
        public OpreatinResult Succedded(string message = "فرآیند مورد نظر با موفقیت انجام شد")
        {
            IsSuccedded=true;
            Message = message;
            return this;
        }

        public OpreatinResult Faild(string message)
        {
            IsSuccedded=false;
            Message = message;
            return this;
        }
    }
}

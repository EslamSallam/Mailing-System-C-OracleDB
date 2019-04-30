using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing_System
{
    public class Mail
    {
        public string id { set; get; }
        public string subject { set; get; }
        public string to { set; get; }
        public string from { set; get; }
        public string message { set; get; }
        public string attachmentLocation { set; get; }
        public bool visited { set; get; }
        public ConsoleColor col { set; get; }
    }
}

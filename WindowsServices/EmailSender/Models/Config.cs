using System.Collections.Generic;
using System.Linq;

namespace EmailSender.Models
{
    public class Config
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
    }
}
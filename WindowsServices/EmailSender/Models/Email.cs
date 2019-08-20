using System;
using System.Collections.Generic;

namespace EmailSender.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? Intention { get; set; }
        public int Attempts { get; set; }
        public bool UsingTemplate { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<EmailAttachment> Attachments { get; set; }
    }
}

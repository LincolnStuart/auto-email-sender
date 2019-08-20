using System.Collections.Generic;
using EmailSender.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Repositories
{
    public class EmailRepository
    {
        private ApplicationContext context;

        public EmailRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Email> All(int maxAttempts)
        {
            return context.Emails
                .Include(e => e.Attachments)
                .ToList()
                .Where(e => e.SendDate == null &&
                        (e.Intention == null || e.Intention.GetValueOrDefault() <= DateTime.Now)
                        && e.Attempts < maxAttempts);
        }

        public void MarkAsSent(int id)
        {
            var email = context.Emails.Where(e => e.Id == id).SingleOrDefault();
            email.SendDate = DateTime.Now;
            email.ErrorMessage = null;
            context.SaveChanges();
        }

        public void IncrementAttempt(IEnumerable<Email> emails)
        {
            emails.ToList().ForEach(e => e.Attempts++);
            context.SaveChanges();
        }

        public void UpdateError(int id, string errorMessage)
        {
            var email = context.Emails.Where(e => e.Id == id).SingleOrDefault();
            email.ErrorMessage = errorMessage;
            context.SaveChanges();
        }
    }
}

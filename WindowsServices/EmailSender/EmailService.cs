using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Helpers;
using EmailSender.Models;
using EmailSender.Repositories;

namespace EmailSender
{
    class EmailService : ServiceBase
    {
        private CancellationTokenSource cts;
        private ApplicationContext context;
        private EmailRepository repo;

        protected override void OnStart(string[] args)
        {
            cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => InitService(), cts.Token);
        }

        protected override void OnStop()
        {
            cts.Cancel();
        }

        public async void InitService()
        {
            IEnumerable<Email> emails;
            while (true)
            {
                InitRepo();
                emails = repo.All(ConfigHelper.GetMaxAttempts());
                await Process(emails);
                cts.Token.ThrowIfCancellationRequested();
                Thread.Sleep(ConfigHelper.GetThreadFrequencyInMinutes() * 60 * 1000);
            }
        }

        private async Task Process(IEnumerable<Email> emails)
        {
            repo.IncrementAttempt(emails);
            var config = ConfigHelper.GetConfiguration();
            var templateData = ConfigHelper.GetTemplateData();
            var tasks = emails.Select(email =>
                Task.Factory.StartNew(async () =>
                {
                    cts.Token.ThrowIfCancellationRequested();
                    cts.Token.ThrowIfCancellationRequested();
                    await EmailHelper.SendMail(email, config, templateData, repo);
                    repo.MarkAsSent(email.Id);
                })
            );
            cts.Token.ThrowIfCancellationRequested();
            await Task.WhenAll(tasks);
        }

        private void InitRepo()
        {
            CloseRepo();
            context = new ApplicationContext(ConfigHelper.GetConnectionString());
            repo = new EmailRepository(context);
        }

        private void CloseRepo()
        {
            if(context != null)
            {
                context.Dispose();
                context = null;
                repo = null;
            } 
        }
    }
}

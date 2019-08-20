using System;
using System.ServiceProcess;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new EmailService())
            {
                ServiceBase.Run(service);
                //service.InitService();
            }
        }
    }
}

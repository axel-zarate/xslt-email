using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AxSoft.TemplateEmail.Net
{
	public class EmailSender : IEmailSender
	{
		public void Send(MailMessage message)
		{
			using (var smtpServer = new SmtpClient())
			{
				smtpServer.Send(message);
			}
		}

		public Task SendAsync(MailMessage message)
		{
			return Task.Factory.StartNew(() => Send(message));
		}
	}
}
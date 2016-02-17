using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AxSoft.TemplateEmail.Net
{
	public class DummyEmailSender : IEmailSender
	{
		private readonly Action<MailMessage> _writeAction;

		public DummyEmailSender(Action<MailMessage> writeAction)
		{
			if (writeAction == null) throw new ArgumentNullException("writeAction");
			_writeAction = writeAction;
		}

		public void Send(MailMessage message)
		{
			_writeAction(message);
		}

		public Task SendAsync(MailMessage message)
		{
			return Task.Factory.StartNew(() => Send(message));
		}
	}
}
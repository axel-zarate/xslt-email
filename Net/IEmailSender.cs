using System.Net.Mail;
using System.Threading.Tasks;

namespace AxSoft.TemplateEmail.Net
{
	public interface IEmailSender
	{
		void Send(MailMessage message);

		Task SendAsync(MailMessage message);
	}
}
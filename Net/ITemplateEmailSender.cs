using System.Threading.Tasks;

namespace AxSoft.TemplateEmail.Net
{
	public interface ITemplateEmailSender
	{
		string LayoutFilePath { get; set; }

		void Send(string templatePath, object variables, string to, string subject);

		Task SendAsync(string templatePath, object variables, string to, string subject);
	}
}
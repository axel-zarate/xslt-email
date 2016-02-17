using System.Net.Mail;
using System.Text;
using AxSoft.TemplateEmail.Net;
using AxSoft.TemplateEmail.Templating;
using AxSoft.TemplateEmail.Xml;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AxSoft.TemplateEmail
{
	internal class Program
	{
		private static void Dump(MailMessage message)
		{
			Trace.WriteLine(message.Body);
		}

		private static void Main(string[] args)
		{
			// We'll dump the resulting HTML body in a file
			var file = File.Create(Path.Combine(Environment.CurrentDirectory, "output.html"));
			var streamWriter = new StreamWriter(file, Encoding.UTF8);

			var emailSender = new DummyEmailSender(message => streamWriter.Write(message.Body)); // or new DummyEmailSender(Dump)

			var templateCompiler = new TemplateCompiler();
			var xmlSerializer = new CustomXmlSerializer();

			var templateDirectory = Path.Combine(Environment.CurrentDirectory, "Templates");
			var layoutFile = Path.Combine(templateDirectory, "layout.html");
			var xsltFilePath = Path.Combine(templateDirectory, "ActivateAccount.xslt");
			var variables = new
			{
				FirstName = "Axel",
				LastName = "Zarate",
				Username = "azarate",
				Ignored = default(object),
				Logo = "http://www.logotree.com/images/single-logo-design/logo-design-sample-14.jpg",
				ActivationLink = "http://localhost/Account/Activate/azarate",
				Benefits = new[]
				{
					"Free support",
					"Great discounts",
					"Unlimited access"
				},
				IsPremiumUser = true
			};

			var templateEmailSender = new TemplateEmailSender(emailSender, templateCompiler, xmlSerializer)
			{
				LayoutFilePath = layoutFile
			};

			templateEmailSender.Send(xsltFilePath, variables, "axel.zarate@mail.com", "This is a template test");

			// Close the file
			streamWriter.Dispose();
			file.Close();

			Console.WriteLine("Ready.");
			Console.Read();
		}
	}
}
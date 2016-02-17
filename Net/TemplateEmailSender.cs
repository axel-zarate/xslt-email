using AxSoft.TemplateEmail.Templating;
using AxSoft.TemplateEmail.Xml;
using System;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AxSoft.TemplateEmail.Net
{
	public class TemplateEmailSender : ITemplateEmailSender
	{
		#region Fields

		private readonly IEmailSender _emailSender;
		private readonly Regex _replaceRegex = new Regex(Regex.Escape("<!-- Content -->"), RegexOptions.Compiled);
		private readonly ITemplateCompiler _templateCompiler;
		private readonly IXmlSerializer _xmlSerializer;
		private string _layoutHtml;

		#endregion Fields

		public string LayoutFilePath { get; set; }

		protected string LayoutHtml
		{
			get
			{
				lock (this)
				{
					if (LayoutFilePath == null)
					{
						return null;
					}

					return _layoutHtml ?? (_layoutHtml = GetBaseHtmlContent());
				}
			}
		}

		#region Constructor

		public TemplateEmailSender(IEmailSender emailSender, ITemplateCompiler templateCompiler, IXmlSerializer xmlSerializer)
		{
			if (emailSender == null) throw new ArgumentNullException("emailSender");
			if (templateCompiler == null) throw new ArgumentNullException("templateCompiler");
			if (xmlSerializer == null) throw new ArgumentNullException("xmlSerializer");

			_emailSender = emailSender;
			_templateCompiler = templateCompiler;
			_xmlSerializer = xmlSerializer;
		}

		#endregion Constructor

		#region Methods

		public void Send(string templatePath, object variables, string to, string subject)
		{
			var mailMessage = ConstructMailMessage(templatePath, variables, to, subject);

			_emailSender.Send(mailMessage);
		}

		public Task SendAsync(string templatePath, object variables, string to, string subject)
		{
			var mailMessage = ConstructMailMessage(templatePath, variables, to, subject);

			return _emailSender.SendAsync(mailMessage);
		}

		private MailMessage ConstructMailMessage(string templatePath, object variables, string to, string subject)
		{
			if (templatePath == null) throw new ArgumentNullException("templatePath");
			if (to == null) throw new ArgumentNullException("to");

			if (!File.Exists(templatePath))
			{
				throw new FileNotFoundException("Template file not found.", templatePath);
			}

			var compiler = _templateCompiler;
			string compiled;
			using (var xmlStream = new MemoryStream())
			{
				_xmlSerializer.ToXml(variables, xmlStream);
				using (var output = new MemoryStream())
				{
					compiler.CompileXsltFromFile(xmlStream, templatePath, output);

					output.Seek(0, SeekOrigin.Begin);
					using (var reader = new StreamReader(output))
					{
						compiled = reader.ReadToEnd();
					}
				}
			}

			string layoutHtml = LayoutHtml;
			if (layoutHtml != null)
			{
				compiled = _replaceRegex.Replace(layoutHtml, compiled);
			}

			var mailMessage = new MailMessage
			{
				Subject = subject,
				IsBodyHtml = true,
				BodyEncoding = System.Text.Encoding.UTF8,
				Body = compiled
			};
			mailMessage.To.Add(to);

			return mailMessage;
		}

		private string GetBaseHtmlContent()
		{
			string filePath = LayoutFilePath;
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("Layout file not found.", filePath);
			}

			using (var reader = new StreamReader(filePath))
			{
				return reader.ReadToEnd();
			}
		}

		#endregion Methods
	}
}
using System.IO;

namespace AxSoft.TemplateEmail.Templating
{
	public interface ITemplateCompiler
	{
		void CompileXsltFromFile(Stream inputXmlStream, string xsltFilePath, Stream outputStream);

		void CompileXsltFromStream(Stream inputXmlStream, Stream xsltStream, Stream outputStream);
	}
}
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PdfEditorTest
{
    public class PdfEditorHelper
    {
        private string source;
        private string destination;

        PdfReader reader;
        PdfWriter writer;
        PdfDocument pdfDoc;
        PdfAcroForm form;
        IDictionary<string, PdfFormField> sourcePDFFields;
        public PdfEditorHelper(string sourceFile, string destinationFolder)
        {
            source = sourceFile;
            destination = destinationFolder;
        }

        public IDictionary<string, iText.Forms.Fields.PdfFormField> GetAllFieldFromPdfSource()
        {
            reader = new PdfReader(source);
            reader.SetUnethicalReading(true);

            writer = new PdfWriter(destination + "Sample.pdf");
            pdfDoc = new PdfDocument(reader, writer);
            form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            sourcePDFFields = form.GetFormFields();

            return sourcePDFFields;
        }

        public void EditPdfFromListUsers(List<Tuple<string, Dictionary<string, string>>> listUsers)
        {
            foreach(var user in listUsers)
            {
                reader = new PdfReader(source);
                reader.SetUnethicalReading(true);

                writer = new PdfWriter($"{destination }\\{user.Item1}.pdf");
                pdfDoc = new PdfDocument(reader, writer);
                form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                sourcePDFFields = form.GetFormFields();

                foreach (var dicItem in user.Item2)
                {
                    if(dicItem.Value != null)
                        sourcePDFFields[dicItem.Key].SetValue(dicItem.Value);
                }

                form.FlattenFields();
                pdfDoc.Close();

            }

        }

        public object EditPdfFromListUsers(bool v)
        {
            throw new NotImplementedException();
        }

        public void MappingToPDF(Tuple<string, Dictionary<string, string>> tupleRecords)
        {
            writer = new PdfWriter(destination + tupleRecords.Item1);
            pdfDoc = new PdfDocument(reader, writer);
            form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            sourcePDFFields = form.GetFormFields();

            foreach (var item in tupleRecords.Item2)
            {
                sourcePDFFields[item.Key].SetValue(item.Value);
            }

            form.FlattenFields();
            pdfDoc.Close();
        }



    }
}

using NUnit.Framework;
using PdfEditorTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests
{
    public class PdfEditorHelperTests
    {
        PdfEditorHelper pdfEditor;
        [SetUp]
        public void Setup()
        {
            pdfEditor = new PdfEditorHelper("C:\\hack\\firstPdfTool\\ESDC-EMP5624.pdf", "C:\\hack\\PdfEditorTest\\PdfEditorTest\\dest");

        }

        /// <summary>
        /// test to get all editable fields from source form
        /// </summary>
        [Test]
        public void GetAllFieldFromPdfSourceTest()
        {
            var result = pdfEditor.GetAllFieldFromPdfSource();

            foreach(var item in result)
            {
                if (item.Key.Split('.').Length > 2)
                    Debug.WriteLine($"Key: {item.Key}, Tipo: {item.Value.ToString().Split('.')[3]}");
            }

            Assert.AreEqual(result.Count, 580);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listUsers"></param>
        [Test]
        public void EditPdfFromListUsersTest()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<Tuple<string, Dictionary<string, string>>> testData = new List<Tuple<string, Dictionary<string, string>>>();

                dic.Add("EMP5624_E[0].Page1[0].Yes_business[0]", "1"); ;
                dic.Add("EMP5624_E[0].Page1[0].No_business[0]", null); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_first_name[0]", "DENIS"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_mid_name[0]", "DOUGLAS"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_last_name[0]", "CAVALHEIRO"); ;

                testData.Add(new Tuple<string, Dictionary<string, string>>("DENIS DOUGLAS CAVALHEIRO", dic));

                dic = new Dictionary<string, string>();
                dic.Add("EMP5624_E[0].Page1[0].Yes_business[0]", "1"); ;
                dic.Add("EMP5624_E[0].Page1[0].No_business[0]", null); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_first_name[0]", "MARY"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_mid_name[0]", "ANNE"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_last_name[0]", "ANDERSEN CAVALHEIRO"); ;

                testData.Add(new Tuple<string, Dictionary<string, string>>("MARY ANNE ANDERSEN CAVALHEIRO", dic));

                dic = new Dictionary<string, string>();
                dic.Add("EMP5624_E[0].Page1[0].Yes_business[0]", "1"); ;
                dic.Add("EMP5624_E[0].Page1[0].No_business[0]", null); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_first_name[0]", "ANA"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_mid_name[0]", "ELIZABETH"); ;
                dic.Add("EMP5624_E[0].Page1[0].txtF_last_name[0]", "ANDERSEN CAVALHEIRO"); ;

                testData.Add(new Tuple<string, Dictionary<string, string>>("ANA ELIZABETH ANDERSEN CAVALHEIRO", dic));

                pdfEditor.EditPdfFromListUsers(testData);

                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }

            
        }
    }
}
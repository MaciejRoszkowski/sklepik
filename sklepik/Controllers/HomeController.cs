using sklepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using iTextSharp.text;

namespace sklepik.Controllers
{
    public class HomeController : Controller
    {
        private ShopDB _db = new ShopDB();
        public ActionResult Index(string searchTerm =null,int page=1)
        {
            return View(_db.Product
                .Where(p=>p.IsHidden==false&&(searchTerm==null||p.Name.StartsWith(searchTerm)))
                .OrderBy(p=>p.Id)
                .ToPagedList(page, Int32.Parse(Request.Cookies["Pages"].Value)));
        }
        public String GenerateHtml(int? id)
        {
            if (id == null)
            {
                return "xD";
            }
            Product product = _db.Product.Find(id);

            if (product == null)
            {
                return "blad bozy";
            }
            string returnValue = "&lth1&gt" + product.Description + "&lt/h1&gt" + "<br>" + "&ltp&gt" + product.Id + "&lth/p&gt" + "<br>" + "&lth2&gt" + product.Name + "&lt/h2&gt" + "<br>" + "&ltb&gt" + product.Price + "&lt/b&gt" + "<br>" + "&lti&gt" + product.Vat + "&lt/i&gt";



            return returnValue;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileResult CreatePdf()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("CatalogPdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
  
            PdfPTable tableLayout = new PdfPTable(4);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {

            float[] headers = { 50, 45, 24, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  

            List<Product> products = _db.Product.ToList<Product>();



            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0)))) {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Price");
            AddCellToHeader(tableLayout, "Vat");
            AddCellToHeader(tableLayout, "Expert Email");

            ////Add body  

            foreach (var prod in products)
            {

                AddCellToBody(tableLayout, prod.Name);
                AddCellToBody(tableLayout, prod.Price.ToString());
                AddCellToBody(tableLayout, prod.Vat.ToString() + "%");
                AddCellToBody(tableLayout, prod.EmailExpert);

            }

            return tableLayout;
        }
        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
    });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
             {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
    });
        }
    }
}
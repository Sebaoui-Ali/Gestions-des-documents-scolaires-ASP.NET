using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Document_Management_Application.ViewModels;
using Document_Management_Application.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.Net.Mail;
using System.Data.Entity;
using Syncfusion.XPS;

namespace Document_Management_Application.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private ApplicationDbContext _context;
        public ListController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var documents = _context.Documents.Include(m => m.Etudiant).ToList();
            return View(documents);
        }
        public ActionResult Accepted()
        {
            var documents = _context.Documents.Where(m => m.Status==1).Include(m => m.Etudiant).ToList();
            return View(documents);
        }
        public ActionResult Refused()
        {
            var documents = _context.Documents.Where(m => m.Status == 2).Include(m => m.Etudiant).ToList();
            return View(documents);
        }
        public ActionResult Create()
        {
            var viewModel = new Document {};
            return View(viewModel);
        }

        public ActionResult Convension()
        {
            var documents = _context.Documents.Where(m => m.Type == "Convention de stage").Include(m => m.Etudiant).ToList();
            return View(documents);
        }
        public ActionResult Attestation()
        {
            var documents = _context.Documents.Where(m => m.Type == "Attestation de reussite").Include(m => m.Etudiant).ToList();
            return View(documents);
        }
        public ActionResult certficat()
        {
            var documents = _context.Documents.Where(m => m.Type == "Certificat").Include(m => m.Etudiant).ToList();
            return View(documents);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Document model)
        {
            if (!ModelState.IsValid)
            {

                return View("Create", model);
            }
            var document = new Document
            {
                Type = model.Type,
            };
            _context.Documents.Add(document);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        
        public ActionResult CreateDocument(int i,int k)
        {
            Response.Write(i);
           
             
            var etudiant = _context.Etudiants.Find(i);
            var doc = _context.Documents.Find(k);
            

            string phrase = "";
            if(doc.Type== "Attestation de reussite")
            {
                 phrase = "Attestation de reussite   " + "\n" + "\n" +
                   "\n" + "\n" +
                   "Je soussigné, directeur de École Nationale des Sciences Appliquées de Tétouan  certifie que" + "\n" + "\n" + "\n" +
                   "l'étudiant : " + etudiant.Nom + "  " + etudiant.Prenom + "" + "\n"+ "Numéro de CIN : " + etudiant.Cin + "\n" + "Numéro d'apogée :" + etudiant.NumApogee + " \n "+ "a réussi ses années dans notre école..";
               
            }

            else if(doc.Type == "Certificat")
            {
                phrase = "Certificat de Scolarité   " + "\n" + "\n" +
                  "\n" + "\n" +
                  "Je soussigné, directeur de École Nationale des Sciences Appliquées de Tétouan  certifie que" + "\n" + "\n" + "\n" +
                  "l'étudiant : " + etudiant.Nom + "  " + etudiant.Prenom + "" + "\n" + "Numéro de CIN : " + etudiant.Cin + "\n" + "Numéro d'apogée :" + etudiant.NumApogee + " \n " + "continue  ses études dans notre école..";
               
            }
            else if (doc.Type == "Convention de stage")
            {
                phrase = "Convention de stage   " + "\n" + "\n" +
                  "\n" + "\n" +
                  "Je soussigné, directeur de École Nationale des Sciences Appliquées de Tétouan  certifie que" + "\n" + "\n" + "\n" +
                  "l'étudiant : " + etudiant.Nom + "  " + etudiant.Prenom + "" + "\n" + "Numéro de CIN : " + etudiant.Cin + "\n" + "Numéro d'apogée :" + etudiant.NumApogee + " \n " + "continue  ses études dans notre école..";

            }

            //name = nom;
            //Creates a new PDF document

            //Create an instance of PdfDocument.
            using (PdfDocument document = new PdfDocument())
            {
                //Adds page settings
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;
                document.PageSettings.Margins.All = 50;
                //Adds a page to the document
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                //Add a page to the document
                //Loads the image from disk
                PdfImage image = PdfImage.FromFile(Server.MapPath("~/uploads/ensa-tetouan.png"));
                RectangleF bounds = new RectangleF(176, 0, 390, 130);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

               //

                graphics.DrawString(phrase, font, PdfBrushes.Black, new PointF(0, 110));

                // Open the document in browser after saving it
                document.Save(etudiant.Nom + doc.DateCreation+".pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }
            return View();
        }


       
        public async System.Threading.Tasks.Task<ActionResult> SendRefusedEmailAsync(int j,int d)
        {
            
            var etudiant = _context.Etudiants.Find(j);
            var doc = _context.Documents.Find(d);
            doc.Status = 2;
            _context.SaveChanges();

            var mail = new MailMessage();
                mail.To.Add(new MailAddress(etudiant.Email));
                mail.Subject = "Reponde de votre demande";
                mail.Body = string.Format("<p>Email From: {0} ({1})</p><p></p><p>{2}</p>", "Ensaté", "", "Votre demande est refusé \n Merci de redemander ou de nous contacter");
                mail.IsBodyHtml = true;

                
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(mail);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SendHtmlEmailAsync(HttpPostedFileBase file, int g,int b)
        {
            var etudiant = _context.Etudiants.Find(g);
            var doc = _context.Documents.Find(b);
            doc.Status = 1;
            _context.SaveChanges();

            if (file != null && file.ContentLength > 0)
            {
                var mail = new MailMessage();
                mail.To.Add(new MailAddress(etudiant.Email));
                mail.Subject = "Reponde de votre demande";
                mail.Body = string.Format("<p>Email From: {0} ({1})</p><p></p><p>{2}</p>", "Ensaté", "", "Votre demande est accepté \n Voila le document ...");
                mail.IsBodyHtml = true;

                var path = Server.MapPath(@"~/uploads/");
                var attachment = new System.Net.Mail.Attachment(path + file.FileName);
                mail.Attachments.Add(attachment);

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(mail);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");

        }
    }
}
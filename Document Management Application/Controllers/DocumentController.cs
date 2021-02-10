using Document_Management_Application.Models;
using Document_Management_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document_Management_Application.Controllers
{

    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DocumentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult SaveRequest(EtudiantFormViewModel etudiant, string type)
        {
            int userCount;
            int id;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                connection.Open();
                string cmdText = $"SELECT COUNT(*) FROM Etudiants WHERE Email = '{etudiant.Email}' and NumApogee='{etudiant.NumApogee}'";
                using (SqlCommand sqlCommand = new SqlCommand(cmdText: cmdText,
                                                              connection))
                {
                    *//*string cmd = $"SELECT id FROM Etudiants WHERE Email = '{etudiant.Email}' and NumApogee='{etudiant.NumApogee}'";
                    SqlCommand Command = new SqlCommand(cmdText: cmd, connection);


                    userCount = (int)sqlCommand.ExecuteScalar();*/



        /* int cnt = _context.Etudiants.Where(m => m.Email == etudiant.Email).Count();
         if (cnt > 0)
         {
             var ad = _context.Etudiants.Where(m => m.Email == etudiant.Email).First();
             id = ad.Id;
         }*/
        /*id = (int)Command.ExecuteScalar();*//*

        userCount = (int)sqlCommand.ExecuteScalar();
        if (userCount > 0)
        {
            string cmd = $"SELECT id FROM Etudiants WHERE Email = '{etudiant.Email}' and NumApogee='{etudiant.NumApogee}'";
            SqlCommand Command = new SqlCommand(cmdText: cmd, connection);
            id = (int)Command.ExecuteScalar();
            connection.Close();
            var Document = new Document

            {
                EtudiantId = id,
                Type = type,
                Status = 0,
            };
            _context.Documents.Add(Document);
            _context.SaveChanges();
            return View("Message");
        }


        else
        {
            etudiant.erreur = "Cet Email ne correspondant a aucun etudiant";




        }
        connection.Close();
    }



}
}*/

        public ActionResult SaveRequest(EtudiantFormViewModel etudiant, string type)
        {
            int userCount;
            int id;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                connection.Open();
                string cmdText = $"SELECT COUNT(*) FROM Etudiants WHERE Email = '{etudiant.Email}' and NumApogee='{etudiant.NumApogee}'";
                using (SqlCommand sqlCommand = new SqlCommand(cmdText: cmdText,
                                                              connection))
                {
                    userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        string cmd = $"SELECT id FROM Etudiants WHERE Email = '{etudiant.Email}' and NumApogee='{etudiant.NumApogee}'";
                        SqlCommand Command = new SqlCommand(cmdText: cmd, connection);
                        id = (int)Command.ExecuteScalar();
                        connection.Close();
                        var Document = new Document
                        {
                            EtudiantId = id,
                            Type = type,
                            Status = 0,
                        };
                        _context.Documents.Add(Document);
                        _context.SaveChanges();
                        return View("Message");
                    }


                    else
                    {
                        etudiant.erreur = "Cet Email ou Numero d'appogee est incorrect";
                        return View("Create", etudiant);
                    }



                }

            }





        }
    }
}

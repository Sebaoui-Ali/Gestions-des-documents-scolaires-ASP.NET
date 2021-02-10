using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Document_Management_Application.Models
{
    public class Document
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
       
        public int Status { get; set; }

        public DateTime DateCreation { get; set; }

        public int EtudiantId { get; set; }

        public Etudiant Etudiant { get; set; }
        public Document()
        {
            DateCreation = DateTime.Now;
        }
    }
}

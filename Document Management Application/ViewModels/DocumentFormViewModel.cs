using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Document_Management_Application.Models;

namespace Document_Management_Application.ViewModels
{
    public class DocumentFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }

        public int Status { get; set; }

        public DateTime DateCreation { get; set; }

        public int EtudiantId { get; set; }

        public Etudiant Etudiant { get; set; }
    }
}
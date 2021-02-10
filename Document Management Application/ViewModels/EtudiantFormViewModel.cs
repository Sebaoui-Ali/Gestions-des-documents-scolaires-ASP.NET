using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Document_Management_Application.ViewModels
{
    public class EtudiantFormViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Display(Name ="Numero d'apogee")]
        [Required]
        [MaxLength(30)]
        public string NumApogee { get; set; }
        [Required]
        [MaxLength(30)]
        public string Cin { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nom { get; set; }
        [Required]
        [MaxLength(30)]
        public string Prenom { get; set; }
        public string erreur { get; internal set; }
    }
}

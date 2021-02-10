using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Document_Management_Application.Models
{
    public class Etudiant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string NumApogee { get; set; }
        [MaxLength(30)]
        public string Cin { get; set; }
        [MaxLength(30)]
        public string Nom { get; set; }
        [MaxLength(30)]
        public string Prenom { get; set; }
        
    }
}

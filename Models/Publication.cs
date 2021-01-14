using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentrumBibliotektet_Uppdaterad.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public int PublicationDate { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
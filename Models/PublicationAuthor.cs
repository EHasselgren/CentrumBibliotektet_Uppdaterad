using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentrumBibliotektet_Uppdaterad.Models
{

    public class PublicationAuthor
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}
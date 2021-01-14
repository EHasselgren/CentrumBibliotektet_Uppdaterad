using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentrumBibliotektet_Uppdaterad.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
        public List<Loan> Loans { get; set; }
        // kolla om bok är tillgänglig:
        public bool Available
        {
            get
            {
                if (Loans == null)
                {
                    return true;
                }
                else if (Loans.Count == 0)
                {
                    return true;
                }
                else if (Loans.All(l => l.Returned))
                {
                    return true;
                }

                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentrumBibliotektet_Uppdaterad.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int InventoryId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Publication Publication { get; set; }
        public Inventory Inventory { get; set; }
        public Customer Customer { get; set; }

        //calculate dueDate

        public DateTime CalcDueDate
        {
            get
            {
                return DueDate = RentalDate.AddMinutes(1);
            }
        }

        // kolla om bok är inlämnad
        public bool Returned {
            get
            {
                if (ReturnDate == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                //return ReturnDate != null ? false : true;
            }
        }
    }
}



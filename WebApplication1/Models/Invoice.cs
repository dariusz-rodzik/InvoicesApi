using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Invoice
    {
        [Required]
        public string buyer { get; set; }
        [Required]
        public string supplier { get; set; }
        [Required]
        public string invoice { get; set; }
        [Required]
        public DateTime invoice_date { get; set; }
        [Required]
        public DateTime invoice_due { get; set; }
        [Required]
        public string currency { get; set; }
        [Required]
        public float netto { get; set; }
        [Required]
        public float brutto { get; set; }
    }
}

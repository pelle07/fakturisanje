using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fakture___trial.Models
{
    public class FaktureModel
    {
        public int id { get; set; }
        [Display(Name ="Broj fakture")]
        public int number { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum fakture")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_created { get; set; }

        [Display(Name = "Ukupna vrednost fakture")]
        public double total { get; set; }
        
    }

    public class StavkaModel
    {
        public int id { get; set; }
        public int faktura_id { get; set; }

        [Required(ErrorMessage = "Unesite ime stavke")]
        [Display(Name = "Naziv stavke")]
        public string name { get; set; }

        [Required(ErrorMessage = "Unesite cenu")]
        [Display(Name = "Cena stavke")]
        [RegularExpression(@"^\d * (\.\d{0, 2})?$",
         ErrorMessage = "Samo decimalni brojevi do 2 decimale")]
        public double unit_price { get; set; }

        [Required(ErrorMessage = "Unesite kolicinu")]
        [Display(Name = "Kolicina")]
        [Range(0,1000000000)]
        public int quantity { get; set; }
        public double total_cost { get; set; }

    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ExchangeRate
    {        
        [ForeignKey("ExchangeOfficer")]
        public string ExchangeOfficerId { get; set; }
            
        public Currencies Currency { get; set; }

        public virtual ExchangeOfficer ExchangeOfficer { get; set; }

        public decimal BuyRate { get; set; }

        public decimal SellRate { get; set; }
    }
    
    public enum Currencies
    {
        USD,
        EUR,
        CHF,
    }
}
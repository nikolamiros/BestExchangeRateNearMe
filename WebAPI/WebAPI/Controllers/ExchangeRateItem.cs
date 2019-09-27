namespace WebAPI.DataContracts
{
    public class ExchangeRateItem
    {
        public string Currency { get; set; }

        public decimal BuyRate { get; set; }

        public decimal SellRate { get; set; }
    }
}

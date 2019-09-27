namespace WebAPI.DataContracts
{
    public class SetExchangeRateCommand
    {
        public decimal BuyRate { get; set; }

        public decimal SellRate { get; set; }
    }
}

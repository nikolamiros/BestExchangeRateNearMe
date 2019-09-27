using System.Collections.Generic;

namespace WebAPI.DataContracts
{
    public class GetExchangeRateResponse
    {
        public string ExchangeOfficerIdentifier { get; set; }

        public List<ExchangeRateItem> ExchangeRates { get; set; }
    }    
}

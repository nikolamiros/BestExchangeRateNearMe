using System.Collections.Generic;

namespace WebAPI.DataContracts
{
    public class SearchExchangeRateResponse
    {
        public string Currency { get; set; }

        public List<SearchExchangeRateResponseItem> Items { get; set; }
    }

    public class SearchExchangeRateResponseItem
    {
        public string ExchangeOfficerIdentifier { get; set; }

        public string ExchangeOfficerName { get; set; }

        public string ExchangeOfficerAddress { get; set; }

        //public double ExchangeOfficerLongitude { get; set; }

        //public double ExchangeOfficerLatitude { get; set; }

        public int ExchangeOfficerDistance { get; set; }

        public decimal Rate { get; set; }

        public string RateType { get; set; }
    }
}

export class ExchangeRateSearchParameters {

    currency: string;
    intention: string;
    latitude: number = 0;   
    longitude: number = 0;
    distance: number = 0;
}

export class SearchExchangeRateResponse
{
    currency: string;

    items:  SearchExchangeRateResponseItem[];
}

export class SearchExchangeRateResponseItem
{
    exchangeOfficerIdentifier: string;

    exchangeOfficerName: string;

    exchangeOfficerAddress: string;

    exchangeOfficerDistance: number;

    rate: number;

    rateType: string;
}
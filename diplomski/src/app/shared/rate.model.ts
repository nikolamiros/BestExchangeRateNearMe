export interface GetExchangeRateResponse {
    exchangeOfficerIdentifier: string;
    exchangeRates: ExchangeRateItem[];
}

export interface ExchangeRateItem {
    currency: string;
    buyRate: number;
    sellRate: number;
}

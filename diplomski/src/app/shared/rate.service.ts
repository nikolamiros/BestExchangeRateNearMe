import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetExchangeRateResponse, ExchangeRateItem } from './rate.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  readonly BaseURI = 'http://localhost:49909/api';

  constructor(private http: HttpClient) { }

  getExchangeRates(): Observable<GetExchangeRateResponse> {
    return this.http.get<GetExchangeRateResponse>(this.BaseURI + '/ExchangeRate');
  }

  saveExchangeRate(currentExchangeRate: ExchangeRateItem) {

    return this.http.post(
      this.BaseURI + '/ExchangeRate/' + currentExchangeRate.currency,
      {
        sellRate: currentExchangeRate.sellRate,
        buyRate: currentExchangeRate.buyRate,
      });
  }
}

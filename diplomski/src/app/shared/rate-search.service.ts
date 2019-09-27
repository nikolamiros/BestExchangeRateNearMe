import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { ExchangeRateSearchParameters, SearchExchangeRateResponse } from './rate-search.model';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RateSearchService {

  constructor(private http: HttpClient){ }

  readonly BaseURI = 'http://localhost:49909/api';

  private exchangeRateSearchedSource = new Subject<ExchangeRateSearchParameters>();

  
  exchangeRateSearched$ = this.exchangeRateSearchedSource.asObservable();

  
  exchangeRateSearch(searchParameters: ExchangeRateSearchParameters) {

    this.exchangeRateSearchedSource.next(searchParameters);
  }

  searchExchangeRate(searchParameters: ExchangeRateSearchParameters): Observable<SearchExchangeRateResponse>{

    const params = new HttpParams()
      .set('currency', searchParameters.currency)
      .set('distance', searchParameters.distance.toString())
      .set('longitude', searchParameters.longitude.toString())
      .set('latitude', searchParameters.latitude.toString())
      .set('intention', searchParameters.intention);

    return this.http.get<SearchExchangeRateResponse>(this.BaseURI + '/ExchangeRate/search',{params});
  }
}

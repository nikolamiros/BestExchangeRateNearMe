import { Component, OnInit } from '@angular/core';
import { RateSearchService } from 'src/app/shared/rate-search.service';
import { SearchExchangeRateResponse } from 'src/app/shared/rate-search.model';

@Component({
  selector: 'app-tabela-kurseva',
  templateUrl: './tabela-kurseva.component.html',
  styleUrls: ['./tabela-kurseva.component.css']
})
export class TabelaKursevaComponent implements OnInit {

  searchExchangeRateResponse: SearchExchangeRateResponse = new SearchExchangeRateResponse();

  constructor(private rateSearchService: RateSearchService) { }

  ngOnInit() {

    this.rateSearchService.exchangeRateSearched$.subscribe(searchParameters => {

        this.rateSearchService.searchExchangeRate(searchParameters).subscribe((res) => {

          this.searchExchangeRateResponse = res;
      });
    });
  }  
}

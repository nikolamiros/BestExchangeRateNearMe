import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { RateSearchService } from '../shared/rate-search.service';
import { ExchangeRateSearchParameters } from '../shared/rate-search.model';
import { UserGeolocation } from '../shared/user.model';

@Component({
  selector: 'app-korisnik',
  templateUrl: './korisnik.component.html',
  styleUrls: ['./korisnik.component.css']
})
export class KorisnikComponent implements OnInit {

  showTable = false;
  userDetails;
  exchangeRateSearchParameters: ExchangeRateSearchParameters = new ExchangeRateSearchParameters(); 
  constructor(
    private router: Router, 
    private service: UserService,
    private rateSearchService: RateSearchService) {             
    }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );    
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

  onSubmitRateSearchForm() {

    this.showTable = true;
    navigator.geolocation.getCurrentPosition((position: Position) => {
      if (position) {
        this.exchangeRateSearchParameters.latitude = position.coords.latitude;
        this.exchangeRateSearchParameters.longitude = position.coords.longitude;          
      }      
      this.rateSearchService.exchangeRateSearch(this.exchangeRateSearchParameters);      
    },(error: PositionError) => console.log(error));    
  }
}

import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { RateService } from '../shared/rate.service';
import { ExchangeRateItem } from '../shared/rate.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-menjacnica',
  templateUrl: './menjacnica.component.html',
  styleUrls: ['./menjacnica.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class MenjacnicaComponent implements OnInit {

  userDetails;
  rates: ExchangeRateItem[];
  closeResult: string;
  editExchangeRateForm: NgbModalRef;
  currentExchangeRate: ExchangeRateItem;

  constructor(private toastr: ToastrService, private modalService: NgbModal, private router: Router, private service: UserService, private rateService: RateService, private ser: RateService) { }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      },
    );

    this.rateService.getExchangeRates().subscribe(
      res => {
        this.rates = res.exchangeRates;
        console.log(this.rates)
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

  openExchangeEditForm(content, exchangeRate) {

    this.currentExchangeRate = exchangeRate

    this.editExchangeRateForm = this.modalService.open(content,
      {
        ariaLabelledBy: 'modal-basic-title',
        backdrop: 'static',
        keyboard: false,
        windowClass: 'custom-class'
      });
  }

  onSubmitExchangeEditForm() {

    this.rateService.saveExchangeRate(this.currentExchangeRate).subscribe((res) => {
      this.editExchangeRateForm.close();
      this.toastr.success('Novi kurs je postavljen!', 'Usprešna promena.');
    })
  }

}

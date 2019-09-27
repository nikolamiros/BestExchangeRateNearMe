import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    // if (localStorage.getItem('token') != null)
    //   this.router.navigateByUrl('/home');
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
        var role = payLoad.role;
        console.log(payLoad.UserID);
        if(role == 'Menjacnica')
        {
          this.router.navigateByUrl('/menjacnica');
        }
        else{
          this.router.navigateByUrl('/korisnik');
        }
        
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Netačno korisničko ime ili lozinka.', 'Neuspela autentifikacija.');
        else
          console.log(err);
      }
    );
  }
}

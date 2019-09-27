import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AgmCoreModule } from '@agm/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/user.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './home/map/map.component';
import { MenjacnicaComponent } from './menjacnica/menjacnica.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { KorisnikComponent } from './korisnik/korisnik.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { TabelaKursevaComponent } from './korisnik/tabela-kurseva/tabela-kurseva.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    MapComponent,
    MenjacnicaComponent,
    ForbiddenComponent,
    KorisnikComponent,
    TabelaKursevaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBf6HOKPSJyGXRLRJLjeFnlUSfRqoPPJBw'
    })
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

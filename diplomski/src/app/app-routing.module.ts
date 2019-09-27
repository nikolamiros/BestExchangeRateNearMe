import { AuthGuard } from './auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './home/map/map.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { MenjacnicaComponent } from './menjacnica/menjacnica.component';
import { KorisnikComponent } from './korisnik/korisnik.component';

const routes: Routes = [
  {path:'',redirectTo:'/user/login',pathMatch:'full'},
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {
    path:'home',component:HomeComponent,canActivate:[AuthGuard],
    children: [
      { path: 'map', component: MapComponent }
    ]
  },
  {path:'forbidden',component:ForbiddenComponent},
  {path:'menjacnica',component:MenjacnicaComponent,canActivate:[AuthGuard],data :{permittedRoles:['Menjacnica']}},
  {path:'korisnik',component:KorisnikComponent,canActivate:[AuthGuard],data :{permittedRoles:['Korisnik']},
  children: []}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
// import { NgModule } from '@angular/core';
import {  Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './security/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/auth-guard.service';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
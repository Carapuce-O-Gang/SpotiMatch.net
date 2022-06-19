import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from '@components/login/login.component';
import { RegisterComponent } from '@components/register/register.component';
import { HomepageComponent } from '@components/homepage/homepage.component';
import { GoalApiComponent } from '@components/goal-api/goal-api.component';
import { MatchComponent } from '@components/match/match.component';
import { AuthorizeGuard } from 'src/guards/authorize';

const routes: Routes = [
  { path: 'home', component: HomepageComponent },
  { path: 'match', component: MatchComponent },
	{ path: 'login', component: LoginComponent },
	{ path: 'register', component: RegisterComponent },
	{ path: 'goal-api', component: GoalApiComponent },
	{ path: '', component: LoginComponent, pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }

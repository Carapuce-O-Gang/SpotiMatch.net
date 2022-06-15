import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/guards/authorize';
import { HomepageComponent } from './homepage/homepage.component';
import { MatchComponent } from './match/match.component';


const routes: Routes = [
  { path: 'home', component: HomepageComponent },
  { path: 'match', component: MatchComponent },
  { path: '', canActivate: [AuthorizeGuard], component: HomepageComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

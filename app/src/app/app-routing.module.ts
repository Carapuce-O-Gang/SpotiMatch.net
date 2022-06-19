import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GoalApiComponent } from '@components/goal-api/goal-api.component';
import { HomepageComponent } from '@components/homepage/homepage.component';
import { MatchComponent } from '@components/match/match.component';

const routes: Routes = [
  { path: 'home', component: HomepageComponent },
  { path: 'match', component: MatchComponent },
  { path: 'goal-api', component: GoalApiComponent },
  { path: '', component: HomepageComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { Component, OnInit } from '@angular/core';
import { average } from 'color.js'
import { CdkDragDrop, copyArrayItem, moveItemInArray } from '@angular/cdk/drag-drop';
import { User } from 'src/app/model/user';
import { ApiService } from '@services/api-service/api.service';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {
  num = 60;
  
  //public
  public friendlist: User[] = [];
  public target: User[] = [];
  public showText: boolean = false;
  public options:number = 0;
  public style: any;
  public user!: User;
  public subscriptions: Subscription[] = [];
  //private
  private userColorDominant: any;
  private otherUserColorDominant: any;
  

  constructor(private apiService:ApiService) { }

  ngOnInit(){
    this.subscriptions.push(
      this.apiService.getUser().subscribe( user => this.user = user),
      this.apiService.getFriendList().subscribe( friendList => this.friendlist = friendList)
    );
    average('https://i.scdn.co/image/ab6775700000ee857b6e5a4ffdf90095c68ae386',{ format: 'hex' }).then( colorHexa => {
      this.userColorDominant =  colorHexa ;
    },);
  }

  ngOnDestroy(){
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  userSetMyStyles() {
    let styles = {
      'background': 'linear-gradient(180deg,' + this.userColorDominant + ' 0%, rgba(12,12,12,1) 100%)',
    };
    return styles;
  }

  otherUserSetMyStyles() {
    console.log(this.otherUserColorDominant);
    let styles = {
      'background': 'linear-gradient(180deg,' + this.otherUserColorDominant + ' 0%, rgba(12,12,12,1) 100%)',
    };
    return styles;
  }

  matchButtonClick(){
    this.showText = true;
    if(this.num < 30 ){
      this.options = 1;
    }
    if(this.num >= 30 && this.num < 80){
      this.options = 2;
    }
    if(this.num >= 80){
      this.options = 3;
    }
  }

  drop(event: CdkDragDrop<any[]>){
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      if(this.target.length === 0){
        copyArrayItem(
          event.previousContainer.data,
          event.container.data,
          event.previousIndex,
          event.currentIndex,
        );
      }
      else{
        this.target.splice(0);
        copyArrayItem(
          event.previousContainer.data,
          event.container.data,
          event.previousIndex,
          event.currentIndex,
        );
      }
      average(this.target[0].name,{ format: 'hex' }).then( colorHexa => {
        this.otherUserColorDominant =  colorHexa ;
        this.style = this.otherUserSetMyStyles();
      },);
    }
  }

}

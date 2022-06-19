import { Component, OnInit } from '@angular/core';
import { average } from 'color.js'
import { CdkDragDrop, copyArrayItem, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {

  userColorDominant: any;
  otherUserColorDominant: any;
  showText: boolean = false;
  num = 60;
  options:number = 0;
  style: any;


  constructor() { }

  ngOnInit(): void {
    average('https://i.scdn.co/image/ab6775700000ee857b6e5a4ffdf90095c68ae386',{ format: 'hex' }).then( colorHexa => {
      this.userColorDominant =  colorHexa ;
    },);
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

  friendlist = [{name:'Pedro',age:'14', img:'https://pbs.twimg.com/media/E0o2L4iWUAUiA9I.jpg'},{name:'Paul',age:'14', img:'https://i.scdn.co/image/ab6775700000ee857b6e5a4ffdf90095c68ae386'}];

  target: any[] = [];

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
      average(this.target[0].img,{ format: 'hex' }).then( colorHexa => {
        this.otherUserColorDominant =  colorHexa ;
        this.style = this.otherUserSetMyStyles();
      },);
    }
  }


}

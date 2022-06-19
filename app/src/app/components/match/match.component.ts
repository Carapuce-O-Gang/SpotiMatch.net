import { Component, OnInit } from '@angular/core';
import { average } from 'color.js'
import { CdkDragDrop, copyArrayItem, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {

  colorDominant: any;
  showText: boolean = false;
  num = 60;
  options:number = 0;


  constructor() { }

  ngOnInit(): void {
    average(this.friendlist[0].img,{ format: 'hex' }).then( x => {
      this.colorDominant =  x ;
      document.documentElement.style.setProperty("colorDominant",this.colorDominant);
    },);
  }

  setMyStyles() {
    let styles = {
      'background': 'linear-gradient(180deg,' + this.colorDominant + ' 0%, rgba(12,12,12,1) 100%)',
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

  friendlist = [{name:'Paul',age:'14', img:'https://pbs.twimg.com/media/E0o2L4iWUAUiA9I.jpg'}];

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
    }
  }


}

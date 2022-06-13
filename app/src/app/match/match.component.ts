import { Component, OnInit } from '@angular/core';
import { average } from 'color.js'
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss']
})
export class MatchComponent implements OnInit {

  colorDominant: any;
  showText: boolean = false;
  num = '90%';


  constructor() { }

  ngOnInit(): void {
    average('https://i.scdn.co/image/ab6775700000ee857b6e5a4ffdf90095c68ae386',{ format: 'hex' }).then( x => {
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
  }

  todo = ['Get to work', 'Pick up groceries', 'Go home', 'Fall asleep'];
  friendlist = [{name:'Paul',age:'14'}, {name:'Baba',age:'15'},{name:'Pedro',age:'16'}];
  target: any[] = [];

  drop(event: CdkDragDrop<string[]>) {
    //moveItemInArray(this.movies, event.previousIndex, event.currentIndex);
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      this.target.length = 0;
      this.target.push(event.container.data);
      console.log(this.target);
    }
  }
}
import { Component, OnInit } from '@angular/core';
import { average } from 'color.js'

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {
  
  constructor() { }
  colorDominant: any;
  ngOnInit(): void {
    average('https://i.scdn.co/image/ab6775700000ee857b6e5a4ffdf90095c68ae386',{ format: 'hex' }).then(x =>{
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


}

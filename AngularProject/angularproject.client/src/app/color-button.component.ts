import { Component } from '@angular/core';

@Component({
  selector: 'app-color-button',
  standalone: true,     
  template: `<button (click)="changeColor()" [style.backgroundColor]="color">
                Click me to change color
             </button>`,
  styles: [`button { color: white; padding: 10px 20px; border: none; cursor: pointer; }`]
})
export class ColorButtonComponent {
  color = 'blue';

  changeColor() {
    this.color = this.color === 'blue' ? 'red' : 'blue';
  }
}

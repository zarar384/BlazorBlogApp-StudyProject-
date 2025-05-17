import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CUSTOM_ELEMENTS_SCHEMA, NgModule, Injector } from '@angular/core';
import { createCustomElement } from '@angular/elements';

import { ColorButtonComponent } from './color-button.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ColorButtonComponent 
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [],
  //bootstrap: [AppComponent]
})

export class AppModule {
  constructor(private injector: Injector) { }

  ngDoBootstrap() {
    console.log('Registering color-button custom element');
    const el = createCustomElement(ColorButtonComponent, { injector: this.injector });
    if (!customElements.get('color-button')) {
      customElements.define('color-button', el);
    }
  }
}

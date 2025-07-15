import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { routes } from './app/app.routes';
import { FormsModule } from '@angular/forms';

bootstrapApplication(AppComponent, {
    providers: [
      importProvidersFrom(
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(routes)
      ),
    ],
  })
  .catch((err:any) => console.error(err));

  
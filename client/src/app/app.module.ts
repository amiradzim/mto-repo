import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PublicTableComponent } from './public-table/public-table.component';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { QueryFormComponent } from './query-form/query-form.component';
import { MainComponent } from './main/main.component';
import { RegisterComponent } from './register/register.component';
import { UserGuideComponent } from './user-guide/user-guide.component';
import { SharedModule } from './_modules/shared.module';
import { MaterialModule } from './_modules/material.module';
import { UploadFileComponent } from './upload-file/upload-file.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    PublicTableComponent,
    QueryFormComponent,
    MainComponent,
    RegisterComponent,
    UserGuideComponent,
    UploadFileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }

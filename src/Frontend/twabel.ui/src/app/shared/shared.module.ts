import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SearchModalComponent } from './components/search-modal/search-modal.component';
import { FooterComponent } from './components/footer/footer.component';
import { CopyrightComponent } from './components/copyright/copyright.component';
import { BackToTopComponent } from './components/back-to-top/back-to-top.component';



@NgModule({
  declarations: [
    HeaderComponent,
    SpinnerComponent,
    NavbarComponent,
    SearchModalComponent,
    FooterComponent,
    CopyrightComponent,
    BackToTopComponent],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }

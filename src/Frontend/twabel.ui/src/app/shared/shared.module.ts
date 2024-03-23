import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SearchModalComponent } from './components/search-modal/search-modal.component';
import { FooterComponent } from './components/footer/footer.component';
import { CopyrightComponent } from './components/copyright/copyright.component';
import { BackToTopComponent } from './components/back-to-top/back-to-top.component';



@NgModule({
  declarations: [
    SpinnerComponent,
    NavbarComponent,
    SearchModalComponent,
    FooterComponent,
    CopyrightComponent,
    BackToTopComponent],
  imports: [
    CommonModule
  ],
  exports: [NavbarComponent, FooterComponent, SpinnerComponent,CopyrightComponent, BackToTopComponent, SearchModalComponent]
})
export class SharedModule { }

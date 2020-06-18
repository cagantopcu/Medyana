import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ClinicListComponent } from './clinic-list/clinic-list.component';

@NgModule({
  imports: [
    CommonModule,   
    FormsModule,
    HttpClientModule,
    RouterModule.forChild([
      {
        path: 'clinic-list',
        component: ClinicListComponent
      }
      //,
      //{
      //  path: 'priceDefinitionView',
      //  component: PriceDefinitionViewComponent
      //}     
    ])
  ],
  declarations: [
    ClinicListComponent
  ]
})
export class ClinicModule {
  constructor() {
  }
}

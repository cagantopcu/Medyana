import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';

import {
  DxCheckBoxModule,
  DxSelectBoxModule,
  DxNumberBoxModule,
  DxFormModule
} from 'devextreme-angular';

import { ClinicModel } from '../clinicModel';
import { ApiResult } from '../../common/apiResult';
import { GlobalConstants } from 'src/app/common/globalConstants';

@Component({
  selector: 'app-clinic-detail',
  templateUrl: './clinic-detail.component.html',
  styleUrls: ['./clinic-detail.component.css']
})
export class ClinicDetailComponent implements OnInit {

  sub: any;
  selectedRecordId: number;
  viewType: string;
  clinicRecord: ClinicModel;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpClient: HttpClient,
    private location: Location
  ) {

  }

  ngOnInit() {

    debugger;

    this.sub = this.route
      .queryParams
      .subscribe(params => {
        // Defaults to 0 if no query param provided.
        this.selectedRecordId = params['id'] || 0;
        this.viewType = params['viewType'] || '';
        this.getClinicDetailById();
      });
  }

  /**
  * Close the current page and returns to previous
  */
  onCloseClick = () => {
    debugger;
    this.location.back();
  }

  isDisabled() {
    return (this.viewType === 'view');
  }

  onSaveClick = () => {

    debugger;

    if (this.viewType === 'edit') {
      this.updateClinic();
    }
  }

  private getClinicDetailById() {

    const endpoint = 'clinic/' + this.selectedRecordId;

    this.httpClient.get(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {
        this.clinicRecord = apiResult.result;
      }

    }, error => console.error(error));

  }

  private updateClinic() {

    const endpoint = 'clinic';

    this.httpClient.put(GlobalConstants.apiURL + endpoint, this.clinicRecord).subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {
        this.clinicRecord = apiResult.result;
      }

    }, error => console.error(error));

  }

  private addClinic() {
  }
}

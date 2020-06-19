import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import notify from 'devextreme/ui/notify';
import { confirm } from 'devextreme/ui/dialog';

import { GlobalConstants } from '../../common/globalConstants';
import { ClinicModel } from '../clinicModel';
import { ApiResult } from '../../common/apiResult';

@Component({
  selector: 'app-clinic-list',
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {

  clinicList: any = [];

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.getClinics();
  }
  /**
   * redirects to view detail of selected clinic item
   */
  onViewClicked = (event: any) => {

    this.navigateToStep(event, 'clinic-detail', 'view');
  }

  /**
   * Determines whether edit clicked on
   */
  onEditClicked = (event: any) => {

    this.navigateToStep(event, 'clinic-detail', 'edit');

  }

  /**
   * Delete event is clicked
   */
  onDeleteClicked = (event: any) => {

    const result = confirm(
      '<i>Are you sure you want to delete this record?</i>', 'Confirm changes'
    );

    result.then(dialogResult => {
      if (dialogResult) {
        this.deleteRecord(event.row.data.id);
      }
    });

  }

  /**
  * Delete event is clicked
  */
  onAddClicked = (event: any) => {



  }

  private getClinics() {

    this.httpClient.get(GlobalConstants.apiURL + 'clinic').subscribe(result => {

      const apiResult: ApiResult<ClinicModel> = result as ApiResult<ClinicModel>;

      if (apiResult.isSucceed) {
        this.clinicList = apiResult.result;
      }

    }, error => notify(error));

  }

  private deleteRecord(id: number) {

    if (!id) {
      return;
    }

    const endpoint = 'clinic/' + id;

    this.httpClient.delete(GlobalConstants.apiURL + endpoint).subscribe(result => {

      const apiResult: ApiResult<boolean> = result as ApiResult<boolean>;

      if (apiResult.isSucceed) {

        notify(apiResult.successMessage);
        this.getClinics();
        return;
      }

      notify(apiResult.errorMessage, 'error');

    }, error => notify(error)
    );

  }

  private navigateToStep(event: any, routeName: string, viewType: string) {

    this.router.navigate(['clinic-detail'], {
      queryParams:
      {
        id: event.row.data.id,
        viewType: viewType
      }
    });

  }
}

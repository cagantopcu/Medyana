import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalConstants } from '../../common/global-constants';

@Component({
  selector: 'app-clinic-list',
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {

  apiUrl: string = "http://localhost:63088/api/";

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getClinics();
  }

  public getClinics() {
    
    this.httpClient.get(GlobalConstants.apiURL + "clinic").subscribe(result => {
      debugger;
    }, error => console.error(error));

  }
}

import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {FormControl,} from '@angular/forms';
import {EntriesService} from '../_services/entries.service';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import * as XLSX from "xlsx";

@Component({
  selector: 'app-query-form',
  templateUrl: './query-form.component.html',
  styleUrls: ['./query-form.component.css']
})

export class QueryFormComponent implements OnInit {
  isLinear = false;
  fileName = 'consolidated-mto.xlsx'
  toSelectGroup: FormGroup;
  toGroupByGroup: FormGroup;
  toSumGroup: FormGroup;
  queryBuilderFormGroup: FormGroup;
  allComplete: boolean = false;

  projectNameList: string[] = [];
  platformNameList: string[] = [];
  selectColumnList: string[] = ['ProjName', 'PlatName', 'StructType', 'StructArea', 'PlatArea', 'SubArea', 'MatType', 'MatVariant', 'ProcMethod', 'DwgNo', 'DwgCode', 'MatGroup', 'Description', 'Diameter', 'Thickness', 'Nal', 'UnitWeight', 'BaseWeight', 'SurfaceArea'];
  sumColumnList: string[] = ['BaseWeight', 'SurfaceArea'];
  public displayedColumns: string[] = [];


  public entries: MatTableDataSource<any>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private _formBuilder: FormBuilder, private _entriesService: EntriesService) {
  }

  ngOnInit(): void {

    this._entriesService.getProjects().subscribe(response => {
      this.projectNameList = response;
    }, error => {
      console.log(error);
    })

    this._entriesService.getPlatforms().subscribe(response => {
      this.platformNameList = response;
    }, error => {
      console.log(error);
    })

    this.queryBuilderFormGroup = this._formBuilder.group({
      selectedProject: new FormControl(),
      selectedPlatforms: new FormControl(),
      selectColumn: new FormControl(),
      sumColumn: new FormControl(),
    });
    // this.toSelectGroup = this._formBuilder.group({
    //   firstCtrl: ['', Validators.required]
    // });
    // this.toGroupByGroup = this._formBuilder.group({
    //   secondCtrl: ['', Validators.required]
    // });
    // this.toSumGroup = this._formBuilder.group({
    //   thirdCtrl: ['', Validators.required]
    // });
  }

  onSubmit() {
    debugger;
    console.warn(this.queryBuilderFormGroup.value);
    this._entriesService.queryBuilder(this.queryBuilderFormGroup.value).subscribe(response => {
      // console.log(response);
      if (response && response.length > 0) {
        this.displayedColumns = Object.keys(response[0]);
        this.entries = new MatTableDataSource(response);
        this.entries.sort = this.sort;
        this.entries.paginator = this.paginator;
      }
    }, error => {
      console.log(error);

    })
  }

  /*exportcsv() {
    var html = document.getElementById('entries-table').outerHTML;
    this.export_table_to_csv(html, "table.csv");
  }
  download_csv(csv, filename) {
    var csvFile;
    var downloadLink;

    // CSV FILE
    csvFile = new Blob([csv], { type: "text/csv" });

    // Download link
    downloadLink = document.createElement("a");

    // File name
    downloadLink.download = filename;

    // We have to create a link to the file
    downloadLink.href = window.URL.createObjectURL(csvFile);

    // Make sure that the link is not displayed
    downloadLink.style.display = "none";

    // Add the link to your DOM
    document.body.appendChild(downloadLink);

    // Lanzamos
    downloadLink.click();
  }

  export_table_to_csv(html, filename) {
    var csv = [];
    var rows = document.querySelectorAll("table tr");

    for (var i = 0; i < rows.length; i++) {
      var row = [], cols = rows[i].querySelectorAll("td, th");

      for (var j = 0; j < cols.length; j++)
        row.push((cols[j] as any).innerText);

      csv.push(row.join(","));
    }

    // Download CSV
    this.download_csv(csv.join("\n"), filename);
  }*/

  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('entries-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }

  onRowClicked(row) {
    console.log('Row clicked: ', row);
  }

}

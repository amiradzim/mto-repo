import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import * as XLSX from 'xlsx';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TableFilter } from '../_models/table-filter';
import { EntryItems } from '../_models/entry-items';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-public-table',
  templateUrl: './public-table.component.html',
  styleUrls: ['./public-table.component.css']
})

export class PublicTableComponent implements OnInit {

  public displayedColumns: string[] = ['id', 'projName', 'platNo', 'platType', 'platArea',
    'subArea', 'matType', 'matVariant', 'procMethod', 'dwgNo', 'dwgCode', 'matGroup', 'description',
    'diameter', 'thickness', 'nal', 'unitWeight', 'baseWeight', 'surfaceArea'];

  public entries: MatTableDataSource<EntryItems>;
  public fileName = 'MTOTable.xlsx';
  public searchForm: FormGroup;

  public projName = '';
  public platNo = '';
  public platType = '';
  public platArea = '';
  public subArea = '';
  public matType = '';
  public matVariant = '';
  public procMethod = '';
  public matGroup = '';

  public dwgNo = '';
  public dwgCode = '';
  public description = '';
  public diameter = '';
  public thickness = '';
  public nal = '';
  public unitWeight = '';
  public baseWeight = '';
  public surfaceArea = '';

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    this.getEntries();
    this.searchFormInit();

    this.getFilterPredicate();

  }

  getEntries() {
    this.http.get(environment.apiUrl + 'entries').subscribe((response: any[]) => {
      this.entries = new MatTableDataSource(response);
      this.entries.sort = this.sort;
      this.entries.paginator = this.paginator;
      this.entries.filterPredicate = this.getFilterPredicate();
    }, error => {
      console.log(error);
    })
  }

  searchFormInit() {
    this.searchForm = new FormGroup({
      projName: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      platNo: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      platType: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      platArea: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      subArea: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      matType: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      matVariant: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      procMethod: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      matGroup: new FormControl('', Validators.pattern('^[a-zA-Z ]+$')),
      dwgNo: new FormControl(''),
      dwgCode: new FormControl(''),
      description: new FormControl(''),
      diameter: new FormControl(''),
      thickness: new FormControl(''),
      nal: new FormControl(''),
      unitWeight: new FormControl(''),
      baseWeight: new FormControl(''),
      surfaceArea: new FormControl(''),
    });
  }

  applyFilter() {

    if (this.entries.paginator) {
      this.entries.paginator.firstPage();
    }

    const prn = this.searchForm.get('projName').value;
    const pln = this.searchForm.get('platNo').value;
    const plt = this.searchForm.get('platType').value;
    const pla = this.searchForm.get('platArea').value;
    const psa = this.searchForm.get('subArea').value;
    const mty = this.searchForm.get('matType').value;
    const mva = this.searchForm.get('matVariant').value;
    const prm = this.searchForm.get('procMethod').value;
    const pgr = this.searchForm.get('matGroup').value;

    const dwgNo_c = this.searchForm.get('dwgNo').value;
    const dwgCode_c = this.searchForm.get('dwgCode').value;
    const description_c = this.searchForm.get('description').value;
    const diameter_c = this.searchForm.get('diameter').value;
    const thickness_c = this.searchForm.get('thickness').value;
    const nal_c = this.searchForm.get('nal').value;
    const unitWeight_c = this.searchForm.get('unitWeight').value;
    const baseWeight_c = this.searchForm.get('baseWeight').value;
    const surfaceArea_c = this.searchForm.get('surfaceArea').value;

    this.projName = prn === null ? '' : prn;
    this.platNo = pln === null ? '' : pln;
    this.platType = plt === null ? '' : plt;
    this.platArea = pla === null ? '' : pla;
    this.subArea = psa === null ? '' : psa;
    this.matType = mty === null ? '' : mty;
    this.matVariant = mva === null ? '' : mva;
    this.procMethod = prm === null ? '' : prm;
    this.matGroup = pgr === null ? '' : pgr;

    this.dwgNo = dwgNo_c === null ? '' : dwgNo_c
    this.dwgCode = dwgCode_c === null ? '' : dwgCode_c
    this.description = description_c === null ? '' : description_c
    this.diameter = diameter_c === null ? '' : diameter_c
    this.thickness = thickness_c === null ? '' : thickness_c
    this.nal = nal_c === null ? '' : nal_c
    this.unitWeight = unitWeight_c === null ? '' : unitWeight_c
    this.baseWeight = baseWeight_c === null ? '' : baseWeight_c
    this.surfaceArea = surfaceArea_c === null ? '' : surfaceArea_c

    // create string of our searching values and split if by '$'
    const filterValue = this.projName + '$' + this.platNo + '$' + this.platType +
      '$' + this.platArea + '$' + this.subArea + '$' + this.matType + '$' +
      this.matVariant + '$' + this.procMethod + '$' + this.matGroup +
      '$' + this.dwgNo +
      '$' + this.dwgCode +
      '$' + this.description +
      '$' + this.diameter +
      '$' + this.thickness +
      '$' + this.nal +
      '$' + this.unitWeight +
      '$' + this.baseWeight +
      '$' + this.surfaceArea

    this.entries.filter = filterValue.trim().toLowerCase();

  }

  getFilterPredicate() {
    return (row: TableFilter, filters: string) => {

      // split string per '$' to array
      const filterArray = filters.split('$');
      const projName = filterArray[0];
      const platNo = filterArray[1];
      const platType = filterArray[2];
      const platArea = filterArray[3];
      const subArea = filterArray[4];
      const matType = filterArray[5];
      const matVariant = filterArray[6];
      const procMethod = filterArray[7];
      const matGroup = filterArray[8];
      let dwgNo = filterArray[9];
      let dwgCode = filterArray[10];
      let description = filterArray[11];
      let diameter = filterArray[12];
      let thickness = filterArray[13];
      let nal = filterArray[14];
      let unitWeight = filterArray[15];
      let baseWeight = filterArray[16];
      let surfaceArea = filterArray[17];
      const matchFilter = [];

      // Fetch data from row
      const columnProjName = row.projName;
      const columnPlatNo = row.platNo;
      const columnPlatType = row.platType;
      const columnPlatArea = row.platArea;
      const columnSubArea = row.subArea;
      const columnMatType = row.matType;
      const columnMatVariant = row.matVariant;
      const columnProcMethod = row.procMethod;
      const columnMatGroup = row.matGroup;

      let columndwgNo = row.dwgNo;
      let columndwgCode = row.dwgCode;
      let columndescription = row.description;
      let columndiameter = row.diameter;
      let columnthickness = row.thickness;
      let columnnal = row.nal;
      let columnunitWeight = row.unitWeight;
      let columnbaseWeight = row.baseWeight;
      let columnsurfaceArea = row.surfaceArea;

      // verify fetching data by our searching values
      const customFilterPRN = columnProjName.toLowerCase().includes(projName);
      const customFilterPLN = columnPlatNo.toLowerCase().includes(platNo);
      const customFilterPLT = columnPlatType.toLowerCase().includes(platType);
      const customFilterPLA = columnPlatArea.toLowerCase().includes(platArea);
      const customFilterPSA = columnSubArea.toLowerCase().includes(subArea);
      const customFilterMTY = columnMatType.toLowerCase().includes(matType);
      const customFilterMVA = columnMatVariant.toLowerCase().includes(matVariant);
      const customFilterPRM = columnProcMethod.toLowerCase().includes(procMethod);
      const customFilterPGR = columnMatGroup.toLowerCase().includes(matGroup);

      let customFilterdwgNo = columndwgNo.toLowerCase().includes(dwgNo);
      let customFilterdwgCode = columndwgCode.toLowerCase().includes(dwgCode);
      let customFilterdescription = columndescription.toLowerCase().includes(description);
      let customFilterdiameter = columndiameter.toString().toLowerCase().includes(diameter);
      let customFilterthickness = columnthickness.toString().toLowerCase().includes(thickness);
      let customFilternal = columnnal.toString().toLowerCase().includes(nal);
      let customFilterunitWeight = columnunitWeight.toString().toLowerCase().includes(unitWeight);
      let customFilterbaseWeight = columnbaseWeight.toString().toLowerCase().includes(baseWeight);
      let customFiltersurfaceArea = columnsurfaceArea.toString().toLowerCase().includes(surfaceArea);

      // push boolean values into array

      matchFilter.push(customFilterPRN);
      matchFilter.push(customFilterPLN);
      matchFilter.push(customFilterPLT);
      matchFilter.push(customFilterPLA);
      matchFilter.push(customFilterPSA);
      matchFilter.push(customFilterMTY);
      matchFilter.push(customFilterMVA);
      matchFilter.push(customFilterPRM);
      matchFilter.push(customFilterPGR);

      matchFilter.push(customFilterdwgNo);
      matchFilter.push(customFilterdwgCode);
      matchFilter.push(customFilterdescription);
      matchFilter.push(customFilterdiameter);
      matchFilter.push(customFilterthickness);
      matchFilter.push(customFilternal);
      matchFilter.push(customFilterunitWeight);
      matchFilter.push(customFilterbaseWeight);
      matchFilter.push(customFiltersurfaceArea);

      // return true if all values in array is true
      // else return false
      return matchFilter.every(Boolean);
    };
  }

  onRowClicked(row) {
    console.log('Row clicked: ', row);
  }

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

}

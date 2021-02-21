import { Component, OnInit, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import * as XLSX from 'xlsx';
import { EntriesService } from '../_services/entries.service';
import { AccountService } from '../_services/account.service';
@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})

export class UploadFileComponent implements OnInit {
  @ViewChild('inputFile')
  inputFile: ElementRef;
  public progress: number;
  public message: string;
  public selectedFile: File;
  public isUser: boolean;
  @Output() public onUploadFinished = new EventEmitter();
  constructor(private http: HttpClient, private _entriesService: EntriesService, private _accountService: AccountService) { }

  ngOnInit(): void {
    this.isUser = this._accountService.getCurrentUser().role == "User";

  }
  selectFile(files) {



    if (files.length === 0) {
      return;
    }
    this.selectedFile = <File>files[0];
  }
  public uploadFile = () => {
    if (!this.selectedFile) {
      return;
    }
    let fileToUpload = this.selectedFile;

    let fileReader = new FileReader();
    fileReader.readAsBinaryString(fileToUpload);
    fileReader.onload = (event) => {
      let data = event.target.result;
      let workbook = XLSX.read(data, { type: "binary" });
      console.log(workbook);
      workbook.SheetNames.forEach(sheet => {
        let rowObject: any = XLSX.utils.sheet_to_json(workbook.Sheets[sheet]);
        let req: any = { Entries: JSON.stringify(rowObject, undefined, 4) }
        this._entriesService.insertEntries(req).subscribe(response => {

          if (response) {
            this.message = "Data Successfully Imported"
          }
        }, error => {
          this.message = "Data import failed"


        });

      });
      console.log(this.inputFile.nativeElement.files);
      this.inputFile.nativeElement.value = "";
      console.log(this.inputFile.nativeElement.files);
    }
  }
}

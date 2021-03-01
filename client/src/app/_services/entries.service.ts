import {HttpClient, HttpHeaders, JsonpInterceptor} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
//import { Member } from '../_models/member';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class EntriesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getProjects() {
    return this.http.get<string[]>(this.baseUrl + 'entries/projname-list', httpOptions);
  }

  getPlatforms() {
    return this.http.get<string[]>(this.baseUrl + 'entries/platname-list', httpOptions);
  }

  getStructType() {
    return this.http.get<string[]>(this.baseUrl + 'entries/structtype-list', httpOptions);
  }

  getStructArea() {
    return this.http.get<string[]>(this.baseUrl + 'entries/structarea-list', httpOptions);
  }

  getPlatArea() {
    return this.http.get<string[]>(this.baseUrl + 'entries/platarea-list', httpOptions);
  }

  getSubArea() {
    return this.http.get<string[]>(this.baseUrl + 'entries/subarea-list', httpOptions);
  }

  getMatType() {
    return this.http.get<string[]>(this.baseUrl + 'entries/mattype-list', httpOptions);
  }

  getMatVariant() {
    return this.http.get<string[]>(this.baseUrl + 'entries/matvariant-list', httpOptions);
  }

  getProcMethod() {
    return this.http.get<string[]>(this.baseUrl + 'entries/procmethod-list', httpOptions);
  }

  getMatGroup() {
    return this.http.get<string[]>(this.baseUrl + 'entries/matgroup-list', httpOptions);
  }

  queryBuilder(model: any) {
    return this.http.post<any[]>(this.baseUrl + 'entries/queryBuilder', model);
  }

  insertEntries(model: any) {
    return this.http.post<any>(this.baseUrl + 'entries/insert', model);
  }


}

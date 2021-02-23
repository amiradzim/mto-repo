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
    return this.http.get<string[]>(this.baseUrl + 'entries/projects', httpOptions);
  }

  getPlatforms() {
    return this.http.get<string[]>(this.baseUrl + 'entries/platforms', httpOptions);
  }

  queryBuilder(model: any) {
    return this.http.post<any[]>(this.baseUrl + 'entries/queryBuilder', model);
  }

  insertEntries(model: any) {
    return this.http.post<any>(this.baseUrl + 'entries/insert', model);
  }


}

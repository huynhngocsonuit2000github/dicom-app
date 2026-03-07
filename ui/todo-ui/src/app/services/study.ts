import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Study } from '../models/study.model';

@Injectable({ providedIn: 'root' })
export class StudyService {

  private url = `${environment.apiBaseUrl}/api/studies`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Study[]> {
    return this.http.get<Study[]>(this.url, { withCredentials: true });
  }

  create(files: File[], patientName: string) {
    const formData = new FormData();

    files.forEach(file => {
      formData.append('files', file);
    });

    formData.append('patientName', patientName);

    return this.http.post(this.url, formData, { withCredentials: true });
  }

  delete(id: number) {
    return this.http.delete(`${this.url}/${id}`, { withCredentials: true });
  }
}
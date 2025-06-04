import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
<<<<<<< HEAD
  private apiUrl = 'http://localhost:7215/Labs';
=======
  private apiUrl = 'http://localhost:5208/Labs';
>>>>>>> 875b81d (Initial commit to main)

  constructor(private http: HttpClient) {}

  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiUrl}/upload`, formData);
  }

  downloadFile(fileId: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/download/${fileId}`, { responseType: 'blob' });
  }
}

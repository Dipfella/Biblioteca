import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, catchError, throwError } from 'rxjs';
import { LibroModel } from '../../shared/models/libro.model';

@Injectable({ providedIn: 'root' })
export class LibrosService {
  private readonly apiUrl = `${environment.apiUrl}/Libros`;

  constructor(private readonly http: HttpClient) { }

  getLibros(
    genero?: string,
    autor?: string,
    titulo?: string,
    cantidadRegistros: number = 50
  ): Observable<LibroModel[]> {
    let params = new HttpParams()
      .set('cantidadRegistros', cantidadRegistros)
      .set('genero', genero || '')
      .set('autor', autor || '')
      .set('titulo', titulo || '');

    return this.http.get<LibroModel[]>(this.apiUrl, { params }).pipe(
      catchError(err => {
        console.error('Error al obtener libros:', err);
        return throwError(() => new Error('No se pudieron cargar los libros.'));
      })
    );
  }

  getById(id: number): Observable<LibroModel> {
    return this.http.get<LibroModel>(`${this.apiUrl}/${id}`);
  }

  create(book: LibroModel): Observable<LibroModel> {
    return this.http.post<LibroModel>(this.apiUrl, book);
  }

  update(id: number, book: LibroModel): Observable<LibroModel> {
    return this.http.put<LibroModel>(`${this.apiUrl}/${id}`, book);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

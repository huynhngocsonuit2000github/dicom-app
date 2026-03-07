import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Todo {
  id: number;
  title: string;
  isDone: boolean;
}

@Injectable({ providedIn: 'root' })
export class TodoService {
  private url = `${environment.apiBaseUrl}/api/todos`;

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.url, {
      withCredentials: true
    });
  }

  createTodo(title: string): Observable<Todo> {
    return this.http.post<Todo>(this.url, { title }, {
      withCredentials: true
    });
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`, {
      withCredentials: true
    });
  }
}

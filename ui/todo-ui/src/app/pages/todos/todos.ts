import { Component, OnInit } from '@angular/core';
import { Todo, TodoService } from '../../services/todo';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './todos.html',
  styleUrls: ['./todos.scss']
})
export class Todos implements OnInit {

  todos$ = new BehaviorSubject<Todo[]>([]);

  title = '';
  error = '';
  loading = false;

  constructor(private todoSvc: TodoService) {
  }

  ngOnInit(): void {

    this.load();
  }

  test() {
    this.load();
  }

  load() {
    this.loading = true;

    this.todoSvc.getTodos().subscribe({
      next: r => {
        this.todos$.next(r);
        this.loading = false;
      },
      error: _ => {
        this.error = 'Load failed';
        this.loading = false;
      }
    });
  }

  create() {

    if (!this.title.trim()) return;

    this.todoSvc.createTodo(this.title).subscribe({
      next: t => {
        this.title = '';
        this.load();
        console.log('create nnn');

      },
      error: _ => this.error = 'Create failed'
    });
  }

  delete(id: number) {
    this.todoSvc.deleteTodo(id).subscribe({
      next: () => {
        console.log('success');
        this.title = '';
        this.load();
      },
      error: _ => this.error = 'Delete failed!'
    });

  }
}

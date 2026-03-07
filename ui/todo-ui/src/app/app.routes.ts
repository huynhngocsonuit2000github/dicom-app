import { Routes } from '@angular/router';
import { Todos } from './pages/todos/todos';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { About } from './pages/about/about';
import { Studies } from './pages/studies/studies';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'todos', component: Todos },
    { path: 'studies', component: Studies },
    { path: 'about', component: About },
    { path: 'login', component: Login },
    { path: '**', redirectTo: '' },
];
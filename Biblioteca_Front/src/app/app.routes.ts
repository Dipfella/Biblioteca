import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'libros',
    loadChildren: () =>
      import('./modules/libros/libros.module').then(m => m.LibrosModule)
  },
  {
    path: '',
    redirectTo: 'libros',
    pathMatch: 'full'
  }
];

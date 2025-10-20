import { Routes } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';

export const routes: Routes = [
  {
    path: 'libros',
    component: LayoutComponent,
    loadChildren: () =>
      import('./modules/libros/libros.module').then(m => m.LibrosModule)
  },
  {
    path: '',
    redirectTo: 'libros',
    pathMatch: 'full'
  }
];

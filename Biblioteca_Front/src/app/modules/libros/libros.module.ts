import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LIBROS_ROUTES } from './libros.routes';
import { ListaLibrosComponent } from './pages/lista-libros/lista-libros.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(LIBROS_ROUTES),
    ListaLibrosComponent
  ]
})
export class LibrosModule {}

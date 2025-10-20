import { Component, OnInit } from '@angular/core';
import { LibrosService } from '../../../../core/services/libros.service.service';
import { LibroModel } from '../../../../shared/models/libro.model';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../../environments/environment';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lista-libros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './lista-libros.component.html'
})
export class ListaLibrosComponent implements OnInit {
  model: LibroModel = {
    id: 0,
    titulo: '',
    autor: '',
    genero: '',
    portadaUrl: ''
  };
  libros: LibroModel[] = [];
  librosSeleccionados: any[] = [];
  ejemplaresSeleccionados: { [libroId: number]: any[] } = {};
  librosExtendidos: { [libroId: number]: boolean } = {};
  nombresLibros: any[] = [];
  autoresLibros: any[] = [];
  generosLibros: any[] = [];
  loading = true;
  errorMessage = '';
  environment = environment;

  constructor(private readonly librosService: LibrosService) { }

  ngOnInit(): void {
    this.getLibros();
  }
  getLibros() {
    this.model.autor = this.model.autor || '';
    this.model.genero = this.model.genero || '';
    this.model.titulo = this.model.titulo || '';
    this.loading = true;
    this.librosService.getLibros(this.model.genero, this.model.autor, this.model.titulo, 20).subscribe({
      next: data => {
        this.libros = data;
        this.nombresLibros = this.limpiarDuplicados(this.libros, 'titulo');
        this.autoresLibros = this.limpiarDuplicados(this.libros, 'autor');
        this.generosLibros = this.limpiarDuplicados(this.libros, 'genero');
        this.loading = false;
      },
      error: err => {
        this.errorMessage = err.message;
        this.loading = false;
      }
    });
  }

  limpiarDuplicados(array: any[], propiedad: string): any[] {
    const mapa = new Map<string, any>();
    for (const item of array) {
      if (!mapa.has(item[propiedad])) {
        mapa.set(item[propiedad], item);
      }
    }
    return Array.from(mapa.values());
  }

  onImageError(event: Event) {
    const img = event.target as HTMLImageElement;
    img.onerror = null;
    img.src = 'images/no-cover.png';
  }

  esNoDisponible(libro: any): boolean {
    return libro.ejemplares.every((e: { disponible: any; }) => !e.disponible);
  }

  // Método para seleccionar o deseleccionar un libro
  toggleSeleccion(libro: any) {
    if (this.esNoDisponible(libro)) { return };

    const index = this.librosSeleccionados.findIndex(l => l.id === libro.id);
    if (index > -1) {
      this.librosSeleccionados.splice(index, 1);
      delete this.ejemplaresSeleccionados[libro.id];
      delete this.librosExtendidos[libro.id];
    } else {
      this.librosSeleccionados.push(libro);
      this.ejemplaresSeleccionados[libro.id] = [];
      this.librosExtendidos[libro.id] = true;
    }
  }

  // Devuelve los ejemplares disponibles de un libro
  getEjemplaresDisponibles(libro: any) {
    return libro.ejemplares.filter((e: any) => e.disponible);
  }

  // Selecciona o deselecciona un ejemplar de un libro
  toggleEjemplar(libroId: number, ejemplar: any) {
    const seleccionados = this.ejemplaresSeleccionados[libroId];
    const index = seleccionados.findIndex((e: any) => e.codigoInventario === ejemplar.codigoInventario);

    if (index > -1) {
      seleccionados.splice(index, 1);
    } else {
      seleccionados.push(ejemplar);
    }
  }

  // Verifica si un ejemplar está seleccionado
  estaSeleccionadoEjemplar(libroId: number, ejemplar: any) {
    return this.ejemplaresSeleccionados[libroId]?.some((e: any) => e.codigoInventario === ejemplar.codigoInventario);
  }

  estaSeleccionado(libro: any): boolean {
    return this.librosSeleccionados.some(l => l.id === libro.id);
  }

  generarPrestamo() {
    const prestamos = this.librosSeleccionados.map(libro => {
      const ejemplares = this.ejemplaresSeleccionados[libro.id] || [];

      return {
        libroId: libro.id,
        titulo: libro.titulo,
        ejemplares: ejemplares.map((e: any) => ({
          id: e.id,
          libroId: e.libroId,
          codigoInventario: e.codigoInventario,
          disponible: e.disponible
        }))
      };
    });

    console.log('Resultado final:', prestamos);
  }

}
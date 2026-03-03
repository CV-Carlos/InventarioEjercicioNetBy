import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Producto, Categoria } from '../../../../core/models';
import { ProductoService, CategoriaService, MensajeService } from '../../../../core/services';
import { AlertaComponent } from '../../../../shared/components/alerta/alerta.component';

@Component({
  selector: 'app-lista-productos',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, AlertaComponent],
  templateUrl: './lista-productos.component.html',
  styleUrl: './lista-productos.component.scss',
})
export class ListaProductosComponent implements OnInit {
  productos: Producto[] = [];
  categorias: Categoria[] = [];
  cargando = false;
  mensaje: { texto: string; tipo: 'success' | 'danger' } | null = null;

  // Filtros
  filtroNombre = '';
  filtroCategoriaId: number | undefined;
  filtroPrecioMin: number | undefined;
  filtroPrecioMax: number | undefined;
  filtroActivo: boolean | undefined;

  // Paginación
  totalItems = 0;
  paginaActual = 1;
  itemsPorPagina = 10;
  totalPaginas = 0;

  constructor(
    private productoService: ProductoService,
    private categoriaService: CategoriaService,
    private mensajeService: MensajeService
  ) {}

  ngOnInit(): void {
    this.cargarCategorias();
    this.cargarProductos();
  }

  cargarCategorias(): void {
    this.categoriaService.obtenerTodos().subscribe({
      next: (data) => (this.categorias = data),
    });
  }

  cargarProductos(): void {
    this.cargando = true;
    this.productoService
      .obtenerTodos({
        nombre: this.filtroNombre || undefined,
        categoriaId: this.filtroCategoriaId,
        precioMin: this.filtroPrecioMin,
        precioMax: this.filtroPrecioMax,
        activo: this.filtroActivo,
        pagina: this.paginaActual,
        itemsPorPagina: this.itemsPorPagina,
      })
      .subscribe({
        next: (data) => {
          this.mensajeService.cerrar();
          this.productos = data.items;
          this.totalItems = data.totalItems;
          this.totalPaginas = data.totalPaginas;
          this.cargando = false;
        },
        error: () => {
          this.productos = [];
          this.cargando = false;
        },
      });
  }

  cambiarPagina(pagina: number): void {
    if (pagina < 1 || pagina > this.totalPaginas) return;
    this.paginaActual = pagina;
    this.cargarProductos();
  }

  limpiarFiltros(): void {
    this.filtroNombre = '';
    this.filtroCategoriaId = undefined;
    this.filtroPrecioMin = undefined;
    this.filtroPrecioMax = undefined;
    this.filtroActivo = undefined;
    this.paginaActual = 1;
    this.cargarProductos();
  }

  eliminar(id: number): void {
    if (!confirm('¿Está seguro de eliminar este producto?')) return;
    this.productoService.eliminar(id).subscribe({
      next: () => {
        this.mensajeService.mostrar('Producto eliminado correctamente', 'success');
        this.cargarProductos();
      },
      error: () => {},
    });
  }

  get paginas(): number[] {
    return Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
  }
}

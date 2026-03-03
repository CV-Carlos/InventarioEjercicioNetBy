import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Transaccion, Producto, ProductoSelect } from '../../../../core/models';
import { TransaccionService, ProductoService, MensajeService } from '../../../../core/services';
import { AlertaComponent } from '../../../../shared/components/alerta/alerta.component';

@Component({
  selector: 'app-lista-transacciones',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, AlertaComponent],
  templateUrl: './lista-transacciones.component.html',
  styleUrl: './lista-transacciones.component.scss',
})
export class ListaTransaccionesComponent implements OnInit {
  transacciones: Transaccion[] = [];
  productos: ProductoSelect[] = [];
  cargando = false;
  mensaje: { texto: string; tipo: 'success' | 'danger' } | null = null;

  // Filtros
  filtroProductoId: number | undefined;
  filtroTipo: string = '';
  filtroFechaInicio: string = '';
  filtroFechaFin: string = '';

  // Paginación
  totalItems = 0;
  paginaActual = 1;
  itemsPorPagina = 10;
  totalPaginas = 0;

  constructor(
    private transaccionService: TransaccionService,
    private productoService: ProductoService,
    private mensajeService: MensajeService
  ) {}

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarTransacciones();
  }

  cargarProductos(): void {
    this.productoService.obtenerParaSelect().subscribe({
      next: (data) => (this.productos = data),
      error: () => {},
    });
  }

  cargarTransacciones(): void {
    this.cargando = true;
    this.transaccionService
      .obtenerTodos({
        productoId: this.filtroProductoId,
        tipo: this.filtroTipo || undefined,
        fechaInicio: this.filtroFechaInicio || undefined,
        fechaFin: this.filtroFechaFin || undefined,
        pagina: this.paginaActual,
        itemsPorPagina: this.itemsPorPagina,
      })
      .subscribe({
        next: (data) => {
          this.mensajeService.cerrar();
          this.transacciones = data.items;
          this.totalItems = data.totalItems;
          this.totalPaginas = data.totalPaginas;
          this.cargando = false;
        },
        error: () => {
          this.transacciones = [];
          this.cargando = false;
        },
      });
  }

  limpiarFiltros(): void {
    this.filtroProductoId = undefined;
    this.filtroTipo = '';
    this.filtroFechaInicio = '';
    this.filtroFechaFin = '';
    this.paginaActual = 1;
    this.cargarTransacciones();
  }

  eliminar(id: number): void {
    if (!confirm('¿Está seguro de eliminar esta transacción?')) return;
    this.transaccionService.eliminar(id).subscribe({
      next: () => {
        this.mensajeService.mostrar('Transacción eliminada correctamente', 'success');
        this.cargarTransacciones();
      },
      error: () => {},
    });
  }

  get transaccionesPaginadas(): Transaccion[] {
    const inicio = (this.paginaActual - 1) * this.itemsPorPagina;
    return this.transacciones.slice(inicio, inicio + this.itemsPorPagina);
  }

  cambiarPagina(pagina: number): void {
    if (pagina < 1 || pagina > this.totalPaginas) return;
    this.paginaActual = pagina;
    this.cargarTransacciones();
  }

  get paginas(): number[] {
    return Array.from({ length: this.totalPaginas }, (_, i) => i + 1);
  }
}

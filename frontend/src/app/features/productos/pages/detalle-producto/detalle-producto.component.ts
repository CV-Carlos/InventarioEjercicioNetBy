import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductoService } from '../../../../core/services';
import { Producto } from '../../../../core/models';

@Component({
  selector: 'app-detalle-producto',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './detalle-producto.component.html',
  styleUrl: './detalle-producto.component.scss',
})
export class DetalleProductoComponent implements OnInit {
  producto: Producto | null = null;
  cargando = false;

  constructor(
    private route: ActivatedRoute,
    private productoService: ProductoService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.cargando = true;
    this.productoService.obtenerPorId(id).subscribe({
      next: (data) => {
        this.producto = data;
        this.cargando = false;
      },
      error: () => (this.cargando = false),
    });
  }
}

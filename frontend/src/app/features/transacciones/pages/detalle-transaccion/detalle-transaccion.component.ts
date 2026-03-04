import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TransaccionService } from '../../../../core/services';
import { Transaccion } from '../../../../core/models';

@Component({
  selector: 'app-detalle-transaccion',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './detalle-transaccion.component.html',
  styleUrl: './detalle-transaccion.component.scss',
})
export class DetalleTransaccionComponent implements OnInit {
  transaccion: Transaccion | null = null;
  cargando = false;

  constructor(
    private route: ActivatedRoute,
    private transaccionService: TransaccionService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.cargando = true;
    this.transaccionService.obtenerPorId(id).subscribe({
      next: (data) => {
        this.transaccion = data;
        this.cargando = false;
      },
      error: () => (this.cargando = false),
    });
  }
}

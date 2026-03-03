import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Producto, ProductoSelect } from '../../../../core/models';
import { TransaccionService, ProductoService, MensajeService } from '../../../../core/services';
import { AlertaComponent } from '../../../../shared/components/alerta/alerta.component';

@Component({
  selector: 'app-form-transaccion',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, AlertaComponent],
  templateUrl: './form-transaccion.component.html',
  styleUrl: './form-transaccion.component.scss',
})
export class FormTransaccionComponent implements OnInit {
  form!: FormGroup;
  productos: ProductoSelect[] = [];
  productoSeleccionado: ProductoSelect | null = null;
  esEdicion = false;
  transaccionId?: number;
  cargando = false;
  guardando = false;
  mensaje: { texto: string; tipo: 'success' | 'danger' } | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private transaccionService: TransaccionService,
    private productoService: ProductoService,
    private mensajeService: MensajeService
  ) {}

  ngOnInit(): void {
    this.inicializarForm();
    this.cargarProductos();

    this.transaccionId = this.route.snapshot.params['id'];
    if (this.transaccionId) {
      this.esEdicion = true;
      this.cargarTransaccion();
    }
  }

  inicializarForm(): void {
    this.form = this.fb.group({
      tipo: ['', Validators.required],
      productoId: [null, Validators.required],
      cantidad: [null, [Validators.required, Validators.min(1)]],
      precioUnitario: [null, [Validators.required, Validators.min(0)]],
      detalle: [''],
    });

    // Cuando cambia el producto, cargamos su info
    this.form.get('productoId')?.valueChanges.subscribe((id) => {
      this.productoSeleccionado = this.productos.find((p) => p.id === +id) || null;
    });
  }

  cargarProductos(): void {
    this.productoService.obtenerParaSelect().subscribe({
      next: (data) => (this.productos = data),
      error: () => {},
    });
  }

  cargarTransaccion(): void {
    this.cargando = true;
    this.transaccionService.obtenerPorId(this.transaccionId!).subscribe({
      next: (transaccion) => {
        this.form.patchValue(transaccion);
        this.cargando = false;
      },
      error: () => {
        this.cargando = false;
      },
    });
  }

  guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    // Validación de stock en frontend
    const tipo = this.form.get('tipo')?.value;
    const cantidad = this.form.get('cantidad')?.value;
    if (
      tipo === 'Venta' &&
      this.productoSeleccionado &&
      cantidad > this.productoSeleccionado.stock
    ) {
      this.mensajeService.mostrar(
        `Stock insuficiente. Stock disponible: ${this.productoSeleccionado.stock}`,
        'danger'
      );
      return;
    }

    this.guardando = true;
    const datos = this.form.value;

    const operacion = this.esEdicion
      ? this.transaccionService.actualizar(this.transaccionId!, datos)
      : this.transaccionService.crear(datos);

    operacion.subscribe({
      next: () => {
        this.mensajeService.mostrar(
          `Transacción ${this.esEdicion ? 'actualizada' : 'creada'} correctamente`,
          'success'
        );
        setTimeout(() => this.router.navigate(['/transacciones']), 1500);
      },
      error: (err) => {
        this.guardando = false;
      },
    });
  }

  campoInvalido(campo: string): boolean {
    const control = this.form.get(campo);
    return !!(control?.invalid && control?.touched);
  }
}

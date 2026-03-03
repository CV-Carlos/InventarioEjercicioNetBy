import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Categoria } from '../../../../core/models';
import { ProductoService, CategoriaService, MensajeService } from '../../../../core/services';
import { AlertaComponent } from '../../../../shared/components/alerta/alerta.component';

@Component({
  selector: 'app-form-producto',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, AlertaComponent],
  templateUrl: './form-producto.component.html',
  styleUrl: './form-producto.component.scss',
})
export class FormProductoComponent implements OnInit {
  form!: FormGroup;
  categorias: Categoria[] = [];
  esEdicion = false;
  productoId?: number;
  cargando = false;
  guardando = false;
  mensaje: { texto: string; tipo: 'success' | 'danger' } | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private productoService: ProductoService,
    private categoriaService: CategoriaService,
    private mensajeService: MensajeService
  ) {}

  ngOnInit(): void {
    this.inicializarForm();
    this.cargarCategorias();

    this.productoId = this.route.snapshot.params['id'];
    if (this.productoId) {
      this.esEdicion = true;
      this.cargarProducto();
    }
  }

  inicializarForm(): void {
    this.form = this.fb.group({
      nombre: ['', [Validators.required, Validators.maxLength(200)]],
      descripcion: [''],
      categoriaId: [null, Validators.required],
      imagenUrl: [''],
      precio: [null, [Validators.required, Validators.min(0)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      activo: [true],
    });
  }

  cargarCategorias(): void {
    this.categoriaService.obtenerTodos().subscribe({
      next: (data) => (this.categorias = data),
      error: () => {},
    });
  }

  cargarProducto(): void {
    this.cargando = true;
    this.productoService.obtenerPorId(this.productoId!).subscribe({
      next: (producto) => {
        this.form.patchValue(producto);
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

    this.guardando = true;
    const datos = this.form.value;

    const operacion = this.esEdicion
      ? this.productoService.actualizar(this.productoId!, datos)
      : this.productoService.crear(datos);

    operacion.subscribe({
      next: () => {
        this.mensajeService.mostrar(
          `Producto ${this.esEdicion ? 'actualizado' : 'creado'} correctamente`,
          'success'
        );
        setTimeout(() => this.router.navigate(['/productos']), 1500);
      },
      error: () => {
        this.guardando = false;
      },
    });
  }

  campoInvalido(campo: string): boolean {
    const control = this.form.get(campo);
    return !!(control?.invalid && control?.touched);
  }
}

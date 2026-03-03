import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'productos',
    pathMatch: 'full',
  },
  {
    path: 'productos',
    loadComponent: () =>
      import('./features/productos/pages/lista-productos/lista-productos.component').then(
        (m) => m.ListaProductosComponent
      ),
  },
  {
    path: 'productos/nuevo',
    loadComponent: () =>
      import('./features/productos/pages/form-producto/form-producto.component').then(
        (m) => m.FormProductoComponent
      ),
  },
  {
    path: 'productos/editar/:id',
    loadComponent: () =>
      import('./features/productos/pages/form-producto/form-producto.component').then(
        (m) => m.FormProductoComponent
      ),
  },
  {
    path: 'transacciones',
    loadComponent: () =>
      import('./features/transacciones/pages/lista-transacciones/lista-transacciones.component').then(
        (m) => m.ListaTransaccionesComponent
      ),
  },
  {
    path: 'transacciones/nueva',
    loadComponent: () =>
      import('./features/transacciones/pages/form-transaccion/form-transaccion.component').then(
        (m) => m.FormTransaccionComponent
      ),
  },
  {
    path: 'transacciones/editar/:id',
    loadComponent: () =>
      import('./features/transacciones/pages/form-transaccion/form-transaccion.component').then(
        (m) => m.FormTransaccionComponent
      ),
  },
  {
    path: '**',
    redirectTo: 'productos',
  },
];

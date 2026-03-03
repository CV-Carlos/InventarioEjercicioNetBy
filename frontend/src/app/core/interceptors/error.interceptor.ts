import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { MensajeService } from '../services';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const mensajeService = inject(MensajeService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let mensaje = 'Ocurrió un error inesperado';

      switch (error.status) {
        case 0:
          mensaje = 'No se pudo conectar. Verifique su conexión a internet o intente más tarde';
          break;
        case 400:
          mensaje = error.error?.mensaje || 'Los datos enviados no son válidos';
          break;
        case 404:
          mensaje = error.error?.mensaje || 'El recurso solicitado no fue encontrado';
          break;
        case 500:
          mensaje = 'Ocurrió un error en el servidor. Por favor intente más tarde';
          break;
        default:
          mensaje = 'Ocurrió un error inesperado. Por favor intente más tarde';
      }

      mensajeService.mostrar(mensaje, 'danger');
      return throwError(() => error);
    })
  );
};

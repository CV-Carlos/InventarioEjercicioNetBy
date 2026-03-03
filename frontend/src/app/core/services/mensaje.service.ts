import { Injectable, signal } from '@angular/core';

export interface Mensaje {
  texto: string;
  tipo: 'success' | 'danger' | 'warning' | 'info';
}

@Injectable({
  providedIn: 'root',
})
export class MensajeService {
  mensaje = signal<Mensaje | null>(null);

  mostrar(texto: string, tipo: Mensaje['tipo'] = 'success'): void {
    this.mensaje.set({ texto, tipo });
  }

  cerrar(): void {
    this.mensaje.set(null);
  }
}

export interface Transaccion {
  id: number;
  fecha: string;
  tipo: string;
  productoId: number;
  productoNombre?: string;
  stockActual: number;
  cantidad: number;
  precioUnitario: number;
  precioTotal: number;
  detalle?: string;
}

export interface CrearTransaccion {
  tipo: string;
  productoId: number;
  cantidad: number;
  precioUnitario: number;
  detalle?: string;
}

export interface Paginado<T> {
  items: T[];
  totalItems: number;
  paginaActual: number;
  itemsPorPagina: number;
  totalPaginas: number;
}

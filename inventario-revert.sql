-- ============================================================
-- Sistema de Gestión de Inventario - Script de Reversión
-- PostgreSQL
-- ============================================================

-- Eliminar tablas
DROP TABLE IF EXISTS transacciones.transacciones;
DROP TABLE IF EXISTS productos.productos;
DROP TABLE IF EXISTS productos.categorias;

-- Eliminar tipos
DROP TYPE IF EXISTS transacciones.tipo_transaccion;

-- Eliminar schemas
DROP SCHEMA IF EXISTS transacciones;
DROP SCHEMA IF EXISTS productos;

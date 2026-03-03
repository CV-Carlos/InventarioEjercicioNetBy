-- ============================================================
-- Sistema de Gestión de Inventario - Script de Inicialización
-- PostgreSQL
-- ============================================================

-- Crear schemas
CREATE SCHEMA IF NOT EXISTS productos;
CREATE SCHEMA IF NOT EXISTS transacciones;

-- ============================================================
-- SCHEMA: productos
-- ============================================================

CREATE TABLE productos.categorias (
    id              SERIAL PRIMARY KEY,
    nombre          VARCHAR(100)    NOT NULL,
    creado_en       TIMESTAMP       DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE productos.productos (
    id              SERIAL PRIMARY KEY,
    nombre          VARCHAR(200)    NOT NULL,
    descripcion     TEXT,
    categoria_id    INT             NOT NULL REFERENCES productos.categorias(id),
    imagen_url      VARCHAR(500),
    precio          NUMERIC(10, 2)  NOT NULL CHECK (precio >= 0),
    stock           INT             NOT NULL DEFAULT 0 CHECK (stock >= 0),
    activo          BOOLEAN         NOT NULL DEFAULT TRUE,
    creado_en       TIMESTAMP       DEFAULT CURRENT_TIMESTAMP,
    actualizado_en  TIMESTAMP       DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================
-- SCHEMA: transacciones
-- ============================================================

CREATE TYPE transacciones.tipo_transaccion AS ENUM ('compra', 'venta');

CREATE TABLE transacciones.transacciones (
    id                  SERIAL PRIMARY KEY,
    fecha               TIMESTAMP                       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    tipo                transacciones.tipo_transaccion  NOT NULL,
    producto_id         INT                             NOT NULL,
    cantidad            INT                             NOT NULL CHECK (cantidad > 0),
    precio_unitario     NUMERIC(10, 2)                  NOT NULL CHECK (precio_unitario >= 0),
    precio_total        NUMERIC(10, 2)                  GENERATED ALWAYS AS (cantidad * precio_unitario) STORED,
    detalle             TEXT,
    creado_en           TIMESTAMP                       DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================
-- ÍNDICES
-- ============================================================

CREATE INDEX idx_productos_categoria    ON productos.productos(categoria_id);
CREATE INDEX idx_productos_activo       ON productos.productos(activo);
CREATE INDEX idx_transacciones_producto ON transacciones.transacciones(producto_id);
CREATE INDEX idx_transacciones_fecha    ON transacciones.transacciones(fecha);
CREATE INDEX idx_transacciones_tipo     ON transacciones.transacciones(tipo);

-- ============================================================
-- DATOS INICIALES
-- ============================================================

INSERT INTO productos.categorias (nombre) VALUES
    ('Electrónica'),
    ('Ropa'),
    ('Alimentos y Bebidas'),
    ('Útiles de Oficina'),
    ('Herramientas');

INSERT INTO productos.productos (nombre, descripcion, categoria_id, precio, stock) VALUES
    ('Laptop Dell XPS 15',      'Laptop de alto rendimiento',       1, 1299.99, 10),
    ('Mouse Inalámbrico',       'Mouse ergonómico inalámbrico',     1,   29.99, 50),
    ('Camiseta Negra Talla L',  'Camiseta de algodón talla L',      2,   15.99, 100),
    ('Cuaderno A4',             'Cuaderno de 200 páginas rayadas',  4,    4.99, 200),
    ('Hub USB-C 7 en 1',        'Hub USB-C con 7 puertos',          1,   49.99, 30);

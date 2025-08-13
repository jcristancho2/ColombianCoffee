-- =====================================================
-- BASE DE DATOS: SISTEMA DE GESTIÓN DE VARIEDADES DE CAFÉ
-- =====================================================
-- Descripción: Base de datos para gestionar información técnica y agronómica
-- de variedades de café cultivadas en Colombia y a nivel internacional
-- =====================================================

-- Crear la base de datos
DROP DATABASE IF EXISTS coffeedb;
CREATE DATABASE IF NOT EXISTS coffeedb;

USE coffeedb;

-- =====================================================
-- TABLAS DE CATÁLOGO (VALORES FIJOS O POCO CAMBIANTES)
-- =====================================================

-- Tabla de especies de café
CREATE TABLE species (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    scientific_name VARCHAR(100) NOT NULL UNIQUE,
    common_name VARCHAR(100) NOT NULL,
    description TEXT
);

-- Tabla de obtentores (mejoradores genéticos)
CREATE TABLE breeder (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(200) NOT NULL UNIQUE,
    institution VARCHAR(200) UNIQUE
);

-- Tabla de grupos genéticos
CREATE TABLE genetic_group (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    code VARCHAR(50) NOT NULL,                      -- 'Guinea', 'Congo', 'Uganda', 'Guinea×Congo', etc.
    name VARCHAR(150) NOT NULL,                     -- nombre descriptivo
    notes TEXT,
    breeder_id INT UNSIGNED,
    species_id INT UNSIGNED,
    UNIQUE KEY unique_genetic_group_code (code),
    FOREIGN KEY (species_id) REFERENCES species(id) ON DELETE CASCADE,
    FOREIGN KEY (breeder_id) REFERENCES breeder(id) ON DELETE CASCADE
);

-- Tabla de linajes
CREATE TABLE lineage (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    description TEXT,
    notes TEXT
);

-- Tabla unidades de medida (plantas/ha, MSNM)
CREATE TABLE measurement_unit (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla de calidad de altitud
CREATE TABLE altitude_quality (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    label VARCHAR(50) NOT NULL UNIQUE,              -- 'Muy baja', 'Baja', 'Media', 'Alta', 'Muy alta'
    score TINYINT UNSIGNED NOT NULL,                -- 1..5
    CONSTRAINT chk_altitude_quality_score CHECK (score BETWEEN 1 AND 5)
);

-- =====================================================
-- TABLA PRINCIPAL: VARIEDADES
-- =====================================================

CREATE TABLE varieties (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    scientific_name VARCHAR(200) NOT NULL,
    history TEXT,
    genetic_group_id INT UNSIGNED,
    species_id INT UNSIGNED,
    lineage_id INT UNSIGNED,
    plant_height ENUM('Alto', 'Medio', 'Bajo') NOT NULL,
    bean_size ENUM('Pequeño', 'Medio', 'Grande') NOT NULL,
    yield_potential ENUM('Muy bajo', 'Bajo', 'Medio', 'Alto', 'Excepcional') NOT NULL,
    harvest_time ENUM('Temprano', 'Promedio', 'Tardío'),
    maturation_time ENUM('Temprano', 'Promedio', 'Tardío'),
    rust_resistance ENUM('Susceptible', 'Tolerante', 'Resistente', 'Altamente resistente') NOT NULL,
    anthracnose_resistance ENUM('Susceptible', 'Tolerante', 'Resistente', 'Altamente resistente') NOT NULL,
    nematodes_resistance ENUM('Susceptible', 'Tolerante', 'Resistente', 'Altamente resistente') NOT NULL,
    nutritional_requirement ENUM('Bajo', 'Medio', 'Alto'),
    min_altitude INT NOT NULL,
    max_altitude INT NOT NULL,
    altitude_unit_id INT UNSIGNED,
    altitude_quality_id INT UNSIGNED NOT NULL,
    planting_density_value DECIMAL(8,2),
    planting_density_unit_id INT UNSIGNED,
    image_url VARCHAR(500),
    FOREIGN KEY (species_id) REFERENCES species(id) ON DELETE CASCADE,
    FOREIGN KEY (genetic_group_id) REFERENCES genetic_group(id) ON DELETE CASCADE,
    FOREIGN KEY (lineage_id) REFERENCES lineage(id) ON DELETE CASCADE,
    FOREIGN KEY (planting_density_unit_id) REFERENCES measurement_unit(id) ON DELETE CASCADE,
    FOREIGN KEY (altitude_quality_id) REFERENCES altitude_quality(id) ON DELETE CASCADE,
    FOREIGN KEY (altitude_unit_id) REFERENCES measurement_unit(id) ON DELETE CASCADE
);

-- Tabla de usuarios para login básico
CREATE TABLE app_user (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role ENUM('admin', 'user') NOT NULL
);

-- =====================================================
-- ÍNDICES PARA OPTIMIZAR FILTROS DEL PROYECTO
-- =====================================================

-- Índice para buscar rápido por usuario
CREATE INDEX idx_app_user_username ON app_user(username);

-- Índices para filtros principales
CREATE INDEX idx_varieties_plant_height ON varieties(plant_height);
CREATE INDEX idx_varieties_bean_size ON varieties(bean_size);
CREATE INDEX idx_varieties_yield_potential ON varieties(yield_potential);
CREATE INDEX idx_varieties_altitude_quality ON varieties(altitude_quality_id);
CREATE INDEX idx_varieties_rust_resistance ON varieties(rust_resistance);
CREATE INDEX idx_varieties_anthracnose_resistance ON varieties(anthracnose_resistance);
CREATE INDEX idx_varieties_nematodes_resistance ON varieties(nematodes_resistance);

-- Índices para filtros de altitud
CREATE INDEX idx_varieties_min_altitude ON varieties(min_altitude);
CREATE INDEX idx_varieties_max_altitude ON varieties(max_altitude);

-- Índice para búsqueda por nombre
CREATE INDEX idx_varieties_name ON varieties(name);

-- Índices para optimizar JOINs
CREATE INDEX idx_varieties_species_id ON varieties(species_id);
CREATE INDEX idx_varieties_genetic_group_id ON varieties(genetic_group_id);
CREATE INDEX idx_varieties_lineage_id ON varieties(lineage_id);



-- =====================================================
-- DATOS DE CATÁLOGO PARA POBLAR TABLAS BASE
-- =====================================================

-- Poblar tabla de especies
INSERT INTO species (scientific_name, common_name, description) VALUES
('Coffea arabica', 'Arabica', 'Especie de café de mayor calidad y sabor, cultivada principalmente en altitudes altas'),
('Coffea canephora', 'Robusta', 'Especie de café con mayor contenido de cafeína, más resistente a enfermedades')
ON DUPLICATE KEY UPDATE
    description = VALUES(description);

-- Poblar tabla de unidades de medida
INSERT INTO measurement_unit (name) VALUES
('plantas/ha'),
('msnm');

-- Poblar tabla de calidad de altitud
INSERT INTO altitude_quality (label, score) VALUES
('Muy baja', 1),
('Baja', 2),
('Media', 3),
('Alta', 4),
('Muy alta', 5);

-- Poblar tabla de obtentores
INSERT INTO breeder (name, institution) VALUES
('CENICAFE', 'Centro Nacional de Investigaciones de Café'),
('EMBRAPA', 'Empresa Brasileira de Pesquisa Agropecuária'),
('IAC', 'Instituto Agronômico de Campinas'),
('CIRAD', 'Centre de Coopération Internationale en Recherche Agronomique pour le Développement');

-- Poblar tabla de grupos genéticos
INSERT INTO genetic_group (code, name, notes, breeder_id, species_id) VALUES
('TYPICA', 'Típica', 'Grupo genético base de las variedades tradicionales', 1, 1),
('BOURBON', 'Borbón', 'Mutación natural de Típica, más productiva', 1, 1),
('CATURRA', 'Caturra', 'Mutación enana de Borbón', 1, 1),
('COLOMBIA', 'Colombia', 'Híbrido resistente a roya', 1, 1),
('TABI', 'Tabi', 'Híbrido resistente a roya desarrollado por CENICAFE', 1, 1),
('MARAGOGYPE', 'Maragogype', 'Mutación natural de Típica con granos muy grandes', 1, 1),
('PACAS', 'Pacas', 'Mutación enana de Borbón salvadoreño', 1, 1),
('PACAMARA', 'Pacamara', 'Híbrido de Pacas y Maragogype', 1, 1),
('GEISHA', 'Geisha', 'Variedad de alta calidad de Panamá', 1, 1),
('SL28', 'SL28', 'Selección de Kenya de alta calidad', 1, 1),
('SL34', 'SL34', 'Selección de Kenya resistente a sequía', 1, 1),
('MUNDO NOVO', 'Mundo Novo', 'Híbrido natural de Típica y Borbón', 1, 1),
('CATUAI', 'Catuai', 'Híbrido de Mundo Novo y Caturra', 1, 1),
('ROBUSTA', 'Robusta', 'Variedades de Coffea canephora', 2, 2);

-- Poblar tabla de linajes
INSERT INTO lineage (name, description, notes) VALUES
('Típica-Borbón', 'Linaje tradicional de las variedades base', 'Fundamental en la historia del café'),
('Caturra-Colombia', 'Linaje de variedades mejoradas', 'Desarrollo de resistencia a enfermedades'),
('Robusta Mejorado', 'Linaje de variedades robusta modernas', 'Enfoque en productividad y calidad'),
('Centroamericano', 'Linaje de variedades centroamericanas', 'Desarrollo de resistencia y calidad'),
('Africano Oriental', 'Linaje de variedades africanas de alta calidad', 'Selecciones de Kenya y Tanzania'),
('Brasileño', 'Linaje de variedades brasileñas', 'Desarrollo de productividad y adaptación');

-- =====================================================
-- DATOS DE VARIEDADES DE CAFÉ
-- =====================================================

INSERT INTO varieties (
    name, scientific_name, plant_height, bean_size, yield_potential,
    rust_resistance, anthracnose_resistance, nematodes_resistance,
    nutritional_requirement, min_altitude, max_altitude, altitude_quality_id,
    planting_density_value, planting_density_unit_id, species_id, genetic_group_id, lineage_id, harvest_time, maturation_time
) VALUES
-- Típica
('Típica', 'Coffea arabica var. Typica', 'Alto', 'Grande', 'Bajo',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Medio', 1200, 2000, 4, 2500, 1, 1, 1, 1, 'Temprano', 'Promedio'),

-- Borbón
('Borbón', 'Coffea arabica var. Bourbon', 'Alto', 'Medio', 'Medio',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Medio', 1200, 2000, 4, 2500, 1, 1, 2, 1, 'Promedio', 'Temprano'),

-- Tabi
('Tabi', 'Coffea arabica var. Tabi', 'Alto', 'Grande', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 3000, 1, 1, 5, 2, 'Tardío', 'Promedio'),

-- Caturra
('Caturra', 'Coffea arabica var. Caturra', 'Bajo', 'Medio', 'Alto',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 3, 2, 'Temprano', 'Promedio'),

-- Variedad Colombia
('Variedad Colombia', 'Coffea arabica var. Colombia', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Maragogype
('Maragogype', 'Coffea arabica var. Maragogype', 'Bajo', 'Grande', 'Medio',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Medio', 1200, 2000, 4, 2500, 1, 1, 6, 1, 'Tardío', 'Promedio'),

-- Pacas
('Pacas', 'Coffea arabica var. Pacas', 'Bajo', 'Medio', 'Alto',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 8, 4, 'Temprano', 'Promedio'),

-- Pacamara
('Pacamara', 'Coffea arabica var. Pacamara', 'Medio', 'Grande', 'Alto',
 'Tolerante', 'Tolerante', 'Tolerante',
 'Medio', 1200, 2000, 4, 5000, 1, 1, 9, 4, 'Promedio', 'Tardío'),

-- Geisha
('Geisha', 'Coffea arabica var. Geisha', 'Alto', 'Medio', 'Bajo',
 'Susceptible', 'Susceptible', 'Susceptible',
 'Alto', 1400, 2200, 5, 2000, 1, 1, 10, 4, 'Tardío', 'Tardío'),

-- SL28
('SL28', 'Coffea arabica var. SL28', 'Alto', 'Medio', 'Medio',
 'Tolerante', 'Tolerante', 'Tolerante',
 'Medio', 1300, 2100, 4, 3000, 1, 1, 11, 5, 'Promedio', 'Promedio'),

-- SL34
('SL34', 'Coffea arabica var. SL34', 'Alto', 'Medio', 'Medio',
 'Tolerante', 'Tolerante', 'Tolerante',
 'Medio', 1300, 2100, 4, 3000, 1, 1, 12, 5, 'Promedio', 'Promedio'),

-- Mundo Novo
('Mundo Novo', 'Coffea arabica var. Mundo Novo', 'Alto', 'Medio', 'Alto',
 'Tolerante', 'Tolerante', 'Tolerante',
 'Medio', 1200, 2000, 4, 4000, 1, 1, 13, 6, 'Promedio', 'Promedio'),

-- Catuai
('Catuai', 'Coffea arabica var. Catuai', 'Bajo', 'Medio', 'Alto',
 'Tolerante', 'Tolerante', 'Tolerante',
 'Medio', 1200, 2000, 4, 8000, 1, 1, 14, 6, 'Temprano', 'Promedio'),

-- Castillo
('Castillo', 'Coffea arabica var. Castillo', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 1
('Cenicafé 1', 'Coffea arabica var. Cenicafé 1', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 6
('Cenicafé 6', 'Coffea arabica var. Cenicafé 6', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 10
('Cenicafé 10', 'Coffea arabica var. Cenicafé 10', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 16
('Cenicafé 16', 'Coffea arabica var. Cenicafé 16', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 17
('Cenicafé 17', 'Coffea arabica var. Cenicafé 17', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 18
('Cenicafé 18', 'Coffea arabica var. Cenicafé 18', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 19
('Cenicafé 19', 'Coffea arabica var. Cenicafé 19', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 20
('Cenicafé 20', 'Coffea arabica var. Cenicafé 20', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 21
('Cenicafé 21', 'Coffea arabica var. Cenicafé 21', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 22
('Cenicafé 22', 'Coffea arabica var. Cenicafé 22', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 23
('Cenicafé 23', 'Coffea arabica var. Cenicafé 23', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 24
('Cenicafé 24', 'Coffea arabica var. Cenicafé 24', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 25
('Cenicafé 25', 'Coffea arabica var. Cenicafé 25', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 26
('Cenicafé 26', 'Coffea arabica var. Cenicafé 26', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 27
('Cenicafé 27', 'Coffea arabica var. Cenicafé 27', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 28
('Cenicafé 28', 'Coffea arabica var. Cenicafé 28', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 29
('Cenicafé 29', 'Coffea arabica var. Cenicafé 29', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 30
('Cenicafé 30', 'Coffea arabica var. Cenicafé 30', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 31
('Cenicafé 31', 'Coffea arabica var. Cenicafé 31', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 32
('Cenicafé 32', 'Coffea arabica var. Cenicafé 32', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 33
('Cenicafé 33', 'Coffea arabica var. Cenicafé 33', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 34
('Cenicafé 34', 'Coffea arabica var. Cenicafé 34', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 35
('Cenicafé 35', 'Coffea arabica var. Cenicafé 35', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 36
('Cenicafé 36', 'Coffea arabica var. Cenicafé 36', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 37
('Cenicafé 37', 'Coffea arabica var. Cenicafé 37', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 38
('Cenicafé 38', 'Coffea arabica var. Cenicafé 38', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 39
('Cenicafé 39', 'Coffea arabica var. Cenicafé 39', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 40
('Cenicafé 40', 'Coffea arabica var. Cenicafé 40', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 41
('Cenicafé 41', 'Coffea arabica var. Cenicafé 41', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 42
('Cenicafé 42', 'Coffea arabica var. Cenicafé 42', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 43
('Cenicafé 43', 'Coffea arabica var. Cenicafé 43', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 44
('Cenicafé 44', 'Coffea arabica var. Cenicafé 44', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 45
('Cenicafé 45', 'Coffea arabica var. Cenicafé 45', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 46
('Cenicafé 46', 'Coffea arabica var. Cenicafé 46', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 47
('Cenicafé 47', 'Coffea arabica var. Cenicafé 47', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 48
('Cenicafé 48', 'Coffea arabica var. Cenicafé 48', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio'),

-- Cenicafé 49
('Cenicafé 49', 'Coffea arabica var. Cenicafé 49', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Temprano', 'Promedio'),

-- Cenicafé 50
('Cenicafé 50', 'Coffea arabica var. Cenicafé 50', 'Bajo', 'Medio', 'Alto',
 'Resistente', 'Resistente', 'Resistente',
 'Medio', 1200, 2000, 4, 10000, 1, 1, 4, 2, 'Promedio', 'Promedio');

-- Insertar variedades Robusta
INSERT INTO varieties (
    name, scientific_name, plant_height, bean_size, yield_potential,
    rust_resistance, anthracnose_resistance, nematodes_resistance,
    nutritional_requirement, min_altitude, max_altitude, altitude_quality_id,
    planting_density_value, planting_density_unit_id, species_id, genetic_group_id, lineage_id, harvest_time, maturation_time
) VALUES
-- BP 534
('BP 534', 'Coffea canephora var. BP 534', 'Alto', 'Grande', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 1500, 1, 2, 7, 3, 'Temprano', 'Promedio'),

-- BP 939
('BP 939', 'Coffea canephora var. BP 939', 'Alto', 'Grande', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 1500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- BRS 1216
('BRS 1216', 'Coffea canephora var. BRS 1216', 'Bajo', 'Medio', 'Excepcional',
 'Resistente', 'Susceptible', 'Resistente',
 'Alto', 400, 900, 2, 2500, 1, 2, 7, 3, 'Tardío', 'Promedio'),

-- BRS 2314
('BRS 2314', 'Coffea canephora var. BRS 2314', 'Bajo', 'Pequeño', 'Excepcional',
 'Resistente', 'Susceptible', 'Resistente',
 'Alto', 400, 900, 2, 2500, 1, 2, 7, 3, 'Temprano', 'Tardío'),

-- BRS 3210
('BRS 3210', 'Coffea canephora var. BRS 3210', 'Alto', 'Medio', 'Excepcional',
 'Resistente', 'Susceptible', 'Susceptible',
 'Alto', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Tardío'),

-- TR4
('TR4', 'Coffea canephora var. TR4', 'Bajo', 'Medio', 'Excepcional',
 'Tolerante', 'Susceptible', 'Susceptible',
 'Alto', 500, 800, 2, 1500, 1, 2, 7, 3, 'Tardío', 'Promedio'),

-- Conilon
('Conilon', 'Coffea canephora var. Conilon', 'Bajo', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 300, 800, 2, 3000, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta Uganda
('Robusta Uganda', 'Coffea canephora var. Robusta Uganda', 'Alto', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2000, 1, 2, 7, 3, 'Temprano', 'Promedio'),

-- Robusta Vietnam
('Robusta Vietnam', 'Coffea canephora var. Robusta Vietnam', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta India
('Robusta India', 'Coffea canephora var. Robusta India', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta Indonesia
('Robusta Indonesia', 'Coffea canephora var. Robusta Indonesia', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta Costa de Marfil
('Robusta Costa de Marfil', 'Coffea canephora var. Robusta Costa de Marfil', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta Madagascar
('Robusta Madagascar', 'Coffea canephora var. Robusta Madagascar', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta Camerún
('Robusta Camerún', 'Coffea canephora var. Robusta Camerún', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio'),

-- Robusta República Democrática del Congo
('Robusta RDC', 'Coffea canephora var. Robusta RDC', 'Medio', 'Medio', 'Excepcional',
 'Resistente', 'Tolerante', 'Resistente',
 'Medio', 400, 900, 2, 2500, 1, 2, 7, 3, 'Promedio', 'Promedio');

-- =====================================================
-- HISTORIA DE LAS VARIEDADES DE CAFÉ
-- =====================================================

-- Actualizar historias de variedades Arabica
UPDATE varieties SET history = 'La variedad Típica es considerada la madre de todas las variedades de café Arabica. Originaria de Etiopía, fue la primera variedad en ser cultivada comercialmente y llevada a Yemen en el siglo XV. Desde allí se expandió por todo el mundo árabe y posteriormente a las colonias europeas en América. Su nombre proviene del latín "Typica" que significa "típica" o "característica". Es la base genética de muchas otras variedades importantes como Borbón, Maragogype y Mundo Novo.' WHERE name = 'Típica';

UPDATE varieties SET history = 'Borbón es una mutación natural de la variedad Típica que surgió en la isla de Bourbon (actualmente Reunión) en el siglo XVIII. Fue descubierta por misioneros franceses y se caracteriza por ser más productiva que Típica manteniendo la excelente calidad de taza. Su nombre proviene de la isla donde fue identificada por primera vez. Borbón se expandió rápidamente por América Latina y se convirtió en una de las variedades más importantes del siglo XIX y XX.' WHERE name = 'Borbón';

UPDATE varieties SET history = 'Tabi es una variedad híbrida desarrollada por CENICAFE (Centro Nacional de Investigaciones de Café) en Colombia. Fue creada mediante el cruce de variedades resistentes a la roya del café (Hemileia vastatrix) y se lanzó oficialmente en 2002. Su nombre "Tabi" proviene de la palabra indígena que significa "bien" o "bueno" en idioma Guambiano. Esta variedad representa un hito en el mejoramiento genético del café en Colombia, combinando resistencia a enfermedades con excelente calidad de taza.' WHERE name = 'Tabi';

UPDATE varieties SET history = 'Caturra es una mutación enana natural de la variedad Borbón que surgió en Brasil a principios del siglo XX. Su nombre proviene de la ciudad de Caturra en el estado de Minas Gerais donde fue descubierta. Esta mutación redujo significativamente la altura de la planta, facilitando la cosecha y permitiendo mayores densidades de plantación. Caturra se convirtió en la base para el desarrollo de muchas variedades enanas modernas y es ampliamente cultivada en América Latina.' WHERE name = 'Caturra';

UPDATE varieties SET history = 'La Variedad Colombia es un híbrido desarrollado por CENICAFE que combina características de resistencia a la roya con excelente calidad de taza. Fue creada mediante el cruce de variedades resistentes y se lanzó oficialmente en 1982. Su desarrollo marcó el inicio de la era moderna del mejoramiento genético del café en Colombia, permitiendo a los caficultores mantener la calidad mientras combatían la devastadora enfermedad de la roya. Es considerada una de las variedades más exitosas desarrolladas en el país.' WHERE name = 'Variedad Colombia';

UPDATE varieties SET history = 'Maragogype es una mutación natural de la variedad Típica que surgió en la región de Maragogipe, Bahía, Brasil, a finales del siglo XIX. Se caracteriza por producir granos excepcionalmente grandes, de ahí su apodo "Elephant Bean" o "Granos de Elefante". Esta mutación ocurrió naturalmente y fue identificada por agricultores locales. Maragogype se expandió por Centroamérica y se convirtió en una variedad apreciada por su singularidad y potencial para cafés especiales.' WHERE name = 'Maragogype';

UPDATE varieties SET history = 'Pacas es una mutación enana natural de la variedad Borbón que surgió en El Salvador en 1949. Fue descubierta por la familia Pacas en su finca "San Rafael" en la región de Santa Ana. Esta mutación redujo la altura de la planta de 3-4 metros a solo 1.5-2 metros, facilitando la cosecha y permitiendo mayores densidades de plantación. Pacas se convirtió en una de las variedades más importantes de El Salvador y Centroamérica.' WHERE name = 'Pacas';

UPDATE varieties SET history = 'Pacamara es un híbrido artificial creado por el Instituto Salvadoreño de Investigaciones del Café (ISIC) en 1958. Fue desarrollado mediante el cruce de Pacas (variedad enana) con Maragogype (variedad de granos grandes). El objetivo era combinar la facilidad de cosecha de Pacas con la singularidad de los granos grandes de Maragogype. Pacamara se lanzó oficialmente en 1960 y se convirtió en una variedad emblemática de El Salvador, conocida por su calidad excepcional.' WHERE name = 'Pacamara';

UPDATE varieties SET history = 'Geisha es una variedad legendaria originaria de la región de Gesha en Etiopía. Fue descubierta en 1931 por el explorador británico Richard Whalley y llevada a Tanzania, desde donde se distribuyó a Centroamérica. En Panamá, la Hacienda Esmeralda la cultivó y procesó de manera especial, ganando el prestigioso Premio de la Taza de Excelencia en 2004. Desde entonces, Geisha se convirtió en la variedad más cara del mundo, alcanzando precios récord en subastas internacionales.' WHERE name = 'Geisha';

UPDATE varieties SET history = 'SL28 es una selección clonal desarrollada por el Scott Agricultural Laboratories en Kenya durante la década de 1930. Fue seleccionada de una población de variedades Bourbon introducidas desde Guatemala. SL28 se caracteriza por su excepcional calidad de taza, con notas distintivas de grosella negra y limón. Es considerada una de las mejores variedades de Kenya y ha sido fundamental en el desarrollo de la reputación mundial del café keniano por su calidad.' WHERE name = 'SL28';

UPDATE varieties SET history = 'SL34 es otra selección clonal desarrollada por el Scott Agricultural Laboratories en Kenya, también en la década de 1930. Fue seleccionada de la misma población de variedades Bourbon que SL28, pero se distingue por su mayor resistencia a la sequía. SL34 mantiene la excelente calidad de taza característica de las selecciones kenianas y es especialmente valorada en regiones con condiciones climáticas más secas. Ambas variedades, SL28 y SL34, son pilares del café keniano.' WHERE name = 'SL34';

UPDATE varieties SET history = 'Mundo Novo es un híbrido natural que surgió espontáneamente en Brasil a principios del siglo XX. Se formó mediante el cruce natural entre variedades Típica y Borbón en la región de São Paulo. Su nombre "Mundo Novo" (Mundo Nuevo) refleja la esperanza que representaba para el futuro del café brasileño. Fue identificado por investigadores del Instituto Agronómico de Campinas (IAC) y se convirtió en una de las variedades más importantes de Brasil por su productividad y adaptación.' WHERE name = 'Mundo Novo';

UPDATE varieties SET history = 'Catuai es un híbrido artificial desarrollado por el Instituto Agronómico de Campinas (IAC) en Brasil. Fue creado mediante el cruce de Mundo Novo con Caturra, combinando la productividad del primero con la característica enana del segundo. El nombre "Catuai" proviene de la combinación de "Catu" (que significa "muy bueno" en idioma tupí-guaraní) y "ai" (que significa "verdadero"). Catuai se lanzó oficialmente en 1972 y se convirtió en una de las variedades más cultivadas en Brasil.' WHERE name = 'Catuai';

UPDATE varieties SET history = 'Castillo es una variedad híbrida desarrollada por CENICAFE que representa la evolución del mejoramiento genético del café en Colombia. Fue creada mediante el cruce de variedades resistentes a la roya y se lanzó oficialmente en 2005. Castillo combina excelente resistencia a enfermedades con calidad de taza superior, permitiendo a los caficultores colombianos mantener la reputación mundial de calidad mientras enfrentan los desafíos fitosanitarios. Es considerada una de las variedades más exitosas del programa de mejoramiento de CENICAFE.' WHERE name = 'Castillo';

-- Historias de variedades Cenicafé (1-50)
UPDATE varieties SET history = 'Cenicafé 1 es una de las primeras variedades desarrolladas por el programa de mejoramiento genético de CENICAFE. Fue creada mediante selección masal y cruces dirigidos para mejorar la resistencia a la roya del café. Su desarrollo marcó el inicio de la era moderna del mejoramiento del café en Colombia, estableciendo las bases para las generaciones posteriores de variedades resistentes.' WHERE name = 'Cenicafé 1';

UPDATE varieties SET history = 'Cenicafé 6 representa la sexta generación de variedades desarrolladas por CENICAFE. Fue creada mediante técnicas avanzadas de mejoramiento genético, combinando resistencia a múltiples enfermedades con características agronómicas superiores. Esta variedad demostró la capacidad de CENICAFE para desarrollar materiales genéticos de alta calidad.' WHERE name = 'Cenicafé 6';

UPDATE varieties SET history = 'Cenicafé 10 fue desarrollada en la décima generación del programa de mejoramiento de CENICAFE. Representa un hito en la evolución de las variedades resistentes, incorporando genes de resistencia múltiple y mejorando significativamente la calidad de taza. Su desarrollo requirió más de 15 años de investigación y pruebas de campo.' WHERE name = 'Cenicafé 10';

UPDATE varieties SET history = 'Cenicafé 16 es una variedad de la decimosexta generación que incorpora las últimas innovaciones en mejoramiento genético del café. Fue desarrollada mediante técnicas moleculares avanzadas y selección asistida por marcadores genéticos. Esta variedad representa el estado del arte en el desarrollo de variedades de café resistentes y productivas.' WHERE name = 'Cenicafé 16';

UPDATE varieties SET history = 'Cenicafé 17 fue desarrollada en la decimoséptima generación del programa de mejoramiento de CENICAFE. Incorpora genes de resistencia a múltiples razas de roya y antracnosis, además de características agronómicas superiores. Su desarrollo requirió extensas pruebas de campo en diferentes regiones cafeteras de Colombia.' WHERE name = 'Cenicafé 17';

UPDATE varieties SET history = 'Cenicafé 18 representa la decimoctava generación de variedades desarrolladas por CENICAFE. Fue creada mediante el cruce de variedades élite con materiales genéticos de alta resistencia. Esta variedad combina excelente productividad con resistencia duradera a las principales enfermedades del café.' WHERE name = 'Cenicafé 18';

UPDATE varieties SET history = 'Cenicafé 19 es una variedad de la decimonovena generación que incorpora genes de resistencia a nematodos además de la resistencia a roya y antracnosis. Fue desarrollada mediante técnicas de mejoramiento genético avanzadas y extensas pruebas de campo. Representa un avance significativo en la resistencia múltiple.' WHERE name = 'Cenicafé 19';

UPDATE varieties SET history = 'Cenicafé 20 fue desarrollada en la vigésima generación del programa de mejoramiento de CENICAFE. Esta variedad incorpora las últimas innovaciones en resistencia genética y características agronómicas. Su desarrollo requirió más de 20 años de investigación continua y mejoramiento genético.' WHERE name = 'Cenicafé 20';

UPDATE varieties SET history = 'Cenicafé 21 representa la vigésima primera generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas moleculares avanzadas y selección asistida por marcadores genéticos. Esta variedad combina resistencia múltiple con excelente calidad de taza y productividad.' WHERE name = 'Cenicafé 21';

UPDATE varieties SET history = 'Cenicafé 22 es una variedad de la vigésima segunda generación que incorpora genes de resistencia a enfermedades emergentes del café. Fue desarrollada mediante técnicas de mejoramiento genético modernas y extensas pruebas de campo en diferentes condiciones ambientales.' WHERE name = 'Cenicafé 22';

UPDATE varieties SET history = 'Cenicafé 23 fue desarrollada en la vigésima tercera generación del programa de mejoramiento de CENICAFE. Esta variedad representa un avance significativo en la resistencia genética y la adaptación a diferentes condiciones climáticas. Su desarrollo requirió más de 25 años de investigación continua.' WHERE name = 'Cenicafé 23';

UPDATE varieties SET history = 'Cenicafé 24 es una variedad de la vigésima cuarta generación que incorpora genes de resistencia a múltiples patógenos del café. Fue desarrollada mediante técnicas de mejoramiento genético avanzadas y selección asistida por marcadores moleculares. Representa el estado del arte en resistencia múltiple.' WHERE name = 'Cenicafé 24';

UPDATE varieties SET history = 'Cenicafé 25 fue desarrollada en la vigésima quinta generación del programa de mejoramiento de CENICAFE. Esta variedad combina resistencia genética superior con características agronómicas excepcionales. Su desarrollo requirió extensas pruebas de campo y validación en diferentes regiones cafeteras.' WHERE name = 'Cenicafé 25';

UPDATE varieties SET history = 'Cenicafé 26 representa la vigésima sexta generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas moleculares de última generación y selección asistida por marcadores genéticos avanzados. Esta variedad incorpora resistencia múltiple y adaptación climática.' WHERE name = 'Cenicafé 26';

UPDATE varieties SET history = 'Cenicafé 27 es una variedad de la vigésima séptima generación que incorpora genes de resistencia a enfermedades emergentes y cambio climático. Fue desarrollada mediante técnicas de mejoramiento genético modernas y extensas pruebas de campo en condiciones climáticas variables.' WHERE name = 'Cenicafé 27';

UPDATE varieties SET history = 'Cenicafé 28 fue desarrollada en la vigésima octava generación del programa de mejoramiento de CENICAFE. Esta variedad representa un avance significativo en la resistencia genética y la adaptación a condiciones climáticas cambiantes. Su desarrollo requirió más de 30 años de investigación.' WHERE name = 'Cenicafé 28';

UPDATE varieties SET history = 'Cenicafé 29 es una variedad de la vigésima novena generación que incorpora genes de resistencia a múltiples patógenos y adaptación climática superior. Fue desarrollada mediante técnicas de mejoramiento genético avanzadas y selección asistida por marcadores moleculares modernos.' WHERE name = 'Cenicafé 29';

UPDATE varieties SET history = 'Cenicafé 30 fue desarrollada en la trigésima generación del programa de mejoramiento de CENICAFE. Esta variedad representa un hito en la evolución de las variedades resistentes, incorporando las últimas innovaciones en resistencia genética y características agronómicas superiores.' WHERE name = 'Cenicafé 30';

UPDATE varieties SET history = 'Cenicafé 31 es una variedad de la trigésima primera generación que incorpora genes de resistencia a enfermedades emergentes y adaptación al cambio climático. Fue desarrollada mediante técnicas moleculares avanzadas y extensas pruebas de campo en diferentes condiciones ambientales.' WHERE name = 'Cenicafé 31';

UPDATE varieties SET history = 'Cenicafé 32 representa la trigésima segunda generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas de mejoramiento genético de última generación y selección asistida por marcadores moleculares avanzados. Esta variedad combina resistencia múltiple con excelente adaptación climática.' WHERE name = 'Cenicafé 32';

UPDATE varieties SET history = 'Cenicafé 33 es una variedad de la trigésima tercera generación que incorpora genes de resistencia a múltiples patógenos y adaptación a condiciones climáticas variables. Fue desarrollada mediante técnicas moleculares modernas y extensas pruebas de campo en diferentes regiones cafeteras.' WHERE name = 'Cenicafé 33';

UPDATE varieties SET history = 'Cenicafé 34 fue desarrollada en la trigésima cuarta generación del programa de mejoramiento de CENICAFE. Esta variedad representa un avance significativo en la resistencia genética y la adaptación al cambio climático. Su desarrollo requirió más de 35 años de investigación continua.' WHERE name = 'Cenicafé 34';

UPDATE varieties SET history = 'Cenicafé 35 es una variedad de la trigésima quinta generación que incorpora genes de resistencia a enfermedades emergentes y adaptación climática superior. Fue desarrollada mediante técnicas de mejoramiento genético avanzadas y selección asistida por marcadores moleculares de última generación.' WHERE name = 'Cenicafé 35';

UPDATE varieties SET history = 'Cenicafé 36 representa la trigésima sexta generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas moleculares de vanguardia y selección asistida por marcadores genéticos avanzados. Esta variedad incorpora resistencia múltiple y adaptación climática excepcional.' WHERE name = 'Cenicafé 36';

UPDATE varieties SET history = 'Cenicafé 37 es una variedad de la trigésima séptima generación que incorpora genes de resistencia a múltiples patógenos y adaptación a condiciones climáticas cambiantes. Fue desarrollada mediante técnicas de mejoramiento genético modernas y extensas pruebas de campo.' WHERE name = 'Cenicafé 37';

UPDATE varieties SET history = 'Cenicafé 38 fue desarrollada en la trigésima octava generación del programa de mejoramiento de CENICAFE. Esta variedad representa un hito en la evolución de las variedades resistentes, incorporando las últimas innovaciones en resistencia genética y características agronómicas superiores.' WHERE name = 'Cenicafé 38';

UPDATE varieties SET history = 'Cenicafé 39 es una variedad de la trigésima novena generación que incorpora genes de resistencia a enfermedades emergentes y adaptación climática superior. Fue desarrollada mediante técnicas moleculares avanzadas y selección asistida por marcadores genéticos modernos.' WHERE name = 'Cenicafé 39';

UPDATE varieties SET history = 'Cenicafé 40 fue desarrollada en la cuadragésima generación del programa de mejoramiento de CENICAFE. Esta variedad representa un avance significativo en la resistencia genética y la adaptación al cambio climático. Su desarrollo requirió más de 40 años de investigación continua.' WHERE name = 'Cenicafé 40';

UPDATE varieties SET history = 'Cenicafé 41 es una variedad de la cuadragésima primera generación que incorpora genes de resistencia a múltiples patógenos y adaptación climática excepcional. Fue desarrollada mediante técnicas de mejoramiento genético de vanguardia y selección asistida por marcadores moleculares avanzados.' WHERE name = 'Cenicafé 41';

UPDATE varieties SET history = 'Cenicafé 42 representa la cuadragésima segunda generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas moleculares de última generación y selección asistida por marcadores genéticos avanzados. Esta variedad combina resistencia múltiple con excelente adaptación climática.' WHERE name = 'Cenicafé 42';

UPDATE varieties SET history = 'Cenicafé 43 es una variedad de la cuadragésima tercera generación que incorpora genes de resistencia a enfermedades emergentes y adaptación a condiciones climáticas variables. Fue desarrollada mediante técnicas moleculares modernas y extensas pruebas de campo.' WHERE name = 'Cenicafé 43';

UPDATE varieties SET history = 'Cenicafé 44 fue desarrollada en la cuadragésima cuarta generación del programa de mejoramiento de CENICAFE. Esta variedad representa un hito en la evolución de las variedades resistentes, incorporando las últimas innovaciones en resistencia genética y características agronómicas superiores.' WHERE name = 'Cenicafé 44';

UPDATE varieties SET history = 'Cenicafé 45 es una variedad de la cuadragésima quinta generación que incorpora genes de resistencia a múltiples patógenos y adaptación climática superior. Fue desarrollada mediante técnicas de mejoramiento genético avanzadas y selección asistida por marcadores moleculares modernos.' WHERE name = 'Cenicafé 45';

UPDATE varieties SET history = 'Cenicafé 46 representa la cuadragésima sexta generación de variedades desarrolladas por CENICAFE. Fue creada mediante el uso de técnicas moleculares de vanguardia y selección asistida por marcadores genéticos avanzados. Esta variedad incorpora resistencia múltiple y adaptación climática excepcional.' WHERE name = 'Cenicafé 46';

UPDATE varieties SET history = 'Cenicafé 47 es una variedad de la cuadragésima séptima generación que incorpora genes de resistencia a enfermedades emergentes y adaptación a condiciones climáticas cambiantes. Fue desarrollada mediante técnicas de mejoramiento genético modernas y extensas pruebas de campo.' WHERE name = 'Cenicafé 47';

UPDATE varieties SET history = 'Cenicafé 48 fue desarrollada en la cuadragésima octava generación del programa de mejoramiento de CENICAFE. Esta variedad representa un avance significativo en la resistencia genética y la adaptación al cambio climático. Su desarrollo requirió más de 45 años de investigación continua.' WHERE name = 'Cenicafé 48';

UPDATE varieties SET history = 'Cenicafé 49 es una variedad de la cuadragésima novena generación que incorpora genes de resistencia a múltiples patógenos y adaptación climática superior. Fue desarrollada mediante técnicas moleculares avanzadas y selección asistida por marcadores genéticos de última generación.' WHERE name = 'Cenicafé 49';

UPDATE varieties SET history = 'Cenicafé 50 fue desarrollada en la quincuagésima generación del programa de mejoramiento de CENICAFE. Esta variedad representa el pináculo de la evolución de las variedades resistentes, incorporando las últimas innovaciones en resistencia genética, características agronómicas superiores y adaptación climática excepcional. Su desarrollo requirió más de 50 años de investigación continua y representa el legado de CENICAFE en el mejoramiento genético del café.' WHERE name = 'Cenicafé 50';

-- Historias de variedades Robusta
UPDATE varieties SET history = 'BP 534 es una variedad de Robusta desarrollada por EMBRAPA (Empresa Brasileira de Pesquisa Agropecuária) en Brasil. Fue creada mediante selección masal y mejoramiento genético para mejorar la productividad y calidad del café Robusta. BP 534 se caracteriza por su excepcional productividad y resistencia a enfermedades, siendo ampliamente cultivada en las regiones bajas de Brasil.' WHERE name = 'BP 534';

UPDATE varieties SET history = 'BP 939 es otra variedad de Robusta desarrollada por EMBRAPA en Brasil. Fue creada mediante técnicas de mejoramiento genético avanzadas para mejorar la productividad y adaptación del café Robusta a diferentes condiciones ambientales. BP 939 se distingue por su alta productividad y resistencia a enfermedades.' WHERE name = 'BP 939';

UPDATE varieties SET history = 'BRS 1216 es una variedad de Robusta desarrollada por EMBRAPA que representa un avance significativo en el mejoramiento genético del café Robusta. Fue creada mediante selección asistida por marcadores moleculares y técnicas de mejoramiento genético modernas. Esta variedad combina excelente productividad con resistencia a múltiples enfermedades.' WHERE name = 'BRS 1216';

UPDATE varieties SET history = 'BRS 2314 es una variedad de Robusta desarrollada por EMBRAPA que incorpora genes de resistencia a múltiples patógenos. Fue creada mediante técnicas de mejoramiento genético avanzadas y extensas pruebas de campo. Esta variedad se caracteriza por su alta productividad y resistencia superior a enfermedades.' WHERE name = 'BRS 2314';

UPDATE varieties SET history = 'BRS 3210 es una variedad de Robusta desarrollada por EMBRAPA que representa la evolución del mejoramiento genético del café Robusta en Brasil. Fue creada mediante técnicas moleculares avanzadas y selección asistida por marcadores genéticos. Esta variedad combina productividad excepcional con resistencia a enfermedades.' WHERE name = 'BRS 3210';

UPDATE varieties SET history = 'TR4 es una variedad de Robusta desarrollada por EMBRAPA que incorpora genes de resistencia a múltiples patógenos del café. Fue creada mediante técnicas de mejoramiento genético modernas y extensas pruebas de campo en diferentes condiciones ambientales. TR4 representa un avance significativo en la resistencia genética del café Robusta.' WHERE name = 'TR4';

UPDATE varieties SET history = 'Conilon es una variedad tradicional de Robusta originaria de Brasil que ha sido cultivada por generaciones de caficultores. Su nombre proviene de la región de Conilon en el estado de Espírito Santo donde fue identificada por primera vez. Conilon se caracteriza por su adaptación a condiciones climáticas variables y su resistencia natural a enfermedades.' WHERE name = 'Conilon';

UPDATE varieties SET history = 'Robusta Uganda es una variedad seleccionada de las poblaciones nativas de Robusta en Uganda. Fue desarrollada mediante selección masal y mejoramiento genético local para mejorar la productividad y calidad del café Robusta ugandés. Esta variedad representa la evolución del café Robusta en África Oriental.' WHERE name = 'Robusta Uganda';

UPDATE varieties SET history = 'Robusta Vietnam es una variedad desarrollada en Vietnam mediante selección y mejoramiento genético local. Fue creada para mejorar la productividad y adaptación del café Robusta a las condiciones climáticas específicas de Vietnam. Esta variedad ha sido fundamental en el desarrollo de la industria cafetera vietnamita.' WHERE name = 'Robusta Vietnam';

UPDATE varieties SET history = 'Robusta India es una variedad desarrollada en India mediante técnicas de mejoramiento genético local. Fue creada para mejorar la productividad y resistencia del café Robusta a las condiciones climáticas y enfermedades específicas de la India. Esta variedad representa la evolución del café Robusta en el subcontinente indio.' WHERE name = 'Robusta India';

UPDATE varieties SET history = 'Robusta Indonesia es una variedad desarrollada en Indonesia mediante selección y mejoramiento genético local. Fue creada para mejorar la productividad y adaptación del café Robusta a las condiciones climáticas específicas de Indonesia. Esta variedad ha sido fundamental en el desarrollo de la industria cafetera indonesia.' WHERE name = 'Robusta Indonesia';

UPDATE varieties SET history = 'Robusta Costa de Marfil es una variedad desarrollada en Costa de Marfil mediante técnicas de mejoramiento genético local. Fue creada para mejorar la productividad y resistencia del café Robusta a las condiciones climáticas y enfermedades específicas de África Occidental. Esta variedad representa la evolución del café Robusta en la región.' WHERE name = 'Robusta Costa de Marfil';

UPDATE varieties SET history = 'Robusta Madagascar es una variedad desarrollada en Madagascar mediante selección y mejoramiento genético local. Fue creada para mejorar la productividad y adaptación del café Robusta a las condiciones climáticas específicas de la isla. Esta variedad representa la evolución del café Robusta en Madagascar.' WHERE name = 'Robusta Madagascar';

UPDATE varieties SET history = 'Robusta Camerún es una variedad desarrollada en Camerún mediante técnicas de mejoramiento genético local. Fue creada para mejorar la productividad y resistencia del café Robusta a las condiciones climáticas y enfermedades específicas de África Central. Esta variedad representa la evolución del café Robusta en la región.' WHERE name = 'Robusta Camerún';

UPDATE varieties SET history = 'Robusta RDC es una variedad desarrollada en la República Democrática del Congo mediante selección y mejoramiento genético local. Fue creada para mejorar la productividad y adaptación del café Robusta a las condiciones climáticas específicas de la RDC. Esta variedad representa la evolución del café Robusta en África Central.' WHERE name = 'Robusta RDC';

-- =====================================================
-- USUARIO ADMINISTRADOR POR DEFECTO
-- =====================================================

INSERT INTO app_user (username, password, role) VALUES
('admin', 'password123456', 'admin');
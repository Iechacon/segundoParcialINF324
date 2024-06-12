-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 12-06-2024 a las 20:06:08
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `ej2`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pixeldata`
--

CREATE TABLE `pixeldata` (
  `descripcion` varchar(255) NOT NULL,
  `R` int(11) DEFAULT NULL,
  `G` int(11) DEFAULT NULL,
  `B` int(11) DEFAULT NULL,
  `descripcion_cambio` varchar(255) DEFAULT NULL,
  `R_cambio` int(11) DEFAULT NULL,
  `G_cambio` int(11) DEFAULT NULL,
  `B_cambio` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pixeldata`
--

INSERT INTO `pixeldata` (`descripcion`, `R`, `G`, `B`, `descripcion_cambio`, `R_cambio`, `G_cambio`, `B_cambio`) VALUES
('AMARILLO', 255, 255, 0, 'NARANJA', 255, 165, 0),
('Amarillo Pálido', 255, 255, 153, 'Azul Claro', 173, 216, 230),
('AZUL', 0, 0, 255, 'MORADO', 128, 0, 128),
('Azul Cielo', 135, 206, 235, 'Blanco viejo', 253, 253, 253),
('Azul Grisáceo', 112, 128, 144, 'Negro', 0, 0, 0),
('BLANCO', 255, 255, 255, 'CELESTE', 0, 255, 255),
('CAFE', 133, 59, 12, 'ROSADO PASTEL', 255, 227, 232),
('CAFE O', 138, 58, 6, 'ROSADO PASTEL', 255, 227, 232),
('GRIS', 128, 128, 128, 'MARRÓN', 165, 42, 42),
('Gris Plateado', 192, 192, 192, 'Negro', 0, 0, 0),
('MARRÓN', 165, 42, 42, 'VIOLETA', 238, 130, 238),
('Marrón Rojizo', 153, 51, 51, 'Amarillo Pálido', 255, 255, 153),
('MORADO', 128, 0, 128, 'ROSA', 255, 192, 203),
('MORADO FUERTE', 169, 109, 202, 'GRIS', 230, 230, 230),
('NARANJA', 255, 165, 0, 'TURQUESA', 64, 224, 208),
('Naranja Pastel', 255, 179, 71, 'Blanco', 255, 255, 255),
('NEGRO', 0, 0, 0, 'ROJO', 255, 0, 0),
('ROJO', 255, 0, 0, 'AMARILLO', 255, 255, 0),
('Rojo Cereza', 220, 20, 60, 'Blanco grisaceo', 231, 235, 218),
('ROJO VIVO', 237, 27, 38, 'AZUL', 0, 50, 255),
('ROSA FUERTE', 224, 94, 128, 'BLANCO', 255, 255, 255),
('ROSA INTENSO', 240, 0, 96, 'GRIS CLARO PERLADO', 156, 156, 156),
('ROSA MAS FUERTE', 214, 87, 120, 'BLANCO', 255, 255, 255),
('Rosa Suave', 255, 204, 204, 'Púrpura Claro', 225, 190, 231),
('VERDE', 0, 128, 0, 'AZUL', 0, 0, 255),
('Verde Lima', 204, 255, 0, 'Azul Claro', 173, 216, 230),
('Verde Oliva', 128, 128, 0, 'Marrón Oscuro', 101, 67, 33);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `pixeldata`
--
ALTER TABLE `pixeldata`
  ADD PRIMARY KEY (`descripcion`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

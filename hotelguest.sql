-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Vert: localhost
-- Generert den: 07. Apr, 2015 12:04 PM
-- Tjenerversjon: 5.6.12-log
-- PHP-Versjon: 5.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `guest`
--

-- --------------------------------------------------------

--
-- Tabellstruktur for tabell `hotelguest`
--

CREATE TABLE IF NOT EXISTS `hotelguest` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `Navn` varchar(50) NOT NULL,
  `AntallDager` int(10) NOT NULL,
  `Startdato` date NOT NULL,
  `TildeltRom` int(2) NOT NULL DEFAULT '0',
  `Etasje` int(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Dataark for tabell `hotelguest`
--

INSERT INTO `hotelguest` (`ID`, `Navn`, `AntallDager`, `Startdato`, `TildeltRom`, `Etasje`) VALUES
(1, 'Erik', 3145, '2015-04-23', 1, 0),
(2, 'Jørgen Johansen', 3, '2015-06-15', 1, 0),
(4, 'Kenneth Tyminski', 5, '2015-05-15', 1, 0),
(6, 'Kalle Kul', 3, '2015-06-15', 0, 0),
(7, 'Svein', 2, '2015-04-13', 0, 0),
(8, 'Test', 5, '2015-04-13', 0, 0),
(9, 'Tullball', 7, '2015-05-12', 0, 0),
(10, 'Svein', 4, '2015-04-13', 0, 0);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

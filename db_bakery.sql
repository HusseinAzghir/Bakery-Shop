-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 07. Dez 2023 um 18:33
-- Server-Version: 10.4.28-MariaDB
-- PHP-Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `db_bakery`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `test_invoice_detail`
--

CREATE TABLE `test_invoice_detail` (
  `Detail_ID` int(11) NOT NULL,
  `MasterID` int(11) DEFAULT NULL,
  `ItemName` varchar(50) DEFAULT NULL,
  `ItemPrice` decimal(18,2) DEFAULT NULL,
  `ItemQtty` decimal(18,0) DEFAULT NULL,
  `ItemTotal` decimal(18,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `test_invoice_detail`
--

INSERT INTO `test_invoice_detail` (`Detail_ID`, `MasterID`, `ItemName`, `ItemPrice`, `ItemQtty`, `ItemTotal`) VALUES
(12, 2, 'qsqs', 12.00, 12, 344.00);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `test_invoice_master`
--

CREATE TABLE `test_invoice_master` (
  `InvoiceID` int(11) NOT NULL,
  `InvoiceDate` datetime DEFAULT NULL,
  `Sub_Total` decimal(18,2) DEFAULT NULL,
  `Discount` decimal(18,2) DEFAULT NULL,
  `Net_Amount` decimal(18,2) DEFAULT NULL,
  `Paid_Amount` decimal(18,2) DEFAULT NULL,
  `Balance` decimal(18,2) GENERATED ALWAYS AS (`Paid_Amount` - `Net_Amount`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Daten für Tabelle `test_invoice_master`
--

INSERT INTO `test_invoice_master` (`InvoiceID`, `InvoiceDate`, `Sub_Total`, `Discount`, `Net_Amount`, `Paid_Amount`) VALUES
(3, '2023-12-07 18:29:57', 123.00, 1.00, 133.00, 12.00);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `test_invoice_detail`
--
ALTER TABLE `test_invoice_detail`
  ADD PRIMARY KEY (`Detail_ID`),
  ADD KEY `idx_Test_Invoice_Detail_Detail_ID` (`Detail_ID`);

--
-- Indizes für die Tabelle `test_invoice_master`
--
ALTER TABLE `test_invoice_master`
  ADD PRIMARY KEY (`InvoiceID`),
  ADD KEY `idx_Test_Invoice_Master_InvoiceID` (`InvoiceID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `test_invoice_detail`
--
ALTER TABLE `test_invoice_detail`
  MODIFY `Detail_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT für Tabelle `test_invoice_master`
--
ALTER TABLE `test_invoice_master`
  MODIFY `InvoiceID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

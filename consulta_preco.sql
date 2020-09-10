-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 26-Ago-2020 às 03:50
-- Versão do servidor: 10.4.13-MariaDB
-- versão do PHP: 7.4.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `consulta_preco`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `grupo`
--

CREATE TABLE `grupo` (
  `idGrupo` int(11) NOT NULL,
  `descricaoGrupo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `grupo`
--

INSERT INTO `grupo` (`idGrupo`, `descricaoGrupo`) VALUES
(1, 'ALIMENTOS'),
(2, 'BEBIDAS');

-- --------------------------------------------------------

--
-- Estrutura da tabela `marca`
--

CREATE TABLE `marca` (
  `idMarca` int(11) NOT NULL,
  `descricaoMarca` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `marca`
--

INSERT INTO `marca` (`idMarca`, `descricaoMarca`) VALUES
(1, 'ARROZ GLÓRIA'),
(2, 'FEIJÃO SÃO JOÃO'),
(3, 'ÁGUA CRISTAL'),
(4, 'CERVEJA MALTE');

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

CREATE TABLE `produto` (
  `idProduto` int(11) NOT NULL,
  `idGrupo` int(11) NOT NULL,
  `idMarca` int(11) NOT NULL,
  `codigobarrasProduto` int(11) NOT NULL,
  `descricaoProduto` varchar(100) NOT NULL,
  `unidadeMedidaProduto` varchar(2) NOT NULL,
  `aplicacaoProduto` text NOT NULL,
  `ativoProduto` int(11) NOT NULL,
  `precoProduto` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`idProduto`, `idGrupo`, `idMarca`, `codigobarrasProduto`, `descricaoProduto`, `unidadeMedidaProduto`, `aplicacaoProduto`, `ativoProduto`, `precoProduto`) VALUES
(1, 1, 1, 12345, 'ARROZ BRANCO PCT', 'Kg', 'Arroz Branco primeira linha. Produto da cesta básica.', 0, '3.50'),
(2, 1, 2, 12346, 'FEIJÃO PRETO PCT', 'Kg', 'Feijão preto primeira linha. Produto da cesta básica.', 0, '5.80'),
(3, 2, 3, 12347, 'ÁGUA MINERAL 500 ML', 'Un', 'Água mineral com Fonte Águas Doce', 0, '2.20'),
(4, 2, 4, 12348, 'CERVEJA PURO MALTE 355 ML', 'Un', 'Cerveja com apenas os ingredientes: Água, Malte e Lúpulo.', 0, '2.80'),
(12, 0, 0, 12349, 'TESTE ATIVIDADE 25/08/2020', 'Un', 'Teste Aplicação', 0, '9.99');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `grupo`
--
ALTER TABLE `grupo`
  ADD PRIMARY KEY (`idGrupo`);

--
-- Índices para tabela `marca`
--
ALTER TABLE `marca`
  ADD PRIMARY KEY (`idMarca`);

--
-- Índices para tabela `produto`
--
ALTER TABLE `produto`
  ADD PRIMARY KEY (`idProduto`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `grupo`
--
ALTER TABLE `grupo`
  MODIFY `idGrupo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `marca`
--
ALTER TABLE `marca`
  MODIFY `idMarca` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `idProduto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

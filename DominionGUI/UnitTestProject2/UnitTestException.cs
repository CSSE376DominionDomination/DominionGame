﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominionCards;
using DominionCards.KingdomCards;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestException
    {
        [TestMethod]
        public void TieExceptionReturnsCorrectStringForTwoPlayers()
        {
            Player p1 = new HumanPlayer(1);
            Player p3 = new HumanPlayer(3);
            TieException exc = new TieException(p1, p3, 9, 10);
            String expected = "Player 1, and Player 3 tied for winner! \n";
            Assert.AreEqual(expected, exc.PrintWinners());
        }
        [TestMethod]
        public void TieExceptionReturnsCorrectStringForFourWayTie()
        {
            // TODO
            Player p1 = new HumanPlayer(1);
            Player p2 = new HumanPlayer(2);
            Player p3 = new HumanPlayer(3);
            Player p4 = new HumanPlayer(4);
            TieException exc = new TieException(p1, p2, 9, 10);
            exc.BreaksTie(p3);
            exc.BreaksTie(p4);
            String expected = "Player 1, Player 2, Player 3 and Player 4 tied for winner! \n";
            Assert.AreEqual(expected, exc.PrintWinners());
        }


        [TestMethod]
        public void BreakTieReturnsTrueIfNewPlayerWins()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p3.getDiscard().Add(new Province());
            TieException exception = new TieException(p1, p2, p1.countVictoryPoints(), p1.getTotalMoney());
            Assert.IsTrue(exception.BreaksTie(p3));
        }
        [TestMethod]
        public void BreakTieReturnsFalseIfNewPlayerLoses()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p1.getDiscard().Add(new Province());
            p2.getDiscard().Add(new Province());
            TieException exception = new TieException(p1, p2, p1.countVictoryPoints(), p1.getTotalMoney());
            Assert.IsFalse(exception.BreaksTie(p3));
        }
        [TestMethod]
        public void BreakTieAddsPlayerToTieIfNewPlayerTies()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            TieException exception = new TieException(p1, p2, p1.countVictoryPoints(), p1.getTotalMoney());
            int arraySize = exception.getArraySize();
            exception.BreaksTie(p3);
            Assert.AreEqual(arraySize + 1, exception.getArraySize());
        }
        [TestMethod]
        public void BreakTieDoesNotAddPlayerToTieIfNewPlayerLoses()
        {
            Player p1 = new HumanPlayer();
            Player p2 = new HumanPlayer();
            Player p3 = new HumanPlayer();
            p1.getDiscard().Add(new Province());
            p2.getDiscard().Add(new Province());
            TieException exception = new TieException(p1, p2, p1.countVictoryPoints(), p1.getTotalMoney());
            int arraySize = exception.getArraySize();
            exception.BreaksTie(p3);
            Assert.AreEqual(arraySize, exception.getArraySize());
        }
    }
}

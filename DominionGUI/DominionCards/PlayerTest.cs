﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;

namespace DominionCards
{

    [TestFixture()]
    class PlayerTest
    {
        [Test()]
        public void testPlayerStartsWithCorrectNumberOfEstates()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();
            int numbEstates = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (((Card)hand[i]).getID() == 3)
                {
                    numbEstates++;
                }
            }
            Assert.AreEqual(3, numbEstates);
        }
        [Test()]
        public void testPlayerStartsWithCorrectNumberOfCopper()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();
            int numbCopper = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (((Card)hand[i]).getID() == 0)
                {
                    numbCopper++;
                }
            }
            Assert.AreEqual(7, numbCopper);
        }
        [Test()]
        public void testPlayerStartsWithCorrectNumberOfCards()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();
            Assert.AreEqual(10, hand.Count);
        }
//        [Test()]
//        public void testPlayerStartsWithFiveCardHand()
//        {
//            Player p1 = new HumanPlayer();
//           Assert.Fail();
//        }
        [Test()]
        public void testPlayerStartsWithOnlyEstatesAndCopper()
        {
            Player p1 = new HumanPlayer();
            ArrayList hand = p1.getHand();
            for (int i = 0; i < hand.Count; i++)
            {
                if (((Card)hand[i]).getID() != 0 && ((Card)hand[i]).getID() != 3)
                {
                    Assert.Fail();
                }
            }
        }
    }
}

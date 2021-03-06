﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Militia : AttackCard
    {
        private static int ID = 18;
        public Militia()
            : base(0, 2, 0, 0, 4, ID)
        {
            // TODO implement
        }

        public override String ToString()
        {
            return "Militia";
        }
        public override void MakeDelayedAttack(Player playerAttacked)
        {
            int numCardsToDiscard = playerAttacked.getHand().Count - 3;
            if (numCardsToDiscard <= 0)
            {
                return;
            }
            //pick card(s) to discard
            ArrayList DiscardableCards = new ArrayList();
            foreach (Card card in playerAttacked.getHand())
            {
                DiscardableCards.Add(card);

            }
            ArrayList cards = playerAttacked.SelectCards(DiscardableCards, "Player " + playerAttacked.getNumber() + ": You were attacked by a militia!!! \n Choose " + numCardsToDiscard + " cards to discard", numCardsToDiscard);
            while (cards.Count != numCardsToDiscard)
            {
                DialogResult result1 = MessageBox.Show("You must discard exactly " + numCardsToDiscard + " cards.  Try again");
                cards = playerAttacked.SelectCards(DiscardableCards, "You were attacked by a militia!!! \n Choose " + numCardsToDiscard + " cards to discard", numCardsToDiscard);
            }
            for (int i = 0; i < numCardsToDiscard; i++)
            {
                Card cardSelected = (Card)cards[i];
                playerAttacked.getHand().Remove(cardSelected);
                playerAttacked.getDiscard().Add(cardSelected);
            }
           
            
            
        }
    }
}

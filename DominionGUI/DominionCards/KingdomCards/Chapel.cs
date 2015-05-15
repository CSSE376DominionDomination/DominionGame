﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DominionCards.KingdomCards
{
    public class Chapel : ActionCard
    {
        public Chapel()
            : base(0, 0, 0, 0, 4, 10)
        {
            // TODO implement
        }

        public void textEffect()
        {
            //todo
        }
        public override void Play(Player player)
        {
            ArrayList cardsToList = new ArrayList();
            foreach (Card card in player.getHand())
            {
                cardsToList.Add(card);
                
            }
            if (cardsToList.Count == 0)
            {
                MessageBox.Show("You have no cards to play with the chapel!");
                return;
            }
            ArrayList cards = player.SelectCards(cardsToList, "Chapel", 4);
            for (int i = 0; i < cards.Count; i++)
            {
                Card toTrash = (Card)cards[i];
                player.getHand().Remove(toTrash);
            }

            
            
            

            
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RandomGenerateCards;

namespace DominionCards
{
    public abstract class Player
    {
        private int number;
        private Stack<Card> deck = new Stack<Card>();
        private ArrayList hand = new ArrayList();
        private ArrayList discard = new ArrayList();
        private Stack<Card> attacks = new Stack<Card>();

        public int actions, buys, money; // TODO set this to public temporarily so code would compile. 
        public Player()
        {
            for (int i = 0; i < 3; i++)
            {
                deck.Push(new KingdomCards.Estate());
            }
            for (int i = 0; i < 7; i++)
            {
                deck.Push(new KingdomCards.Copper());
            }
            // TODO shuffle the deck
        }
        public void setNumber(int numb)
        {
            number = numb;
        }
        public int getNumber()
        {
            return number;
        }
        public Card drawCard()
        {
            if (deck.Count == 0)
            {
                deck = Shuffle(discard);
            }
            return deck.Pop();
        }
        public void drawHand()
        {
            // discard old hand
            for (int i = 0; i < hand.Count; i++)
            {
                discard.Add((Card)hand[i]);
            }
            hand.Clear();
            // draw five new cards
            for (int i = 0; i < 5; i++)
            {
                hand.Add(drawCard());
            }
        }
        private void drawCardsFromPartialDeck()
        {
            throw new NotImplementedException("your deck ran out while drawing cards!!!");
        }
        public void endTurn()
        {
            // this method should discard remaining hand, and draw new cards, then reset actions and buys to 1.
            // TODO implement this.
        }

        public abstract void actionPhase();
        public abstract void buyPhase();
        public abstract void selectToDiscard();

        public int getTotalMoney()
        {
            int money = 0;
            Stack<Card> tempStack = new Stack<Card>();
            // get money from cards in deck
            while (deck.Count > 0)
            {
                Card card = deck.Pop();
                tempStack.Push(card);
                int cardID = card.getID();
                if (cardID == 0 || cardID == 1 || cardID == 2)
                {
                    money += ((TreasureCard)card).getValue();
                }
            }
            while (tempStack.Count > 0)
            {
                deck.Push(tempStack.Pop());
            }
            // get money for cards in discard
            for (int i = 0; i < discard.Count; i++)
            {

                Card card = (Card)discard[i];
                int cardID = card.getID();
                if (cardID == 0 || cardID == 1 || cardID == 2)
                {
                    money += ((TreasureCard)card).getValue();
                }
            }
            // get money for cards in hand
            for (int i = 0; i < hand.Count; i++)
            {
                Card card = (Card)hand[i];
                int cardID = card.getID();
                if (cardID == 0 || cardID == 1 || cardID == 2)
                {
                    money += ((TreasureCard)card).getValue();
                }
            }
            return money;
        }

        public ArrayList getHand()
        {
            return hand;
        }
        public void setHand(ArrayList h)
        {
            hand = h; // THIS METHOD IS FOR TESTING USE
        }
        public void setDiscard(ArrayList dis)
        {
            discard = dis; // THIS METHOD IS USED FOR TESTING
        }
        public void setDeck(Stack<Card> d)
        {
            deck = d; // THIS METHOD IS FOR TESTING USE
        }
        public Stack<Card> getDeck()
        {
            return deck;
        }
        public ArrayList getDiscard()
        {
            return discard;
        }
        public Stack<Card> getAttacks()
        {
            return attacks;
        }
        public int actionsLeft()
        {
            return actions;
        }
        public int buysLeft()
        {
            return buys;
        }
        public int moneyLeft()
        {
            return money;
        }

        public bool buyCard(Card card)
        {
            if (this.money < card.getPrice())
            {
                return false;
            }
                  discard.Add(card);
                buys--;
                this.money -= card.getPrice();
                return true;
            
        }

        public void addCardToHand(Card card)
        {
            hand.Add(card);
        }
        public int countVictoryPoints()
        {
            int vps = 0;
            Stack<Card> tempStack = new Stack<Card>();
            // get points from cards in deck
            while (deck.Count > 0)
            {
                Card card = deck.Pop();
                vps += card.getVictoryPoints();
                tempStack.Push(card);
            }
            // Returns all cards to the deck
            while (tempStack.Count > 0)
            {
                deck.Push(tempStack.Pop());
            }
            // get points for cards in discard
            for (int i = 0; i < discard.Count; i++)
            {
                vps += ((Card)discard[i]).getVictoryPoints();
            }
            // get points for cards in hand
            for (int i = 0; i < hand.Count; i++)
            {
                vps += ((Card)hand[i]).getVictoryPoints();
            }
            return vps;
        }
        public int playCard(Card c)
        {
            // finds the card that was played in the player's hand, then removes it.
            int handSize = hand.Count;
            if (c.getID() == 3 || c.getID() == 4 || c.getID() == 5 || c.getID() == 14)
            {
                throw new Exception("you cannot play victory cards!!");
            }

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] == c)
                {
                    hand.Remove(c);
                    break;
                }
            }
            // makse sure a card was removed.
            if (handSize - 1 != hand.Count)
            {
                throw new Exception("tried to play a card not in your hand!!!"); // USE A BETTER EXCEPTION
            }
            // check if card is treasure, and handle it specially
            if (c.getID() == 0 || c.getID() == 1 || c.getID() == 2)
            {
                Console.WriteLine("treasure card played, value " + ((TreasureCard)c).getValue());
                money += ((TreasureCard)c).getValue();
                return actions;
            }

            // resolves generic card effects
            ActionCard card = (ActionCard) c;
            actions--;
            for (int i = 0; i < card.cards; i++)
            {
                hand.Add(drawCard());
            }
            actions += card.actions;
            buys += card.buys;
            money += card.money;
            card.Play(this);
            return actions;
        }
        public static Stack<Card> Shuffle(ArrayList discard)
        {
            Stack<Card> newDeck; // = new Stack<Card>();
            Stack temp = GenerateRandom.SuffleDeck(discard);
            newDeck = ConvertStackToCardStack(temp);
            discard.Clear();
            return newDeck;
        }

        public static Stack<Card> ConvertStackToCardStack(Stack s)
        {
            Stack<Card> deck = new Stack<Card>();
            while (s.Count > 0)
            {
                deck.Push((Card)s.Pop());
            }
            return deck;
        }
        public virtual void TakeTurn(GameBoard board)
        {
            Console.WriteLine("player" + number + " taking turn.");
            // TODO implement this method.
        }
        public override string ToString()
        {
            return "Player " + number;
        }
        public void ProcessAttack()
        {

        }
    }
}

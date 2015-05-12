﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominionCards
{
    public abstract class AttackCard : ActionCard
    {
        Stack<Player> targets;
        public AttackCard(int extraCards, int extraMoney, int extraBuys, int extraActions, int price, int idNumb)
            : base(extraCards, extraMoney, extraBuys, extraActions, price, idNumb)
        {
            // TODO implement this class
        }
        public abstract void MakeAttack(Player p);
        public bool needsAction() {

            return false;
        }

        public void PushAttackToAttacks(Player p)
        {
            GameBoard board = GameBoard.getInstance();
            while (board.turnOrder.Peek() != p)
            {
                Player current = board.NextPlayer();
                current.getAttacks().Push(this);
            }
            board.NextPlayer();
        }

        public virtual void GetAttackerDecision(Player attacker, Player target)
        {
            // does nothing by default.
        }
        public virtual void GetResponse(Player target)
        {
            // does nothing.
        }

        public override void Play(Player player)
        {
            base.Play(player);
            PushAttackToAttacks(player);
        }

    }
}

using System;
using System.Collections.Generic;
using Ostinato.Core;
using Sirenix.Serialization;
using Sirenix.Utilities;
namespace Combat {
	[Serializable]
	public class TurnCycle : ITurnCycle {
		[OdinSerialize] public Queue<CombatTurn> CombatTurns { get; private set; }
		public void SetCombat(CombatState combat) { CombatTurns.ForEach(x => x.Combat = combat); }
		public ICombatTurn Current() => CombatTurns.Peek();
		public ICombatTurn Cycle() {
			var turn = CombatTurns.Dequeue();
			CombatTurns.Enqueue(turn);
			return Current();
		}
		public ITurnCycle Clone() {
			var turnCycle = new TurnCycle {
				CombatTurns = new(CombatTurns),
			};
			return turnCycle;
		}
	}
	public interface ITurnCycle : ICloneable<ITurnCycle> {
		public void SetCombat(CombatState combat);
		public ICombatTurn Current();
		public ICombatTurn Cycle();
	}

	public interface ICloneable<T> {
		public T Clone();
	}
}

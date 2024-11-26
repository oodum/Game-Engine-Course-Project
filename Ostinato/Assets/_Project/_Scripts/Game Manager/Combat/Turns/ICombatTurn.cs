using Ostinato.Core;
using UnityEngine;
namespace Combat {
	public interface ICombatTurn : IState {
		Sprite Icon { get; }
		string Name { get; }
	}
}

using Input;
using UnityEngine;

namespace Ostinato.Core.Player {
	public class PlayerInput : MonoBehaviour, IGameStateVisitor {
		[SerializeField] PlayerInputProcessor input;
	}
}

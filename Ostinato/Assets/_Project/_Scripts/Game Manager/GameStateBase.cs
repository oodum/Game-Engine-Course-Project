using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace Ostinato.Core {
	public abstract class GameStateBase : IState {
		protected readonly GameManager Manager;
		protected GameStateBase(GameManager manager) {
			Manager = manager;
		}
		public virtual void OnEnter() {
			Debug.Log($"GAME: Entering {GetType().Name}");
		}
		public virtual void Update() { }
		public virtual void OnExit() {
			Debug.Log($"GAME: Exiting {GetType().Name}");
		}
	}
}

using System;
using Ostinato.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace Combat {

	[Serializable]
	public abstract class CombatTurn : SerializedScriptableObject, ICombatTurn {
		public CombatState Combat;
		[OdinSerialize] public Sprite Icon { get; private set; }
		[OdinSerialize] public string Name { get; private set; }

		public virtual void OnEnter() {
			Debug.Log($"Entering {Name}");
		}
		public virtual void Update() { }

		public virtual void OnExit() {
			Debug.Log($"Exiting {Name}");
		}
	}
}

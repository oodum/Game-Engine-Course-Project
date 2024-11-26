using System;
using Combat;
using Ostinato.Core;
using EventBus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEntity {
	public class BasicCPUEntity : Entity {
		[SerializeField] GameObject model;
		void Start() {
			EventBus<CombatRegistrationEvent>.Raise(new(this, CombatRegistrationEvent.CombatSide.Right));
		}

		void Update() {
			if (Health <= 0) {
				model.SetActive(false);
			}
		}

		protected override void Register() { throw new NotImplementedException(); }
		protected override void Deregister() { throw new NotImplementedException(); }
	}
}

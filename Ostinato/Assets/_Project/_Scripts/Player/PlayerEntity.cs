using System;
using Combat;
using EventBus;
using IncantationSystem.Castables;
using Ostinato.Core;
using UnityEngine;
namespace GameEntity {
	[RequireComponent(typeof(PlayerCaster), typeof(PlayerMovement))]
	public class PlayerEntity : Entity {
		void Start() {
			EventBus<CombatRegistrationEvent>.Raise(new(this, CombatRegistrationEvent.CombatSide.Left));
		}
		protected override void Register() { throw new NotImplementedException(); }
		protected override void Deregister() { throw new NotImplementedException(); }
	}
}

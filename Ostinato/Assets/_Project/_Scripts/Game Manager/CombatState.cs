using System.Threading.Tasks;
using Combat;
using EventBus;
using GameEntity;
using IncantationSystem.Castables;
using MusicEngine;
using Ostinato.Core.Incantations;
using UnityEngine;
namespace Ostinato.Core {
	public class CombatRegistrationState : GameStateBase {
		readonly EventBinding<CombatRegistrationEvent> registrationEvent;
		public bool IsFulfilled { get; private set; }
		public CombatEntities Entities { get; private set; }
		public CombatRegistrationState(GameManager manager) : base(manager) {
			registrationEvent = new(OnCombatRegistration);
		}
		public override void OnEnter() {
			base.OnEnter();
			Manager.Input.SetFight();
			Entities = new();
			EventBus<CombatRegistrationEvent>.Register(registrationEvent);
		}

		public override void OnExit() {
			base.OnExit();
			EventBus<CombatRegistrationEvent>.Deregister(registrationEvent);
		}

		void OnCombatRegistration(CombatRegistrationEvent @event) {
			Debug.Log($"Combat registration: {@event.Entity.name} on {@event.Side}");
			if (@event.Side == CombatRegistrationEvent.CombatSide.Left) {
				Entities = new() {
					Left = @event.Entity,
					Right = Entities.Right,
				};
			} else {
				Entities = new() {
					Left = Entities.Left,
					Right = @event.Entity,
				};
			}
			if (Entities.Fulfilled) _ = SetFulfilled();
		}

		async Task SetFulfilled() {
			await Awaitable.WaitForSecondsAsync(1);
			IsFulfilled = true;
		}
	}
	public class CombatState : GameStateBase {
		ITurnCycle turnCycle;
		public CombatEntities Entities;
		public IIncantation SelectedIncantation;
		readonly CombatConfig combatConfig;
		readonly EventBinding<BeatEvent> beatEvent;
		readonly RequestBinding<PlayerQuiverRequest> quiverRequest;
		public CombatState(GameManager manager, CombatConfig config) : base(manager) {
			combatConfig = config;
			beatEvent = new(OnBeat);
			quiverRequest = new(SupplyQuiver);
		}

		public override void OnEnter() {
			base.OnEnter();
			EventBus<BeatEvent>.Register(beatEvent);
			EventBus<PlayerQuiverRequest>.Register(quiverRequest);
			InitializeCombatStates();
			MusicManager.Instance.StartMusic();
		}

		public override void OnExit() {
			base.OnExit();
			turnCycle.Current().OnExit();
			EventBus<BeatEvent>.Deregister(beatEvent);
			EventBus<PlayerQuiverRequest>.Deregister(quiverRequest);
			MusicManager.Instance.StopMusic();
		}

		void InitializeCombatStates() {
			turnCycle = combatConfig.CombatTurns.Clone();
			turnCycle.SetCombat(this);
			turnCycle.Current().OnEnter();
			EventBus<CombatTurnChangedEvent>.Raise(new(turnCycle.Current()));
		}

		public override void Update() {
			turnCycle.Current().Update();
		}

		void OnBeat(BeatEvent @event) {
			if (!@event.IsDownBeat) return;
			var current = turnCycle.Current();
			var turn = turnCycle.Cycle();
			if (current != turn) {
				current.OnExit();
				turn.OnEnter();
				EventBus<CombatTurnChangedEvent>.Raise(new(turn));
			}
		}

		PlayerQuiverRequest SupplyQuiver(PlayerQuiverRequest @event) {
			@event.Quiver = Entities.Left.GetComponent<IncantationQuiver>();
			return @event;
		}

		void OnCast(CastEvent @event) {
			Debug.Log($"Cast event: {@event.Caster.name}");
		}
	}

	public struct CombatRegistrationEvent : IEvent {
		public Entity Entity;
		public CombatSide Side;

		public CombatRegistrationEvent(Entity entity, CombatSide combatSide) {
			Entity = entity;
			Side = combatSide;
		}

		public enum CombatSide {
			Left,
			Right,
		}
	}

	public struct CombatEntities {
		public Entity Left, Right;
		public bool Fulfilled => Left != null && Right != null;
	}

	public class NormalState : GameStateBase {
		public NormalState(GameManager manager) : base(manager) { }
	}
}

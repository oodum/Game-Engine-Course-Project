using EventBus;
using GameEntity;
using IncantationSystem.Castables;
using MusicEngine;
using UnityEngine;
namespace Combat {
	[CreateAssetMenu(menuName = "Combat/Turns/Attack Turn", fileName = "AttackTurn", order = 0)]
	public class AttackTurn : CombatTurn {
		readonly RequestBinding<CastEvent> castEvent;
		public AttackTurn() {
			castEvent = new(OnCast);
		}
		public override void OnEnter() {
			base.OnEnter();
			EventBus<CastEvent>.Register(castEvent);
		}

		public override void Update() {
			MuseDashIncantationPlayView.Instance.ScrollAmount = -(float)MusicManager.Instance.RelativeProgress;
		}

		public override void OnExit() {
			base.OnExit();
			EventBus<CastEvent>.Deregister(castEvent);
		}

		CastEvent OnCast(CastEvent cast) {
			Debug.Log($"Casting: {cast.Caster.name}");
			if (cast.Caster is not PlayerEntity) return default;
			Combat.Entities.Right.Damage(5);
			var request = new CastEvent() {
				Succeeded = true,
			};
			return request;
		}
	}
}

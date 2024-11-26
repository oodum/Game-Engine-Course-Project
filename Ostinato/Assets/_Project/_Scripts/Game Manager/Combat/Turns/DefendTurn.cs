using EventBus;
using GameEntity;
using MusicEngine;
using UnityEngine;
namespace Combat {
	[CreateAssetMenu(menuName = "Combat/Turns/Defend Turn", fileName = "DefendTurn", order = 0)]
	public class DefendTurn : CombatTurn {
		RequestBinding<DashEvent> dashEvent;
		public DefendTurn() {
			dashEvent = new(OnDash);
		}

		public override void OnEnter() {
			base.OnEnter();
			EventBus<DashEvent>.Register(dashEvent);
		}

		public override void OnExit() {
			base.OnExit();
			EventBus<DashEvent>.Deregister(dashEvent);
		}

		public override void Update() {
			MuseDashIncantationPlayView.Instance.ScrollAmount = -(float)MusicManager.Instance.RelativeProgress;
		}

		DashEvent OnDash(DashEvent @event) {
			Debug.Log($"Dashing: {@event.Entity.name}");
			if (@event.Entity is not PlayerEntity) return @event;
			var request = new DashEvent() {
				Successful = true,
				Entity = @event.Entity,
				Movement = @event.Movement,
			};
			return request;
		}
	}
}

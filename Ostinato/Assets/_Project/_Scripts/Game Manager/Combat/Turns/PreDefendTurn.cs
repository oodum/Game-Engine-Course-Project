using EventBus;
using IncantationSystem.Castables;
using MusicEngine;
using UnityEngine;
namespace Combat {
	[CreateAssetMenu(menuName = "Combat/Turns/Pre-Defend Turn", fileName = "PreDefendTurn", order = 0)]
	public class PreDefendTurn : CombatTurn {
		EventBinding<BeatEvent> beatEvent;

		public PreDefendTurn() {
			beatEvent = new(OnBeat);
		}

		public override void OnEnter() {
			base.OnEnter();
			EventBus<BeatEvent>.Register(beatEvent);
		}

		public override void OnExit() {
			base.OnExit();
			EventBus<BeatEvent>.Deregister(beatEvent);
		}

		void OnBeat(BeatEvent beat) {
			if (beat.Beat == 4) {
				EventBus<SelectIncantationEvent>.Raise(new(Combat.Entities.Right.GetComponent<IncantationQuiver>().Incantations[0]));
			}
		}
	}
}

using EventBus;
using IncantationSystem.Castables;
using MusicEngine;
using Ostinato.Core.Incantations;
using UnityEngine;
namespace Combat {
	[CreateAssetMenu(menuName = "Combat/Turns/Pre-Attack Turn", fileName = "PreAttackTurn", order = 0)]
	public class PreAttackTurn : CombatTurn {
		EventBinding<DirectionalInputEvent> inputEvent;
		EventBinding<BeatEvent> beatEvent;
		IncantationQuiver quiver;
		int selection;
		public PreAttackTurn() {
			inputEvent = new(OnInput);
			beatEvent = new(OnBeat);
		}

		public override void OnEnter() {
			base.OnEnter();
			EventBus<DirectionalInputEvent>.Register(inputEvent);
			quiver = GetQuiver();
			EventBus<DisplayIncantationEvent>.Raise(new(quiver, true));
			EventBus<BeatEvent>.Register(beatEvent);

			selection = 0;
			EventBus<NavigateIncantationEvent>.Raise(new(selection));
		}

		IncantationQuiver GetQuiver() {
			var request = EventBus<PlayerQuiverRequest>.Request(new());
			return request.Quiver;
		}
		public override void OnExit() {
			base.OnExit();
			EventBus<DirectionalInputEvent>.Deregister(inputEvent);
			EventBus<BeatEvent>.Deregister(beatEvent);
			EventBus<DisplayIncantationEvent>.Raise(new(null, false));
		}

		void OnBeat(BeatEvent beat) {
			if (beat.Beat == 4) {
				EventBus<SelectIncantationEvent>.Raise(new(quiver.Incantations[selection]));
			}
		}

		void OnInput(DirectionalInputEvent input) {
			switch(input.Direction) {
				case Direction.Up: {
					selection--;
					if (selection < 0) selection = quiver.Incantations.Count - 1;
					EventBus<NavigateIncantationEvent>.Raise(new(selection));
					break;
				}
				case Direction.Down: {
					selection++;
					if (selection >= quiver.Incantations.Count) selection = 0;
					EventBus<NavigateIncantationEvent>.Raise(new(selection));
					break;
				}
				case Direction.Left:
				case Direction.Right:
				default: break;
			}
		}
	}

	public struct DisplayIncantationEvent : IEvent {
		public bool Show;
		public IncantationQuiver Quiver;
		public DisplayIncantationEvent(IncantationQuiver quiver, bool show) {
			Quiver = quiver;
			Show = show;
		}
	}

	public struct PlayerQuiverRequest : IEvent {
		public IncantationQuiver Quiver;
	}

	public struct SelectIncantationEvent : IEvent {
		public IIncantation Incantation;
		public SelectIncantationEvent(IIncantation incantation) {
			Incantation = incantation;
		}
	}

	public struct NavigateIncantationEvent : IEvent {
		public int Selection;
		public NavigateIncantationEvent(int selection) {
			Selection = selection;
		}
	}
}

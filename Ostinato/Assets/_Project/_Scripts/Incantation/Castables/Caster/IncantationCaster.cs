using EventBus;
using Extensions;
using GameEntity;
using MusicEngine;
using Scoring;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IncantationSystem.Castables {
	[RequireComponent(typeof(IncantationQuiver))]
	public class IncantationCaster : SerializedMonoBehaviour {
		IncantationQuiver quiver;
		ConfidenceComputer confidenceComputer;
		Entity entity;
		[SerializeField] FMODUnity.EventReference castSound;

		EventBinding<BeatEvent> beatEvent;

		void Awake() {
			quiver = gameObject.GetOrAdd<IncantationQuiver>();
			entity = gameObject.GetOrAdd<Entity>();
		}

		public void Cast() {
			FMODUnity.RuntimeManager.PlayOneShotAttached(castSound, gameObject);
		}

		void Start() {
			ServiceLocator.ServiceLocator.For(this).Get(out confidenceComputer);
		}
	}
}

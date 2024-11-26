using EventBus;
using Extensions;
using GameEntity;
using Input;
using Scoring;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IncantationSystem.Castables {
	[RequireComponent(typeof(IncantationCaster))]
	public class PlayerCaster : MonoBehaviour {
		IncantationCaster caster;
		Entity entity;
		[SerializeField, Required] PlayerInputProcessor inputProcessor;

		void Awake() {
			caster = gameObject.GetOrAdd<IncantationCaster>();
			entity = gameObject.GetOrAdd<Entity>();
		}

		void OnEnable() {
			inputProcessor.OnCastPressed += Cast;
		}

		void OnDisable() {
			inputProcessor.OnCastPressed -= Cast;
		}

		void Cast() {
			var request = EventBus<CastEvent>.Request(new(entity));
			if (!request.Succeeded) return;
			caster.Cast();
		}
	}

	public struct CastEvent : IEvent {
		public Entity Caster;
		public bool Succeeded;
		public CastEvent(Entity caster) {
			Caster = caster;
			Succeeded = false;
		}
	}
}

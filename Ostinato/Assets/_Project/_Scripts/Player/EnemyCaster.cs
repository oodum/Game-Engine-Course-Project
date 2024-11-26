using System;
using System.Linq;
using Combat;
using EventBus;
using Extensions;
using IncantationSystem.Castables;
using MusicEngine;
using Ostinato.Core.Incantations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEntity {
	[RequireComponent(typeof(IncantationCaster))]
	public class EnemyCaster : SerializedMonoBehaviour {
		IIncantation chosenIncantation;
		IncantationCaster caster;
		IncantationQuiver quiver;
		MusicManager musicManager;
		CombatManager combatManager;
		EventBinding<BeatEvent> beatEvent;
		//bool CanCast => combatManager.CurrentTurn is DefendTurn;
		void Awake() {
			caster = gameObject.GetOrAdd<IncantationCaster>();
			quiver = gameObject.GetOrAdd<IncantationQuiver>();
		}

		void Start() {
			ServiceLocator.ServiceLocator.For(this).Get(out combatManager);
			musicManager = MusicManager.Instance;
		}

		void OnEnable() {
			beatEvent = new(OnBeat);
			EventBus<BeatEvent>.Register(beatEvent);
		}

		void OnDisable() { EventBus<BeatEvent>.Deregister(beatEvent); }

		void Update() {
			/*
			if (!CanCast) return;
			double progress = musicManager.RelativeProgress;
			var timing = chosenIncantation.Notes[0].Timing;//.GetNextUnplayedNote().Timing;
			if (progress >= timing) Cast(timing);
		*/
		}

		void Cast(double timing) {
			//if (!CanCast) return;
		}

		void OnBeat(BeatEvent @event) {
			if (@event.IsDownBeat) chosenIncantation = quiver.Incantations[Random.Range(0, quiver.Incantations.Count)];
		}
	}
}
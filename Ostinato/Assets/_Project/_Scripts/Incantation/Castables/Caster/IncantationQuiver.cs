using System;
using System.Collections.Generic;
using Ostinato.Core.Incantations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace IncantationSystem.Castables {
	public class IncantationQuiver : SerializedMonoBehaviour {
		[OdinSerialize] public List<IIncantation> Incantations;

		void Awake() {
			if (Incantations == null || Incantations.Count == 0) {
				Debug.LogError("IncantationQuiver has no incantations");
			}
		}
	}
}

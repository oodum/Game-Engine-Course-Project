using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace Ostinato.Core.Incantations {
	[CreateAssetMenu(menuName = "Incantations 2/New Incantation", fileName = "Incantation", order = 0)]
	public class IncantationBuilder : SerializedScriptableObject, IIncantation {
		[Button]
		void ForceValidate() => OnValidate();
		[OdinSerialize] Incantation incantation;
		[OdinSerialize, ReadOnly] float Length {
			get {
				OnValidate();
				return incantation.Notes.Sum(x => x.Element.Duration);
			}
			set { }
		}
		void OnValidate() {
			incantation ??= new();
			incantation.EnsureNotesExist();
			incantation.ComputeTimings();
			incantation.ComputeDirections();
		}

		public INote[] Notes => incantation.Notes;
		public string Name => incantation.Name;
	}
}

using Ostinato.Core.Incantations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace Ostinato.Core.UI {
	[CreateAssetMenu(menuName = "Incantations 2/UI Note Library", fileName = "UI Note Library", order = 0)]
	public class UINoteLibrary : SerializedScriptableObject {
		[OdinSerialize] UINoteConfig quarterNote,
		                             eighthNote, eighthNoteLeft, eighthNoteRight,
		                             quarterRest,
		                             eighthRest;

		public UINoteConfig Get(INote note) => note.Element switch {
			QuarterNote => quarterNote,
			EighthNote => note.Direction switch {
				Direction.Left => eighthNoteLeft,
				Direction.Right => eighthNoteRight,
				_ => eighthNote,
			},
			QuarterRest => quarterRest,
			EighthRest => eighthRest,
			_ => default(UINoteConfig),
		};
	}
}

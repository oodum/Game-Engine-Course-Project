using System;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;
namespace Ostinato.Core.Incantations {
	public interface ISheetMusicElement {
		public float Duration { get; }
	}
	public interface ISheetMusicNote : ISheetMusicElement { }
	public interface ISheetMusicRest : ISheetMusicElement { }

	[Serializable]
	public class QuarterNote : ISheetMusicNote {
		public float Duration => 1;
	}

	[Serializable]
	public class EighthNote : ISheetMusicNote {
		public float Duration => 0.5f;
	}

	[Serializable]
	public class QuarterRest : ISheetMusicRest {
		public float Duration => 1;
	}

	[Serializable]
	public class EighthRest : ISheetMusicRest {
		public float Duration => 0.5f;
	}
	public interface INote {
		public ISheetMusicElement Element { get; }
		public float Timing { get; set; }
		public Direction Direction { get; set; }
	}

	public enum Direction {
		None,
		Left,
		Right,
	}

	[Serializable]
	public class Note : INote {

		[OdinSerialize] public ISheetMusicElement Element { get; private set; }
		[OdinSerialize] public float Timing { get; set; }
		[OdinSerialize] public Direction Direction { get; set; }
		public Note() {
			Element = new QuarterNote();
		}
		public Note(ISheetMusicElement element) {
			Element = element;
		}
		public Note(ISheetMusicElement element, float timing) {
			Element = element;
			Timing = timing;
		}
	}

	public interface IIncantation {
		string Name { get; }
		INote[] Notes { get; }
	}

	[Serializable]
	public class Incantation : IIncantation {
		[OdinSerialize] public string Name { get; private set; }
		[OdinSerialize] public INote[] Notes { get; private set; }
		IIncantationHandler timingHandler = new IncantationTimingHandler();
		IIncantationHandler directionHandler = new IncantationDirectionHandler();
		public void ComputeTimings() {
			timingHandler ??= new IncantationTimingHandler();
			timingHandler.Handle(this);
		}
		public void ComputeDirections() {
			directionHandler ??= new IncantationDirectionHandler();
			directionHandler.Handle(this);
		}
		public void EnsureNotesExist() {
			Notes ??= Array.Empty<INote>();
		}
	}

	public interface IIncantationHandler {
		public void Handle(IIncantation incantation);
	}

	public class IncantationDirectionHandler : IIncantationHandler {
		public void Handle(IIncantation incantation) {
			var positions = NotePositionals.Parser.Parse(incantation.Notes
				.Select(x => {
					if (x.Element is ISheetMusicRest rest) return -rest.Duration;
					return x.Element.Duration;
				})
				.ToArray());
			for (var i = 0; i < incantation.Notes.Length; i++) {
				var note = incantation.Notes[i];
				note.Direction = positions[i] switch {
					NotePositionals.Position.Left => Direction.Left,
					NotePositionals.Position.Right => Direction.Right,
					_ => Direction.None,
				};
			}
		}
	}
	public class IncantationTimingHandler : IIncantationHandler {
		public void Handle(IIncantation incantation) {
			float timing = 0;
			foreach (var note in incantation.Notes) {
				note.Timing = timing;
				timing += note.Element.Duration;
			}
		}
	}
}

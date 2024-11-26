using Extensions;
using Ostinato.Core.Incantations;
using UnityEngine;
using UnityEngine.UIElements;
namespace Ostinato.Core.UI {
	public class Note : VisualElement {
		public VisualElement Icon;
		public Sprite IconSprite;
		public int Index => parent.IndexOf(this);

		public Note() {
			this.AddClass("note");
			Icon = this.CreateChild("note-icon");
		}

		public void Set(Sprite icon, INote note) {
			IconSprite = icon;
			Icon.style.backgroundImage= IconSprite != null ? icon.texture : null;

			Icon.AddClass(note.Element switch {
				QuarterNote => "quarter-note",
				EighthNote => "eighth-note",
				QuarterRest => "quarter-note",
				EighthRest => "eighth-note",
				_ => "",
			});
		}

		public void Remove() {
			Icon.style.backgroundImage = null;
		}
	}
}

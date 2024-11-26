using Ostinato.Core.Incantations;
using Ostinato.Core.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Note = Ostinato.Core.UI.Note;
public class IncantationDisplay: MonoBehaviour , IIncantationDisplay {
	[SerializeField] UINoteLibrary Library;

	public void Display(IIncantation incantation, VisualElement container) {
		foreach (var note in incantation.Notes) {
			var noteElement = new Note();
			var config = Library.Get(note);
			noteElement.Set(config.Sprite, note);
			container.Add(noteElement);
		}
	}
}
public interface IIncantationDisplay {
	public void Display(IIncantation incantation, VisualElement container);
}

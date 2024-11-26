using System.Collections.Generic;
using Combat;
using EventBus;
using Extensions;
using IncantationSystem.Castables;
using Ostinato.Core.Incantations;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseIncantationView : MonoBehaviour {
	[SerializeField] UIDocument document;
	VisualElement Root, Container;
	EventBinding<DisplayIncantationEvent> displayIncantationEvent;
	EventBinding<NavigateIncantationEvent> selectIncantationEvent;

	List<VisualElement> incantationElements = new();

	void OnEnable() {
		displayIncantationEvent = new(DisplayIncantation);
		selectIncantationEvent = new(SelectIncantation);
		EventBus<DisplayIncantationEvent>.Register(displayIncantationEvent);
		EventBus<NavigateIncantationEvent>.Register(selectIncantationEvent);
	}

	void OnDisable() {
		EventBus<DisplayIncantationEvent>.Deregister(displayIncantationEvent);
		EventBus<NavigateIncantationEvent>.Deregister(selectIncantationEvent);
	}

	void DisplayIncantation(DisplayIncantationEvent @event) {
		if (@event.Show) OnDisplayIncantation(@event.Quiver);
		else OnHideIncantation();
	}

	void SelectIncantation(NavigateIncantationEvent navigate) {
		foreach (var element in incantationElements) {
			element.RemoveFromClassList("incantationSelector-incantation-listitem__selected");
		}
		incantationElements[navigate.Selection].AddToClassList("incantationSelector-incantation-listitem__selected");
	}

	void OnDisplayIncantation(IncantationQuiver quiver) {
		Root ??= document.rootVisualElement;
		Container ??= Root.Q<VisualElement>("ChooseIncantation-Container");

		Container.Clear();
		incantationElements.Clear();

		if (!TryGetComponent(out IIncantationDisplay displayer)) Debug.LogError("No IIncantationDisplay component found on this object");
		foreach (var incantation in quiver.Incantations) {
			var element = new VisualElement()
				.AddClass("incantationSelector-incantation-listitem")
				.AddTo(Container);
			incantationElements.Add(element);
			var label = new Label(incantation.Name).AddClass("incantationSelector-incantation-name").AddTo(element);
			var incantationView = new VisualElement().AddClass("incantationSelector-incantation-view").AddTo(element);
			var incantationContainer = new VisualElement().AddClass("note-scroll", "incantation-container").AddTo(incantationView);
			displayer.Display(incantation, incantationContainer);
		}

		Root.style.opacity = 1;
	}

	void OnHideIncantation() {
		Root.style.opacity = 0;
	}
}

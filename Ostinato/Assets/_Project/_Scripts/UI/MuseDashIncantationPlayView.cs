using System;
using Combat;
using EventBus;
using Ostinato.Core.Incantations;
using UnityEngine;
using UnityEngine.UIElements;
using Note = Ostinato.Core.UI.Note;
public class MuseDashIncantationPlayView : IncantationPlayView {
	public static MuseDashIncantationPlayView Instance { get; private set; }

	EventBinding<SelectIncantationEvent> selectIncantationEvent;

	void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}

	void OnEnable() {
		selectIncantationEvent = new(SelectIncantation);
		EventBus<SelectIncantationEvent>.Register(selectIncantationEvent);
	}

	void OnDisable() {
		EventBus<SelectIncantationEvent>.Deregister(selectIncantationEvent);
	}

	[SerializeField, Min(1)] int pixelsPerBeat = 128;
	public override float ScrollAmount {
		get {
			if (Container == null) return 0;
			return Container.transform.position.x;
		}
		set {
			Container ??= Root.Q<VisualElement>("NoteScroll");
			Container.transform.position = new(value * pixelsPerBeat, 0, 0);
		}
	}

	void SelectIncantation(SelectIncantationEvent @event) {
		if (@event.Incantation == null) return;
		InitializeView(@event.Incantation);
	}

	public override void InitializeView(IIncantation incantation) {
		Root = Document.rootVisualElement;
		Root.styleSheets.Add(StyleSheet);
		Container = Root.Q<VisualElement>("IncantationContainer");
		Container.Clear();
		ScrollAmount = 0;
		if (!TryGetComponent(out IIncantationDisplay displayer)) Debug.LogError("No IIncantationDisplay component found on this object");
		displayer.Display(incantation, Container);
	}
}

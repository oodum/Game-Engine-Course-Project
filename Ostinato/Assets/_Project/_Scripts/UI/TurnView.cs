using System;
using Combat;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

public class TurnView : MonoBehaviour {
	Image image;
	EventBinding<CombatTurnChangedEvent> turnChangedEventBinding;
	void OnEnable() {
		turnChangedEventBinding = new(UpdateTurnSprite);
		EventBus<CombatTurnChangedEvent>.Register(turnChangedEventBinding);
	}
	void OnDisable() {
		EventBus<CombatTurnChangedEvent>.Deregister(turnChangedEventBinding);
	}
	void Awake() {
		image = GetComponent<Image>();
	}
	void UpdateTurnSprite(CombatTurnChangedEvent @event) {
		image.sprite = @event.Turn.Icon;
	}
}

using System;
using EventBus;
using Extensions;
using FMODUnity;
using GameEntity;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat {
	public class PlayerMovement : MonoBehaviour {
		[SerializeField, Required] PlayerInputProcessor inputProcessor;
		[SerializeField, Required] EventReference dashEvent;
		CombatManager combatManager;
		Entity entity;
		//bool IsEnemyTurn => combatManager.CurrentTurn is DefendTurn;
		public bool CanDash = true;

		[SerializeField] float dashDistance;
		[SerializeField] float dashSpeed;
		Vector2 position;

		void Awake() {
			entity = gameObject.GetOrAdd<Entity>();
		}

		void Start() {
			ServiceLocator.ServiceLocator.Global.Get(out combatManager);
			position = transform.position.Flatten();
		}

		void OnEnable() {
			inputProcessor.OnUpPressed += MoveUp;
			inputProcessor.OnDownPressed += MoveDown;
			inputProcessor.OnLeftPressed += MoveLeft;
			inputProcessor.OnRightPressed += MoveRight;
		}

		void OnDisable() {
			inputProcessor.OnUpPressed -= MoveUp;
			inputProcessor.OnDownPressed -= MoveDown;
			inputProcessor.OnLeftPressed -= MoveLeft;
			inputProcessor.OnRightPressed -= MoveRight;
		}

		void MoveUp() {
			Dash(Vector2.up);
			EventBus<DirectionalInputEvent>.Raise(new(Direction.Up));
		}

		void MoveDown() {
			Dash(Vector2.down);
			EventBus<DirectionalInputEvent>.Raise(new(Direction.Down));
		}

		void MoveLeft() {
			Dash(Vector2.left);
			EventBus<DirectionalInputEvent>.Raise(new(Direction.Left));
		}

		void MoveRight() {
			Dash(Vector2.right);
			EventBus<DirectionalInputEvent>.Raise(new(Direction.Right));
		}

		void Update() { transform.position = Vector3.MoveTowards(transform.position, position.UnFlatten(), dashSpeed).With(y: transform.position.y); }

		void Dash(Vector2 direction) {
			var request =  EventBus<DashEvent>.Request(new(this, entity));
			if (!request.Successful) return;
			position += direction * dashDistance;
			FMODUnity.RuntimeManager.PlayOneShotAttached(dashEvent, gameObject);
		}
	}
}
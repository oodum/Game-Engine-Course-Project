using System;
using Combat;
using Input;
using Ostinato.Core;
using Scoring;
using Sirenix.Serialization;
using UnityEngine;
using Utility;

public class GameManager : Singleton<GameManager> {
	[OdinSerialize] public PlayerInputProcessor Input { get; private set; }

	StateMachine stateMachine;
	ScoreComputer scoreComputer;
	DodgeComputer dodgeComputer;
	[SerializeField] CombatConfig combatConfig;

	CombatRegistrationState combatRegistrationState;
	CombatState combatState;
	NormalState normalState;


	protected override void Awake() {
		base.Awake();

		stateMachine = new();

		combatRegistrationState = new(this);
		combatState = new(this, combatConfig);
		normalState = new(this);

		stateMachine.AddTransition(combatRegistrationState, combatState, new FuncPredicate(() => {
			combatState.Entities = combatRegistrationState.Entities;
			return combatRegistrationState.IsFulfilled;
		}));
		stateMachine.AddTransition(combatState, normalState, new FuncPredicate(() => combatState.Entities.Left.Health <= 0 || combatState.Entities.Right.Health <= 0));

		stateMachine.SetState(combatRegistrationState);
	}

	void OnDisable() {
		stateMachine.CurrentState?.OnExit();
	}

	void Start() {
		ServiceLocator.ServiceLocator.For(this)
			.Get(out scoreComputer)
			.Get(out dodgeComputer);
	}

	void Update() {
		stateMachine.Update();
	}
	public ScoreResult GetScore(float timing) {
		ScoreResult scoreResult = scoreComputer.CalculateScore(timing);

		return scoreResult;
	}

	public DodgeResult GetDodge(float timing) {
		DodgeResult dodgeResult = dodgeComputer.CalculateDodge(timing);

		return dodgeResult;
	}

	public void Pause() {
		//noop
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using EventBus;
using UnityEngine;
namespace Ostinato.Core {
	public interface IPredicate {
		public bool Evaluate();
	}
	public class FuncPredicate : IPredicate {
		readonly Func<bool> func;
		public FuncPredicate(Func<bool> func) {
			this.func = func;
		}
		public bool Evaluate() => func();
	}
	public class EventPredicate<T> : IPredicate where T : IEvent {
		readonly EventBinding<T> eventBinding;
		bool triggered;
		EventPredicate() {
			eventBinding = new(OnEvent);
			EventBus<T>.Register(eventBinding);
		}

		~EventPredicate() {
			EventBus<T>.Deregister(eventBinding);
		}

		void OnEvent() {
			triggered = true;
		}
		public bool Evaluate() {
			if (!triggered) return false;
			triggered = false;
			return true;
		}
	}
	public interface ITransition {
		public IState To { get; }
		public IPredicate Predicate { get; }
	}
	public class Transition : ITransition {
		public IState To { get; }
		public IPredicate Predicate { get; }
		public Transition(IState to, IPredicate predicate) {
			To = to;
			Predicate = predicate;
		}
	}
	public class StateMachine {
		StateNode current;
		Dictionary<Type, StateNode> nodes = new();
		HashSet<ITransition> anyTransitions = new();
		public IState CurrentState => current.State;

		public void Update() {
			var transition = GetTransition();
			if (transition != null) {
				ChangeState(transition.To);
			}
			current.State.Update();
		}

		void ChangeState(IState state) {
			if (state == current.State) return;
			current.State?.OnExit();
			current = nodes[state.GetType()];
			current.State?.OnEnter();
		}

		public void SetState(IState state) {
			current?.State.OnExit();
			current = nodes[state.GetType()];
			current.State?.OnEnter();
		}

		ITransition GetTransition() {
			foreach (var transition in anyTransitions) {
				if (transition.Predicate.Evaluate()) return transition;
			}
			foreach (var transition in current.Transitions) {
				if (transition.Predicate.Evaluate()) return transition;
			}
			return null;
		}

		public void AddTransition(IState from, IState to, IPredicate predicate) {
			GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, predicate);
		}

		public void AddAnyTransition(IState from, IState to, IPredicate predicate) {
			anyTransitions.Add(new Transition(GetOrAddNode(to).State, predicate));
		}

		StateNode GetOrAddNode(IState state) {
			if (nodes.TryGetValue(state.GetType(), out var node)) return node;
			node = new(state);
			nodes.Add(state.GetType(), node);
			return node;
		}

		class StateNode {
			public IState State;
			public HashSet<ITransition> Transitions { get; }
			public StateNode(IState state) {
				State = state;
				Transitions = new();
			}
			public void AddTransition(IState to, IPredicate predicate) => Transitions.Add(new Transition(to, predicate));
		}
	}
	public interface IState {
		public void OnEnter();
		public void Update();
		public void OnExit();
	}
	public interface IGameStateVisitor { }

	public interface IGameStateVisitable {
		public void Accept(IGameStateVisitor visitor);
	}
}

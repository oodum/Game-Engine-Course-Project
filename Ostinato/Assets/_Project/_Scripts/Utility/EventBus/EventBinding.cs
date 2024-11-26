using System;
namespace EventBus {
	public interface IEventBinding<T> {
		public Action<T> OnEvent { get; set; }
		public Action OnEventNoArgs { get; set; }
	}

	public class EventBinding<T> : IEventBinding<T> where T : IEvent {
		Action<T> onEvent = _ => { };
		Action onEventNoArgs = () => { };

		Action<T> IEventBinding<T>.OnEvent {
			get => onEvent;
			set => onEvent = value;
		}

		Action IEventBinding<T>.OnEventNoArgs {
			get => onEventNoArgs;
			set => onEventNoArgs = value;
		}

		public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
		public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

		public void Add(Action onEvent) => onEventNoArgs += onEvent;
		public void Remove(Action onEvent) => onEventNoArgs -= onEvent;

		public void Add(Action<T> onEvent) => this.onEvent += onEvent;
		public void Remove(Action<T> onEvent) => this.onEvent -= onEvent;
	}

	public interface IRequestBinding<T> {
		public Func<T, T> OnRequest { get; set; }
		public Func<T> OnRequestNoArgs { get; set; }
	}

	public class RequestBinding<T> : IRequestBinding<T> where T : IEvent {
		Func<T, T> onRequest = _ => default;
		Func<T> onRequestNoArgs = () => default;

		Func<T, T> IRequestBinding<T>.OnRequest {
			get => onRequest;
			set => onRequest = value;
		}

		Func<T> IRequestBinding<T>.OnRequestNoArgs {
			get => onRequestNoArgs;
			set => onRequestNoArgs = value;
		}

		public RequestBinding(Func<T, T> onRequest) => this.onRequest = onRequest;
	}
}

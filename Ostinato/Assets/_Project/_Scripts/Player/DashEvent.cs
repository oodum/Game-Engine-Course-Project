using EventBus;
using GameEntity;

namespace Combat {
	public struct DashEvent : IEvent {
		public PlayerMovement Movement;
		public Entity Entity;
		public bool Successful;
		
		public DashEvent(PlayerMovement movement, Entity entity, bool successful = false) {
			Movement = movement;
			Entity = entity;
			Successful = false;
		}
	}

	public struct DirectionalInputEvent : IEvent {
		public Direction Direction;
		public DirectionalInputEvent(Direction direction) { Direction = direction; }
	}

	public enum Direction {
		Up,
		Down,
		Left,
		Right,
	}
}
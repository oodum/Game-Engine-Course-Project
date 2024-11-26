using EventBus;
using GameEntity;
namespace Combat {

	public struct CombatTurnChangedEvent : IEvent {
		public ICombatTurn Turn;
		public CombatTurnChangedEvent(ICombatTurn turn) {
			Turn = turn;
		}
	}
	public struct DamageReceivedEvent : IEvent {
		public Entity Receiver;
		public int Damage;

		public DamageReceivedEvent(Entity receiver, int damage) {
			Receiver = receiver;
			Damage = damage;
		}
	}
}

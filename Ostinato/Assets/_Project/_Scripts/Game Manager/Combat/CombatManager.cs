using System.Collections.Generic;
using EventBus;
using GameEntity;
using Sirenix.OdinInspector;

namespace Combat {
	public class CombatManager : SerializedMonoBehaviour {
		bool InCombat { get; set; }
		[HorizontalGroup("Entities")] [VerticalGroup("Entities/Left")]
		public List<Entity> GoodEntities;
		[VerticalGroup("Entities/Right")] public List<Entity> BadEntities;

		void Awake() { ServiceLocator.ServiceLocator.Global.Register(this); }

		public void RegisterAsGoodEntity(Entity entity) {
			if (GoodEntities.Contains(entity) || BadEntities.Contains(entity)) return;
			print($"Registering <b>{entity}</b> as good entity");
			GoodEntities.Add(entity);
		}

		public void RegisterAsBadEntity(Entity entity) {
			if (BadEntities.Contains(entity) || BadEntities.Contains(entity)) return;
			print($"Registering <b>{entity}</b> as bad entity");
			BadEntities.Add(entity);
		}

		public void DeregisterEntity(Entity entity) {
			if (GoodEntities.Contains(entity)) {
				GoodEntities.Remove(entity);
			} else if (BadEntities.Contains(entity)) {
				BadEntities.Remove(entity);
			}
		}

		void AttackBadEntities(int damage) {
			foreach (Entity badEntity in BadEntities) {
				badEntity.Damage(damage);
				EventBus<DamageReceivedEvent>.Raise(new(badEntity, damage));
			}
		}

		void OnDamageReceived(DamageReceivedEvent @event) {
			var entity = @event.Receiver;
			if (entity.Health <= 0) {
				entity.Kill();
				InCombat = false;
			}
		}
	}
}

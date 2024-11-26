using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
namespace Combat {
	[CreateAssetMenu(menuName = "Combat/New Combat Config", fileName = "CombatConfig", order = 0)]
	public class CombatConfig : SerializedScriptableObject {
		[OdinSerialize] public ITurnCycle CombatTurns;
	}
}

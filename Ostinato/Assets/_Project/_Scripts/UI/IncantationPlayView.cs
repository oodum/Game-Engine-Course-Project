using Ostinato.Core.Incantations;
using Ostinato.Core.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public abstract class IncantationPlayView : SerializedMonoBehaviour {
	[OdinSerialize] protected IIncantation Incantation;

	[SerializeField] protected UIDocument Document;
	[SerializeField] protected StyleSheet StyleSheet;

	[SerializeField] protected UINoteLibrary Library;

	protected VisualElement Root, Container;
	
	public abstract float ScrollAmount { get; set; }

	public abstract void InitializeView(IIncantation incantation);
	void Start() {
		InitializeView(Incantation);
	}
}

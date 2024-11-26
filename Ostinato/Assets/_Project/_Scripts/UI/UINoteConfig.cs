using System;
using UnityEngine;
namespace Ostinato.Core.UI {
	[Serializable]
	public readonly struct UINoteConfig {
		public readonly Sprite Sprite;
		public readonly int Width;
		public UINoteConfig(Sprite sprite, int width) {
			Sprite = sprite;
			Width = width;
		}
	}
}

using UnityEngine.UIElements;
namespace Extensions {
	public static class VisualElementExtensions {
		public static VisualElement CreateChild(this VisualElement parent, params string[] classes) {
			var child = new VisualElement();
			child.AddClass(classes).AddTo(parent);
			return child;
		}

		public static T CreateChild<T>(this VisualElement parent, params string[] classes) where T : VisualElement, new() {
			var child = new T();
			child.AddClass(classes).AddTo(parent);
			return child;
		}

		public static T AddClass<T>(this T visualElement, params string[] classes) where T : VisualElement {
			foreach (var @class in classes) {
				if (!string.IsNullOrEmpty(@class))
					visualElement.AddToClassList(@class);
			}
			return visualElement;
		}

		public static T AddTo<T>(this T visualElement, VisualElement parent) where T : VisualElement {
			parent.Add(visualElement);
			return visualElement;
		}

		public static T WithManipulator<T>(this T visualElement, IManipulator manipulator) where T : VisualElement {
			visualElement.AddManipulator(manipulator);
			return visualElement;
		}
	}
}

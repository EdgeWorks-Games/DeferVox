using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTK;

namespace DeferVox
{
	public class GameObject
	{
		private readonly List<GameObject> _children = new List<GameObject>();
		private readonly List<IObjectComponent> _components = new List<IObjectComponent>();

		public GameObject()
		{
			Children = new ReadOnlyCollection<GameObject>(_children);
			Components = new ReadOnlyCollection<IObjectComponent>(_components);
		}

		public GameObject Parent { get; private set; }
		public ReadOnlyCollection<GameObject> Children { get; private set; }
		public ReadOnlyCollection<IObjectComponent> Components { get; private set; }

		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }

		public void AddChild(GameObject child)
		{
			if (child == null)
				throw new ArgumentNullException("child");

			_children.Add(child);
			child.NotifyParented(this);
		}

		private void NotifyParented(GameObject parent)
		{
			Parent = parent;
		}

		public void Add(IObjectComponent component)
		{
			_components.Add(component);
		}

		public Matrix4 CreateIsolatedMatrix()
		{
			return
				Matrix4.CreateRotationX(Rotation.X) *
				Matrix4.CreateRotationY(Rotation.Y) *
				Matrix4.CreateRotationZ(Rotation.Z) *
				Matrix4.CreateTranslation(Position);
		}
	}
}
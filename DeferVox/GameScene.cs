using System;
using System.Collections.Generic;
using System.Linq;
using DeferVox.ObjectComponents;
using OpenTK;

namespace DeferVox
{
	public sealed class GameScene : IDisposable
	{
		public GameScene()
		{
			Root = new GameObject();
		}

		public GameObject Root { get; private set; }

		public void Dispose()
		{
		}

		public IEnumerable<ComponentMatrixPair<TComponent>> TempGetComponent<TComponent>() where TComponent : IObjectComponent
		{
			var pairList = new List<ComponentMatrixPair<TComponent>>();
			ScanComponentRecursive(pairList, Root, Matrix4.Identity);
			return pairList;
		}

		private static void ScanComponentRecursive<TComponent>(List<ComponentMatrixPair<TComponent>> list, GameObject obj, Matrix4 previousMatrix)
		{
			var matrix = previousMatrix * obj.CreateIsolatedMatrix();

			list.AddRange(obj.Components
				.OfType<TComponent>()
				.Select(c => new ComponentMatrixPair<TComponent>(c, matrix, obj)));

			foreach (var child in obj.Children)
				ScanComponentRecursive(list, child, matrix);
		}

		public struct ComponentMatrixPair<TComponent>
		{
			private readonly TComponent _component;
			private readonly Matrix4 _matrix;
			private readonly GameObject _object;

			public ComponentMatrixPair(TComponent component, Matrix4 matrix, GameObject obj)
			{
				_component = component;
				_matrix = matrix;
				_object = obj;
			}

			public TComponent Component
			{
				get { return _component; }
			}

			public Matrix4 Matrix
			{
				get { return _matrix; }
			}

			public GameObject Object
			{
				get { return _object; }
			}
		}
	}
}
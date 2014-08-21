using System;
using DeferVox.Rendering;
using OpenTK;

namespace DeferVox.BasicEntities
{
	public sealed class PlayerEntity : IRenderableEntity
	{
		public PlayerEntity(Vector3 position, Camera camera)
		{
			Position = position;
			Camera = camera;
		}

		public Vector3 Position { get; set; }
		public float Rotation { get; set; }
		public Camera Camera { get; set; }

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
			Rotation += (float) delta.TotalSeconds*1.0f;

			Camera.Position = Position + new Vector3(0, 1.5f, 0);
			Camera.Rotation = new Vector3(0, Rotation, 0);
		}

		public void Render(IRenderer renderer)
		{
		}
	}
}
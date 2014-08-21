using System;
using DeferVox.Input;
using DeferVox.Rendering;
using OpenTK;

namespace DeferVox.BasicEntities
{
	public sealed class PlayerEntity : IRenderableEntity
	{
		public PlayerEntity(InputGameComponent component, Vector3 position, Camera camera)
		{
			Position = position;
			Camera = camera;

			component.AimChange += input_AimChange;
		}

		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Camera Camera { get; set; }

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
			Camera.Position = Position + new Vector3(0, 1.5f, 0);
			Camera.Rotation = new Vector3(Rotation.X, Rotation.Y, 0);
		}

		public void Render(IRenderer renderer)
		{
			// Perhaps here render the player in case of reflections and shadows?
		}

		private void input_AimChange(object sender, AimEventArgs e)
		{
			var rotation = Rotation;

			rotation.Y -= e.XDelta * 0.0015f;
			rotation.X -= e.YDelta * 0.0015f;
			rotation.X = Math.Min(rotation.X, MathHelper.DegreesToRadians(80));
			rotation.X = Math.Max(rotation.X, -MathHelper.DegreesToRadians(80));

			Rotation = rotation;
		}
	}
}
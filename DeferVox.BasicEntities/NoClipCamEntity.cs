using System;
using DeferVox.Input;
using DeferVox.Rendering;
using OpenTK;
using OpenTK.Input;

namespace DeferVox.BasicEntities
{
	public sealed class NoClipCamEntity : IRenderableEntity
	{
		public NoClipCamEntity(InputGameComponent component, Vector3 position, Camera camera)
		{
			Position = position;
			Camera = camera;

			component.AimChange += input_AimChange;
		}

		public NoClipCamEntity()
		{
			// Default values, once C# 6.0 rolls around we can do this inline
			Speed = 1.0f;
		}

		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Camera Camera { get; set; }

		public float Speed { get; set; }
		public float FastSpeed { get; set; }

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
			var rotationMatrix =
				Matrix4.CreateRotationX(Rotation.X)*
				Matrix4.CreateRotationY(Rotation.Y)*
				Matrix4.CreateRotationZ(Rotation.Z);

			var backwards = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
			var right = Vector3.Transform(Vector3.UnitX, rotationMatrix);

			var keyboard = Keyboard.GetState();
			var targetDirection = new Vector3();

			if (keyboard.IsKeyDown(Key.S))
				targetDirection += backwards;
			if (keyboard.IsKeyDown(Key.W))
				targetDirection -= backwards;
			if (keyboard.IsKeyDown(Key.D))
				targetDirection += right;
			if (keyboard.IsKeyDown(Key.A))
				targetDirection -= right;

			targetDirection.NormalizeFast();
			Position += delta.PerSecond(targetDirection*(keyboard.IsKeyDown(Key.ShiftLeft) ? FastSpeed : Speed));

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

			rotation.Y -= e.XDelta*0.0015f;
			rotation.X -= e.YDelta*0.0015f;
			rotation.X = Math.Min(rotation.X, MathHelper.DegreesToRadians(90));
			rotation.X = Math.Max(rotation.X, -MathHelper.DegreesToRadians(90));

			Rotation = rotation;
		}
	}
}
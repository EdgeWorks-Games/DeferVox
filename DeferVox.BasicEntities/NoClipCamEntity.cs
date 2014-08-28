using System;
using DeferVox.Input;
using DeferVox.ObjectComponents;
using DeferVox.Rendering;
using OpenTK;
using OpenTK.Input;

namespace DeferVox.BasicEntities
{
	public sealed class NoClipCamEntity
	{
		public NoClipCamEntity(InputGameComponent input, CameraObjectComponent camera)
		{
			Camera = camera;

			input.AimChange += input_AimChange;

			// Default values, once C# 6.0 rolls around we can do this inline
			Speed = 1.0f;
			FastSpeed = 2.0f;
		}

		public CameraObjectComponent Camera { get; set; }

		public float Speed { get; set; }
		public float FastSpeed { get; set; }

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
			/*var rotationMatrix =
				Matrix4.CreateRotationX(Camera.Rotation.X)*
				Matrix4.CreateRotationY(Camera.Rotation.Y);

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
			Camera.Position += delta.PerSecond(targetDirection*(keyboard.IsKeyDown(Key.ShiftLeft) ? FastSpeed : Speed));*/
		}
		
		private void input_AimChange(object sender, AimEventArgs e)
		{
			/*var rotation = Camera.Rotation.Xy + (-e.Delta.Yx * 0.0015f);
			Camera.Rotation = new Vector3(
				 MathHelper.Clamp(rotation.X, -MathHelper.PiOver2, MathHelper.PiOver2),
				 rotation.Y, 0);*/
		}
	}
}
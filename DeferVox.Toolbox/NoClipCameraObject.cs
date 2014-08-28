using System.Drawing;
using DeferVox.Input;
using DeferVox.ObjectComponents;
using OpenTK;
using OpenTK.Input;

namespace DeferVox.Toolbox
{
	public class NoClipCameraObject : GameObject
	{
		public NoClipCameraObject(GameEngine engine, InputGameComponent input, Size resolution)
		{
			Position = new Vector3(4, 4, 4);
			Rotation = new Vector3(MathHelper.DegreesToRadians(-20), MathHelper.DegreesToRadians(45), 0);

			// Default values, once C# 6.0 rolls around we can do this inline
			Speed = 1.0f;
			FastSpeed = 2.0f;

			engine.AtUpdate += EngineOnUpdate;
			input.AimChange += InputOnAimChange;

			Add(new CameraObjectComponent
			{
				Resolution = resolution,
				VerticalFieldOfView = MathHelper.DegreesToRadians(70)
			});
		}

		public float Speed { get; set; }
		public float FastSpeed { get; set; }

		private void EngineOnUpdate(object s, UpdateEventArgs e)
		{
			var rotationMatrix =
				Matrix4.CreateRotationX(Rotation.X)*
				Matrix4.CreateRotationY(Rotation.Y);

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
			Position += e.Delta.PerSecond(
				targetDirection*(
					keyboard.IsKeyDown(Key.ShiftLeft)
						? FastSpeed
						: Speed));
		}

		private void InputOnAimChange(object sender, AimEventArgs e)
		{
			var rotation = Rotation.Xy + (-e.Delta.Yx * 0.0015f);
			Rotation = new Vector3(
				 MathHelper.Clamp(rotation.X, -MathHelper.PiOver2, MathHelper.PiOver2),
				 rotation.Y, 0);
		}
	}
}
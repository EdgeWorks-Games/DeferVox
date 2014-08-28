using System.Drawing;
using DeferVox.ObjectComponents;
using OpenTK;

namespace DeferVox.Toolbox
{
	public class NoClipCameraObject : GameObject
	{
		public NoClipCameraObject(Size resolution)
		{
			Position = new Vector3(4, 4, 4);
			Rotation = new Vector3(MathHelper.DegreesToRadians(-20), MathHelper.DegreesToRadians(45), 0);

			Add(new CameraObjectComponent
			{
				Resolution = resolution,
				VerticalFieldOfView = MathHelper.DegreesToRadians(70)
			});
		}

		public float Speed { get; set; }
		public float FastSpeed { get; set; }
	}
}
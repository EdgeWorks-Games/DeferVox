using System.Drawing;
using DeferVox.ObjectComponents;
using OpenTK;

namespace DeferVox.Toolbox
{
	public class NoClipCameraObject : GameObject
	{
		public NoClipCameraObject()
		{
			Position = new Vector3(0, 0, 4);

			Add(new CameraObjectComponent
			{
				Resolution = new Size(1280, 720),
				VerticalFieldOfView = MathHelper.DegreesToRadians(70)
			});
		}

		public float Speed { get; set; }
		public float FastSpeed { get; set; }
	}
}
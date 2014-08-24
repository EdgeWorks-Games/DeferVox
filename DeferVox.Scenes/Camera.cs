using System.Drawing;
using OpenTK;

namespace DeferVox.Scenes
{
	public sealed class Camera
	{
		public Point ScreenPosition { get; set; }
		public Size Resolution { get; set; }

		public float VerticalFieldOfView { get; set; }

		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }

		public float Ratio
		{
			get { return (float) Resolution.Width/Resolution.Height; }
		}
	}
}
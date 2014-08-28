using System.Drawing;

namespace DeferVox.ObjectComponents
{
	public sealed class CameraObjectComponent : IObjectComponent
	{
		public Point ScreenPosition { get; set; }
		public Size Resolution { get; set; }

		public float VerticalFieldOfView { get; set; }

		public float Ratio
		{
			get { return (float) Resolution.Width/Resolution.Height; }
		}
	}
}
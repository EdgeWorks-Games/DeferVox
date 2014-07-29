using System;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;

namespace DeferVox.Entities
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PositionColorVertex
	{
		public readonly Vector3 Position;
		public readonly Vector3 Color;

		public PositionColorVertex(float x, float y, float z, Color color)
		{
			Position = new Vector3(x, y, z);
			Color = new Vector3(color.R / 255f, color.G / 255f, color.B / 255f);
		}

		public static readonly int SizeInBytes = Marshal.SizeOf(new PositionColorVertex());
	}

	public sealed class TestEntity : IEntity
	{
		private static readonly PositionColorVertex[] TestBufferData =
		{
			// Front
			new PositionColorVertex(-1f, -1f, 0f, Color.FromArgb(0,255,0)), // Left Bottom
			new PositionColorVertex(1f, -1f, 0f, Color.FromArgb(0,255,0)), // Right Bottom
			new PositionColorVertex(-1f, 1f, 0f, Color.FromArgb(0,255,0)), // Left Top

			new PositionColorVertex(1f, -1f, 0f, Color.Red), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.Red), // Right Top
			new PositionColorVertex(-1f, 1f, 0f, Color.Red), // Left Top

			// Back
			new PositionColorVertex(1f, -1f, 0f, Color.Blue), // Left Bottom 
			new PositionColorVertex(-1f, -1f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.Blue), // Left Top

			new PositionColorVertex(-1f, -1f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(-1f, 1f, 0f, Color.Blue), // Right Top
			new PositionColorVertex(1f, 1f, 0f, Color.Blue) // Left Top
		};

		private float _rotation;

		public void Update(TimeSpan delta)
		{
			_rotation += 2f*(float) delta.TotalSeconds;
		}

		public void Render(IRenderer renderer)
		{
			renderer.RenderStreamedMesh(Vector3.Zero, new Vector3(0, _rotation, 0), TestBufferData);
		}

		public void Dispose()
		{
		}
	}
}
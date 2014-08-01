using System;
using System.Drawing;
using System.Numerics;
using DeferVox.Graphics;

namespace DeferVox.Entities
{
	public sealed class TestEntity : IEntity
	{
		private static readonly PositionColorVertex[] TestBufferData =
		{
			// Front
			new PositionColorVertex(-.5f, -.5f, 0f, Color.FromArgb(0, 255, 0)), // Left Bottom
			new PositionColorVertex(.5f, -.5f, 0f, Color.FromArgb(0, 255, 0)), // Right Bottom
			new PositionColorVertex(-.5f, .5f, 0f, Color.FromArgb(0, 255, 0)), // Left Top

			new PositionColorVertex(.5f, -.5f, 0f, Color.Red), // Right Bottom
			new PositionColorVertex(.5f, .5f, 0f, Color.Red), // Right Top
			new PositionColorVertex(-.5f, .5f, 0f, Color.Red), // Left Top

			// Back
			new PositionColorVertex(.5f, -.5f, 0f, Color.Blue), // Left Bottom 
			new PositionColorVertex(-.5f, -.5f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(.5f, .5f, 0f, Color.Blue), // Left Top

			new PositionColorVertex(-.5f, -.5f, 0f, Color.Yellow), // Right Bottom
			new PositionColorVertex(-.5f, .5f, 0f, Color.Yellow), // Right Top
			new PositionColorVertex(.5f, .5f, 0f, Color.Yellow) // Left Top
		};

		private float _rotation;

		public Vector3f Position { get; set; }

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
			_rotation += 2f*(float) delta.TotalSeconds;
		}

		public void Render(IRenderer renderer)
		{
			renderer.RenderStreamedMesh(Position, new Vector3f(0, _rotation, 0), TestBufferData);
		}
	}
}
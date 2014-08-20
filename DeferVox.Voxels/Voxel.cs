using System.Drawing;
using DeferVox.Rendering;

namespace DeferVox.Voxels
{
	internal struct Voxel
	{
		public static readonly PositionColorVertex[] Mesh =
		{
			// Front
			new PositionColorVertex(0f, 0f, 0f, Color.FromArgb(0, 130, 0)), // Left Bottom
			new PositionColorVertex(1f, 0f, 0f, Color.FromArgb(0, 130, 0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 0f, Color.FromArgb(0, 130, 0)), // Left Top

			new PositionColorVertex(1f, 0f, 0f, Color.FromArgb(0, 130, 0)), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.FromArgb(0, 130, 0)), // Right Top
			new PositionColorVertex(0f, 1f, 0f, Color.FromArgb(0, 130, 0)), // Left Top
			
			// Left
			new PositionColorVertex(0f, 0f, 1f, Color.FromArgb(0, 120, 0)), // Left Bottom 
			new PositionColorVertex(0f, 0f, 0f, Color.FromArgb(0, 120, 0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 1f, Color.FromArgb(0, 120, 0)), // Left Top

			new PositionColorVertex(0f, 0f, 0f, Color.FromArgb(0, 120, 0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 0f, Color.FromArgb(0, 120, 0)), // Right Top
			new PositionColorVertex(0f, 1f, 1f, Color.FromArgb(0, 120, 0)), // Left Top

			// Right
			new PositionColorVertex(1f, 0f, 0f, Color.FromArgb(0, 120, 0)), // Left Bottom 
			new PositionColorVertex(1f, 0f, 1f, Color.FromArgb(0, 120, 0)), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.FromArgb(0, 120, 0)), // Left Top

			new PositionColorVertex(1f, 0f, 1f, Color.FromArgb(0, 120, 0)), // Right Bottom
			new PositionColorVertex(1f, 1f, 1f, Color.FromArgb(0, 120, 0)), // Right Top
			new PositionColorVertex(1f, 1f, 0f, Color.FromArgb(0, 120, 0)), // Left Top

			// Back
			new PositionColorVertex(1f, 0f, 1f, Color.FromArgb(0, 110, 0)), // Left Bottom 
			new PositionColorVertex(0f, 0f, 1f, Color.FromArgb(0, 110, 0)), // Right Bottom
			new PositionColorVertex(1f, 1f, 1f, Color.FromArgb(0, 110, 0)), // Left Top

			new PositionColorVertex(0f, 0f, 1f, Color.FromArgb(0, 110, 0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 1f, Color.FromArgb(0, 110, 0)), // Right Top
			new PositionColorVertex(1f, 1f, 1f, Color.FromArgb(0, 110, 0)), // Left Top

			// Top
			new PositionColorVertex(0f, 1f, 0f, Color.FromArgb(0, 140, 0)), // Left Bottom 
			new PositionColorVertex(1f, 1f, 0f, Color.FromArgb(0, 140, 0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 1f, Color.FromArgb(0, 140, 0)), // Left Top

			new PositionColorVertex(1f, 1f, 0f, Color.FromArgb(0, 140, 0)), // Right Bottom
			new PositionColorVertex(1f, 1f, 1f, Color.FromArgb(0, 140, 0)), // Right Top
			new PositionColorVertex(0f, 1f, 1f, Color.FromArgb(0, 140, 0)), // Left Top

			// Bottom
			new PositionColorVertex(0f, 0f, 1f, Color.FromArgb(0, 100, 0)), // Left Bottom 
			new PositionColorVertex(1f, 0f, 1f, Color.FromArgb(0, 100, 0)), // Right Bottom
			new PositionColorVertex(0f, 0f, 0f, Color.FromArgb(0, 100, 0)), // Left Top

			new PositionColorVertex(1f, 0f, 1f, Color.FromArgb(0, 100, 0)), // Right Bottom
			new PositionColorVertex(1f, 0f, 0f, Color.FromArgb(0, 100, 0)), // Right Top
			new PositionColorVertex(0f, 0f, 0f, Color.FromArgb(0, 100, 0)) // Left Top
		};

		public readonly bool IsSolid;

		public Voxel(bool isSolid)
		{
			IsSolid = isSolid;
		}
	}
}
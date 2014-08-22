using DeferVox.Rendering;

namespace DeferVox.BasicEntities.Voxels
{
	internal struct Voxel
	{
		public static readonly PositionUvVertex[] Mesh =
		{
			// Front
			new PositionUvVertex(0f, 0f, 0f, 0, 0), // Left Bottom
			new PositionUvVertex(1f, 0f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 1f, 0f, 0, 1), // Left Top

			new PositionUvVertex(1f, 0f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 1f, 0f, 1, 1), // Right Top
			new PositionUvVertex(0f, 1f, 0f, 0, 1), // Left Top
			
			// Left
			new PositionUvVertex(0f, 0f, 1f, 0, 0), // Left Bottom 
			new PositionUvVertex(0f, 0f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 1f, 1f, 0, 1), // Left Top

			new PositionUvVertex(0f, 0f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 1f, 0f, 1, 1), // Right Top
			new PositionUvVertex(0f, 1f, 1f, 0, 1), // Left Top

			// Right
			new PositionUvVertex(1f, 0f, 0f, 0, 0), // Left Bottom 
			new PositionUvVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 1f, 0f, 0, 1), // Left Top

			new PositionUvVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 1f, 1f, 1, 1), // Right Top
			new PositionUvVertex(1f, 1f, 0f, 0, 1), // Left Top

			// Back
			new PositionUvVertex(1f, 0f, 1f, 0, 0), // Left Bottom 
			new PositionUvVertex(0f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 1f, 1f, 0, 1), // Left Top

			new PositionUvVertex(0f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 1f, 1f, 1, 1), // Right Top
			new PositionUvVertex(1f, 1f, 1f, 0, 1), // Left Top

			// Top
			new PositionUvVertex(0f, 1f, 0f, 0, 0), // Left Bottom 
			new PositionUvVertex(1f, 1f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 1f, 1f, 0, 1), // Left Top

			new PositionUvVertex(1f, 1f, 0f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 1f, 1f, 1, 1), // Right Top
			new PositionUvVertex(0f, 1f, 1f, 0, 1), // Left Top

			// Bottom
			new PositionUvVertex(0f, 0f, 1f, 0, 0), // Left Bottom 
			new PositionUvVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(0f, 0f, 0f, 0, 1), // Left Top

			new PositionUvVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new PositionUvVertex(1f, 0f, 0f, 1, 1), // Right Top
			new PositionUvVertex(0f, 0f, 0f, 0, 1) // Left Top
		};

		public readonly bool IsSolid;

		public Voxel(bool isSolid)
		{
			IsSolid = isSolid;
		}
	}
}
namespace DeferVox.Toolbox
{
	internal struct Voxel
	{
		public static readonly TexturedVertex[] Mesh =
		{
			// Front
			new TexturedVertex(0f, 0f, 0f, 0, 0), // Left Bottom
			new TexturedVertex(1f, 0f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 1f, 0f, 0, 1), // Left Top

			new TexturedVertex(1f, 0f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 1f, 0f, 1, 1), // Right Top
			new TexturedVertex(0f, 1f, 0f, 0, 1), // Left Top
			
			// Left
			new TexturedVertex(0f, 0f, 1f, 0, 0), // Left Bottom 
			new TexturedVertex(0f, 0f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 1f, 1f, 0, 1), // Left Top

			new TexturedVertex(0f, 0f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 1f, 0f, 1, 1), // Right Top
			new TexturedVertex(0f, 1f, 1f, 0, 1), // Left Top

			// Right
			new TexturedVertex(1f, 0f, 0f, 0, 0), // Left Bottom 
			new TexturedVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 1f, 0f, 0, 1), // Left Top

			new TexturedVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 1f, 1f, 1, 1), // Right Top
			new TexturedVertex(1f, 1f, 0f, 0, 1), // Left Top

			// Back
			new TexturedVertex(1f, 0f, 1f, 0, 0), // Left Bottom 
			new TexturedVertex(0f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 1f, 1f, 0, 1), // Left Top

			new TexturedVertex(0f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 1f, 1f, 1, 1), // Right Top
			new TexturedVertex(1f, 1f, 1f, 0, 1), // Left Top

			// Top
			new TexturedVertex(0f, 1f, 0f, 0, 0), // Left Bottom 
			new TexturedVertex(1f, 1f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 1f, 1f, 0, 1), // Left Top

			new TexturedVertex(1f, 1f, 0f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 1f, 1f, 1, 1), // Right Top
			new TexturedVertex(0f, 1f, 1f, 0, 1), // Left Top

			// Bottom
			new TexturedVertex(0f, 0f, 1f, 0, 0), // Left Bottom 
			new TexturedVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(0f, 0f, 0f, 0, 1), // Left Top

			new TexturedVertex(1f, 0f, 1f, 1, 0), // Right Bottom
			new TexturedVertex(1f, 0f, 0f, 1, 1), // Right Top
			new TexturedVertex(0f, 0f, 0f, 0, 1) // Left Top
		};

		public readonly bool IsSolid;

		public Voxel(bool isSolid)
		{
			IsSolid = isSolid;
		}
	}
}
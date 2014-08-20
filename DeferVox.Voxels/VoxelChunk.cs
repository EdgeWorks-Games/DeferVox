namespace DeferVox.Voxels
{
	internal class VoxelChunk
	{
		public const int Size = 16;

		public Vector3I Position { get; private set; }
		public Voxel[][][] Voxels { get; private set; }

		public static VoxelChunk Generate(Vector3I position)
		{
			var voxels = new Voxel[Size][][];
			for (var x = 0; x < Size; x++)
			{
				voxels[x] = new Voxel[Size][];

				for (var y = 0; y < Size; y++)
					voxels[x][y] = new Voxel[Size];

				for (var z = 0; z < Size; z++)
					voxels[x][0][z] = new Voxel(true);
			}

			return new VoxelChunk
			{
				Position = position,
				Voxels = voxels
			};
		}
	}
}
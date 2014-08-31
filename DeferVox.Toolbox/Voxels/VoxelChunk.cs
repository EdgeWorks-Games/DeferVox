using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpNoise.Modules;

namespace DeferVox.Toolbox.Voxels
{
	internal class VoxelChunk
	{
		public const float PerlinMapSize = 0.02f;
		public const int Size = 16;
		private static readonly Perlin Perlin = new Perlin();

		public Vector3I Position { get; private set; }
		public Voxel[][][] Voxels { get; private set; }

		public static VoxelChunk Generate(Vector3I position)
		{
			// TODO: Make chunk generation be done by a separate class.
			// Some terrain features go accross chunks, such as the actual height map.

			var voxels = new Voxel[Size][][];
			for (var x = 0; x < Size; x++)
			{
				voxels[x] = new Voxel[Size][];

				for (var y = 0; y < Size; y++)
					voxels[x][y] = new Voxel[Size];

				for (var z = 0; z < Size; z++)
				{
					var perlinHeight = Perlin.GetValue(
						((position.X*Size) + x)*PerlinMapSize, 0.5,
						((position.Z*Size) + z)*PerlinMapSize);
					var height = (perlinHeight + 1)*6;
					height = MathHelper.Clamp(height, 1, Size - 0.1);

					for (var y = 0; y < height; y++)
						voxels[x][y][z] = new Voxel(true);
				}
			}

			return new VoxelChunk
			{
				Position = position,
				Voxels = voxels
			};
		}

		public Mesh CreateMesh()
		{
			// Generate a mesh from the chunk
			var meshVertices = new List<TexturedVertex>();
			for (var x = 0; x < Voxels.Length; x++)
			{
				for (var y = 0; y < Voxels[x].Length; y++)
				{
					for (var z = 0; z < Voxels[x][y].Length; z++)
					{
						if (!Voxels[x][y][z].IsSolid)
							continue;

						var matrix = Matrix4.CreateTranslation(x, y, z);
						meshVertices.AddRange(Voxel.Mesh.Select(v =>
							new TexturedVertex(Vector3.Transform(v.Position, matrix), v.Uv)));
					}
				}
			}

			return new Mesh(meshVertices.ToArray());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace DeferVox.Entities
{
	internal struct Voxel
	{
		public readonly bool IsSolid;

		public Voxel(bool isSolid)
		{
			IsSolid = isSolid;
		}

		public static PositionColorVertex[] VoxelMesh =
		{
			// Front
			new PositionColorVertex(0f, 0f, 0f, Color.Red), // Left Bottom
			new PositionColorVertex(1f, 0f, 0f, Color.FromArgb(0,128,0)), // Right Bottom
			new PositionColorVertex(0f, 1f, 0f, Color.Blue), // Left Top

			new PositionColorVertex(1f, 0f, 0f, Color.Blue), // Right Bottom
			new PositionColorVertex(1f, 1f, 0f, Color.Blue), // Right Top
			new PositionColorVertex(0f, 1f, 0f, Color.Blue), // Left Top

			// Back
			new PositionColorVertex(1f, 0f, 1f, Color.Blue), // Left Bottom 
			new PositionColorVertex(0f, 0f, 1f, Color.Blue), // Right Bottom
			new PositionColorVertex(1f, 1f, 1f, Color.Blue), // Left Top

			new PositionColorVertex(0f, 0f, 1f, Color.Blue), // Right Bottom
			new PositionColorVertex(0f, 1f, 1f, Color.Blue), // Right Top
			new PositionColorVertex(1f, 1f, 1f, Color.Blue) // Left Top
		};
	}

	internal class VoxelChunk
	{
		public const int Size = 16;

		public Vector3i Position { get; set; }
		public Voxel[][][] Voxels { get; set; }

		public static VoxelChunk Generate(Vector3i position)
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

	public class VoxelMapEntity : IEntity
	{
		private readonly List<VoxelChunk> _chunks = new List<VoxelChunk>();

		public VoxelMapEntity()
		{
			_chunks.Add(VoxelChunk.Generate(new Vector3i(0, 0, 0)));
			_chunks.Add(VoxelChunk.Generate(new Vector3i(-1, 0, -1)));

			Trace.TraceInformation("Generated {0} chunk{1}!", _chunks.Count, _chunks.Count == 1 ? "" : "s");
		}

		public void Dispose()
		{
		}

		public void Update(TimeSpan delta)
		{
		}

		public void Render(IRenderer renderer)
		{
		}
	}
}
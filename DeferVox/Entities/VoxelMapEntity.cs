using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using DeferVox.Graphics;

namespace DeferVox.Entities
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
			_chunks[0].Voxels[1][1][1] = new Voxel(true);

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
			foreach (var chunk in _chunks)
			{
				for (var x = 0; x < VoxelChunk.Size; x++)
				{
					for (var y = 0; y < VoxelChunk.Size; y++)
					{
						for (var z = 0; z < VoxelChunk.Size; z++)
						{
							if (!chunk.Voxels[x][y][z].IsSolid)
								continue;

							// TODO: Make chunks have one big mesh instead of many small ones
							renderer.RenderStreamedMesh(
								new Vector3f(
									(chunk.Position.X*VoxelChunk.Size) + x,
									(chunk.Position.Y*VoxelChunk.Size) + y,
									(chunk.Position.Z*VoxelChunk.Size) + z),
								Vector3f.Zero,
								Voxel.Mesh);
						}
					}
				}
			}
		}
	}
}
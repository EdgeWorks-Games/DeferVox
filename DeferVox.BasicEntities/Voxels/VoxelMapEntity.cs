using System;
using System.Collections.Generic;
using System.Diagnostics;
using DeferVox.Rendering;
using OpenTK;

namespace DeferVox.BasicEntities.Voxels
{
	public sealed class VoxelMapEntity : IRenderableEntity
	{
		private readonly List<VoxelChunk> _chunks = new List<VoxelChunk>();
		private readonly StaticMesh<PositionUvVertex> _voxelMesh;

		public VoxelMapEntity()
		{
			_chunks.Add(VoxelChunk.Generate(new Vector3I(0, 0, 0)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(-1, 0, 0)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(0, 0, -1)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(-1, 0, -1)));
			Trace.TraceInformation("Generated {0} chunk{1}!", _chunks.Count, _chunks.Count == 1 ? "" : "s");

			_voxelMesh = new StaticMesh<PositionUvVertex>(Voxel.Mesh, PositionUvVertex.SizeInBytes);
		}

		public void Dispose()
		{
			_voxelMesh.Dispose();
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

							// TODO: Make chunks have one big mesh instead of the block mesh many times
							renderer.RenderMesh(
								new Vector3(
									(chunk.Position.X*VoxelChunk.Size) + x,
									(chunk.Position.Y*VoxelChunk.Size) + y,
									(chunk.Position.Z*VoxelChunk.Size) + z),
								Vector3.Zero,
								_voxelMesh);
						}
					}
				}
			}
		}
	}
}
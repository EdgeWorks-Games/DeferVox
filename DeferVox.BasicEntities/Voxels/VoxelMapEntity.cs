using System.Collections.Generic;
using System.Diagnostics;

namespace DeferVox.BasicEntities.Voxels
{
	public sealed class VoxelMapEntity
	{
		private readonly List<VoxelChunk> _chunks = new List<VoxelChunk>();
		//private readonly RenderMesh<TexturedVertex> _voxelMesh;

		public VoxelMapEntity()
		{
			_chunks.Add(VoxelChunk.Generate(new Vector3I(0, 0, 0)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(-1, 0, 0)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(0, 0, -1)));
			_chunks.Add(VoxelChunk.Generate(new Vector3I(-1, 0, -1)));
			Trace.TraceInformation("Generated {0} chunk{1}!", _chunks.Count, _chunks.Count == 1 ? "" : "s");

			//_voxelMesh = new RenderMesh<TexturedVertex>(Voxel.Mesh, TexturedVertex.SizeInBytes);
		}
	}
}
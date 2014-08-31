using System.Collections.Generic;
using System.Diagnostics;
using DeferVox.ObjectComponents;

namespace DeferVox.Toolbox.Voxels
{
	public class VoxelMapObject : GameObject
	{
		private const int Size = 8;

		public VoxelMapObject()
		{
			var chunks = new List<VoxelChunk>();
			for (var x = -Size; x < Size; x++)
			{
				for (var z = -Size; z < Size; z++)
				{
					var chunk = VoxelChunk.Generate(new Vector3I(x, 0, z));
					chunks.Add(chunk);

					// Add the mesh to the scene
					var obj = new GameObject()
					{
						Position = chunk.Position.ToVector3() * VoxelChunk.Size
					};
					obj.AddComponent(new MeshObjectComponent(chunk.CreateMesh()));
					AddChild(obj);
				}
			}

			Trace.TraceInformation("Generated {0} chunk{1}!", chunks.Count, chunks.Count == 1 ? "" : "s");
		}
	}
}
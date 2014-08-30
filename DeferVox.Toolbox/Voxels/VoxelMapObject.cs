using System.Collections.Generic;
using System.Diagnostics;
using DeferVox.ObjectComponents;

namespace DeferVox.Toolbox.Voxels
{
	public class VoxelMapObject : GameObject
	{
		public VoxelMapObject()
		{
			var chunks = new List<VoxelChunk>();
			for (var x = -2; x < 2; x++)
			{
				for (var y = -2; y < 2; y++)
				{
					var chunk = VoxelChunk.Generate(new Vector3I(x, 0, y));
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
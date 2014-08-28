using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DeferVox.ObjectComponents;
using OpenTK;

namespace DeferVox.Toolbox.Voxels
{
	public class VoxelMapObject : GameObject
	{
		public VoxelMapObject()
		{
			var chunks = new List<VoxelChunk>
			{
				VoxelChunk.Generate(new Vector3I(0, 0, 0)),
				VoxelChunk.Generate(new Vector3I(-1, 0, 0)),
				VoxelChunk.Generate(new Vector3I(0, 0, -1)),
				VoxelChunk.Generate(new Vector3I(-1, 0, -1))
			};
			Trace.TraceInformation("Generated {0} chunk{1}!", chunks.Count, chunks.Count == 1 ? "" : "s");

			foreach (var chunk in chunks)
			{
				// Generate a mesh from the chunk
				var meshVertices = new List<TexturedVertex>();
				for (var x = 0; x < chunk.Voxels.Length; x++)
				{
					for (var y = 0; y < chunk.Voxels[x].Length; y++)
					{
						for (var z = 0; z < chunk.Voxels[x][y].Length; z++)
						{
							if (!chunk.Voxels[x][y][z].IsSolid)
								continue;

							var matrix = Matrix4.CreateTranslation(x, y, z);
							meshVertices.AddRange(Voxel.Mesh.Select(v =>
								new TexturedVertex(Vector3.Transform(v.Position, matrix), v.Uv)));
						}
					}
				}

				// Add the mesh to the scene
				var obj = new GameObject()
				{
					Position = chunk.Position.ToVector3()*VoxelChunk.Size
				};
				obj.Add(new MeshObjectComponent(new Mesh(meshVertices.ToArray())));
				AddChild(obj);
			}

			Add(new MeshObjectComponent(new Mesh(Voxel.Mesh)));
		}
	}
}
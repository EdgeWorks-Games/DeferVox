using DeferVox.Rendering;

namespace DeferVox.Toolbox
{
	public class VoxelMapObject : GameObject
	{
		public VoxelMapObject()
		{
			Add(new TempMeshObjectComponent(
				new StaticMesh<PositionUvVertex>(
					Voxel.Mesh,
					PositionUvVertex.SizeInBytes)));
		}
	}
}
using DeferVox.ObjectComponents;

namespace DeferVox.Toolbox
{
	public class VoxelMapObject : GameObject
	{
		public VoxelMapObject()
		{
			Add(new MeshObjectComponent(new Mesh(Voxel.Mesh)));
		}
	}
}
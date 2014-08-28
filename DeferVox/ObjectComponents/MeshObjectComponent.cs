namespace DeferVox.ObjectComponents
{
	public class MeshObjectComponent : IObjectComponent
	{
		public MeshObjectComponent(Mesh mesh)
		{
			Mesh = mesh;
		}

		public Mesh Mesh { get; private set; }
	}
}
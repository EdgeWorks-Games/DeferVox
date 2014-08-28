namespace DeferVox.Rendering
{
	public class TempMeshObjectComponent : IObjectComponent
	{
		public StaticMesh<PositionUvVertex> TempMesh { get; set; }

		public TempMeshObjectComponent(StaticMesh<PositionUvVertex> mesh)
		{
			TempMesh = mesh;
		}
	}
}
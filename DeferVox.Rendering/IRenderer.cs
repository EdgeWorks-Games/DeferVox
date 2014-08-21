using OpenTK;

namespace DeferVox.Rendering
{
	public interface IRenderer
	{
		void RenderMesh(Vector3 position, Vector3 rotation, StaticMesh<PositionUvVertex> mesh);
	}
}
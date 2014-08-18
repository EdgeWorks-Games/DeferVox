using System.Numerics;

namespace DeferVox.Rendering
{
	public interface IRenderer
	{
		void RenderMesh(Vector3f position, Vector3f rotation, StaticMesh<PositionColorVertex> mesh);
	}
}
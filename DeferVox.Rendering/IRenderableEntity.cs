using DeferVox.Scenes;

namespace DeferVox.Rendering
{
	public interface IRenderableEntity : IEntity
	{
		void Render(IRenderer renderer);
	}
}
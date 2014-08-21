using System;

namespace DeferVox.Scenes
{
	public interface IEntity : IDisposable
	{
		void Update(TimeSpan delta);
	}
}
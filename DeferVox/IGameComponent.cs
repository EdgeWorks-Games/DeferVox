using System;

namespace DeferVox
{
	public interface IGameComponent
	{
		void Load();

		void PreUpdate();
		void Update(TimeSpan delta);

		void Render();
	}
}
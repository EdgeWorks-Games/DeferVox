using System;

namespace DeferVox
{
	public interface IEntity : IDisposable
	{
		void Update(TimeSpan delta);
	}
}
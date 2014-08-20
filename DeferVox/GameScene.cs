using System;
using System.Collections.ObjectModel;

namespace DeferVox
{
	public sealed class GameScene : IDisposable
	{
		public GameScene()
		{
			Entities = new Collection<IEntity>();
		}

		public Collection<IEntity> Entities { get; private set; }

		public void Dispose()
		{
			foreach(var entity in Entities)
				entity.Dispose();
		}

		public void Update(TimeSpan delta)
		{
			foreach (var entity in Entities)
				entity.Update(delta);
		}
	}
}
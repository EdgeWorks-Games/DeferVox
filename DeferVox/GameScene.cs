using System;
using System.Collections.Generic;

namespace DeferVox
{
	public sealed class GameScene : IDisposable
	{
		public GameScene()
		{
			Entities = new List<IEntity>();
		}

		public List<IEntity> Entities { get; set; }

		public void Dispose()
		{
			Entities.ForEach(e => e.Dispose());
		}

		internal void Update(TimeSpan delta)
		{
			Entities.ForEach(e => e.Update(delta));
		}
	}
}
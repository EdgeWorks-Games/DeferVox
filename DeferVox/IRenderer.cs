﻿using System;
using DeferVox.Entities;
using OpenTK;

namespace DeferVox
{
	public interface IRenderer : IDisposable
	{
		void Render(GameScene scene);
		void RenderStreamedMesh(Vector3 position, Vector3 rotation, PositionColorVertex[] testBufferData);
	}
}
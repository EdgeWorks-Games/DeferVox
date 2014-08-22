using System;
using System.Drawing;
using System.Linq;
using DeferVox.Scenes;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering.Deferred
{
	// TODO: Not actually deferred yet

	public sealed class DeferredSceneRenderer : ISceneRenderer, IDisposable
	{
		private readonly DeferredRenderer _renderer = new DeferredRenderer();

		public void Dispose()
		{
			_renderer.Dispose();
		}

		public void RenderScene(GameScene scene)
		{
			// Set up OpenGL settings
			GL.Disable(EnableCap.Blend);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);
			// For some reason, it's culling the exact opposite faces so I flipped it, the correct order IS counter-clockwise
			GL.FrontFace(FrontFaceDirection.Cw);

			// Clear the default buffer
			GL.ClearColor(Color.CornflowerBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			foreach (var camera in scene.Cameras)
			{
				GL.Clear(ClearBufferMask.DepthBufferBit);
				GL.Viewport(
					camera.ScreenPosition.X, camera.ScreenPosition.Y,
					camera.Resolution.Width, camera.Resolution.Height);

				var view =
					Matrix4.CreateTranslation(-camera.Position) *
					Matrix4.CreateRotationY(-camera.Rotation.Y) *
					Matrix4.CreateRotationX(-camera.Rotation.X)*
					Matrix4.CreateRotationZ(-camera.Rotation.Z);
				var projection = Matrix4.CreatePerspectiveFieldOfView(camera.VerticalFieldOfView, camera.Ratio, 0.1f, 100f);
				_renderer.PvMatrix = view*projection;

				// Render the currently active scene
				// If this becomes a performance problem, cache the renderable entities
				foreach (var entity in scene.Entities.OfType<IRenderableEntity>())
				{
					entity.Render(_renderer);
				}
			}
		}
	}
}
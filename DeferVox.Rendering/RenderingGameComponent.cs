using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DeferVox.ObjectComponents;
using DeferVox.Window;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DeferVox.Rendering
{
	public class RenderingGameComponent : IGameComponent, IDisposable
	{
		private readonly DeferredRenderer _renderer;
		private readonly WindowGameComponent _window;

		public RenderingGameComponent(GameEngine engine, WindowGameComponent window)
		{
			_window = window;

			// Post update is called after we're done with updating the world state.
			// Changing the world state in post update means undefined behavior,
			// so we can safely render the world in it.
			engine.PostUpdate += EngineOnPostUpdate;

			_renderer = new DeferredRenderer();
		}

		public void Dispose()
		{
			_renderer.Dispose();
			_window.Dispose();
		}

		private void EngineOnPostUpdate(object sender, UpdateEventArgs e)
		{
			// If the window isn't visible we don't need to do anything
			if (!_window.Visible)
				return;

			_window.MakeCurrent();

			// Set up OpenGL settings
			// TODO: Abstract this better with a settings API, also accessable to options menus
			GL.Disable(EnableCap.Blend);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);
			// For some reason, it's culling the exact opposite faces so I flipped it, the correct order IS counter-clockwise
			GL.FrontFace(FrontFaceDirection.Cw);

			// Clear the Color buffer to prepare it for rendering
			GL.ClearColor(Color.CornflowerBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			// Flatten the scene tree into lists of objects
			// TODO: Actually do that. Right now we're just looking for cameras and meshes.
			// We need to support rendering of other stuff in an extensible way.
			var cameraPairs = e.Scene.TempGetComponent<CameraObjectComponent>();
			var meshPairs = e.Scene.TempGetComponent<TempMeshObjectComponent>().ToList();

			foreach (var cameraPair in cameraPairs)
			{
				var camera = cameraPair.Component;

				GL.Clear(ClearBufferMask.DepthBufferBit);
				GL.Viewport(
					camera.ScreenPosition.X, camera.ScreenPosition.Y,
					camera.Resolution.Width, camera.Resolution.Height);

				// Set up the base ProjectionView that scene notes will use as base
				var view = cameraPair.Matrix.Inverted();
				var projection = Matrix4.CreatePerspectiveFieldOfView(camera.VerticalFieldOfView, camera.Ratio, 0.1f, 100f);
				_renderer.PvMatrix = view*projection;

				foreach (var mesh in meshPairs)
				{
					_renderer.RenderMesh(mesh.Object.Position, mesh.Object.Rotation, mesh.Component.TempMesh);
				}
			}

			_window.SwapBuffers();
		}
	}
}
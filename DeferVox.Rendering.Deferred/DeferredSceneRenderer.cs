using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace DeferVox.Rendering.Deferred
{
	public sealed class DeferredSceneRenderer : ISceneRenderer
	{
		private readonly Matrix4 _projection;
		private readonly DeferredRenderer _renderer = new DeferredRenderer();
		private readonly Size _resolution;
		private float _rot;

		public DeferredSceneRenderer(Size resolution)
		{
			_resolution = resolution;
			_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70), 1280f/720f, 0.1f, 100f);
		}

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

			GL.Viewport(0, 0, _resolution.Width, _resolution.Height);

			// Clear the default buffer
			GL.ClearColor(Color.CornflowerBlue);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			// Multiply the projection and view in advance
			_rot += 0.01f;
			var view =
				Matrix4.CreateRotationY(_rot)*
				Matrix4.CreateTranslation(0, -5, -5)*
				Matrix4.CreateRotationX(MathHelper.DegreesToRadians(40));
			_renderer.PvMatrix = view*_projection;

			// Render the currently active scene
			// If this becomes a performance problem, cache the renderable entities
			foreach (var entity in scene.Entities.OfType<IRenderableEntity>())
			{
				entity.Render(_renderer);
			}
		}
	}
}
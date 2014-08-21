using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using GLPixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace DeferVox.Rendering
{
	public sealed class Texture2D : IDisposable
	{
		private readonly int _texture;

		public Texture2D(string path)
		{
			using (var bitmap = new Bitmap(path))
			{

				// Save some metadata
				Width = bitmap.Width;
				Height = bitmap.Height;

				// Load the data from the bitmap
				var textureData = bitmap.LockBits(
					new Rectangle(0, 0, bitmap.Width, bitmap.Height),
					ImageLockMode.ReadOnly,
					PixelFormat.Format32bppArgb);

				// Generate and bind a new OpenGL texture
				_texture = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, _texture);

				// Configure the texture
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMinFilter, (int) (TextureMinFilter.Nearest));
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMagFilter, (int) (TextureMinFilter.Nearest));

				// Load the texture
				GL.TexImage2D(
					TextureTarget.Texture2D,
					0, // level
					PixelInternalFormat.Rgba,
					bitmap.Width, bitmap.Height,
					0, // border
					GLPixelFormat.Bgra,
					PixelType.UnsignedByte,
					textureData.Scan0);

				// Free the data since we won't need it anymore
				bitmap.UnlockBits(textureData);
				GL.BindTexture(TextureTarget.Texture2D, 0);
			}
		}

		public int Width { get; private set; }
		public int Height { get; private set; }

		public Size Size
		{
			get { return new Size(Width, Height); }
		}

		/// <summary>
		///     Deletes the texture.
		/// </summary>
		public void Dispose()
		{
			GL.DeleteTexture(_texture);
			GC.SuppressFinalize(this);
		}

		~Texture2D()
		{
			Trace.TraceWarning("[RESOURCE LEAK] Texture finalizer invoked!");
			Dispose();
		}

		public void Bind()
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, _texture);
		}

		public static void ClearBind()
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
	}
}
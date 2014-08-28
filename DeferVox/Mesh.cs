namespace DeferVox
{
	public class Mesh
	{
		public Mesh(TexturedVertex[] vertices)
		{
			Vertices = vertices;
		}

		public TexturedVertex[] Vertices { get; private set; }
	}
}
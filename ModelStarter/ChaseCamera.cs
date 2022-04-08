using System;

namespace ModelStarter
{
	public class ChaseCamera : ICamera
	{
		Game game;
		Matrix projection;
		Matrix view;

		public IFollowable Target { get; set; }

		public Vector3 Offset { get; set; }

		public Matrix View => view;

		public Matrix Projection => projection;

		public ChaseCamera(Game game, Vector3 offset)
		{
			this.game = game;
			this.Offset = offset;
			this.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
				game.GraphicsDevice.Viewport.AspectRatio, 1, 1000);
			this.view = Matrix.CreateLookAt(
				Vector3.Zero,
				offset,
				Vector3.Up
				);
		}

		public void Update(GameTime gameTime)
        {
			if (Target == null) return;

			var position = Target.Position + Vector3.Transform(Offset,
				Matrix.CreateRotationY(Target.Facing));
			this.view = Matrix.CreateLookAt(
				position,
				Target.Position,
				Vector3.Up);
        }
	}
}

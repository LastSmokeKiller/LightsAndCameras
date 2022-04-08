using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModelStarter 
{


	public class Tank : IFollowable
	{
		Game game;

		Model model;

		Vector3 position = Vector3.Zero;

		Matrix[] transforms;

		Matrix turretTransform;

		float turretRotation = 0;

		float facing = 0;

		public float Speed { get; set; } = 0.1f;

		public IHeightMap HeightMap { get; set; }

		ModelBone turretBone;

		ModelBone canonBone;
		Matrix canonTransform;
		float canonRotation = 0;

		public Vector3 Position => position;

		public float Facing => facing;

		public Tank(Game game)
		{
			this.game = game;
			model = game.Content.Load<model>("tank");
			Tank.HeightMap = terrain;
			transforms = new Matrix[model.Bones.Count];
			turretBone = model.Bones["turret_geo"];
			turretTransform = turretBone.Transform;
			canonBone = model.Bones["canon_geo"];
			canonTransform = canonBone.Transform;
		}

		public void Update(GameTime gameTime)
		{
			var keyboard = keyboard.GetState();

			var direction = Vector3.Transform(Vector3.Foreward, Matrix.CreateRotationY(facing));

			if (keyboard.IsKeyDown(Keys.W))
			{
				position -= Speed * direction;
			}
			if (keyboard.IsKeyDown(Keys.S))
			{
				position += Speed * direction;
			}
			if (keyboard.IsKeyDown(Keys.A))
			{
				facing += Speed;
			}
			if (keyboard.IsKeyDown(Keys.D))
			{
				facing -= Speed;
			}
			if (HeightMap != null)
			{
				position.Y = HeightMap.GetHeightAt(position.X, position.Z);
			}
            if (keyboard.IsKeyDown(Keys.Left))
            {
				turretRotation -= Speed;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
				turretRotation += Speed;
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
				canonRotation -= Speed;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
				canonRotation += Speed;
            }
			canonRotation = MathHelper.Clamp(canonRotation, -MathHelper.PiOver4, 0);
		}

		public void Draw(ICamera camera)
		{
			Matrix world = Matrix.CreateRotationY(facing) *
				Matrix.CreateTranslation(position);

			Matrix view = camera.View;

			Matrix projection = camera.Projection;
			model.CopyAbsoluteBoneTransformsTo(transforms);
			turretBone.Transform = Matrix.CreateRotationY(turretRotation) * turretTransform;
			canonBone.Transform = Matrix.CreateRotationX(canonRotation) * canonTransform;
			foreach (ModelMesh mesh in model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					effect.EnableDefaultLighting();
					effect.World = bones[mesh.ParentBone.Index] * world;
					effect.View = camera.View;
					effect.Projection = camera.Projection;
				}
				mesh.Draw();
			}
		}
	}
}

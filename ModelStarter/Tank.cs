using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Tank
{
	Game game;

	Model model;

	Vector3 position = Vector3.Zero;

	float facing = 0;

	public float Speed { get; set; } = 0.1f;

	public IHeightMap HeightMap { get; set; }

	public Tank(Game game)
	{
		this.game = game;
		model = game.Content.Load<model>("tank");
		Tank.HeightMap = terrain;
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
		if(HeightMap != null)
        {
			position.Y = HeightMap.GetHeightAt(position.X, position.Z);
        }
    }

	public void Draw(ICamera camera)
    {
		Matrix world = Matrix.CreateRotationY(facing) *
			Matrix.CreateTranslation(position);

		Matrix view = camera.View;

		Matrix projection = camera.Projection;

		model.Draw(world, view, projection);
    }
}

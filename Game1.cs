using System.Reflection.Metadata;
using C_GameProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace C_GameProject;

//Note: Game1 contains the core, use game1 as a conductor, where things are called, but does not contain actual game logic (design principles)!

//: is used for inheritance in c sharp, for both implements and extends!
public class Game1 : Game
{
    
    //These members are used for drawing to the screen
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private float ballSpeed;
    private Texture2D ballTexture;
    private Vector2 ballPosition;
    private Player player;

    private Texture2D spaceTexture;

   



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        //BackBufferWidth is width of screen and backBufferHeight is height of screen, obtained from GraphicsDevice (resolution the game is running at) 
        //_graphics.ApplyChanges();
        //player = 

        //ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        //ballSpeed = 100f;
        player = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 100f, "character");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        ballTexture = Content.Load<Texture2D>("character");
        

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        //This multiplies by the time of an update call (changes in framerates)
        //Note: Experiment with what happens if omitting this multiplication!
        float updatedBallSpeed = ballSpeed* (float)gameTime.ElapsedGameTime.TotalSeconds;


        var kstate = Keyboard.GetState();

        //Is the uparrow key being pressed?
        if (kstate.IsKeyDown(Keys.Up))
        {
            ballPosition.Y -= updatedBallSpeed;

        }
        if (kstate.IsKeyDown(Keys.Down))
        {
            ballPosition.Y += updatedBallSpeed;
        }
        
        if (kstate.IsKeyDown(Keys.Left))
        {
            ballPosition.X -= updatedBallSpeed;
        }
        
        if (kstate.IsKeyDown(Keys.Right))
        {
            ballPosition.X += updatedBallSpeed;
        }

        //Do not let the ball go out of the bounds of the screen
        if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
        {
            ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
        }
        else if (ballPosition.X < ballTexture.Width / 2)
        {
            ballPosition.X = ballTexture.Width / 2;
        }

        if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
        {
            ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
        }
        else if (ballPosition.Y < ballTexture.Height / 2)
        {
            ballPosition.Y = ballTexture.Height / 2;
        }



        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        //Sprite, location, color are the draw params
        //_spriteBatch.Draw(ballTexture, new Vector2(0, 0),  Color.White);
        //_spriteBatch.Draw(ballTexture, ballPosition, Color.White);
        _spriteBatch.Draw(
        ballTexture,
        ballPosition,
        null,
        Color.White,
        0f,
        new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
        Vector2.One,
        SpriteEffects.None,
        0f
        );
        
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}

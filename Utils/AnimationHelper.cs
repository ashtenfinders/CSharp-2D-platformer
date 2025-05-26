namespace C_GameProject.Utils;

using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using C_GameProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//Helper class for handling animated textures

public class AnimationHelper
{

    private int frameCount;
    private Texture2D spriteSheet;

    //Frames to draw per second 
    private float timePerFrame;
    //The current frame being drawn 
    private int frame;

    //Total amount of tume the animation has been running
    private float totalElapsed;
    //Is the animation currently running?
    private bool isPaused;
    //The cyrrent rotation, scale and draw depth for the animation
    public float Rotation, Scale, Depth;

    //The origin point of the animated texture
    public Vector2 Origin;



    public AnimationHelper(Vector2 origin, float rotation, float scale, float depth)
    {
        this.Origin = origin;
        this.Rotation = rotation;
        this.Scale = scale;
        this.Depth = depth;
    }

    public void Load(ContentManager content, string asset, int frameCount, int framesPerSec)
    {
        this.frameCount = frameCount;
        spriteSheet = content.Load<Texture2D>(asset);
        timePerFrame = (float)1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;
        isPaused = false;
    }
    public void UpdateFrame(float elapsed)
    {
        if (isPaused)
        {
            return;
        }
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame)
        {
            frame++;
            //Keep the Frame between 0 and the total frames, minus one. 
            frame %= frameCount;
            totalElapsed -= timePerFrame;

        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
    {
        DrawFrame(batch, frame, screenPos);
    }
    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
    {
        int FrameWidth = spriteSheet.Width / frameCount;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0, FrameWidth, spriteSheet.Height);
        batch.Draw(spriteSheet, screenPos, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void Reset()
    {
        frame = 0;
        totalElapsed = 0f;
    }
    public void Stop()
    {
        Pause();
        Reset();
    }
    public void Play()
    {
        isPaused = false;
    }
    public void Pause()
    {
        isPaused = true;
    }
    



}
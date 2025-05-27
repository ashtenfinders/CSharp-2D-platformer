namespace C_GameProject.Utils;

using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using C_GameProject.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//Helper class for handling animated textures

public class AnimatedTexture
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
    //The current rotation, draw depth for the animation
    public float Rotation, Depth;


    //The origin point of the animated texture
    public Vector2 Origin, Scale;




    public AnimatedTexture(Vector2 origin, float rotation, Vector2 scale, float depth)
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
    //This is for spritesheets where there are multiple animations, in a grid 
    public void MergedSheetDrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
    {
        int row = 0;
        int FrameWidth = spriteSheet.Width / frameCount;
        int FrameHeight = spriteSheet.Height / 4;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, row* FrameHeight, FrameWidth, FrameHeight);
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
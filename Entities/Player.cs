using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace C_GameProject.Entities;

public class Player
{
    //This will be a sprite texture assigned to the player!
    private string textureName;
    private Vector2 playerPosition;
    private float playerSpeed;




    public Player(Vector2 initialPosition, float playerSpeed, string textureName)
    {
        playerPosition = initialPosition;
        this.playerSpeed = playerSpeed;


    }


    
    




}
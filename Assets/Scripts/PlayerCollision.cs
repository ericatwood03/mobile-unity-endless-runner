using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Variables
    public GameManager gameEnd;

    //Checks if an obstacle collides with the Star, if so ends the game and deactivates this game object
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle")){
            gameEnd.GameOver();
            gameObject.SetActive(false);
        }
    }
}

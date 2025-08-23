using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public GameOverHUD OverHUD;
    public Score fscore;
    public SpawnAndChase obstacles;
    
    // Stops and deactivates score and obstacle spawn. Setups GameOver screen. And Saves fragment data 
    public void GameOver(){
        fscore.stopScore();
        SaveAndLoad.SAL.SaveData();
        OverHUD.Setup(fscore.getScore());
        obstacles.Clear();
        Destroy(this.gameObject.GetComponent<PowerUpManager>());
        Destroy(gameObject);
    }
}

using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    //Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    private double timeSurvived = 0;
    private bool gameEnded = false;
    private static bool timePause = false;

    //Updates timeSurvived and then updates the scoreText to timeSurvived
    void Update()
    {
        if(!gameEnded && !timePause){
            timeSurvived += Time.deltaTime;
            scoreText.text = timeSurvived.ToString("0.00") + "s";
        }
    }

    public double getScore(){
        return timeSurvived;
    }

    //Stops score counting and deactivates the game object
    public void stopScore(){
        gameEnded = true;
        gameObject.SetActive(false);
    }

    //Pauses timeSurvived from updating
    public void StopTime(bool pause){
        timePause = pause;
    }
}

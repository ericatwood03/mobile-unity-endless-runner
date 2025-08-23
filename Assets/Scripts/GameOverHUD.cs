using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHUD : MonoBehaviour
{

    //Variables
    public TextMeshProUGUI finalscoreText;
    public TextMeshProUGUI fragmentText;
    //Activates the GameOverHUD and sets the score text to the parameter taken in.
    public void Setup(double score)
    {
        gameObject.SetActive(true);
        finalscoreText.text = score.ToString("0.00") + "s Survived";
        fragmentText.text = FragmentManager.fManager.getFragments().ToString("0");
    }

    //Reloads the game scene
    public void PlayAgain(){
        AudioManager.audioManager.PlaySFX();
        SceneManager.LoadScene("PlayScene");
    }
    
    //Loads Menu scene
    public void ToMenu(){
        AudioManager.audioManager.PlaySFX();
        AudioManager.audioManager.MenuMusic();
        SceneManager.LoadScene("Menu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{

    //Variables
    public GameObject instructions;
    public TextMeshProUGUI fragmentText;

    //Updates Fragment UI to current amount
    public void Start()
    {
        fragmentText.text = FragmentManager.fManager.getFragments().ToString("0");
    }

    //Loads the game scene
    public void Play()
    {
        AudioManager.audioManager.PlaySFX();
        AudioManager.audioManager.GameMusic();
        SceneManager.LoadScene("PlayScene");

    }

    //Activates the Instructions text canvas
    public void Instruct(){
        AudioManager.audioManager.PlaySFX();
        instructions.SetActive(true);
    }

    //Deactivates the Instructions canvas
    public void Back(){
        AudioManager.audioManager.PlaySFX();
        instructions.SetActive(false);
    }

    //Loads the Shop Scene
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    
    //Exits the game
    public void Quit()
    {
        AudioManager.audioManager.PlaySFX();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}

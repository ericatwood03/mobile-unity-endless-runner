using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Variables
    public static AudioManager audioManager { get; private set; } //Set it as a Singleton
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip buttonClick;
    public Sprite unmute;
    public Sprite mute;
    private Sprite sprite;
    private bool muted = false;

    //Initializes script if it doesn't exist, destroys itself if one does exist
    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    //As soon as game is started Menu music plays if it isnt muted
    private void Start()
    {    
        musicSource.clip = menuMusic;
        if(!muted)
            musicSource.Play();
    }

    //Checks to see if on the menu screen then if muted changes the music button sprite
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && muted)
        {
            GameObject.Find("/Buttons/Music Button").GetComponent<Image>().sprite = mute;
        }
    }

    //Stops the music then changes it to Game music before playing again
    public void GameMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameMusic;
        if (!muted)
            musicSource.Play();
    }

    //Stops the music then changes it to Menu music before playing again
    public void MenuMusic()
    {
        musicSource.Stop();
        musicSource.clip = menuMusic;
        if(!muted)
            musicSource.Play();
    }

    //Plays the buttonClick sound once
    public void PlaySFX()
    {
        SFXSource.PlayOneShot(buttonClick);
    }

    //Toggles music playability on and off and changes button sprite
    public void toggleMusic()
    {
        if (!muted)
        {
            muted = true;
            musicSource.Stop();
            GameObject.Find("/Buttons/Music Button").GetComponent<Image>().sprite = mute;
        }
        else
        {
            muted = false;
            musicSource.Play();
            GameObject.Find("/Buttons/Music Button").GetComponent<Image>().sprite = unmute;
        }
    }

}

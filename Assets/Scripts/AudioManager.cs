using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Variables
    public static AudioManager audioManager { get; private set; } //Set it as a Singleton

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip buttonClick;

    [Header("Sprites")]
    public Sprite unmute;
    public Sprite mute;

    private bool muted = false;
    private Image musicButtonImage;

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

    //Updates Active Scene every OnEnable call
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    //Checks to see if on the menu screen then sets the music button image 
    //if muted changes the music button sprite
    private void OnActiveSceneChanged(Scene oldscene, Scene newScene)
    {
        if (newScene.name == "Menu")
        {
            musicButtonImage = GameObject.Find("/Buttons/Music Button").GetComponent<Image>();
            if(musicButtonImage != null && muted)
                musicButtonImage.sprite = mute;
        }
    }

    //As soon as game is started Menu music plays if it isnt muted
    private void Start()
    {
        musicSource.clip = menuMusic;
        if (!muted)
            musicSource.Play();
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
            musicButtonImage.sprite = mute;
        }
        else
        {
            muted = false;
            musicSource.Play();
            musicButtonImage.sprite = unmute;
        }
    }

}

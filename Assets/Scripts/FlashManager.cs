using UnityEngine;

public class FlashManager : MonoBehaviour
{
    //Variables
    public static FlashManager Manager { get; private set; } //Set it as a Singleton

    [ColorUsage(true, true)] //Allow for intensity change
    [SerializeField] private Color _flashColor = Color.red;
    [SerializeField] private float _flashTime = 0.25f;
    [SerializeField] private AnimationCurve _flashSpeedCurve;
    [SerializeField] private GameObject particeObj;
    [SerializeField] private ParticleSystem explosion = default;
    
    //Initializes script if it doesn't exist, destroys itself if one does exist
    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
    public Color getColor(){
        return _flashColor;
    }
    public float getTime(){
        return _flashTime;
    }
    public AnimationCurve getCurve(){
        return _flashSpeedCurve;
    }
    //Takes a Vector3 parameter position, and then uses that to play the explosion particle at the Vector3 location given
    public void Explode(Vector3 position){
        particeObj.transform.position = position;
        explosion.Play();
    }
}

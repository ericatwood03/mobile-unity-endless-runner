using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    //Variables
    public float speed;
    [SerializeField] private Renderer bgRenderer;
    private float x, y;
    private float timeCheck;
    private float elapseTime;
    private float spdIncr = 0.01f;
    private int interval = 5;
    private bool scrollPause = false;
    
    //Initially sets x & y then sets timeCheck to 0
    void Start()
    {
        x = (float)-0.02;
        y = (float)-0.02;
        timeCheck = 0;
    }
    
    //Constantly updates elapseTime and checks if the elapsedTime changed by interval 
    //If so the  negative x & y speed is increased by spdIncr
    void Update()
    {
        if(!scrollPause){
            elapseTime += Time.deltaTime;
            if (elapseTime - timeCheck >= interval)
            {
                x = x - spdIncr;
                y = y - spdIncr;
                timeCheck = elapseTime;
            }
            bgRenderer.material.mainTextureOffset+= new Vector2(x,y) * Time.deltaTime; 
            // does not move the object. It moves how the texture is mapped onto the object
        }
    }

    // Pauses/Unpauses the scrolling and time updating
    public void stopScroll(bool Scroll){
        scrollPause = Scroll;
    }
}

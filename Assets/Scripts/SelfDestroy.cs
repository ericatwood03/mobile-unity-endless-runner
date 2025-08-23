using UnityEditor;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    //Scripts
    public SpawnAndChase reference;
    public HitFlash hitFlash; 


    //Variables
    private int hp;
        
    //Calls SetHP() first
    void Start()
    {
        SetHP();
    }

    // Constantly checks for touch input then checks if then runs checkCollide()
    void Update()
    {
        foreach(Touch touch in Input.touches) //Array with info about all current touches   
        {
            
            //Checks if a touch starterd
            if (touch.phase == TouchPhase.Began)
            {
                checkCollide();
            }
        }
    }

    //Sets hp based on what the obstacle is
    private void SetHP(){
        if(this.gameObject.name.Contains("AsteroidM") || this.gameObject.name.Contains("AsteroidS") || this.gameObject.name.Contains("AsteroidXS") ){
            hp = 1;
        }
        else if(this.gameObject.name.Contains("AsteroidL")){
            hp = 2;
        }
        else if(this.gameObject.name.Contains("Moon_1") || this.gameObject.name.Contains("Moon_2") || this.gameObject.name.Contains("Moon_3") || this.gameObject.name.Contains("OddMoon")){
            hp = 3;
        }
        else if(this.gameObject.name.Contains("Planet_1") || this.gameObject.name.Contains("Planet_2")){
            hp = 4;
        }
        else if(this.gameObject.name.Contains("Planet_3") || this.gameObject.name.Contains("Planet_4")){
            hp = 5;
        }
        else if(this.gameObject.name.Contains("Planet_5") || this.gameObject.name.Contains("Planet_6") || 
        this.gameObject.name.Contains("Planet_7") || this.gameObject.name.Contains("Planet_8") || this.gameObject.name.Contains("Planet_9")){
            hp = 6;
        }
        else if(this.gameObject.name.Contains("Planet_10") || this.gameObject.name.Contains("Planet_11")){
            hp = 8;
        }
        else{
            hp = 10;;
        }
    }
    //Removes a hitpoint from the obstacle and calls the HitFlash script until it reaches 0, which then destroys the obstacle
    public void Hit(){
        FragmentManager.fManager.addFragments(1);
        hp -= TapDamage.Damager.getDmg();
        if(hp <= 0){
            FlashManager.Manager.Explode(this.gameObject.transform.position); // Calls a Particle Explosion
            Destroy(this.gameObject); //Destroys object
        }
        else{
            Flash();
        }
    }

    //Adds a new HitFlash component and setups its parameter with the obstacle's sprite renderer. Then calls CallHitFlash
    private void Flash(){
        hitFlash = this.gameObject.AddComponent<HitFlash>();
        hitFlash.Setup(this.gameObject.GetComponent<SpriteRenderer>());
        hitFlash.CallHitFlash();
    }
    
    //Creates a raycast and then checks if the raycast collider and this obstacle's collider interact if getIgnore() is false
    //Else just runs Hit() if getPause() returns false
    private void checkCollide(){
        if(!reference.getIgnore()){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            if(hit.collider != null && hit.collider == this.GetComponent<Collider2D>()){
                Hit();
            }
        }
        else if(reference.getIgnore() && !reference.getPause()){
            Hit();
        }
    }
}


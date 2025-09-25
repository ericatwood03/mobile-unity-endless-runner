using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    //Scripts
    private SpawnAndChase reference;
    private HitFlash hitFlash; 

    //Variables
    private int hp;

    //Sets reference script
    public void SetScripts(SpawnAndChase sacScript)
    {
        reference = sacScript;
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
    public void SetHP(int startHP)
    {
        hp = startHP;
    }

    //Removes a hitpoint from the obstacle and calls the HitFlash script until it reaches 0, which then destroys the obstacle
    public void Hit()
    {
        FragmentManager.fManager.addFragments(1);
        hp -= TapDamage.Damager.getDmg();
        if (hp <= 0)
        {
            FlashManager.Manager.Explode(this.gameObject.transform.position); // Calls a Particle Explosion
            if(hitFlash != null)
                hitFlash.Revert();
            reference.CallRelease(this.gameObject); 
        }
        else
        {
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


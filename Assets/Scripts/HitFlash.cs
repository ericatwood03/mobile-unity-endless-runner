using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [ColorUsage(true, true)] //Allows for intensity to be adjusted

    //Variables
    private Color _flashColor = Color.red;
    private float _flashTime = 0.25f;
    private AnimationCurve _flashSpeedCurve;
    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private Coroutine _hitFlashCoroutine; //Coroutine to run flash

    //Called before using CallHitFlash to set the sprite renderer and then call Init()
    public void Setup(SpriteRenderer spriteRenderer){
        _spriteRenderer = spriteRenderer;
        Init();
    }

    //Initializes the material, flash color, flash time, and the flash animation curve. 
    // The latter 3 are retrieved from the Flash Manager script
    private void Init()
    {
        _material = _spriteRenderer.material;
        _flashColor = FlashManager.Manager.getColor();
        _flashTime = FlashManager.Manager.getTime();
        _flashSpeedCurve = FlashManager.Manager.getCurve();
    }

    //Starts a coroutine on HitFlasher
    public void CallHitFlash(){
        _hitFlashCoroutine = StartCoroutine(HitFlasher());
    }
    
    //Sets flash until the flash time is over
    private IEnumerator HitFlasher(){
        SetFlashColor();
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < _flashTime){
            elapsedTime += Time.deltaTime;

            //Transitions from 0.8 to the flashSpeedCurve evaluation in (elapsedTime/flashTime) amount of time
            //The animation curve returns the curve at the elapsedTime
            //Meaning that while flashTime > elapsedTime, the flash amount will constantlystart at the updating curve 
            currentFlashAmount = Mathf.Lerp(0.8f, _flashSpeedCurve.Evaluate(elapsedTime), elapsedTime/_flashTime); 
            
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }

    //Sets flash color on the shader
    private void SetFlashColor(){
        _material .SetColor("_FlashColor", _flashColor);    
    }

    //Sets the flash amount on the shader
    private void SetFlashAmount(float amount){
        _material.SetFloat("_FlashAmount", amount);
    }
}

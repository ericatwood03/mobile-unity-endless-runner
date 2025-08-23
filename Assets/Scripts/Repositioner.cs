using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repositioner : MonoBehaviour
{
    private Vector3 screenPosition;
    private float edgeDistanceX = 120.5f;

    //Sets Screen width and height then repositions star based on the screen dimensions
    void Awake()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        screenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        if(this.gameObject != null){
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth - edgeDistanceX, screenPosition.y, screenPosition.z));
            screenPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        }
    }
}

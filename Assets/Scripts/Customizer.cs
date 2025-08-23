using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour
{
    //Variables
    public Sprite modified;
    public ParticleSystem starTrail;
    private Color32 color;

    //Checks if gold star is in use, if not calls CustomizeStar()
    void Start()
    {
        color = CustomizationManager.CM.getColor();
        if (CustomizationManager.CM.getSelected() != "Gold")
        {
            CustomizeStar();
            CustomizeTrail();
        }
    }

    //Changes star sprite to the modifiable version and sets the color to the selected color
    private void CustomizeStar()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = modified;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    //Changes trail color to the selected color
    private void CustomizeTrail()
    {
        var colorOverTime = starTrail.colorOverLifetime;
        if (colorOverTime.enabled)
        {
            Gradient gradient = new Gradient();

            // Set color keys keeping color the same throughout
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0].color = color;
            colorKeys[0].time = 0.0f;
            colorKeys[1].color = color;
            colorKeys[1].time = 1.0f;

            // Sets alpha keys from opaque to transparent
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0].alpha = 1.0f; // Fully opaque at start
            alphaKeys[0].time = 0.0f;
            alphaKeys[1].alpha = 0.0f; // Fully transparent at end
            alphaKeys[1].time = 1.0f;

            gradient.SetKeys(colorKeys, alphaKeys);
            colorOverTime.color = new ParticleSystem.MinMaxGradient(gradient);
        }
    }
}

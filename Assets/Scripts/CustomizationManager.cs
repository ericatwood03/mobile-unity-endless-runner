using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    //Variables
    private string selected = "Gold";
    private Color32 currentColor;
    public static CustomizationManager CM { get; private set; } //Set it as a Singleton

    //Initializes script if it doesn't exist, destroys itself if one does exist
    void Awake()
    {
        if (CM == null)
        {
            CM = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    //Calls Deselect on previously selected star and then sets values to reflect newly selected star
    public void Customize(string star, Color32 color)
    {
        GameObject tempStar = GameObject.Find("/Shop UI/Panel/Content/Color Purchases/" + selected);
        tempStar.GetComponent<Selector>().Deselect();
        selected = star;
        currentColor = color;
    }

    //Presets the selected star and color values when loading saved data
    public void Preset(string star, Color32 color)
    {
        selected = star;
        currentColor = color;
    }
    public string getSelected()
    {
        return selected;
    }

    public Color32 getColor()
    {
        return currentColor;
    }
    
}

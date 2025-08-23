using System;
using System.IO;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{
    //Variables
    public static FragmentManager fManager { get; private set; } //Set it as a Singleton
    private int fragments = 0;

    //Initializes script if it doesn't exist, destroys itself if one does exist
    private void Awake()
    {
        if (fManager == null)
        {
            fManager = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    public void addFragments(int amount)
    {
        fragments += amount;
    }

    public int getFragments()
    {
        return fragments;
    }

    public void useFragments(int amount)
    {
        fragments -= amount;
    }

    public void setFragments(int amount)
    {
        fragments = amount;
    }
}
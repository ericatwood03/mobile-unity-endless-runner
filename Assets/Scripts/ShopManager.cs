using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager sManager { get; private set; } //Set it as a Singleton
    public TextMeshProUGUI fragmentText;
    private List<string> unlockeds = new List<string>();

    //Initializes script if it doesn't exist, destroys itself if one does exist
    //Loads previous customization data and allows saving list data
    private void Awake()
    {
        if (sManager == null)
        {
            sManager = this;
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
        SaveAndLoad.SAL.LoadLater();
        SaveAndLoad.SAL.SaveList(true);
    }

    //Sets player fragment count text
    void Start()
    {
        AmountCheck();
    }

    //Resets player fragment count text
    public void AmountCheck()
    {
        fragmentText.text = FragmentManager.fManager.getFragments().ToString("0");
    }

    //Saves data again, disallows attempting to save from unlockeds, sends a temp last save to the
    //SaveAndLoad script then returns to Menu scene.
    public void ToMenu()
    {
        SaveAndLoad.SAL.SaveData();
        SaveAndLoad.SAL.SaveList(false);
        SaveAndLoad.SAL.TempSave(unlockeds);
        SceneManager.LoadScene("Menu");
    }

    //Adds the star name given to the list
    public void AddToList(string star)
    {
        unlockeds.Add(star);
    }

    //Takes a string and checks the list for said string and returns true or false based on it.
    public bool CheckList(string star)
    {
        for (int i = 0; i < unlockeds.Count; i++)
            if (unlockeds[i] == star)
            {
                return true;
            }
        return false;
    }
    
    public List<string> getList()
    {
        return unlockeds;
    }

    public void setList(List<string> stars)
    {
        unlockeds = stars;
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    public static ShopManager sManager { get; private set; } //Set it as a Singleton
    public TextMeshProUGUI fragmentText;
    public ScrollRect scrollArea;
    private HashSet<string> unlockeds = new HashSet<string>();

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

    //Sets player fragment count text and starts scroll area at the top
    void Start()
    {
        AmountCheck();
        if (scrollArea != null)
        {
            scrollArea.verticalNormalizedPosition = 1f;
        }
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
    public void AddToUnlockeds(string star)
    {
        unlockeds.Add(star);
    }

    //Takes a string and checks the list for said string and returns true or false based on it.
    public bool CheckList(string star)
    {
        return unlockeds.Contains(star);
    }

    public HashSet<string> getUnlockeds()
    {
        return unlockeds;
    }

    public void setUnlockeds(IEnumerable<string> stars)
    {
        unlockeds = new HashSet<string>(stars);
    }
}

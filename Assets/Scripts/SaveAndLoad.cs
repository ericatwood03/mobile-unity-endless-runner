using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    //Variables
    public static SaveAndLoad SAL { get; private set; } //Set it as a Singleton
    private bool saveList = false;
    private List<string> tempList = new List<string>();

    //Initializes script if it doesn't exist, destroys itself if one does exist
    //Loads previous player data
    void Start()
    {
        if (SAL == null)
        {
            SAL = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
        LoadData();
    }
    
    //Boolean that checks whether list can be saved or not based on if ShopManager exists 
    public void SaveList(bool toggle)
    {
        saveList = toggle;
    }

    //Saves the last remembered instance
    public void TempSave(List<string> temp)
    {
        tempList = temp;
    }

    //Sets model variables and then converts to JSON
    //Then writes bytes to a path that will save across perpetual runs.
    //If saveList is true saves list from ShopManager script 
    // else saves the last list before leaving the shop scene
    public void SaveData()
    {
        SaveDataModel model = new SaveDataModel();
        model.fragments = FragmentManager.fManager.getFragments();
        model.selected = CustomizationManager.CM.getSelected();
        model.color = CustomizationManager.CM.getColor();
        if (saveList)
            model.unlockeds = ShopManager.sManager.getList();
        else
        {
            model.unlockeds = tempList;
        }
        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        Debug.Log("Writing file to: " + Application.persistentDataPath);
    }

    //Retrieves info from the JSON file and sets info if the file exists
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            SaveDataModel model = JsonUtility.FromJson<SaveDataModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
            FragmentManager.fManager.setFragments(model.fragments);
            if (model.selected != null)
                CustomizationManager.CM.Preset(model.selected, model.color);
            if (model.unlockeds != null)
                tempList = model.unlockeds;
        }
    }

    //Retrieves info from the JSON file and sets info if the file exists when Shop Scene is entered
    public void LoadLater()
    {
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            SaveDataModel model = JsonUtility.FromJson<SaveDataModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
            if (model.unlockeds != null)
                ShopManager.sManager.setList(model.unlockeds);
        }
    }

    //Saves when quitting
    void OnApplicationQuit()
    {
        SaveData();
    }

    //Saves when app loses focus due to calls or tabbing out
    void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveData();    
    }

    //Saves when app is paused or put in the background
    void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }

    [Serializable]

    //Info I want to save across run instances is initialized here, 
    //marked as Serializable so Unity can convert it into a byte stream
    public class SaveDataModel
    {
        public int fragments;
        public List<string> unlockeds;
        public string selected;
        public Color32 color;
    }
}

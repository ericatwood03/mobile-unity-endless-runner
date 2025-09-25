using UnityEngine;
using System;
using System.Collections.Generic;

public class DictionarySerializer : MonoBehaviour
{
    [Header ("Obstacle Info Dictionary")]
    [SerializeField]
    NewDict newDict;
    public Dictionary<string, int> obstaclesInfo;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesInfo = newDict.ToDictionary();
    }

}

//Serializable class so that new dictionary items can be added in the inspector
[Serializable]

public class NewDict
{
    [SerializeField]
    NewDictItem[] dictItems;

    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> obstDict = new Dictionary<string, int>();
        foreach (var item in dictItems)
        {
            obstDict.Add(item.obstName, item.hp);
        }

        return obstDict;
    }
}

//Serializable variable so that the variables can be edited in the Inspector
[Serializable]
public class NewDictItem
{
    [SerializeField]
    public string obstName;

    [SerializeField]
    public int hp;
}

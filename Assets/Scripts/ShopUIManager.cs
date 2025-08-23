using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopUIManager : MonoBehaviour
{
    //Variables
    public Button Yes;
    public Button No;
    public TextMeshProUGUI Text;
    private UnityEngine.Events.UnityAction listener1;
    private UnityEngine.Events.UnityAction listener2;
    private Action<bool> sendBack;

    //Activates UI and buttons and then runs Option based on which was clicked
    public void Confirm(int cost, Action<bool> confirmation)
    {
        Text.text = "Would you like to spend " + cost.ToString("0") + " fragments?";
        sendBack = confirmation;
        this.gameObject.SetActive(true);
        if (listener1 != null) Yes.onClick.RemoveListener(listener1);
        if (listener2 != null) No.onClick.RemoveListener(listener2);
        listener1 = () => Option(true);
        listener2 = () => Option(false);
        Yes.onClick.AddListener(listener1);
        No.onClick.AddListener(listener2);
    }

    //Removes listeners and deactivates gameobject after invoking Purchase() in the Selector script
    public void Option(bool yon)
    {
        Yes.onClick.RemoveListener(listener1);
        No.onClick.RemoveListener(listener2);
        sendBack?.Invoke(yon);
        this.gameObject.SetActive(false);
    }
}

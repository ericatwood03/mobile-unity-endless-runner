using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    //Variables
    public Button Button1;
    public Button Button2;
    public TextMeshProUGUI Title1;
    public TextMeshProUGUI Title2;
    public TextMeshProUGUI Desc1;
    public TextMeshProUGUI Desc2;

    private UnityEngine.Events.UnityAction listener1;
    private UnityEngine.Events.UnityAction listener2;
    private PowerUp option1;
    private PowerUp option2;
    private Action<PowerUp> sendBack; // Allows for an easily reusable method
    
    //Sets the powerup options and sendback then shows the UI button choices for each powerUp using the 2 random powerUps passed in
    public void Show(PowerUp op1, PowerUp op2, Action<PowerUp> callback){
        option1 = op1;
        option2 = op2;
        sendBack = callback;

        Title1.text = option1.powerUpName;
        Title2.text = option2.powerUpName;
        Desc1.text = option1.description;
        Desc2.text = option2.description;
        if (listener1 != null) Button1.onClick.RemoveListener(listener1);
        if (listener2 != null) Button2.onClick.RemoveListener(listener2);
        listener1 = () => Chosen(option1);
        listener2 = () => Chosen(option2);
        Button1.onClick.AddListener(listener1);
        Button2.onClick.AddListener(listener2);
        this.gameObject.SetActive(true);
    }

    // Once a power up is chosen the listeners are removed and then sendBack returns the chosen powerUp to the powerUpManager
    // Then the gameObject is deactivated
    private void Chosen(PowerUp pu){
        Button1.onClick.RemoveListener(listener1);
        Button2.onClick.RemoveListener(listener2);
        sendBack?.Invoke(pu);
        this.gameObject.SetActive(false);
    }

}

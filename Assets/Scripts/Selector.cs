using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    //Variables
    public ShopUIManager confirmer;
    private bool isBought = false;
    private bool canBuy;
    private Transform CostObject;
    private int fcost;
    private bool inUse = false;
    private Color buyTextColor;
    private Color starColor;
    private GameObject selectText;
    private GameObject selectedText;
    private GameObject fragmentObj;


    //If not a cost object is created and then the cost of the customization is calculated,
    //Aswell as if the player can currently buy it or not
    void Start()
    {
        SetObjects();
        UpdateStatus();
        if (!isBought)
        {
            SetPrice();
        }
        else
        {
            ChangeText();
        }
    }

    //Changes the Button's color based on if the customization has been bought every frame
    public void Update()
    {
        if (!isBought)
        {
            ColorButton(75, 75, 75, 255);
            ColorChange();
        }
    }

    private void SetObjects()
    {
        CostObject = transform.Find("Cost");
        if(CostObject != null)
            buyTextColor = CostObject.GetComponent<TextMeshProUGUI>().color;
        selectText = transform.Find("Select").gameObject;
        selectedText = transform.Find("Selected").gameObject;
        if(gameObject.name != "Gold")
            fragmentObj = transform.Find("Fragment").gameObject;
        starColor = transform.Find("Image").gameObject.GetComponent<Image>().color;
    }

    //Checks if the star customization has been bought if so changes necessary UI.
    private void UpdateStatus()
    {
        if (gameObject.name == CustomizationManager.CM.getSelected())
        {
            inUse = true;
            ColorButton(155, 9, 255, 255);
        }
        if (gameObject.name != "Gold")
            isBought = ShopManager.sManager.CheckList(gameObject.name);
        else
        {
            isBought = true;
        }
    }

    private void SetPrice()
    {
            fcost = int.Parse(CostObject.GetComponent<TextMeshProUGUI>().text);
            canBuy = FragmentManager.fManager.getFragments() >= fcost;
    }

    //Takes rgba values and then colors the button based on those values
    public void ColorButton(byte r, byte g, byte b, byte a)
    {
        Button star = GetComponent<Button>();
        ColorBlock cb = star.colors;
        cb.normalColor = new Color32(r, g, b, a);
        cb.highlightedColor = new Color32(r, g, b, a);
        cb.selectedColor = new Color32(r, g, b, a);
        cb.pressedColor = new Color32(r, g, b, a);
        star.colors = cb;
    }

    //On click checks isBought  is not then checks canBuy, if not bought but can buy, 
    // brings up the purchase UI. If isBought but not inUse runs Select()
    public void Clicked()
    {
        if (isBought)
        {
            if (!inUse)
                Select();
        }
        else
        {
            if (canBuy)
            {
                confirmer.Confirm(fcost, Purchase);
            }
        }
    }

    //If purchase confirmed, changes the UI, sets isBought to true, recalculates fragments, 
    // and adds star customization to a list in ShopManager
    private void Purchase(bool choice)
    {
        if (choice)
        {
            FragmentManager.fManager.useFragments(fcost);
            ShopManager.sManager.AmountCheck();
            isBought = true;
            CostObject.gameObject.SetActive(false);
            fragmentObj.SetActive(false);
            selectText.SetActive(true);
            ShopManager.sManager.AddToUnlockeds(gameObject.name);
            SaveAndLoad.SAL.SaveData();
            ColorButton(238, 230, 230, 255);
        }
    }

    //Changes color of the pricing text based on whether player has enough fragments
    private void ColorChange()
    {
        if (!canBuy)
        {
            buyTextColor = Color.red;
        }
        else
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString("00149D", out newColor))
            {
                buyTextColor = newColor;
            }
        }
    }

    //Sets inUse to true, passes info to CustomizationManager, and updates UI
    private void Select()
    {
        selectText.SetActive(false);
        selectedText.SetActive(true);
        inUse = true;
        ColorButton(155, 9, 255, 255);
        CustomizationManager.CM.Customize(gameObject.name, starColor);
    }

    //Sets inUse to false and updates UI
    public void Deselect()
    {
        selectText.SetActive(true);
        selectedText.SetActive(false);
        inUse = false;
        ColorButton(238, 230, 230, 255);
    }

    //Changes UI text based on star and whether its inUse
    public void ChangeText()
    {
        if (gameObject.name != "Gold")
        {
            CostObject.gameObject.SetActive(false);
            fragmentObj.SetActive(false);
        }
        if (!inUse)
            selectText.SetActive(true);
        else
        {
            selectedText.SetActive(true);
            selectText.SetActive(false);
        }
    }
    
}

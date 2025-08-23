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

    //Checks if the star customization has been bought if so changes necessary UI.
    //If not a cost object is created and then the cost of the customization is calculated,
    //Aswell as if the player can currently buy it or not
    void Start()
    {
        if (gameObject.name == CustomizationManager.CM.getSelected())
            inUse = true;
        if (gameObject.name != "Gold")
            isBought = ShopManager.sManager.CheckList(gameObject.name);
        else
        {
            isBought = true;
        }
        if (!isBought)
        {
            CostObject = transform.Find("Cost");
            fcost = int.Parse(CostObject.GetComponent<TextMeshProUGUI>().text);
            canBuy = FragmentManager.fManager.getFragments() >= fcost;
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
        else
        {
            if (!inUse)
                ColorButton(238, 230, 230, 255);
            else
            {
                ColorButton(155, 9, 255, 255);
            }
        }
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
            transform.Find("Fragment").gameObject.SetActive(false);
            transform.Find("Select").gameObject.SetActive(true);
            ShopManager.sManager.AddToList(gameObject.name);
            SaveAndLoad.SAL.SaveData();
        }
    }

    //Changes color of the pricing text based on whether player has enough fragments
    private void ColorChange()
    {
        if (!canBuy)
        {
            CostObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString("00149D", out newColor))
            {
                CostObject.GetComponent<TextMeshProUGUI>().color = newColor;
            }
        }
    }

    //Sets inUse to true, passes info to CustomizationManager, and changes UI text
    private void Select()
    {
        transform.Find("Select").gameObject.SetActive(false);
        transform.Find("Selected").gameObject.SetActive(true);
        inUse = true;
        CustomizationManager.CM.Customize(gameObject.name, transform.Find("Image").gameObject.GetComponent<Image>().color);
    }

    //Sets inUse to false and changes UI text
    public void Deselect()
    {
        transform.Find("Select").gameObject.SetActive(true);
        transform.Find("Selected").gameObject.SetActive(false);
        inUse = false;
    }

    //Changes UI text based on star and whether its inUse
    public void ChangeText()
    {
        if (gameObject.name != "Gold")
        {
            transform.Find("Cost").gameObject.SetActive(false);
            transform.Find("Fragment").gameObject.SetActive(false);
        }
        if (!inUse)
            transform.Find("Select").gameObject.SetActive(true);
        else
        {
            transform.Find("Selected").gameObject.SetActive(true);
            transform.Find("Select").gameObject.SetActive(false);
        }
    }
    
}

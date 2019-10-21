using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlokables : MonoBehaviour
{
    public GameObject greenButton;
    public int cashValue;




    void Update()
    {
        cashValue = GlobalCash.TotalCash;
        if (cashValue >= 200)
        {
            greenButton.GetComponent<Button>().interactable = true;
        }
    }

    public void GreenUnlock()
    {
        greenButton.SetActive(false);
        cashValue -= 200;
        GlobalCash.TotalCash -= 200;
        PlayerPrefs.SetInt("SavedCash", GlobalCash.TotalCash);
        PlayerPrefs.SetInt("GreenBought", 200);

    }
}

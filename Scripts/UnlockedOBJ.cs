using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedOBJ : MonoBehaviour
{
    public int greenSelect;
    public GameObject fakeGreen;

    void Start()
    {
        greenSelect = PlayerPrefs.GetInt("GreenBought");
        if (greenSelect == 200)
        {
            fakeGreen.SetActive(false);
        }
    }
}

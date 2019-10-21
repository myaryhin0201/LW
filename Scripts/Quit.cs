using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public void hasquit() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

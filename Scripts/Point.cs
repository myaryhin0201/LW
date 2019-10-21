using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour {

    public GameObject L1;
    public GameObject L2;
    public GameObject Points;
    public GameObject win;
    public GameObject po;
    public static string Po;
    public static int poin;

    private void OnTriggerEnter()
    {

        L1.SetActive(false);
        L2.SetActive(true);
        poin += 1;
        Po = poin.ToString("F0");
        Points.GetComponent<Text> ().text = Po;
        if (poin == 10) { po.SetActive(false); win.SetActive(true); }
    }
}

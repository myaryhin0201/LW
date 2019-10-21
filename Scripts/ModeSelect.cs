using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    public static int RaceMode; // 0=Race, 1=Score, 2=Time
    public GameObject TrackSEL;

    public void ScoreMode()
    {
        RaceMode = 1;
        TrackSEL.SetActive(true);
    }


    public void TimeMode()
    {
        RaceMode = 2;
        TrackSEL.SetActive(true);
    }
    public void TRERACEMode()
    {
        RaceMode = 0;
        TrackSEL.SetActive(true);
    }
}

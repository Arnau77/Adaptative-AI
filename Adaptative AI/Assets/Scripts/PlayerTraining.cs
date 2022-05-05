using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTraining : Player
{

    public GameObject[] ais;
    public string[] aiNames;
    int numberOfEpisodes = -1;
    int totalRangeOfRandomNum = 6;
    
    public override void Reset()
    {
        training = false;
        numberOfEpisodes++;
        int i = Random.Range(0, totalRangeOfRandomNum);
        componentAI = (AI)ais[i].GetComponent(aiNames[i]);
        if (componentAI != null)
        {
            training = true;
        }
        else
        {
            Debug.LogWarning("PLAYER");
        }

        if (Input.GetKey(KeyCode.P))
        {
            totalRangeOfRandomNum = 7;
        }

        else if (Input.GetKey(KeyCode.X))
        {
            totalRangeOfRandomNum = 6;
        }
        base.Reset();
    }

   
}

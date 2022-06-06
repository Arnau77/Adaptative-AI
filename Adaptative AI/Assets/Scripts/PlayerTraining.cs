using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTraining : Player
{

    public GameObject[] ais;
    public string[] aiNames;
    int actualRangeOfRandomNum;
    int totalRangeOfRandomNum;
    int totalEpisodes = -1;
    public int finalNumber;
    public bool checking;

    private void Awake()
    {
        totalRangeOfRandomNum = ais.Length;
        actualRangeOfRandomNum = totalRangeOfRandomNum;
    }

    public override void Reset()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (actualRangeOfRandomNum == totalRangeOfRandomNum)
            {
                actualRangeOfRandomNum--;
            }
            else
            {
                actualRangeOfRandomNum++;
            }
        }
        training = false;
        int i = Random.Range(0, actualRangeOfRandomNum);
        if( i + 1 == totalRangeOfRandomNum)
        {
            i = Random.Range(0, actualRangeOfRandomNum);
        }
        if (checking)
        {
            i = finalNumber;
        }
        componentAI = (AI)ais[i].GetComponent(aiNames[i]);
        if (totalEpisodes > 103*2 && checking)
        {
            componentAI = null;
        }
        totalEpisodes++;
        if (componentAI != null)
        {
            training = true;
        }
        else
        {
            Debug.LogWarning("PLAYER");
        }
        base.Reset();
    }

   
}

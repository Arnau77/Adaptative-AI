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
    int finalNumber = -1;
    
    public override void Reset()
    {
        training = false;
        numberOfEpisodes++;
        int i = Random.Range(0, totalRangeOfRandomNum);
        if (Input.GetKey(KeyCode.Alpha0))
        {
            Debug.Log("AI: Attack And Heal");
            finalNumber = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("AI: Attack, Heal And Defense");
            finalNumber = 1;
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            Debug.Log("AI: Attack While Recovering Mana");
            finalNumber = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            Debug.Log("AI: Full");
            finalNumber = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            Debug.Log("AI: Attack");
            finalNumber = 4;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            Debug.Log("AI: Random");
            finalNumber = 5;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            Debug.Log("AI: Player");
            finalNumber = 6;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            Debug.Log("All AIs");
            finalNumber = -1;
        }
        if (finalNumber != -1)
        {
            i = finalNumber;
        }
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

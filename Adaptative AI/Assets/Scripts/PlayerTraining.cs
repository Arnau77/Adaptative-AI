using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTraining : Player
{

    public GameObject[] ais;
    public string[] aiNames;
    int numberOfEpisodes = -1;
    
    public override void Reset()
    {
        training = false;
        numberOfEpisodes++;
        int i = Random.Range(0, numberOfEpisodes > 900 ? 7 : 6);
        componentAI = (AI)ais[i].GetComponent(aiNames[i]);
        if (componentAI != null)
        {
            training = true;
        }
        else
        {
            Debug.LogWarning("PLAYER");
        }

        if (numberOfEpisodes > 1000)
        {
            gameManager.Stop();
        }
        base.Reset();
    }

   
}

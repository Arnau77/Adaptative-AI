using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{

    public GameObject[] ais;
    public int level = 5;
    private int lastMatch = 0;
    public int totalOfMatchesToWinToChangeLevel = 1;
    private float minLifeToChangeLevelInstantly = 85;
    public UnityEngine.UI.Text levelOfAI = null;

    public override void Reset()
    {
        if (finalLife > 0)
        {
            if (lastMatch == totalOfMatchesToWinToChangeLevel || finalLife >= minLifeToChangeLevelInstantly)
            {
                lastMatch = 0;
                if (level > 0)
                {
                    level--;
                }
            }
            else
            {
                if (lastMatch >= 0)
                {
                    lastMatch++;
                }
                else
                {
                    lastMatch = 1;
                }
            }
        }
        else if (finalEnemyLife > 0)
        {
            if (lastMatch == -totalOfMatchesToWinToChangeLevel || finalEnemyLife >= minLifeToChangeLevelInstantly)
            {
                lastMatch = 0;
                if (level < 10)
                {
                    level++;
                }
            }
            else
            {
                if (lastMatch <= 0)
                {
                    lastMatch--;
                }
                else
                {
                    lastMatch = -1;
                }
            }
        }
        levelOfAI.text = level.ToString();

        componentAI = ais[level].GetComponent<AIAgent>();
        base.Reset();
    }
}

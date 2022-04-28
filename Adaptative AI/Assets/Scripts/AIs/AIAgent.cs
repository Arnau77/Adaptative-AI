using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIAgent : AI
{
    [HideInInspector] public bool aiTurn = false;
    [HideInInspector] public bool victory = false;
    [HideInInspector] public bool matchEnded = false;
    public override void ChooseOption()
    {
        aiTurn = true;
    }

    public bool CheckDecision(int decision)
    {
        return CheckIfOptionIsValid((Player.Options)decision);
    }

    public override void MatchEnded(bool victory)
    {
        matchEnded = true;
        this.victory = victory;
    }

    public void Restart()
    {
        aiTurn = false;
        matchEnded = false;
    }

    public void OptionDecided(int decision)
    {
        aiTurn = false;
        player.DecideOption((Player.Options)decision);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyAttackAI : AI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ChooseOption()
    {
        Player.Options optionChosen = Player.Options.SPECIAL_ATTACK;
        if (!CheckIfOptionIsValid(optionChosen))
        {
            optionChosen=Player.Options.ATTACK;
        }

        player.DecideOption(optionChosen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndHealAI : AI
{
    public int maxLifeToFocusOnAttack = 25;
    public int maxLifeToHeal = 25;
    public int maxLifeToAttackInsteadOfHeal = 10;

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
        Player.Options optionChosen;
        if (player.getLife() <= maxLifeToHeal && player.enemy.getLife() > maxLifeToAttackInsteadOfHeal)
        {
            optionChosen = Player.Options.HEAL;
            if (!CheckIfOptionIsValid(optionChosen))
            {
                optionChosen = Player.Options.RECOVER_MANA;
            }
        }
        else if (player.enemy.getDefendingBonus() > 1)
        {
            optionChosen = Player.Options.RECOVER_MANA;
            if (!CheckIfOptionIsValid(optionChosen))
            {
                optionChosen = Player.Options.ATTACK;
            }
        }
        else if (player.enemy.getLife() <= maxLifeToFocusOnAttack || player.getLife() <= maxLifeToFocusOnAttack){
            optionChosen = Player.Options.SPECIAL_ATTACK;
            if (!CheckIfOptionIsValid(optionChosen))
            {
                optionChosen = Player.Options.ATTACK;
            }
        }
        else
        {
            optionChosen = Player.Options.SPECIAL_ATTACK;
            if (!CheckIfOptionIsValid(optionChosen))
            {
                optionChosen = Player.Options.RECOVER_MANA;
            }
        }
        player.DecideOption(optionChosen);
    }
}

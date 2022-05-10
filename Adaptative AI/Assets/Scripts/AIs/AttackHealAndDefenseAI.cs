using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHealAndDefenseAI : AI
{
    public int maxLifeToFocusOnAttack = 25;
    public int maxLifeToHeal = 30;
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
            int randomChoice = Random.Range(0, 7);
            switch (randomChoice)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    optionChosen = Player.Options.SPECIAL_ATTACK;
                    break;
                case 4:
                    optionChosen = Player.Options.ATTACK;
                    break;
                default:
                    if (player.enemy.getMana() < gameManager.manaSpentWithSpecialAttack)
                    {
                        optionChosen = Player.Options.SPECIAL_ATTACK;
                    }
                    else
                    {
                        optionChosen = Player.Options.DEFENSE;
                    }
                    break;
            }
            if (!CheckIfOptionIsValid(optionChosen))
            {
                optionChosen = Player.Options.RECOVER_MANA;
                if (!CheckIfOptionIsValid(optionChosen))
                {
                    optionChosen = Player.Options.ATTACK;
                }
            }
        }
        player.DecideOption(optionChosen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAI : AI
{
    public int maxLifeToFocusOnAttack = 25;
    public int maxLifeToHeal = 30;
    public int maxLifeToAttackInsteadOfHeal = 10;
    public int levelOfStatToStartIncresingStats = -2;
    public int levelOfStatToStartDecreasingStats = 2;

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
        else if(player.enemy.getLife() > maxLifeToAttackInsteadOfHeal && player.getLevelOfChangeStats()<=levelOfStatToStartIncresingStats&& CheckIfOptionIsValid(Player.Options.INCREASE_STATS))
        {
            optionChosen = Player.Options.INCREASE_STATS;
        }
        else if (player.enemy.getLife() > maxLifeToAttackInsteadOfHeal && player.enemy.getLevelOfChangeStats() >= levelOfStatToStartDecreasingStats && CheckIfOptionIsValid(Player.Options.DECREASE_STATS))
        {
            optionChosen = Player.Options.DECREASE_STATS;
        }
        else if (player.enemy.getDefendingBonus() > 1)
        {
            optionChosen = Player.Options.RECOVER_MANA;
            if (!CheckIfOptionIsValid(optionChosen))
            {
                if (Random.Range(0, 1) == 0)
                {
                    optionChosen = Player.Options.INCREASE_STATS;
                }
                else
                {
                    optionChosen = Player.Options.DECREASE_STATS;
                }
                if (!CheckIfOptionIsValid(optionChosen))
                {
                    optionChosen = Player.Options.ATTACK;
                }
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
            int randomChoice = Random.Range(0, 15);
            switch (randomChoice)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    optionChosen = Player.Options.SPECIAL_ATTACK;
                    break;
                case 6:
                    optionChosen = Player.Options.ATTACK;
                    break;
                case 7:
                case 8:
                case 9:
                    if (randomChoice !=9 && player.getLevelOfChangeStats()>=levelOfStatToStartDecreasingStats)
                    {
                        optionChosen = Player.Options.SPECIAL_ATTACK;
                    }
                    else
                    {
                        optionChosen = Player.Options.INCREASE_STATS;
                    }
                    break;
                case 10:
                case 11:
                case 12:
                    if (randomChoice != 12 && player.enemy.getLevelOfChangeStats() <= levelOfStatToStartDecreasingStats)
                    {
                        optionChosen = Player.Options.SPECIAL_ATTACK;
                    }
                    else
                    {
                        optionChosen = Player.Options.DECREASE_STATS;
                    }
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

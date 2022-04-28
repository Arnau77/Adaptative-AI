using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void ChooseOption()
    {

    }

    virtual public void MatchEnded(bool victory)
    {

    }

    protected bool CheckIfOptionIsValid(Player.Options optionChosen)
    {
        switch (optionChosen)
        {
            case Player.Options.NONE:
                return false;
            case Player.Options.DECREASE_STATS:
                if(player.getMana() < gameManager.manaSpentWithDecreasingStats || player.enemy.getLevelOfChangeStats() <= -gameManager.maxOfTimesChangingLevelOfStats)
                {
                    return false;
                }
                break;
            case Player.Options.DEFENSE:
                if (player.getMana() < gameManager.manaSpentWithDefense)
                {
                    return false;
                }
                break;
            case Player.Options.HEAL:
                if (player.getMana() < gameManager.manaSpentWithHealing || player.getLife() >= player.initialLife)
                {
                    return false;
                }
                break;
            case Player.Options.INCREASE_STATS:
                if (player.getMana() < gameManager.manaSpentWithIncreasingStats || player.getLevelOfChangeStats() >= gameManager.maxOfTimesChangingLevelOfStats)
                {
                    return false;
                }
                break;
            case Player.Options.RECOVER_MANA:
                if (player.getMana() >= player.initialMana)
                {
                    return false;
                }
                break;
            case Player.Options.SPECIAL_ATTACK:
                if (player.getMana() < gameManager.manaSpentWithSpecialAttack)
                {
                    return false;
                }
                break;
        }
        return true;
    }
}

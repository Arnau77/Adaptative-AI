using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public UnityEngine.UI.Button button = null;
    public Player player1 = null;
    public Player player2 = null;
    public GameManager gameManager = null;
    public Player.Options option = Player.Options.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (player1.playerTurn)
        {
            CheckIfButtonActive(player1);
        }
        else if (player2.playerTurn)
        {
            CheckIfButtonActive(player2);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CheckIfButtonActive(Player player)
    {
        switch (option)
        {
            case Player.Options.DECREASE_STATS:
                if (player.getMana() >= gameManager.manaSpentWithDecreasingStats && player.enemy.getLevelOfChangeStats() > -gameManager.maxOfTimesChangingLevelOfStats) 
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case Player.Options.DEFENSE:
                if (player.getMana() >= gameManager.manaSpentWithDefense)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case Player.Options.HEAL:
                if (player.getMana() >= gameManager.manaSpentWithHealing)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case Player.Options.INCREASE_STATS:
                if (player.getMana() >= gameManager.manaSpentWithIncreasingStats && player.getLevelOfChangeStats() < gameManager.maxOfTimesChangingLevelOfStats)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case Player.Options.SPECIAL_ATTACK:
                if (player.getMana() >= gameManager.manaSpentWithSpecialAttack)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
                break;
        }
    }
}

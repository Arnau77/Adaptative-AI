using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player1 = null;
    public Player player2 = null;
    public GameObject buttonsAndLog = null;
    public GameObject pauseMenu = null;
    public GameObject endMenu = null;
    public Text winnerText = null;
    public int basicAttackDamage = 1;
    public int manaAttackDamage = 3;
    public int percentageRecoveryMana = 50;
    public int percentageDefense = 50;
    public int maxOfTimesChangingLevelOfStats = 3;
    public int percentageChangingStats = 10;
    public int percentageHealing = 50;
    public int manaSpentWithSpecialAttack = 20;
    public int manaSpentWithIncreasingStats = 10;
    public int manaSpentWithDecreasingStats = 10;
    public int manaSpentWithHealing = 25;
    public int manaSpentWithDefense = 15;
    bool endGame = false;
    bool decidingOptions = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Pause(true);
            pauseMenu.SetActive(true);
        }

        if (player1.endTurn && player2.endTurn)
        {
            player1.playerTurn = false;
            player2.playerTurn = false;
            player1.endTurn = false;
            player2.endTurn = false;
            DecideFastestPlayer(decidingOptions).playerTurn = true;
            decidingOptions = !decidingOptions;
        }
        else if (player1.endTurn && player1.playerTurn)
        {
            player1.playerTurn = false;
            player2.playerTurn = true;
        }
        else if (player2.endTurn && player2.playerTurn)
        {
            player2.playerTurn = false;
            player1.playerTurn = true;
        }
        if (player1.dead)
        {
            winnerText.text = player2.playerName + " has won the game";
            endGame = true;
        }
        else if(player2.dead)
        {
            winnerText.text = player1.playerName + " has won the game";
            endGame = true;
        }
        if (endGame)
        {
            endMenu.SetActive(true);
            buttonsAndLog.SetActive(false);
        }
    }

    Player DecideFastestPlayer(bool decidingOptions)
    {
        if (decidingOptions)
        {
            return player1;
        }

        int speed1 = player1.getSpeed();
        int speed2 = player2.getSpeed();
        if (speed1 == speed2)
        {
            if (Random.Range(0, 2) == 0)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
        else if (speed1 > speed2)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }

    public void OptionSelected(int optionChosen)
    {
        if (decidingOptions)
            return;
        Player.Options option = (Player.Options)optionChosen;
        if (player1.playerTurn)
        {
            player1.DecideOption(option);
        }
        else if (player2.playerTurn)
        {
            player2.DecideOption(option);
        }
    }

    public void Pause(bool pausing)
    {
        buttonsAndLog.SetActive(!pausing);
    }
    public void Restart()
    {
        player1.Reset();
        player2.Reset();
        endGame = false;
        decidingOptions = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

}

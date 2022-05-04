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
    public GameObject buttons = null;
    public GameObject logObject = null;
    public Text winnerText = null;
    public Text logText = null;
    public int basicAttackDamage = 1;
    public int manaAttackDamage = 3;
    public int percentageRecoveryMana = 70;
    public int percentageDefense = 50;
    public int maxOfTimesChangingLevelOfStats = 3;
    public int percentageChangingStats = 10;
    public int percentageHealing = 50;
    public int manaSpentWithSpecialAttack = 20;
    public int manaSpentWithIncreasingStats = 15;
    public int manaSpentWithDecreasingStats = 15;
    public int manaSpentWithHealing = 30;
    public int manaSpentWithDefense = 15;
    bool endGame = false;
    bool decidingOptions = true;
    bool logging = false;
    bool buttonsToActivate = false;
    List<string> logsToPrint = new List<string>();
    bool stop = false;
    

    // Start is called before the first frame update
    void Start()
    {
        logsToPrint.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stop = false;
            }
            else
            {
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Pause(true);
            pauseMenu.SetActive(true);
        }

        if (logging)
        {
            return;
        }

        if (player1.dead)
        {
            player1.MatchEnded(false);
            player2.MatchEnded(true);
            winnerText.text = player2.playerName + " has won";
            endGame = true;
        }
        else if (player2.dead)
        {
            player1.MatchEnded(true);
            player2.MatchEnded(false);
            winnerText.text = player1.playerName + " has won";
            endGame = true;
        }
        if (endGame)
        {
            endMenu.SetActive(true);
            buttonsAndLog.SetActive(false);
            return;
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
        
        
    }

    Player DecideFastestPlayer(bool decidingOptions)
    {
        if (decidingOptions)
        {
            buttonsToActivate = true;
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

    public void Log(string log)
    {
        if (log.Length == 0)
        {
            if (logsToPrint.Count == 0)
            {
                logObject.SetActive(false);
                logging = false;
                if (buttonsToActivate)
                {
                    buttons.SetActive(true);
                    buttonsToActivate = false;
                }
            }
            else
            {
                logText.text = logsToPrint[0];
                logsToPrint.RemoveAt(0);
            }
        }
        else if (logging)
        {
            logsToPrint.Add(log);
        }
        else 
        {
            logObject.SetActive(true);
            logging = true;
            logText.text = log;
            buttons.SetActive(false);
        }
    }

    public void Pause(bool pausing)
    {
        buttonsAndLog.SetActive(!pausing);
    }
    public void Restart()
    {
        buttonsToActivate = false;
        logging = false;
        logsToPrint.Clear();
        player1.Reset();
        player2.Reset();
        endGame = false;
        decidingOptions = true;
        Pause(false);
    }

    public bool GetEndGame()
    {
        return endGame;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Stop()
    {
        stop = true;
    }

}

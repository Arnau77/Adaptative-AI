using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SecondAgent : Agent
{
    public AIAgent aiAgent;
    public GameObject endGameMenu;
    public UnityEngine.UI.Text text;
    float reward = 0;
    public bool training = false;
    public float rewardMultiplier = 0.5f;
    public bool outOfMoves = false;

    public override void OnEpisodeBegin()
    {
        //rewardMultiplier=Random.Range(0,11)*0.1f;
        if(training)
            text.text = rewardMultiplier.ToString();
        reward = 0;
        aiAgent.Restart();
        if (training)
        {
            aiAgent.gameManager.Restart();
        }
        if (endGameMenu != null)
        {
            endGameMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (aiAgent.matchEnded)
        {
            SetReward(0.5f * (1 - rewardMultiplier));
            if (aiAgent.victory)
            {
                SetReward(1f * rewardMultiplier);
                if (training)
                {
                    reward += 1f * rewardMultiplier;
                    Debug.Log("Victory!");
                }
            }
            else
            {
                SetReward(-1f * rewardMultiplier);
                if (training)
                {
                    reward -= 1f * rewardMultiplier;
                    Debug.Log("Lost!");
                }
                if (outOfMoves)
                {
                    Debug.Log("Out Of Moves");
                    outOfMoves = false;
                }
            }
            if (training)
            {
                text.text = rewardMultiplier.ToString();
            }
            EndEpisode();
        }
        else if (aiAgent.aiTurn)
        {
            RequestDecision();
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(aiAgent.player.getLife());
        sensor.AddObservation(aiAgent.player.enemy.getLife());
        sensor.AddObservation(aiAgent.player.getMana());
        sensor.AddObservation(aiAgent.player.enemy.getMana());
        sensor.AddObservation(aiAgent.player.getLevelOfChangeStats());
        sensor.AddObservation(aiAgent.player.enemy.getLevelOfChangeStats());
        sensor.AddObservation(aiAgent.player.getDefendingBonus());
        sensor.AddObservation(aiAgent.player.enemy.getDefendingBonus());
        sensor.AddObservation(rewardMultiplier);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!aiAgent.aiTurn)
        {
            return;
        }
        int decisionChosen = actions.DiscreteActions[0];
        if(aiAgent.CheckDecision(decisionChosen)){
            if (decisionChosen == 2)
            {
                SetReward(0.01f);
                reward += 0.01f;
            }
            //SetReward(-0.01f * 1f);
            //reward -= 0.01f * 1f;
            aiAgent.OptionDecided(decisionChosen);
        }
        else
        {
            SetReward(-0.05f);
            if (training)
            {
                reward -= 0.05f;
            }
            if (reward < -20f)
            {
                outOfMoves = true;
                aiAgent.matchEnded=true;
                aiAgent.victory = false;
            }
        }
        
    }

}

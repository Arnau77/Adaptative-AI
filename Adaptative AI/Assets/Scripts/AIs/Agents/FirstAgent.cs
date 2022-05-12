using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class FirstAgent : Agent
{
    public AIAgent aiAgent;
    public GameObject endGameMenu;
    public UnityEngine.UI.Text text;
    float reward = 0;
    public bool training = false;

    public override void OnEpisodeBegin()
    {
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
            if (aiAgent.victory)
            {
                SetReward(1f);
                if (training)
                {
                    reward += 1f;
                    //SetReward(aiAgent.player.getLife() * 0.01f);
                    //reward += aiAgent.player.getLife() * 0.01f;
                    Debug.Log("Victory!");
                }
            }
            else
            {
                SetReward(-1f);
                if (training)
                {
                    reward -= 1f;
                    //SetReward(-aiAgent.player.enemy.getLife() * 0.01f);
                    //reward -= aiAgent.player.enemy.getLife() * 0.01f;
                    Debug.Log("Lost!");
                }

            }
            if (training)
            {
                text.text = reward.ToString();
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
            aiAgent.OptionDecided(decisionChosen);
            //SetReward(-10f);
            //reward -= 10f;
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
                aiAgent.matchEnded=true;
                aiAgent.victory = false;
            }
        }
        
    }

}

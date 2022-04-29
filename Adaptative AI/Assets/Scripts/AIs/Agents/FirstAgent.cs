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

    public override void OnEpisodeBegin()
    {
        aiAgent.Restart();
        aiAgent.gameManager.Restart();
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
                SetReward(10000f);
                SetReward(aiAgent.player.getLife());
                Debug.Log("Victory!");
            }
            else
            {
                SetReward(-10000f);
                SetReward(-aiAgent.player.enemy.getLife());
                Debug.Log("Lost!");

            }
            EndEpisode();
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
        if(aiAgent.CheckDecision(actions.DiscreteActions[0] + 1))
        {
            aiAgent.OptionDecided(actions.DiscreteActions[0] + 1);
            SetReward(-100f);
        }
        
    }
}

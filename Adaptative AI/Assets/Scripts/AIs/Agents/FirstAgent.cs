using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class FirstAgent : Agent
{
    public AIAgent aiAgent;
    private bool recollectedObservations = false;

    public override void OnEpisodeBegin()
    {
        recollectedObservations = false;
        aiAgent.Restart();
    }
    public override void CollectObservations(VectorSensor sensor)
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
        else if (!aiAgent.aiTurn || recollectedObservations)
        {
            return;
        }
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
        recollectedObservations = true;
        if(aiAgent.CheckDecision(actions.DiscreteActions[0] + 1))
        {
            aiAgent.OptionDecided(actions.DiscreteActions[0] + 1);
            recollectedObservations = false;
            SetReward(-100f);
        }
        
    }
}

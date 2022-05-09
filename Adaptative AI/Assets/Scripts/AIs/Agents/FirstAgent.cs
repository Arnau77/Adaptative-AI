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

    public override void OnEpisodeBegin()
    {
        reward = 0;
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
                SetReward(1f);
                reward += 1f;
                //SetReward(aiAgent.player.getLife());
                //reward += aiAgent.player.getLife();
                Debug.Log("Victory!");
            }
            else
            {
                SetReward(-1f);
                reward -= 1f;
                //SetReward(-aiAgent.player.enemy.getLife());
                //reward -= aiAgent.player.enemy.getLife();
                Debug.Log("Lost!");

            }
            text.text = reward.ToString();
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
            aiAgent.OptionDecided(decisionChosen);
            //SetReward(-10f);
            //reward -= 10f;
        }
        else
        {
            //SetReward(-500f);
           // reward -= 500f;
        }
        
    }

}

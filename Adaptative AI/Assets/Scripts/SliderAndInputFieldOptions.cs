using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAndInputFieldOptions : MonoBehaviour
{
    public PlayerAI playerAI;
    public Slider levelOfAISlider;
    public InputField numberOfVictoriesToChangeLevel;
    public void ChangeLevelOfAI()
    {
        playerAI.ChangeLevelOfAI((int)levelOfAISlider.value);
    }

    public void ChangeTotalOfVictoriesToChangeLevel()
    {
        int newNumber;
        if (int.TryParse(numberOfVictoriesToChangeLevel.text, out newNumber))
        {
            if (newNumber < 0)
            {
                newNumber = 0;
            }
            playerAI.ChangeNumberOfVictoriesToChangeLevel(newNumber);
        }
    }
}

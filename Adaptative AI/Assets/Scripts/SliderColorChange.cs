using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorChange : MonoBehaviour
{

    public Slider slider = null;
    public Image sliderImage = null;
    public Color colorWhenFull;
    public Color colorWhenMedium;
    public Color colorWhenLow;
    public int mediumValue;
    public int lowValue;
    Color colorWhenDepleted;


    void Start()
    {
        colorWhenDepleted = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    public void OnValueChanged()
    {
        if (slider.value == 0)
        {
            sliderImage.color = colorWhenDepleted;
            return;
        }
        if (sliderImage.color != colorWhenFull && slider.value > mediumValue) 
        {
            sliderImage.color = colorWhenFull;
        }
        else if (sliderImage.color != colorWhenMedium && slider.value > lowValue && slider.value <= mediumValue)
        {
            sliderImage.color = colorWhenMedium;
        }
        else if (sliderImage.color != colorWhenLow && slider.value <= lowValue)
        {
            sliderImage.color = colorWhenLow;
        }
    }
}

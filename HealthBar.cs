using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider, sliderHungry, sliderWater;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetHungry(int hungry)
    {
        sliderHungry.value = hungry;
    }
    public void SetWater(int water)
    {
        sliderWater.value = water;
    }
}

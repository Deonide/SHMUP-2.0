using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [SerializeField] private Slider m_slider;

    public void SetMaxFuel(float fuel)
    {
        m_slider.maxValue = fuel;
        m_slider.value = fuel;
    }

    public void SetFuel(float fuel)
    {
        m_slider.value = fuel;
    }
}

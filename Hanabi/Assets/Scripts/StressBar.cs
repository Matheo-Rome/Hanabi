using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMinStress(int stress)
    {
        slider.minValue = stress;
        slider.value = stress;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetStress(int stress)
    {
        slider.value = stress;

        fill.color = gradient.Evaluate(slider.value);
    }
}

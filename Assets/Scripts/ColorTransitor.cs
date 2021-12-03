using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTransitor : MonoBehaviour
{
    [SerializeField] Image transitionObject;
    [SerializeField] [Range(0f, 5f)] float transitionTime;
    [SerializeField] Color []transitionColors;
    [SerializeField] Gradient m_gradient;
    [SerializeField] GradientColorKey[] colorKey;
    [SerializeField] GradientAlphaKey[] alphaKey;

    [SerializeField] [Range(0f, 1f)] float m_currentColor = 1f;
    

    int colorIndex = 0;

    float t = 0f;

    private int _colorsCount;

    private void Start()
    {
        MarkOutGradient();

        ChangeColorByValue(m_currentColor);
    }


    private void GradiantFomation()
    {
        transitionObject.color = Color.Lerp(transitionObject.color, transitionColors[colorIndex], transitionTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, transitionTime * Time.deltaTime);

        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= transitionColors.Length) ? 0 : colorIndex;
        }
    }

    public void ChangeColorByValue(float val)
    {
        m_currentColor = val;
        transitionObject.color = m_gradient.Evaluate(val);
    }

    [ContextMenu("Apply Color")]
    private void MarkOutGradient()
    {
        _colorsCount = transitionColors.Length;

        var color = new GradientColorKey[_colorsCount];
        var alpha = new GradientAlphaKey[_colorsCount];

        float time = 0f;
        float delta = 1f / _colorsCount;

        //Последний сектору достается больше чем нужно, по этому мы берем его пространство и разбиваем на все, кроме первого, для выравнивания секторов
        float compensation = delta / (_colorsCount - 1);


        for (int i = 0; i < _colorsCount; i++)
        {
            color[i] = new GradientColorKey(transitionColors[i], time);
            alpha[i] = new GradientAlphaKey(1.0f, time);
            time += (delta + compensation);
        }

        m_gradient.SetKeys(color, alpha);
    }
}

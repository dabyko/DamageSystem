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

    int len;

    private void Start()
    {
        InitGradient();
    }

    private void Update()
    {
        ChangeColorByValue(m_currentColor);
        return;

        transitionObject.color = Color.Lerp(transitionObject.color, transitionColors[colorIndex], transitionTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, transitionTime * Time.deltaTime);

        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
          

    }

    public void ChangeColorByValue(float val)
    {
        Debug.Log("Value for color: " + val);
        transitionObject.color = m_gradient.Evaluate(val);
    }

    [ContextMenu("Apply Color")]
    private void InitGradient()
    {
        len = transitionColors.Length;
        var color = new GradientColorKey[len];
        var alpha = new GradientAlphaKey[len];
        float time = 0f;
        float delta = 1f / len;
        float error = delta / (len - 1);
        for (int i = 0; i < len; i++)
        {
            color[i] = new GradientColorKey(transitionColors[i], time);
            alpha[i] = new GradientAlphaKey(1.0f, time);
            time += (delta + error);
        }

        m_gradient.SetKeys(color, alpha);
    }
}

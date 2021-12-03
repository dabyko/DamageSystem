using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class HealthChangeState : UnityEvent<float> { }
[ExecuteInEditMode()]
public class HealthStatus : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/LinearHealthProgressBar")]
    public static void AddHealthProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearHealthProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    public int minimum;

    public int maximum;

    public int current;

    public Image mask;

    public Color fillColor;

    public Image fill;

    public HealthChangeState OnStateChange = new HealthChangeState();

    private void Start()
    {
        fill.color = fillColor;
    }
    private void Update()
    {
        Debug.Log("Current health state" + current);

        CheckFill();
    }

    void CheckFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;

        mask.fillAmount = fillAmount;

        OnStateChange.Invoke(mask.fillAmount);
        //fill.color = fillColor;
    }

    public void ChangeCurrentState(int value)
    {
        current = value;
    }
}

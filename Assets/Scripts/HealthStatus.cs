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

    [SerializeField]  private int m_minHealth;

    [SerializeField]  private int m_maxHealth;

    [SerializeField] private Image m_maskBar; 
    
    [SerializeField] private Image m_fillBar;

    [SerializeField] private int _curHealth; 

    public HealthChangeState OnHealthStateChange = new HealthChangeState();

    private void Update()
    {
        CheckFill();
    }

    void CheckFill()
    {
        float currentOffset = _curHealth - m_minHealth;
        float maximumOffset = m_maxHealth - m_minHealth;
        float fillAmount = currentOffset / maximumOffset;

       
        m_maskBar.fillAmount = fillAmount;

        OnHealthStateChange.Invoke(m_maskBar.fillAmount);
    }

    public void ChangeCurrentState(int value)
    {
        _curHealth = value;
    }
}

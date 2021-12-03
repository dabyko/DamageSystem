using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class ReportCondition  : UnityEvent<int> { }
public class Wall : MonoBehaviour,IScattering
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public ReportCondition OnGetDamage = new ReportCondition();

    public UnityEvent OnDestroy;

    private void Start()
    {
        _currentHealth = _maxHealth;

    }
    public void ApplyImpairment(int impairmentValue)
    {
        _currentHealth -= impairmentValue;

        OnGetDamage.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            OnDestroy.Invoke();
            Destroy(this.gameObject);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class GetDamage : UnityEvent<int> { }
public class Wall : MonoBehaviour,IScattering
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    public GetDamage OnGetDamage = new GetDamage();
    public UnityEvent OnDestroy;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void ApplyImpairment(int impairmentValue)
    {
        _currentHealth -= impairmentValue;
        Debug.Log("Current wall health" + _currentHealth);
        OnGetDamage.Invoke(_currentHealth);


        if (_currentHealth <= 0)
        {
            OnDestroy.Invoke();
            Destroy(this.gameObject);
        }
    }


}

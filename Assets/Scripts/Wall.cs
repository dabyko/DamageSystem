using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IScattering
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void ApplyImpairment(int impairmentValue)
    {
        _currentHealth -= impairmentValue;

        Debug.Log("Wall received damage and now I have " + _currentHealth + " life units");

        if (_currentHealth <= 0)
        {
            Debug.Log(this.gameObject.name + " destroyed");
            Destroy(this.gameObject);
        }
    }
}

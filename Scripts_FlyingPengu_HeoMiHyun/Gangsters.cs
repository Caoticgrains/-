using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangsters : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 1f;
    [SerializeField] private float _damageThreshold = 0.2f;
    
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DamageGangster(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.RemoveGangster(this);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocicy = collision.relativeVelocity.magnitude;

        if (impactVelocicy >= _damageThreshold)
        {
            DamageGangster(impactVelocicy);
        }
    }
}

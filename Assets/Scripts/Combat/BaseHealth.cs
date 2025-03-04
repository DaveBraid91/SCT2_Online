using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, IDamageable
{
    protected Animator cmpAnimator;

    private float currentLife;
    [SerializeField] private LifebarUI lifebarUI;
    [SerializeField] private float maxLife = 100;
    [SerializeField] private float damageCooldown;

    public float CurrentLife { get => currentLife; }
    public float MaxLife { get => maxLife; }

    private bool _canTakeDamage;

    protected virtual void Awake()
    {
        cmpAnimator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        currentLife = maxLife;
        _canTakeDamage = true;
    }

    public void ApplyDamage(float damage)
    {
        if (currentLife <= 0 || !_canTakeDamage) return;

        currentLife -= damage;
        lifebarUI.UpdateLifeBar(this);
        if(currentLife <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageCooldown());
        }
    }

    public void Heal(float heal)
    {
        if (currentLife <= 0) return;

        currentLife += heal;
        lifebarUI.UpdateLifeBar(this);
        if(currentLife > maxLife)
        {
            currentLife = maxLife;
        }
    }
    
    private IEnumerator DamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        _canTakeDamage = true;
    }

    protected abstract void Die();

}

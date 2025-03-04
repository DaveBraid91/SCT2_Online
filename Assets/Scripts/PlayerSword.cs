using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] float damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.ApplyDamage(damage);
    }
}


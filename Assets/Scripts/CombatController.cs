using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombatController : MonoBehaviour
{
    [SerializeField] Collider weaponTrigger;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        weaponTrigger.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
        else
        {
            animator.ResetTrigger("attack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("strongAttack");
        }
        else
        {
            animator.ResetTrigger("strongAttack");
        }
    }


    void AttackAnimEvent() 
    {
        weaponTrigger.enabled= true;
    }

    void EndAttackAnimEvent()
    {
        weaponTrigger.enabled = false;
    }

}

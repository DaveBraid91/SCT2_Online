using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int StrongAttack = Animator.StringToHash("strongAttack");
    private static readonly int Moving = Animator.StringToHash("moving");
    [SerializeField] Collider weaponTrigger;

    Animator _cmpAnimator;
    CharacterController _cmpCc;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        _cmpAnimator = GetComponent<Animator>();
        _cmpCc = GetComponent<CharacterController>();

        weaponTrigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _cmpAnimator.SetBool(Moving, _cmpCc.velocity.sqrMagnitude > 1f);

        if(Input.GetMouseButtonDown(0))
        {
            _cmpAnimator.SetTrigger(Attack);
        }
        else
        {
            _cmpAnimator.ResetTrigger(Attack);
        }
        if (Input.GetMouseButtonDown(1))
        {
            _cmpAnimator.SetTrigger(StrongAttack);
        }
        else
        {
            _cmpAnimator.ResetTrigger(StrongAttack);
        }
    }

    private void AttackAnimEvent()
    {
        weaponTrigger.enabled = true;
    }

    private void EndAttackAnimEvent()
    {
        weaponTrigger.enabled = false;
    }
}

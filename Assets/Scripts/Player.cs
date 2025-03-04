using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamageable, ITargeteable
{
    public float speed = 1.2f;

    public float jumpSpeed = 5;
    public float rotationSpeed;
    public float slidingSpeed;

    public float endJumpDistance = 1.8f;

    public const float GRAVITY = 9.8f;   

    private CharacterController cc;
    private Animator animator;

    private float velocityY;

    private bool isWaitingForJump;
    private bool isJumping;
    private bool isSliding;
    private bool dotIsDone;
    private Vector3 slidingVelocity;

    private bool useRootMotion;


    private float health = 80;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = GetInput();

        animator.SetFloat("zSpeed", input.z * speed);
        animator.SetFloat("xSpeed", input.x * speed);

        Vector3 localVelocity = GetLocalVelocity(GetInput());

        if (cc.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else if(!isJumping)
            {
                velocityY = -0.2f;
            }
        }


        if(isJumping && velocityY < 0)
        {
            if(Physics.Raycast(transform.position , Vector3.down , endJumpDistance ))
            {
                isJumping = false;
                animator.SetTrigger("endJump");
            }
        }




        velocityY -= GRAVITY * Time.deltaTime;

        SlideBehaviour();

        cc.Move(Vector3.up * velocityY * Time.deltaTime +
            localVelocity * Time.deltaTime
           + slidingVelocity * Time.deltaTime);

        Rotation();
    }


    private void OnAnimatorMove()
    {
        if(!isJumping && useRootMotion)
        {
            Vector3 rootMotionMove = animator.rootPosition - transform.position;
            Vector3 totalMovement = rootMotionMove +
                slidingVelocity * Time.deltaTime +
                Vector3.up * velocityY * Time.deltaTime;

            cc.Move(totalMovement);
            transform.rotation = animator.rootRotation;
        }
    }




    private void Rotation()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseXInput * rotationSpeed * Time.deltaTime, 0);
    }

    private void SlideBehaviour()
    {
        Vector3 maxSlidingVelocity = Vector3.zero;
        isSliding = false;
        if (cc.isGrounded && Physics.SphereCast(transform.position + cc.center, 0.2f, Vector3.down, out RaycastHit hitInfo))
        {
            Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.blue, 0.2f);
            Debug.DrawRay(hitInfo.point, Vector3.up, Color.blue, 0.2f);

            float angle = Vector3.Angle(hitInfo.normal, Vector3.up);

            Vector3 position = transform.position;
            position.x = 20;

            Transform myTransform = transform;
            myTransform.position = position;

            if (angle > cc.slopeLimit)
            {
                Vector3 slideDirection = Vector3.ProjectOnPlane(Vector3.down, hitInfo.normal).normalized;
                Debug.DrawRay(hitInfo.point, slideDirection, Color.green, 0.2f);

                isSliding = true;
                maxSlidingVelocity = slideDirection * slidingSpeed;
            }
        }

        slidingVelocity = isSliding ?
            Vector3.Lerp(slidingVelocity, maxSlidingVelocity, Time.deltaTime * 3) :
            Vector3.Lerp(slidingVelocity, Vector3.zero, Time.deltaTime * 5);
    }

    private void Jump()
    {
        isWaitingForJump = true;
       animator.SetTrigger("jump");
    }

    private Vector3 GetLocalVelocity(Vector3 input)
    {
        Vector3 velocity = input * speed;
        Vector3 localVelocity = transform.TransformVector(velocity);
        return localVelocity;
    }

    private static Vector3 GetInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(xInput, 0, zInput);
        if (input.magnitude > 1)
        {
            input.Normalize();
        }

        return input;
    }

    private void JumpAnimEvent()
    {
        isWaitingForJump = false;
        isJumping = true;
        velocityY = jumpSpeed;
    }


    //public abstract void CogerCohe();

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health= 0;
            //Reload scene 
        }
    }

    public void ApplyDotDamage(float damage, float time)
    {
        if (dotIsDone)
        {
            StartCoroutine(DamageOverTime(damage, time));
        }
    }

    private IEnumerator DamageOverTime(float damage, float time)
    {
        dotIsDone = false;
        var dotDelta = 1 / time;
        var totalTicks = time / 1;
        var damagePerTick = damage / totalTicks;
        float ticks = 0;

        while (ticks < totalTicks)
        {
            health -= damagePerTick;
            ticks++;
            yield return new WaitForSeconds(dotDelta);
        }
        
        dotIsDone = true;
    }
}

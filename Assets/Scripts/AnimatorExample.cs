using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExample : MonoBehaviour
{
    public float walkSpeed= 1.2f;
    public float runSpeed = 3.2f;
    public float currentSpeed;

    public float rotationSpeed = 90f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = GetInput();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, 4 * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, 4 * Time.deltaTime);
        }

        animator.SetFloat("zSpeed", input.z * currentSpeed);
        animator.SetFloat("xSpeed", input.x * walkSpeed);

        Rotation();
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

    private void Rotation()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseXInput * rotationSpeed * Time.deltaTime, 0);
    }
}

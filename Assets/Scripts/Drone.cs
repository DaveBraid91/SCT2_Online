using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Drone : MonoBehaviour
{
    public float speed = 6;
    public float rotationSpeed = 90;

    private CharacterController cc;
    private float velocityY;

    private bool hasPlayerControl = true;
    Vector3 localVelocity;
    private float GRAVITY = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerControl)
        {
            Vector3 maxLocalVelocity = GetLocalVelocity(GetInput());

            localVelocity = Vector3.MoveTowards(localVelocity, maxLocalVelocity, Time.deltaTime * 4);
        }
        else
        {
            ApplyGravity();
        }

        cc.Move(Vector3.up * velocityY * Time.deltaTime +
            localVelocity * Time.deltaTime); ;

        Rotation();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((cc.collisionFlags & CollisionFlags.Above) != 0)
        {
            print("Touching Ceiling!");
            hasPlayerControl = false;
        }
    }

    private void ApplyGravity()
    {
        velocityY -= GRAVITY * Time.deltaTime;
    }

    private static Vector3 GetInput()
    {
        float yInput = Input.GetAxis("Up");
        float zInput = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(0, yInput, zInput);
        if (input.magnitude > 1)
        {
            input.Normalize();
        }

        return input;
    }

    private Vector3 GetLocalVelocity(Vector3 input)
    {
        Vector3 velocity = input * speed;
        Vector3 localVelocity = transform.TransformVector(velocity);
        return localVelocity;
    }

    private void Rotation()
    {
        float mouseXInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, mouseXInput * rotationSpeed * Time.deltaTime, 0);
    }
}

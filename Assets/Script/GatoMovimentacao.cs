using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoMovimentacao : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public float speed;
    public float gravity;
    public float rotSpeed;
    private float rot;

    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if(characterController.isGrounded)
        {
            if(Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.forward * speed;
                animator.SetInteger("Transition", 1);
            }

            if(Input.GetKeyUp(KeyCode.W))
            {
                moveDirection = Vector3.zero;
                animator.SetInteger("Transition", 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
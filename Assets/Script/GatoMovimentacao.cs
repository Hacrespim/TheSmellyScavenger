using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoMovimentacao : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    public float speed;
    public float jumpForce;
    public float gravity;
    public float rotSpeed;
    public float interactionDistance;
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

        // Verifica a interação com objetos
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);

            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Interactable"))
                {
                    // faz algo com o objeto interagível
                    Debug.Log("Interagindo com " + col.name);
                }
                else if (col.CompareTag("Person"))
                {
                    // faz algo com a pessoa interagível
                    Debug.Log("Interagindo com " + col.name);
                }
            }
        }
    }

    void Move()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.forward * speed;
                animator.SetInteger("Transition", 1);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDirection = Vector3.zero;
                animator.SetInteger("Transition", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    // Desenha uma esfera de giz para indicar a distância de interação
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }

    // Verifica se o jogador entrou em contato com um objeto interagível ou uma pessoa
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            Debug.Log("Interagindo com " + col.name);
        }
        else if (col.CompareTag("Person"))
        {
            Debug.Log("Interagindo com " + col.name);
        }
    }
}
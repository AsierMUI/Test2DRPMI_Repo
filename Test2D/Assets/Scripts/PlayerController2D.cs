using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem; //libreria para que funcione el New Input System
public class PlayerController2D : MonoBehaviour
{
    //Ref a las antiguas inputs
    float horInput;

    //Referencias generales
    [SerializeField] Rigidbody2D playerRb; //Ref al rigidbody del player
    [SerializeField] PlayerInput playerInput; //Ref al gestor del input del jugador

    [Header("Movement Parameters")]
    public float speed;
    private Vector2 moveInput;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;


    void Start()
    {
        //autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    #region Input Events
    //Para crear un evento:
    //Se define PUBLIC sin tipo de dato (VOID) y con una referecia al input (Callback.Context)

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        }

    }



    #endregion




}

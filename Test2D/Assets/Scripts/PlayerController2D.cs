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
    [SerializeField] Animator playerAnim; //Ref al animator para gestionar las transiciones de animación


    [Header("Movement Parameters")]
    public float speed;
    private Vector2 moveInput; //Almacen del input del player
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;


    void Start()
    {
        //autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;
    }

    void Update()
    {
        HandleAnimations();
        //Flip
            if (moveInput.x > 0)
        {
            if (isFacingRight == false)
            {
                Flip();
            }
        }
            if(moveInput.x < 0)
        {
            if (isFacingRight == true)
            {
                Flip();
            }

        }

    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight; // nombre de bool = !nombre de bool (cambio al estado contrario)
    }

    void HandleAnimations()
    {
        //conector de valores generales con parámetros ed cambios de animación
        playerAnim.SetBool("IsJumping", !isGrounded);
        if (moveInput.x > 0 || moveInput.x < 0) playerAnim.SetBool("IsRunning", true);
        else playerAnim.SetBool("IsRunning", false);
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

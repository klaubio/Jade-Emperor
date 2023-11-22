using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;




    PlayerControls playercontrols;

    [Header("Movimiento")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("Camara")]
    [SerializeField] Vector2 cameraInput;
    public float cameraverticalInput;
    public float camerahorizontalInput;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        HandleMovementInput();
        HandleCameraMovementInput();
    }

    private void OnEnable()
    {
        if(playercontrols == null)
        {
            playercontrols = new PlayerControls();
            playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playercontrols.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

        }

        playercontrols.Enable();
    }



    private void HandleMovementInput()
    {
        verticalInput = movementInput.y; 
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if(moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1)
        {   
            moveAmount = 1;
        }
    }

    private void HandleCameraMovementInput()
    {
        cameraverticalInput = cameraInput.y;
        camerahorizontalInput = cameraInput.x;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerManager player;



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

  

    private void Start()
    {
        player = GetComponent<PlayerManager>();
        instance = this;
        player = FindObjectOfType<PlayerManager>();

    }

    public void Update()
    {
        HandleMovementInput();
        HandleCameraMovementInput();
    }

    public void OnEnable()
    {
        if(playercontrols == null)
        {
            playercontrols = new PlayerControls();
            playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playercontrols.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

        }

        playercontrols.Enable();
    }



    public void HandleMovementInput()
    {

        if (player == null)
        {
            Debug.LogError("Player is null!");
            return;
        }

        if (player.playerAnimatorManager == null)
        {
            Debug.LogError("PlayerAnimatorManager is null!");
            return;
        }






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

        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
    }

    public void HandleCameraMovementInput()
    {
        cameraverticalInput = cameraInput.y;
        camerahorizontalInput = cameraInput.x;
    }
    
}

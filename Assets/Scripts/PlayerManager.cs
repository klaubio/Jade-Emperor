using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerLocoMotionManager playercocomotionmanager;
    public PlayerAnimatorManager playerAnimatorManager;



    protected override void Awake()
    {
        base.Awake();
        playercocomotionmanager = GetComponent<PlayerLocoMotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

 


    public void Update()
    {
        playercocomotionmanager.HandleAllMovement();
    }

    public void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
    }
}

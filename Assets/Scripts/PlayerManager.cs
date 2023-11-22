using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerLocoMotionManager playercocomotionmanager;





    public void Awake()
    {
        playercocomotionmanager = GetComponent<PlayerLocoMotionManager>();
        
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

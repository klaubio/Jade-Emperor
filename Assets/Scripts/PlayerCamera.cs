using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Camera cameraObject;
    public PlayerManager player;
    public Transform cameraPivotTransform;

    [Header("Config CamerA")]
    private float cameraSmoothSpeed = 1;
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAnDownRotationSpeed = 220;
    [SerializeField] float minimunPivot = -30;
    [SerializeField] float maximunPivot = 60;
    [SerializeField] float cameraCollisionRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;


    [Header("Valores Camara")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAnDownLookAngle;
    private float cameraZposition;
    private float targetCameraZPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZposition = cameraObject.transform.localPosition.z;
    }




    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowPlayer();
            HandleRotations();
            HandleCollision();
        }
    }



    public void HandleFollowPlayer()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position,player.transform.position,ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }


    private void HandleRotations()
    {


        leftAndRightLookAngle += (PlayerInputManager.instance.camerahorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        upAnDownLookAngle -=(PlayerInputManager.instance.cameraverticalInput * upAnDownRotationSpeed) * Time.deltaTime;
        upAnDownLookAngle = Mathf.Clamp(upAnDownLookAngle, minimunPivot, maximunPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAnDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }



    private void HandleCollision()
    {
        targetCameraZPosition = cameraZposition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.transform.position, hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }

        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition,0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }




}

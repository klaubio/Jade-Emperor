using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController characterController;

  
    protected virtual void awake ()
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
    }

}

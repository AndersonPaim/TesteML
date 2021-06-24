using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector2 Movement;
    public float LookX;
    public float LookY;

    public ObjectsTag Arrow;

    public bool isJumping;
    public bool isRunning;
    public bool isWalking;
    public bool isAiming; 
    public bool isShooting; 
}
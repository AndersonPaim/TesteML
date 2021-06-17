using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector2 Movement;
    public float LookX;
    public float LookY;

    public objectsTag Arrow;

    public bool Jump;
    public bool Run;
    public bool Walk;
    public bool Aim; //TODO nomes
    public bool Shoot; 
}
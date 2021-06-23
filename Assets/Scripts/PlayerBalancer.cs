using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerBalancer", menuName = "PlayerBalancer")]
public class PlayerBalancer : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float generalSpeed;
    public float jumpForce;
    public float gravity;
    public float acceleration;
    public float deceleration;
    public float startAmmo;
    public float shootForce;
    public float health;

    public ObjectsTag[] arrows;
}
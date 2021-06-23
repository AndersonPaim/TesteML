using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyBalancer", menuName = "EnemyBalancer")]
public class EnemyBalancer : ScriptableObject
{
    public int speed;
    public float attackDistance;

    public float health;
    public float attackCooldown;
    public float destroyDelay;
    public float damage;
    public float dropChance; //item drop chance  1-100 value

    public Material[] armorTexture;

    public ObjectsTag[] itensDrop;
}
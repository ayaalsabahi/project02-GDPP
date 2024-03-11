using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New DollProperties", menuName = "Doll Properties")]
public class DollProperties : ScriptableObject
{
    public float moveSpeed;
    public float jumpStrength;

    public float gravity;
    public Sprite dollSprite;
    public RuntimeAnimatorController animatorController;
}
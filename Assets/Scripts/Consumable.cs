using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Consumables")]
public class Consumable : ScriptableObject
{
    public Sprite sprite;
    public float scorePoint;
    public float rotationSpeed;
}

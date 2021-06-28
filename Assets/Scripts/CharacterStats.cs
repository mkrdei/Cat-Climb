using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float Health  { get; set; }
    public bool Alive { get; set; }

    private void Start()
    {
        Health = 100;
        Alive = true;
    }
}

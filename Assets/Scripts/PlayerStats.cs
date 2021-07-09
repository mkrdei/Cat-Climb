using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Score { get; set; }
    public float Health  { get; set; }
    public bool Alive { get; set; }

    private void Awake()
    {
        Score = 0;
        Health = 100;
        Alive = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _damage, _damageWaitInterval;
    private float lastDamageTime;
    private bool triggerEntered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.name == "Player")
        {
            if (Time.time - lastDamageTime > _damageWaitInterval)
            {
                DealDamage(other.transform);
            }
        }
        
        
    }

    private void DealDamage(Transform player)
    {
        player.GetComponent<PlayerStats>().Health -= _damage;
        Debug.Log(_damage + " damage dealed.");
        lastDamageTime = Time.time;
    }


    
}

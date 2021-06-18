using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] private bool _active;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void InteractionAnimation()
    {
        anim.SetBool("active", _active);
    }
}

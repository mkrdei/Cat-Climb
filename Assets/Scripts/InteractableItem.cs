using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToAnimate;
    [SerializeField] private Animator[] _anims;
    private bool _interactable, _active, _focused;
    private GameObject _interactableBorderSprite, _interactableBorder;
    public Camera cam;
    
    // Start is called before the first frame update
    void Awake()
    {
        _interactableBorderSprite = Resources.Load<GameObject>("UI/interactableBorderUI");

        _interactable = true;
        _active = true;

        _anims = new Animator[objectsToAnimate.Length];
        for (int i = 0; objectsToAnimate.Length > i; i++)
        {
            _anims[i] = objectsToAnimate[i].GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionAnimation()
    {
        foreach (GameObject objectToAnimate in objectsToAnimate)
        {
            if(objectToAnimate.GetComponent<BoxCollider2D>()!=null)
                objectToAnimate.GetComponent<BoxCollider2D>().enabled = !_active;
        }
        foreach (Animator _anim in _anims)
        {
            _anim.SetBool("active", _active);
        }
        _active = !_active;
        
        
    }
    private void OnMouseDown()
    {
        if (_interactable)
        {
            Debug.Log("Interacted: " + _interactable);
            InteractionAnimation();
            _interactable = false;
        }
        
    }
    private void OnMouseUp()
    {
        _interactable = true;
    }

    private void OnMouseOver()
    {
        if (!_focused)
        {
            _interactableBorder = Instantiate(_interactableBorderSprite, transform);
            _interactableBorder.transform.position = transform.position;
            _focused = true;
        }
            
    }
    private void OnMouseExit()
    {
        Destroy(_interactableBorder);
        _focused = false;
    }


}

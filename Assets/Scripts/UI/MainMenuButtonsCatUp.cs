using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class MainMenuButtonsCatUp : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public Image cat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float buttonSize = 90;
        cat.transform.position = new Vector3(transform.position.x, transform.position.y + buttonSize, transform.position.z);
    }
}

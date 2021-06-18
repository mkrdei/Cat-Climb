using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    LineRenderer lr;
    private float _dragPower = 100f;
    private float _dragMultiplier = 5f;
    private float _maxDragMagnitude = 0.6f;
    private bool _dragging;
    public TextMeshProUGUI scoreText;
    private float score;
    private Vector2 _firstDrag, _finalDragPoint, _dragVector, _arrowFinalPoint;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Facing();

        
        

        
    }

    private void LateUpdate()
    {
        if (_dragging)
        {
            _arrowFinalPoint = _dragVector  + (Vector2)transform.position;
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, _arrowFinalPoint );
        }
        else
        {
            lr.positionCount = 0;
        }   
    }
    private void OnMouseDrag()
    {
        _dragging = true;
        _finalDragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, _finalDragPoint, Color.red);
        _dragVector = -(_finalDragPoint - (Vector2)transform.position)* _dragMultiplier;
        _dragVector = Vector3.ClampMagnitude(_dragVector, _maxDragMagnitude);
        
        
    }
    private void OnMouseUp()
    {
        _dragging = false;
        _finalDragPoint = Vector2.zero;
        rb.AddForce(_dragVector*_dragPower);
    }
    private void Facing()
    {
        //Facing while dragging.
        if (_finalDragPoint.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (_finalDragPoint.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        //Facing with velocity.
        if(Mathf.Abs(rb.velocity.x)>0.2)
        if (rb.velocity.x > 0 )
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Consumable")
        {
            
            score += collision.gameObject.GetComponent<ConsumableDisplay>().scorePoint;
            scoreText.text = "SCORE: " + score;
            Destroy(collision.gameObject);
            
        }
        if (Input.GetButtonDown("Interact") && collision.tag == "Interactable")
        {
            collision.gameObject.GetComponent<InteractableAnimation>().InteractionAnimation();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    LineRenderer lr;

    private float _dragPower = 100f;
    private float _dragMultiplier = 5f;
    private float _maxDragMagnitude = 0.6f;

    [SerializeField]
    private float risingAfterDeathSpeed = 2f;

    public Text scoreText;
    private float score;
    private Vector2 _firstDrag, _finalDragPoint, _dragVector, _arrowFinalPoint;
    private PlayerStats playerStats;
    private CircleCollider2D circleCollider2D;
    private Animator animator;
    private bool died, grounded, _dragging;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        playerStats = GetComponent<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging
        Debug.Log("Player is grounded: " + grounded);
        isGrounded();
    }

    private void LateUpdate()
    {
        Facing();

        // Health Control
        if (playerStats.Health <= 0)
        {
            playerStats.Alive = false;
            Die();
        }


        // Dragging and LineRenderer calculations and settings.
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
    
    private void Die()
    {
        circleCollider2D.enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position += Vector3.up * risingAfterDeathSpeed * Time.deltaTime;
        animator.SetBool("died", true);
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
    private void OnMouseDrag()
    {
        if (grounded)
        {
            _dragging = true;
            _finalDragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(transform.position, _finalDragPoint, Color.red);
            _dragVector = -(_finalDragPoint - (Vector2)transform.position) * _dragMultiplier;
            _dragVector = Vector3.ClampMagnitude(_dragVector, _maxDragMagnitude);
        }
    }
    private void OnMouseUp()
    {
        if (grounded)
        {
            _dragging = false;
            _finalDragPoint = Vector2.zero;
            rb.AddForce(_dragVector * _dragPower);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Consumable")
        {
            // This code will change due to security problems.
            playerStats.Score += collision.gameObject.GetComponent<ConsumableDisplay>().scorePoint;
            
            Destroy(collision.gameObject);
        }
        if (collision.transform.name == "Hamster")
        {
            animator.SetBool("love", true);
            UIController uIController = GameObject.Find("UIControllerObject").GetComponent<UIController>();
            uIController.ShowLevelPassedCanvas();
            Debug.Log("Level Passed.");
        }
    }

    private void isGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0f, Vector2.down, extraHeight);
        foreach(RaycastHit2D raycastHit in raycastHits)
        {
            if(raycastHit.collider.name != transform.name)
            {
                grounded = true;
                break;
            }
            else
            {
                grounded = false;
            }
            
        }
        
    }
}

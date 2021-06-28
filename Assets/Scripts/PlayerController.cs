using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    LineRenderer lr;

    private float _dragPower = 100f;
    private float _dragMultiplier = 5f;
    private float _maxDragMagnitude = 0.6f;

    [SerializeField]
    private float risingAfterDeathSpeed = 2f;

    private bool _dragging;
    public TextMeshProUGUI scoreText;
    private float score;
    private Vector2 _firstDrag, _finalDragPoint, _dragVector, _arrowFinalPoint;
    private CharacterStats characterStats;
    private CircleCollider2D circleCollider2D;
    private Animator animator;
    private bool died;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        characterStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
        if (characterStats.Health <= 0)
        {
            characterStats.Alive = false;
            Die();
        }
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
        if (isGrounded())
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
        if (isGrounded())
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
            
            score += collision.gameObject.GetComponent<ConsumableDisplay>().scorePoint;
            scoreText.text = "SCORE: " + score;
            Destroy(collision.gameObject);
            
        }
    }

    private bool isGrounded()
    {
        bool isGrounded = false;
        float extraHeight = 0.1f;
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0f, Vector2.down, extraHeight);
        foreach(RaycastHit2D raycastHit in raycastHits)
        {
            isGrounded = raycastHit.collider.name != transform.name && raycastHit.collider != null;
            Debug.Log("Player Grounded: " + isGrounded);
        }
        
        return isGrounded;
    }
}

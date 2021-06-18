using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableDisplay : MonoBehaviour
{
    public float scorePoint;
    public Consumable consumable;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = consumable.sprite;

        
    }
    private void Start()
    {
        scorePoint = consumable.scorePoint;

        _boxCollider = GetComponent<BoxCollider2D>();

        // We equal sprite size to boxcollider size at start. Dividing to 2 because it looks better.
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        _boxCollider.size = spriteSize/2;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * consumable.rotationSpeed);
    }
    
}

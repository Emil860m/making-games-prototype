using System;
using Unity.VisualScripting;
using UnityEngine;

public class wallController : MonoBehaviour
{
    
    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void ChangeDisableCollider()
    {
        boxCollider.enabled = !boxCollider.enabled;
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnCollisionEnter(Collision other)
    {
        ChangeSprite();
    }
}

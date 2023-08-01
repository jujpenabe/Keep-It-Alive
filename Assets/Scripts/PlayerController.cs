using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public BoxCollider2D attackCollider;
    public Transform lantern;
    
    public float horForce = 1f;
    public float verForce = 10f;
    
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    private Vector3 targetPosition;

    
    private bool inAir;
    private float xDir;
    private float yDir;
    
    private bool _qPressed;
    private bool _wPressed;
    private bool _ePressed;
    private bool _eReleased;
    private bool _rPressed;
    private bool _inAtack;
    private bool _canPlay = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        attackCollider = attackCollider.GetComponent<BoxCollider2D>();
    }
    
    void Start()
    {
        inAir = true;
        StartCoroutine(GetControl(4f));
    }

    private void Update()
    {
        if (!_canPlay) return;
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        _qPressed = Input.GetButtonDown("Fire1");
        _wPressed = Input.GetButtonDown("Fire2");
        _ePressed = Input.GetButtonDown("Fire3");
        _eReleased = Input.GetButtonUp("Fire3");
        _rPressed = Input.GetButtonDown("Fire4");
        
        // Abilities
        if (_inAtack) return;
        if (_qPressed)
        {
            Debug.Log("Q ability pressed");
            anim.SetTrigger("AbilityQ");
            StartCoroutine(Attack());        
            return; 
        }
        if (_wPressed)
        {
            Debug.Log("W ability pressed");
            anim.SetTrigger("AbilityW");
        
            return;
        }
        if (_ePressed)
        {
            Debug.Log("E ability pressed");
            anim.SetTrigger("AbilityE");
            anim.SetBool("Defending", true);
        
            return;
        }
        if (_eReleased)
        {
            Debug.Log("E ability released");
            anim.SetBool("Defending", false);
            return;
        }
        if (_rPressed)
        {
            Debug.Log("R ability pressed");
            anim.SetTrigger("AbilityR");
        
            return;
        }
    }
    
    private void FixedUpdate()
    {
        if (yDir >= 0f)
        {
            anim.SetBool("Crawling", false);
        }
        else
        {
            if (inAir)
            {
                rb.AddForce(new Vector2(0, yDir * verForce * 0.5f), ForceMode2D.Impulse);
            } else {
                anim.SetBool("Crawling", true);
            }
            
        }
        if (xDir != 0)
        {
            anim.SetBool("Running", true);
            if (!inAir)
            {
                rb.AddForce(new Vector2(xDir * horForce, 0), ForceMode2D.Impulse);
            }
            else if (yDir < -0.1f && !inAir)
            {
                rb.AddForce(new Vector2(xDir * horForce * 0.8f, 0), ForceMode2D.Impulse);
            }
            else {
                rb.AddForce(new Vector2(xDir * horForce * 0.6f, 0), ForceMode2D.Impulse);
            }
            rb.velocity = new Vector2(xDir * horForce, rb.velocity.y);
            sr.flipX = xDir < 0;
            if (xDir < -0.1f)
            {
                sr.flipX = true;
                attackCollider.offset = new Vector2(-1.5f, 0);
                lantern.transform.rotation = Quaternion.Euler(0,0,90);
            } else if (xDir > 0.1f)
            {
                sr.flipX = false;
                attackCollider.offset = new Vector2(1.5f, 0);
                lantern.transform.rotation = Quaternion.Euler(0,0,270);
            }
            
        }
        else
        {
            anim.SetBool("Running", false);
        }
        if (yDir > 0 && !inAir)
        {
            rb.AddForce(new Vector2(0, verForce), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);
            inAir = true;
        }
        // Check rb vertical velocity if inAir and set anim bool to Falling if negative, Jumping otherwise
        if (inAir)
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
            else
            {
                anim.SetBool("Falling", false);
                anim.SetBool("Jumping", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            inAir = false;
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            inAir = true;
        }
    }
    
    public void Teleport()
    {
        Debug.Log("Teleport");
        transform.position = targetPosition;
    }
    IEnumerator Attack()
    {
        _inAtack = true;
        yield return new WaitForSeconds(0.2f);
        var enabled1 = attackCollider.enabled;
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = false;
        _inAtack = false;
    }
    
    IEnumerator GetControl(float time)
    {
        yield return new WaitForSeconds(time);
        _canPlay = true;
    }
    
    public void SetXTargetPosition(float x){
        targetPosition = new Vector3(x, targetPosition.y, targetPosition.z);
    }
    public void SetYTargetPosition(float y){
        targetPosition = new Vector3(targetPosition.x, y, targetPosition.z);
    }
    public void SetZTargetPosition(float z){
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, z);
    }
}

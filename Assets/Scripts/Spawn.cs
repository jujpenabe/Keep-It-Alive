using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float alpha=0f;
    
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        sr.color = new Color(1f,1f,1f,0f);
        StartCoroutine(Appear());
    }
    IEnumerator Appear(){
        yield return new WaitForSeconds(2f);
        rb.AddForce(new Vector2(15, 40),ForceMode2D.Impulse);
        while (alpha<1){
            yield return null;
            alpha += 0.02f;
            sr.color = new Color(1f,1f,1f,alpha);
        }
    }

}

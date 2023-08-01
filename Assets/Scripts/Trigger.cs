using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool destroyOnTriggerEnter;
    [SerializeField] string tagFilter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    
    void OnTriggerEnter2D(Collider2D other){
        
        if(!String.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
        if (destroyOnTriggerEnter) Destroy(gameObject);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
    }
}

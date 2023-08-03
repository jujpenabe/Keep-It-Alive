using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampPosition : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float speed = 100f;
    
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.localPosition;
    }
    void Update()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, orb.position, plerp);
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

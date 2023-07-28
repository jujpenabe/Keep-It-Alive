using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
public Transform PocisionCamara;
public float plerp = 0.5f;
void Update(){
	transform.position = Vector3.Lerp(transform.position, PocisionCamara.position, plerp);
}
}

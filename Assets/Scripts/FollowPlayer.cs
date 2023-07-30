using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
public Transform PocisionCamara;
public float plerp = 0f;

private Camera cam;


void Awake() {

	cam = GetComponent<Camera>();
}
void Start() {
	StartCoroutine(WaitToFollow());
}
void Update(){
	transform.position = Vector3.Lerp(transform.position, PocisionCamara.position, plerp);
}

IEnumerator WaitToFollow(){
	yield return new WaitForSeconds(3f);
	StartCoroutine(IncreasePlerp());
	while (cam.orthographicSize < 10f) {
		yield return null;
		cam.orthographicSize += 0.01f;
		
	}
}
IEnumerator IncreasePlerp(){

	while (plerp<0.1f) {
		plerp += 0.002f;
	yield return null;
	}
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjoController : MonoBehaviour
{
	private Transform orbe;
	public bool enRango = false;
	public float plerp = 0.015f;
    // Update is called once per frame
	void Start(){
		orbe = GameObject.FindGameObjectWithTag("Orbe").GetComponent<Transform>();
	}
    void Update()
    {
		if(enRango)
        transform.position = Vector3.MoveTowards(transform.position, orbe.position, plerp);
    }
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Orbe")){
			Debug.Log("Colision con Orbe");
			enRango = true;
		}
	}
}

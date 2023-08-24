using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OjoController : MonoBehaviour
{
	public bool enRango = false;
	public Vector3 velocity = Vector3.zero;
	
	public float speed = 100f;
	
	private Transform orb;
    // Update is called once per frame
	void Start(){
		orb = GameObject.FindGameObjectWithTag("Orb").GetComponent<Transform>();
	}
    void Update()
    {
	    // Move Smoothly to the Orb with smoothdamp
	    if (enRango)
	    {
		    // Evades de character for 1 second and then attacks
		    transform.position = Vector3.SmoothDamp(transform.position, orb.position, ref velocity, speed * Time.deltaTime);
	    }

	    //transform.position = Vector3.MoveTowards(transform.position, orb.position, plerp);
    }
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			Debug.Log("Colision con Orbe");
			enRango = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueQ : MonoBehaviour
{
	private OrbeController orbController;
	
	private void Awake()
	{
		orbController = GameObject.FindGameObjectWithTag("Orb").GetComponent<OrbeController>();
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Enemigo")){
			Debug.Log("Colision con Enemigo");
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("LightOrb"))
		{
			orbController.Heal();
			Destroy(other.gameObject);
		}
		
	}
}

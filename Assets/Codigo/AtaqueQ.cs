using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueQ : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Enemigo")){
			Debug.Log("Colision con Enemigo");
			Destroy(other.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbeController : MonoBehaviour
{
	//public Volume volume;
	public int vida;
    void Start(){
        vida = 10;
    }
    void Update(){
    }
	//Funcion de colision con enemigo
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Enemigo")){
			vida--;
			Debug.Log("Vida: " + vida);
			Destroy(other.gameObject);
			if(vida <= 0){
				Destroy(gameObject);
			}

		}
	}
}

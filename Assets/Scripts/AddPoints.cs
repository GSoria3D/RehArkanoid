using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddPoints : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        //Si se ha roto el bloque
        if (GetComponent<DissapearBlock>().isBroken) {
            //Aumentamos los puntos
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerScript>().addPoints(10);
        }
        
    }
}

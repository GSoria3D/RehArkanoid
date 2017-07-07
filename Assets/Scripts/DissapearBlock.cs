using UnityEngine;
using System.Collections;

public class DissapearBlock : MonoBehaviour {

    public bool isBroken;
    // Use this for initialization
    void Start () {
        isBroken = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter()
    {
        //Hacemos desaparecer el bloque que ha colisionado con la bola
        isBroken = true;
        Destroy(this.gameObject);
    }
}

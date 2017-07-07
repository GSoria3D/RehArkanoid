using UnityEngine;
using System.Collections;

public class LooseLife : MonoBehaviour {

    public GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        //Si la bola ha chocado contra la pared
        GameObject b;

        //Destruimos la antigua bola y creamos una nueva
        Destroy(other.gameObject);
        b =(GameObject) Instantiate(ball, new Vector3(0, 0.6f, 0), new Quaternion());
        b.GetComponent<BallScript>().enabled = true;
        ball = b;

        //Restamos una vida
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerScript>().subLife();
    }
}

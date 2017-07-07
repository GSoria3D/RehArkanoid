using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector3 ballInitialForce;

    public GameObject playerObject;

    public GameObject wallDown;

    public AudioClip pong, breakBlock,loose;

    private AudioSource source;

    // Use this for initialization
    void Start () {
        // Aplicamos una fuerza inicial
        ballInitialForce = new Vector3(10.0f,0, 14.0f);

        // Desactivamos la bola
        ballIsActive = false;

        // Almacenamos la posicion de la bola
        ballPosition = transform.position;

        source = GetComponent<AudioSource>();
        source.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (!ballIsActive)
        {
            // Añadir la fuerza
            GetComponent<Rigidbody>().velocity = new Vector3(ballInitialForce.x, ballInitialForce.y, ballInitialForce.z);
            // Activamos la bola
            ballIsActive = !ballIsActive;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
            source.PlayOneShot(breakBlock);
        else if (coll.gameObject.tag == "Wall")
            source.PlayOneShot(pong);
        else
            source.PlayOneShot(loose);

    }
}

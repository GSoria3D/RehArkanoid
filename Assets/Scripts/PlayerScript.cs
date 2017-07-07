using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class PlayerScript : MonoBehaviour {

    //Objeto lector de los puertos series
    SerialPort stream;

    //Puntos del jugador
    int points = 0;

    //Vector que almacenara los valores leidos desde el puerto serie
    string[] values;

    //Partida pausada
    bool paused;

    //Texturas usadas para indicar si quedan vidas
    public Texture2D life, noLife;

    //Numero de vidas restantes
    int numberLife = 3;

    //Estilo aplicado al GUI
    public GUISkin skin;

    //Factor de movimiento para multiplicarlo segun la dificultad
    float movementFactor = 0.06f;


    //Indica si el jugador ha ganado
    bool end = false;

    public GameObject paredIzquierda, paredDerecha;

    bool error;

    float timeError;

	// Use this for initialization
	void Start () {

        //Inicio y apertura del puerto serie
        stream = new SerialPort("COM3", 9600);
        stream.Open();
        
        //Iniciamos las variables de la partida
        points = 0;
        paused = true;

        //Paramos el juego
        Time.timeScale = 0f;

        //Aumentamos la dificultad del movimiento segun el nivel
        movementFactor -= GameObject.FindGameObjectWithTag("Configuration").GetComponent<UIScript>().level * 0.002f;
        this.transform.localScale = new Vector3(this.transform.localScale.x - GameObject.FindGameObjectWithTag("Configuration").GetComponent<UIScript>().level * 0.1f, 1, 1);

        error = false;
    }

    void OnGUI()
    {

        //Introducimos el estilo
        GUI.skin = skin;

        //Partida ganada
        if (end == true)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "YOU WIN");
            Time.timeScale = 0f;
        }
        else
        {
            //Si el numero de vidas es mayor que 0
            if (numberLife > 0)
            {
                //Mostramos la puntacion del jugador
                GUI.Label(new Rect(20, 20, 100, 100), points.ToString());
                if (error)
                {
                    GUI.Label(new Rect(100,100,200,200), "No bajes la muñeca!");
                    if (timeError + 5 < Time.time)
                        error = false;
                }
            }
            else
            {
                //Partida perdida
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "YOU LOSE");
                Time.timeScale = 0f;
            }

            //Dibujamos tantas vidas como vidas le resten al jugador
            for (int i = 0; i < numberLife; i++)
                GUI.Label(new Rect(Screen.width - i * 64 - 3 * 64, 0, 64, 64), life);
            for (int i = numberLife; i < 3; i++)
                GUI.Label(new Rect(Screen.width - i * 64 - 3 * 64, 0, 64, 64), noLife);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Leemos desde el puerto serial
        string value = stream.ReadLine();

        //Si hay algo que leer
        if (value != "" && end == false) {

            //Partimos los valores
            values = value.Split(';');

            //Si el giro de la mano respecto al eje y esta entre 300 y 310
            if (float.Parse(values[1]) > 300 && float.Parse(values[1]) < 310)
            {
                //Pausamos la partida
                Time.timeScale = 0f;
                paused = true;

                //Si el jugador ha perdido la partida
                if (numberLife == 0 || end == true)
                    //Creamos de nuevo el nivel
                    newLevel();
            }
            else
            {
                //Si el giro de la mano respecto al eje y esta entre 50 y 75
                if (float.Parse(values[1]) > 50 && float.Parse(values[1]) < 75)
                {
                    //Continuamos la partida
                    Time.timeScale = 1f;
                    paused = false;
                }
                //Si el juego no esta pausado
                if (!paused)
                {
                    //Almacenamos el valor de la mano respecto al eje x
                    float movement = float.Parse(values[0]);

                    //Si es mayor a 270
                    if (movement >= 270)
                        //Restamos 360 al valor para normalizar los valores entre -90 y 90
                        movement = movement - 360;
                    if ((float.Parse(values[1]) > 40 && float.Parse(values[1]) < 50) || (float.Parse(values[1]) < 340 && float.Parse(values[1]) > 310))
                    {
                        points -= 5;
                        error = true;
                        timeError = Time.time;
                    }

                    if (gameObject.GetComponent<Collider>().bounds.max.x + (-movement * movementFactor) < paredDerecha.GetComponent<Collider>().bounds.min.x)
                    {
                        if (gameObject.GetComponent<Collider>().bounds.min.x + (-movement * movementFactor) > paredIzquierda.GetComponent<Collider>().bounds.max.x)
                            //Trasladamos el jugador en el eje x
                            transform.Translate((-movement * movementFactor), 0, 0);
                        else
                            transform.position = new Vector3(paredIzquierda.GetComponent<Collider>().bounds.max.x + gameObject.GetComponent<Collider>().bounds.size.x/2, transform.position.y, transform.position.z);
                    }
                    else
                        transform.position = new Vector3(paredDerecha.GetComponent<Collider>().bounds.min.x - gameObject.GetComponent<Collider>().bounds.size.x/2, transform.position.y, transform.position.z);
                }
                
            }
        }
    }

    public void addPoints(int p)
    {
        //Sumar puntos
        this.points += p;
    }

    public void subLife()
    {
        //Restar vida
        numberLife--;
    }

    void newLevel()
    {
        //Buscamos todos los bloques
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        
        //Por cada bloque
        foreach (GameObject g in gos)
            //Destruimos el bloque
            Destroy(g);

        //Cargamos las vidas del jugador y reiniciamos los puntos
        numberLife = 3;
        points = 0;
        end = false;
        //Creamos de nuevo los bloques desde el script almacenado en la camara
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CreateBlocks>().createBlocks();
    }

    public void win()
    {
        end = true;
    }
}

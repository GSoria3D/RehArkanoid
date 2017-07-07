
//Pines a los que estan conectados el Arduino
const int xPin = 0;
const int yPin = 1;
const int zPin = 2;

//Valor minimo y maximo que ofrece el acelerometro
int minVal = 265;
int maxVal = 402;


//Variables que almacenan los valores calculados
double x;
double y;
double z;

unsigned long t;

String coords;
void setup(){
  Serial.begin(9600);
  t = millis();
  coords = String();
}


void loop(){
  //Cada 0,05 segundos
  if(t+50 < millis())
  {
    //Leemos el sensor
    int xRead = analogRead(xPin);
    int yRead = analogRead(yPin);
    int zRead = analogRead(zPin);
  
    //Convertimos los valores entre -90 y 90
    int xAng = map(xRead, minVal, maxVal, -90, 90);
    int yAng = map(yRead, minVal, maxVal, -90, 90);
    int zAng = map(zRead, minVal, maxVal, -90, 90);
  
    //Pasamos el valor entre 0 y 360
    x = RAD_TO_DEG * (atan2(-yAng, -zAng) + PI);
    y = RAD_TO_DEG * (atan2(-xAng, -zAng) + PI);
    z = RAD_TO_DEG * (atan2(-yAng, -xAng) + PI);
  
    //Calculamos las coordenadas y las imprimimos por el puerto serial
    coords = String() + x + ';' + y + ';' + z;

    Serial.println(coords);
    t=millis();
  }
}

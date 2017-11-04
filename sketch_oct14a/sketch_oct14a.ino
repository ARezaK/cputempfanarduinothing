int PS_PIN = 3; // for turning on 2nd psu when arduino gets turned on

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600); // set the baud rate
  Serial.println("Ready"); // print "Ready" once
}

void loop() {
    pinMode(PS_PIN, INPUT); // set Power supply pin to output to turn on, input to turn off. Auto turn on
    char inByte = ' ';
    if(Serial.available()){ // only send data back if data has been sent
      char inByte = Serial.read(); // read the incoming data
      Serial.println(inByte); // send the data back in a new line so that it is not all one long line
      if(inByte == 49) { // ASCII printable characters: 49 means number 1
        pinMode(PS_PIN, OUTPUT); // set Power supply pin to output to turn on
      }
      else if(inByte == 48) { // ASCII printable characters: 48 means number 0
        pinMode(PS_PIN, INPUT); // set Power supply pin to output to turn on
      }
      delay(1000); // delay for 1/10 of a second    
    }
}

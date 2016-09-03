// Touch sensor is connected to sensorport 1

MotorA.On(100);
MotorB.On(100);
while(!Sensor1.isPressed())
{
}
MotorA.Off();
MotorB.Off();
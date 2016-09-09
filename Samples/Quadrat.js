for(var i = 0; i < 4; i++)
{
	MotorA.On(75);
	MotorB.On(75);

	delay(5000);

	MotorB.On(-75);

	delay(500);
}

MotorA.Off();
MotorB.Off();
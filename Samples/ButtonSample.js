var exit = false;
var speed = 0;
var oldSpeed = 0;

MotorA.Off();
MotorB.Off();

Buttons.Up(function() {
	log("Up");
	speed += 10;
	if(speed > 100) speed = 100;
});

Buttons.Down(function() {
	log("Down");
	speed -= 10;
	if(speed < -100) speed = -100;
});

Buttons.Escape(function() {
	log("Escape");
	exit = true;
});
Buttons.Enter(function () {
	speed = 0;
});

log("Exit is: ");
log(exit);

while(!exit)
{
	if(speed != oldSpeed)
	{
		MotorA.On(speed);
		MotorB.On(speed);	
		oldSpeed = speed;
	}
	delay(100);
}
MotorA.Off();
MotorB.Off();
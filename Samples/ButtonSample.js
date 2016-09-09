var exit = false;
var speed = 0;

MotorA.Off();
MotorB.Off();

Buttons.Up(function() {
	speed += 10;
	if(speed > 100) speed = 100;
});

Buttons.Down(function() {
	speed -= 10;
	if(speed < -100) speed = -100;
});

Buttons.Escape(function() {
	exit = true;
});

while(!exit)
{
	MotorA.On(speed);
	MotorB.On(speed);
}
MotorA.Off();
MotorB.Off();
var exit = false;
Buttons.Escape(function () {
	exit = true;
});

Sensor4.setMode(Mode.Color.Ambient);

while(!exit)
{
	var data = Sensor4.Read();
	log(data);
	Display.WriteLine(data);
	delay(100);
}
function WaitUntil(expression)
{
	while(!expression())
	{
		delay(100);
	}
}

Display.WriteLine("Waiting for Sensor.")
Sensor4.setMode(Mode.Color.Ambient);
WaitUntil(function() {
	var value = Sensor4.Read();
	Display.WriteLine(value);
	return value > 30;
});
Display.WriteLine("Done!");
delay(2000);
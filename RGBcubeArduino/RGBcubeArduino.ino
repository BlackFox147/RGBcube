#include <SD.h>
#include <Tlc5940.h>;

int dim = 1;

const int PIN_CHIP_SELECT = 4;
int latchPin = 5; //ST_CP 8
int clockPin = 7; //SH_CP 10
int dataPin = 6; //DS 9

int Rgb[192];
int position = 0;

File myFile;

void setup()
{
	pinMode(latchPin, OUTPUT);
	pinMode(clockPin, OUTPUT);
	pinMode(dataPin, OUTPUT);

	pinMode(10, OUTPUT);
	Tlc.init();
	Tlc.clear();
	Tlc.update();

	if (!SD.begin(PIN_CHIP_SELECT))
	{
		for (int i = 0; i < 192; i += 3) {
			Rgb[i] = 4000;
		}
		return;
	}
}



void loop()
{
	for (int i = 1; i < 256; i *= 2) {

		myFile = SD.open("1.txt");
		if (myFile)
		{
			if (myFile.available()) {
				myFile.seek(0);
				position = 0;
			}
			//Serial.println("read...");
			myFile.seek(position);
			int i = 0;
			while (i < 192 && myFile.available() && position < myFile.size())
			{
				int temp = myFile.read();
				position = myFile.position();

				if (temp != '/') {
					int sum = 0;
					while (temp != ' ') {
						int number = temp - 48;
						sum = sum * 10 + number;
						temp = myFile.read();
					}
					Rgb[i] = sum;
					i++;
				}

			}
			// закрываем файл: 
			myFile.close();
		}

		Tlc.clear();
		for (int channel = 0; channel < 192; channel++) {
			//”станавливаем максимальную €ркость
			Tlc.set(channel, Rgb[channel]);
			//Tlc.set(channel + 1, rgb[1]);
			//Tlc.set(channel + 2, rgb[2]);
		}

		digitalWrite(latchPin, LOW);
		// передаем последовательно на вход данных
		shiftOut(dataPin, clockPin, MSBFIRST, 255 - i);
		Tlc.update();

		digitalWrite(latchPin, HIGH);
	}

	delay(1000);
	//// гасим красный, параллельно разжигаем зеленый
	//for (int i = 4095; i >= 0; i -= 16) {
	//	Tlc.set(0, i / dim);
	//	Tlc.set(1, (4095 - i) / dim);
	//	Tlc.update();
	//	delay(10);
	//}
	//// гасим зеленый, параллельно разжигаем синий
	//for (int i = 4095; i >= 0; i -= 16) {
	//	Tlc.set(1, i / dim);
	//	Tlc.set(2, (4095 - i) / dim);
	//	Tlc.update();
	//	delay(10);
	//}
	//// гасим синий, параллельно разжигаем красный
	//for (int i = 4095; i >= 0; i -= 16) {
	//	Tlc.set(2, i / dim);
	//	Tlc.set(0, (4095 - i) / dim);
	//	Tlc.update();
	//	delay(10);
	//}
}


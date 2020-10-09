
// Main.cpp
#include <iostream>
#include "Socket.h"
using namespace std;
int main()
{
	int nChoice;
	int port = 25252; // вибираємо порт
	string ipAddress; // Адреса сервера
	char receiveMessage[MAXSTRLEN];
	char sendMessage[MAXSTRLEN];
	cout << "1) Start server" << endl;
	cout << "2) Connect to server" << endl;
	cout << "3) Exit" << endl;
	cin >> nChoice;
	if (nChoice == 1)
	{
		ServerSocket server;
		cout << "Starting server ..." << endl;
		// Запускаємо сервер
		server.StartHosting(port);
		while (true)
		{
			cout << "\tWaiting ..." << endl;
			// Отримуємо дані від клієнта і зберігаємо в змінній receiveMessage
			server.ReceiveData(receiveMessage, MAXSTRLEN);
			cout << "Received:" << receiveMessage <<
				endl;
			// Відправляємо дані клієнта
			server.SendDataMessage();
			// Якщо є повідомлення "end", завершуємо
			// роботу
			if (strcmp(receiveMessage, "end") == 0 ||
				strcmp(sendMessage, "end") == 0)
				break;
		}
	}
	else if (nChoice == 2)
	{
		cout << "Enter an IP address:" << endl;
		// запитуваний IP сервера
		cin >> ipAddress;
		ClientSocket client;
		// підключаємося до сервера
		client.ConnectToServer(ipAddress.c_str(), port);
		while (true)
		{
			// відправляємо повідомлення
			client.SendDataMessage();
			cout << "\tWaiting" << endl;
			// отримуємо відповідь
			client.ReceiveData(receiveMessage, MAXSTRLEN);
			cout << "Received:" << receiveMessage <<
				endl;
			if (strcmp(receiveMessage, "end") == 0 ||
				strcmp(sendMessage, "end") == 0)
				break;
		}
		// Закриваємо з'єднання
		client.CloseConnection();
	}
	else if (nChoice == 3)
		return 0;
}
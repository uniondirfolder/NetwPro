#pragma once
#include <iostream>
#include <string>
#include "WinSock2.h" // тут є оголошення для Winsock 2 API.
#include <ws2tcpip.h> // містить функції для роботи з адресами наприклад inet_pton
#pragma comment (lib, "Ws2_32.lib") // лінкуєм бібліотеку Windows Sockets
using namespace std;
const int MAXSTRLEN = 255;
class Socket
{
protected:
	WSADATA wsaData; // структура для зберігання інформації про ініціалізації сокетів
	SOCKET _socket; // дескриптор слухача сокета
	SOCKET acceptSocket; // дескриптор сокета, який пов'язаний з клієнтом
	sockaddr_in addr; // локальна адреса і порт
public:
	Socket();
	~Socket();
	bool SendData(char*); // метод для відправки даних в мережу
	bool ReceiveData(char*, int); // метод для отримання даних
	void CloseConnection(); // метод для закриття з'єднання
	void SendDataMessage(); // метод для відправки повідомлення користувача
};
class ServerSocket : public Socket
{
public:
	void Listen(); // метод для активації "слухача" сокета
	void Bind(int port); // метод для прив'язки сокета до порта
	void StartHosting(int port); // об'єднує виклик двох попередніх методів
};
class ClientSocket : public Socket
{
public:
	// метод для підключення до сервера
	void ConnectToServer(const char* ip, int port);
};
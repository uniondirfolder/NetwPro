//Socket.cpp:
#include "Socket.h"
Socket::Socket()
{
	// якщо ініціалізація сокетів пройшла неуспішно,
	// виводимо повідомлення про помилку
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != NO_ERROR)
	{
		cout << "WSAStartup error \n";
		system("pause");
		WSACleanup();
		exit(10);
	}

	 // створюємо потоковий сокет, протокол TCP
	_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	// при неуспішному створенні сокета виводимо повідомлення про помилку
	if (_socket == INVALID_SOCKET)
	{
		cout << "Socket create error." << endl;
		system("pause");
		WSACleanup();
		exit(11);
	}
}
Socket :: ~Socket()
{
	WSACleanup(); // очищаємо ресурси
}
bool Socket::SendData(char* buffer)
{
	/* Відправляємо повідомлення на вказаний сокет */
	send(_socket, buffer, strlen(buffer), 0);
	return true;
}
bool Socket::ReceiveData(char* buffer, int size)
{
	/* Отримуємо повідомлення і зберігаємо його в буфері.
	Метод є блокуючим! */
	int i = recv(_socket, buffer, size, 0);
	buffer[i] = '\0';
	return true;
}
void Socket::CloseConnection()
{
	// Закриваємо сокет
	closesocket(_socket);
}
void Socket::SendDataMessage()
{
	// Рядок для повідомлення користувача
	char message[MAXSTRLEN];
	// Без цього методу з потоку буде зчитаний
	// останній рядок від користувача, виконуємо скидання.
	cin.ignore();
	cout << "Input message:";
	cin.get(message, MAXSTRLEN);
	SendData(message);
}
void ServerSocket::StartHosting(int port)
{
	Bind(port);
	Listen();
}
void ServerSocket::Listen()
{
	cout << "Waiting for client ..." << endl;
	// При помилці активації сокета в режимі прослуховування
	// виводимо помилку
	if (listen(_socket, 1) == SOCKET_ERROR)
	{
		cout << "Listening error \n";
		system("pause");
		WSACleanup();
		exit(15);
	}
	/*
	Метод є блокуючим, очікуємо підключення
	клієнта.
	Як тільки клієнт підключився, функція accept
	повертає новий сокет, через який
	відбувається обмін даними.
	*/
	acceptSocket = accept(_socket, NULL, NULL);
	while (acceptSocket == SOCKET_ERROR)
	{
		acceptSocket = accept(_socket, NULL, NULL);
	}
	_socket = acceptSocket;
}
void ServerSocket::Bind(int port)
{
	// Вказуємо сімейство адрес IPv4
	addr.sin_family = AF_INET;
	/* Перетворимо адресу "0.0.0.0" в правильну
	структуру зберігання адрес, результат
	зберігаємо в поле sin_addr */
	inet_pton(AF_INET, "0.0.0.0", &addr.sin_addr);
	// Вказуємо порт.
	// Функіця htons виконує перетворення числа в
	// мережевий порядок байт
	addr.sin_port = htons(port);
	cout << "Binding to port:" << port << endl;
	// При невдалому біндінгу до порта, виводимо
	// повідомлення про помилку
	if (bind(_socket, (SOCKADDR*)&addr,
		sizeof(addr)) == SOCKET_ERROR)
	{
		cout << "Failed to bind to port \ n";
		system("pause");
		WSACleanup();
		exit(14);
	}
}
void ClientSocket::ConnectToServer(const char* ipAddress,
	int port)
{
	addr.sin_family = AF_INET;
	inet_pton(AF_INET, ipAddress, &addr.sin_addr);
	addr.sin_port = htons(port);
	// при невдалому підключенні до сервера виводимо
	// повідомлення про помилку
	if (connect(_socket, (SOCKADDR*)&addr,
		sizeof(addr)) == SOCKET_ERROR)
	{
		cout << "Failed to connect to server \ n";
		system("pause");
		WSACleanup();
		exit(13);
	}
}
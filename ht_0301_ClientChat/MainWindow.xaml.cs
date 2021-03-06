﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ht_0301_ClientChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socketClient;
        private string userName;
        public string UserName
        {
            get => userName;
            set { userName = value; Title = userName; }
        }
        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuItems();
            WriteLogin();
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            if (socketClient != null)
            {
                socketClient.Close();
            }
            StatusConnection.Text = "Connected...";
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 10000);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socketClient.Connect(endPoint);
                if (socketClient.Connected)
                {
                    StatusConnection.Text = "Connected";
                    SendMessage($"{UserName}:connect");
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                string message = Encoding.UTF8.GetString(ReceiveAll());

                                if (!String.IsNullOrWhiteSpace(message))
                                {
                                    Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        AddMessage(message);
                                        ConnetctedMenuItem.IsEnabled = !socketClient.Connected;
                                    }));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                StatusConnection.Text = "Error for connect";
                MessageBox.Show(ex.Message);
            }
            ConnetctedMenuItem.IsEnabled = !socketClient.Connected;
        }

        private byte[] ReceiveAll()
        {
            var buffer = new List<byte>();
            if (socketClient.Connected)
            {
                while (socketClient.Available > 0)
                {
                    var currByte = new byte[1];
                    var byteCounter = socketClient.Receive(currByte, currByte.Length, SocketFlags.None);

                    if (byteCounter.Equals(1))
                    {
                        buffer.Add(currByte[0]);
                    }
                }
            }
            return buffer.ToArray();
        }

        private void SendMessage(string msg)
        {
            try
            {
                if (socketClient.Connected)
                {
                    socketClient.Send(Encoding.UTF8.GetBytes($"{msg}"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddMessage(string msg)
        {
            ListBoxItem item = new ListBoxItem() { MaxWidth = 250 };
            string user = msg.Substring(0, msg.IndexOf(':'));
            string message = msg.Substring(msg.IndexOf(':') + 1);
            StackPanel stack = new StackPanel();
            Border border = new Border() { CornerRadius = new CornerRadius(5) };
            border.SetResourceReference(Control.BackgroundProperty, "ElementsColor");
            border.Child = new TextBlock() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5), Text = message };
            TextBlock tbMetaData = new TextBlock() { TextWrapping = TextWrapping.Wrap, HorizontalAlignment = user == UserName ? HorizontalAlignment.Right : HorizontalAlignment.Left, Margin = new Thickness(0, 5, 0, 5), Text = $"{DateTime.Now.ToShortTimeString()} {user}" };
            stack.Children.Add(border);
            stack.Children.Add(tbMetaData);
            item.Content = stack;
            item.HorizontalAlignment = user == UserName ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            MessagesListBox.Items.Add(item);
            MessagesListBox.ScrollIntoView(item);
        }

        private void WriteLogin()
        {
            NewClient wnd = new NewClient();
            if ((bool)wnd.ShowDialog())
            {
                string newName = string.IsNullOrWhiteSpace(wnd.NameUser) ? "unnamed" : wnd.NameUser;
                if (socketClient != null)
                {
                    try
                    {
                        if (socketClient.Connected)
                            SendMessage($"{newName}:{UserName}Renamed to {newName}");
                    }
                    catch { }
                }
                UserName = newName;

            }
            Thread.Sleep(10);
        }

        private void InitializeMenuItems()
        {
            List<string> colors = new List<string>();
            foreach (var color in typeof(Colors).GetProperties())
            {
                colors.Add(color.Name);
            }
            foreach (var color in colors)
            {
                MenuItem menuBackItem = new MenuItem() { Header = color };
                menuBackItem.Click += MenuBackgroundItem_Click;
                BackgroundMenuItem.Items.Add(menuBackItem);

                MenuItem menuColorElItem = new MenuItem() { Header = color };
                menuColorElItem.Click += MenuColorElementItem_Click; ;
                ColorElementsMenuItem.Items.Add(menuColorElItem);

                MenuItem menuFontItem = new MenuItem() { Header = color };
                menuFontItem.Click += MenuFontColorItem_Click; ;
                FontColorMenuItem.Items.Add(menuFontItem);
            }
        }

        private void MenuFontColorItem_Click(object sender, RoutedEventArgs e)
        {
            (GridContent.Resources["TextColor"] as SolidColorBrush).Color = (Color)ColorConverter.ConvertFromString((sender as MenuItem).Header.ToString());
        }

        private void MenuColorElementItem_Click(object sender, RoutedEventArgs e)
        {
            (GridContent.Resources["ElementsColor"] as SolidColorBrush).Color = (Color)ColorConverter.ConvertFromString((sender as MenuItem).Header.ToString());
        }

        private void MenuBackgroundItem_Click(object sender, RoutedEventArgs e)
        {
            (GridContent.Resources["BackgroundColor"] as SolidColorBrush).Color = (Color)ColorConverter.ConvertFromString((sender as MenuItem).Header.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (socketClient != null)
            {
                if (socketClient.Connected)
                {
                    socketClient.Shutdown(SocketShutdown.Both);
                }

                socketClient.Close();
            }
        }

        private void ConnectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InitializeConnection();
        }

        private void NameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WriteLogin();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (socketClient != null)
                if (!String.IsNullOrWhiteSpace(MessageTextBox.Text) && socketClient.Connected)
                {
                    SendMessage($"{UserName}:{MessageTextBox.Text}");
                    MessageTextBox.Text = "";
                }

            if (socketClient != null) ConnetctedMenuItem.IsEnabled = !socketClient.Connected;
        }
    }
}

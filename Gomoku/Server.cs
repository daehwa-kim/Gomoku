using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Match
    {
        // State object for reading client data asynchronously  
        public class StateObject2
        {
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }
        public class AsyncServer
        {
            private static int port = 11000;
            private static string address = "";
            private static Match parentForm = null;

            public static int Port { get => port; set => port = value; }
            public static string Address { get => address; set => address = value; }
            public static Match ParentForm { get => parentForm; set => parentForm = value; }

            // Thread signal.  
            public static ManualResetEvent allDone = new ManualResetEvent(false);

            public AsyncServer()
            {
            }

            public static void StartListening()
            {
                // Data buffer for incoming data.  
                byte[] bytes = new Byte[1024];

                // Establish the local endpoint for the socket.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPAddress.Parse(Address);  // ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.  
                        Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);

                        // Wait until a connection is made before continuing.  
                        allDone.WaitOne();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();

            }

            public static void AcceptCallback(IAsyncResult ar)
            {
                // Signal the main thread to continue.  
                allDone.Set();

                // Get the socket that handles the client request.  
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }

            public static void ReadCallback(IAsyncResult ar)
            {
                String content = String.Empty;

                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket.   
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.  
                    content = state.sb.ToString();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        // All the data has been read from the   
                        // client. Display it on the console.  
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        RunRemoteCommand(content);
                        // Echo the data back to the client.  
                        Send(handler, content);
                    }
                    else
                    {
                        // Not all data received. Get more.  
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }

            private static void Send(Socket handler, String data)
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    // Retrieve the socket from the state object.  
                    Socket handler = (Socket)ar.AsyncState;

                    // Complete sending the data to the remote device.  
                    int bytesSent = handler.EndSend(ar);
                    Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public static int Initialize()
            {
                StartListening();
                return 0;
            }









            public static void RunRemoteCommand(string data)
            {
                string command = "";
                string line = "";
                string[] fields = null;

                if (data.Contains(":") && data.EndsWith("<EOF>"))
                {
                    command = data.Substring(0, data.IndexOf(":"));
                    line = data.Substring(data.IndexOf(":") + 1).Replace("<EOF>", "");
                    fields = line.Split(',');
                }

                if (command == "Put" && fields.Length == 2)
                {
                    int row = -1, col = -1;
                    int.TryParse(fields[0], out row);
                    int.TryParse(fields[1], out col);

                    ParentForm.Invoke(new MethodInvoker(delegate
                    {
                        if (row != -1 && col != -1)
                        {
                            ParentForm.Put(row, col, ParentForm.awayPlayer);
                            //ParentForm.turnPlayer = ParentForm.homePlayer;
                        }
                    }));
                }
                else if (command == "ReplayRequest")
                {
                    if (MessageBox.Show("Replay requested.", "Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        ParentForm.Invoke(new MethodInvoker(delegate
                        {
                            ParentForm.ResetGame();
                            AsyncClient.Initialize("ReplayAccepted:");

                            if (ParentForm.homePlayer.Color == 1)
                            {
                                ParentForm.homePlayer.Color = 2;
                                ParentForm.turnPlayer = ParentForm.awayPlayer;
                            }
                            else
                            {
                                ParentForm.homePlayer.Color = 1;
                                ParentForm.turnPlayer = ParentForm.homePlayer;
                            }
                            ParentForm.DisplayUsers();
                        }));
                    }
                    else
                    {
                        ParentForm.Invoke(new MethodInvoker(delegate
                        {
                            AsyncClient.Initialize("ReplayDenied:");
                        }));
                    }
                }
                else if (command == "ReplayAccepted")
                {
                    ParentForm.ResetGame();
                    if (ParentForm.homePlayer.Color == 1)
                    {
                        ParentForm.homePlayer.Color = 2;
                        ParentForm.turnPlayer = ParentForm.awayPlayer;
                    }
                    else
                    {
                        ParentForm.homePlayer.Color = 1;
                        ParentForm.turnPlayer = ParentForm.homePlayer;
                    }
                    ParentForm.DisplayUsers();
                }
                else if (command == "ReplayDenied")
                {
                    MessageBox.Show("Replay Denied :(");
                }
                else if (command == "Out")
                {
                    MessageBox.Show(ParentForm.awayPlayer.DisplayName + " left the game.");
                    ParentForm.ResetGame();
                    ParentForm.turnPlayer = null;
                    //ParentForm.client = null;
                }
                else if (command == "Chat")
                {
                    //MessageBox.Show(fields[0], ParentForm.awayPlayer.DisplayName);
                    ParentForm.Invoke(new MethodInvoker(delegate
                    {
                        ParentForm.tipChat.Show(fields[0], ParentForm.lblAwayPlayer, 0, 30);
                    }));
                }
                else if (command == "Setup" && fields.Length == 2 || fields.Length == 4)
                {
                    string displayName = fields[0];
                    string ipAddress = "";
                    int color = -1, port = -1;
                    int.TryParse(fields[1], out color);

                    ParentForm.Invoke(new MethodInvoker(delegate
                    {
                        if (displayName != "" && color != -1)
                        {
                            ParentForm.awayPlayer.DisplayName = displayName;
                            ParentForm.awayPlayer.Color = color;

                            if (fields.Length == 4)
                            {
                                if (color == 1)
                                {
                                    ParentForm.homePlayer.Color = 2;
                                    ParentForm.turnPlayer = ParentForm.awayPlayer;
                                }
                                else if (color == 2)
                                {
                                    ParentForm.homePlayer.Color = 1;
                                    ParentForm.turnPlayer = ParentForm.homePlayer;
                                }

                                ipAddress = fields[2];
                                int.TryParse(fields[3], out port);

                                //If connected
                                AsyncClient.Address = ipAddress;
                                AsyncClient.Port = port;
                                AsyncClient.ParentForm = ParentForm;
                                AsyncClient.Initialize("Setup:" + ParentForm.homePlayer.DisplayName + "," + ParentForm.homePlayer.Color.ToString());
                                //ParentForm.bgwClient.RunWorkerAsync();
                                MessageBox.Show("Connected to " + displayName);
                            }
                            else if (fields.Length == 2)
                            {
                                MessageBox.Show("Connected to " + displayName);
                            }
                            ParentForm.DisplayUsers();

                            if (ParentForm.homePlayer.Color == 1)
                            {
                                ParentForm.turnPlayer = ParentForm.homePlayer;
                            }
                            else if (ParentForm.homePlayer.Color == 2)
                            {
                                ParentForm.turnPlayer = ParentForm.awayPlayer;
                            }
                        }
                    }));
                }
            }
        }
    }
}


using System.Numerics;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using WebSocketSharp.Server;


Thread chat = new Thread(new ThreadStart(message));
chat.Start();

while (true)
{
    
    if (Server_Info.Return_Lane() == "0")
    {
        JoinServer();
    }
    else if (Server_Info.Return_Lane() == "~")
    {
        break;
    }
    else
    {
        if (Server_Info.connected = false)
        {
            Connection();
        }
    }
}

Console.ReadKey();

void message()
{
    while (true)
    {
        Server_Info.Add_Message(Console.ReadLine());
    }
}

static void Connection()
{
    Server_Info.connected = true;
    WebSocket Peer_Interface = new WebSocket("ws://127.0.0.1:9037/Connection" + Server_Info.Return_Lane());
    Peer_Interface.OnMessage += Peer_Interface_OnMessage;
    Peer_Interface.Connect();
    Peer_Interface.Send("Player " + Server_Info.player + " joined");
    string message = "";

    do
    {
        if (message != "Empty")
            Peer_Interface.Send(Server_Info.Local().ID + "~" + Server_Info.player + "~" + message);
        Thread.Sleep(100);
        message = Server_Info.Messages();
        if (message == null)
        {
            message = "Empty";
        }
    } while (message.ToLower() != "disconnect");

    Peer_Interface.Send(Server_Info.Local().ID + "~" + Server_Info.player + "~" + "<Disconnect>");
    Server_Info.Update_Lane("0");
    Thread.Sleep(100);
    Peer_Interface.Close();
    Console.WriteLine("Disconnected");
    Server_Info.connected = false;
}

static void Peer_Interface_OnMessage(object sender, MessageEventArgs e)
{
    string[] message = e.Data.Split('~');
    if (Convert.ToInt16(message[0]) == Server_Info.Remote().ID)
    {
        Console.WriteLine("Player {0}: {1}", message[1], message[2]);
    }
}

static void Client_Interface_OnMessage(object sender, MessageEventArgs e)
{
    Console.WriteLine(e.Data);
    string[] command = e.Data.Split(' ');
    try
    {
        if(command[0] == "ID_Assignment:")
        {
            User user = new User(Convert.ToInt16(command[1]), command[2]);
            Server_Info.Local_User(user);
        } 
        else if (command[0] == "ConnectionRequest")
        {
            if (Convert.ToInt16(command[3]) == Server_Info.Local().ID)
            {
                Server_Info.Remote_User(new User(Convert.ToInt16(command[1]), command[2]));
            }
            else if (Convert.ToInt16(command[1]) == Server_Info.Local().ID)
            {
                Server_Info.Remote_User(new User(Convert.ToInt16(command[3]), command[4]));
            }
        } 
        else if (command[0] == "ConnectionAssignment")
        {
            bool connected = false;
            if (Convert.ToInt16(command[1]) == Server_Info.Local().ID)
            {
                Server_Info.player = 1;
                connected = true;
            } else if (Convert.ToInt16(command[2]) == Server_Info.Local().ID)
            {
                Server_Info.player = 2;
                connected = true;
            }

            if (connected == true)
            {
                Server_Info.Update_Lane(command[3]);
                Connection();
            }
        }
    }
    catch { }
}

static void JoinServer()
{
    WebSocket Client_Interface = new WebSocket("ws://127.0.0.1:9037/Connect_To_User");

    Client_Interface.OnMessage += Client_Interface_OnMessage;
    Client_Interface.Connect();

    string message = Server_Info.Messages();
    if (message != "Empty")
    {
        Client_Interface.Send(message);

        if (message == "ID")
        {
            User user = Server_Info.Local();
            Console.WriteLine(user.ID);
            Console.WriteLine(user.name);
        }
        else if (message == "Disconnect")
        {
            Server_Info.Update_Lane("~");
        }
    }
    Thread.Sleep(100);
    Client_Interface.Close();
}

record User(int ID, string name) { }

static class Server_Info
{
    static private ManualResetEvent Read_Write_Lock = new ManualResetEvent(false);
    static private User local_User = new User(0, "~");
    static private User remote_User = new User(0, "~");
    static public int player = 1;
    static private string lane = "0";
    static private Queue<string> Message_Queue = new Queue<string>();
    static public bool connected = false;

    static public string Messages()
    {
        //Read_Write_Lock.Reset();
        if(Message_Queue.Count == 0)
        {

            return "Empty";
        }
        else
        {
            return Message_Queue.Dequeue();
        }
    }

    static public void Add_Message(string message)
    {
        Message_Queue.Enqueue(message);
    }

    static public void Local_User(User user)
    {
        local_User = user;
    }

    static public void Remote_User(User user)
    {
        remote_User = user; 
    }

    static public User Local()
    {
        return local_User;
    }

    static public User Remote()
    {
        return remote_User;
    }

    static public void Update_Lane(string Lane)
    {
        lane = Lane;
    }

    static public string Return_Lane()
    {
        return lane;
    }
}

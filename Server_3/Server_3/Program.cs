using System.Data;
using System.Threading.Tasks.Dataflow;
using WebSocketSharp;
using WebSocketSharp.Server;


WebSocketServer server = new WebSocketServer("ws://127.0.0.1:9037");

server.AddWebSocketService<Echo>("/Echo");
server.AddWebSocketService<Connect_To_User>("/Connect_To_User");
server.AddWebSocketService<Connection1>("/Connection1");

server.Start();
Console.WriteLine("Server started");

Console.ReadKey();
server.Stop();

static class Users
{
    static private ManualResetEvent Read_Write_Lock = new ManualResetEvent(false);

    static private User Blank_User = new User(0, "~", false);
    static private List<User> Current_Users = new List<User>();
    static int ID_Counter = 10;

    //Current_Users current_Users = new();

    static public int Add_User(string name)
    {
        //Read_Write_Lock.Reset();

        if (ID_Counter >= 10000)
        {
            ID_Counter = 10;
        }

        int New_ID = ID_Counter;
        while (true) 
        {
            New_ID++;
            bool overlap = false;
            foreach (User user in Current_Users)
            {
                if (user.ID == New_ID) { overlap = true; }
            }
            if (overlap == false) { break; }
        }

        ID_Counter = New_ID;
        Current_Users.Add(new User(New_ID, name, true));

        //Read_Write_Lock.Set();
        return New_ID;
    }

    public static void Update_Available(int ID, bool available)
    {
        User user = Current_Users.Where(x => x.ID == ID).FirstOrDefault();
        Current_Users.Add(new User(user.ID,user.name,available));
        Current_Users.Remove(Current_Users.Where(x => x.ID == ID).FirstOrDefault());
    }

    public static List<User> Request_User_List()
    {
        return Current_Users;
    }

    public static User Request_User(int ID)
    {
        foreach (User user in Current_Users)
        {
            if (user.ID == ID)
            {
                return user;
            }
        }

        return Blank_User;
    }
}

record User(int ID, string name, bool available) { }

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Recieved message: " + e.Data);
        Send(e.Data);
    }
}

public class Connection1 : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);
        string[] message = e.Data.Split('~');
        try
        {
            if (message[2] == "<Disconnect>")
            {
                Users.Update_Available(Convert.ToInt16(message[0]), true);
                Sessions.Broadcast(message[0] + "~" + message[1] + "~Player disconnected");
            }
            else
            {
                Sessions.Broadcast(e.Data);
            }
        }
        catch { }
    }

    protected override void OnOpen()
    {
        Console.WriteLine("Client connected: " + Context.UserEndPoint);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("Connection closed with code {0}: {1}", e.Code, e.Reason);
    }


}

public class Connect_To_User : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        //Send("Recieved");
        //Send(e.Data);
        Console.WriteLine(e.Data);
        string[] command = e.Data.Split(' ');

        if (command.Length < 1)
        {
            Error("No command sent");
        }

        if (command[0] == "NewUser")
        {
            if (command.Length != 2) Error("Invalid use of NewUser command");
            else
            {
                int ID = Users.Add_User(command[1]);
                Send("ID_Assignment: " + ID + " " + command[1]);
                Send_Current_Users();
            }
        }
        else if (command[0] == "Refresh")
        {
            Send_Current_Users();
        } 
        else if (command[0] == "Connect")
        {
            if (command.Length != 4) Error("Incorrect use of Connect");
            User Requested_User = Users.Request_User(Convert.ToInt16(command[1]));
            if (Requested_User.ID != 0 && Requested_User.available == true)
            {
                Sessions.Broadcast("ConnectionRequest " + command[1] + " " + Requested_User.name + " " + command[2] + " " + command[3]);
            }
        }
        else if (command[0] == "AcceptConnect")
        {
            try
            {
                Users.Update_Available(Convert.ToInt16(command[1]), false);
                Users.Update_Available(Convert.ToInt16(command[2]), false);
                Sessions.Broadcast("ConnectionAssignment " + command[1] + " " + command[2] + " 1");

            } catch { }            
        }
        else if (command[0] == "RejectConnect")
        {
            Sessions.Broadcast("ConnectionRejected " + command[1] + " " + command[2]);
        }

        void Send_Current_Users()
        {
            List<User> Current_Users = Users.Request_User_List();

            Send("StartUserList");
            foreach (User user in Current_Users)
            {
                if (user.available == true)
                {
                    Send(user.ID + " " + user.name);
                }
            }
            Send("EndUserList");
        }
    }

    protected void Error(string reason)
    {
        Send(reason);
    }
}
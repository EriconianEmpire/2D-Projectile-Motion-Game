using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WebSocketSharp;


namespace _2D_game_engine_v1
{


    public partial class Display : Form
    {
        Map_Physics Map;
        Pen Projectile_Pen = new Pen(Color.Black);
        Brush Projectile_Brush = new SolidBrush(Color.Black);
        Graphics g;
        string path = Directory.GetCurrentDirectory();
        string MapToLoad = "Default_Map";
        int turn = 0;
        int win = 0;

        const double FRAME_TIME = 0.04;
        const double CANVAS_MULTIPLIER = 20;
        const double ORIGINAL_WINDOW_SIZE = 1904;
        double Window_Size = 1;
        Random random = new Random();
        PictureBox Background;
        Vector PARTICLE_SIZE = new Vector(10, 10);

        /*static double velocity = 0;
        static double gravity = 0;
        static double angle = 0;*/

        List<Basic_Object> objects = new List<Basic_Object>();
        List<Projectile> projectiles = new List<Projectile>();

        bool Simulation_Done = true;

        public Display()
        {
            InitializeComponent();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary Thread";

            Load_Map(MapToLoad);
        }

        public void Load_Map(string mapToLoad)
        {
            string[] Collected_Map_Data = new string[8];

            Collected_Map_Data[0] = "~GravityX";  //GravityX
            Collected_Map_Data[1] = "~GravityY";  //GravityY
            Collected_Map_Data[2] = "~Air_Density";   //Air_Density

            Collected_Map_Data[3] = "~Background";   //Background
            Collected_Map_Data[4] = "~Ground";       //Ground
            Collected_Map_Data[5] = "~Material_1";   //Material_1
            Collected_Map_Data[6] = "~Material_2";   //Material_2
            Collected_Map_Data[7] = "~Material_3";   //Material_3

            mapToLoad = path + @"\\" + mapToLoad + @"\\";

            string[] Map_Data = System.IO.File.ReadAllLines(mapToLoad + @"Map_Data.txt");


            foreach (string line in Map_Data)
            {
                try
                {
                    if (line[0] == '~')
                    {
                        string[] command = line.Split('=');

                        for (int i = 0; i < Collected_Map_Data.Length; i++)
                        {
                            if (command[0] == Collected_Map_Data[i])
                            {
                                Collected_Map_Data[i] = command[1];
                            }
                        }

                    }
                }
                catch { }
            }

            mapPhysics(Collected_Map_Data);


            Basic_Object[] basic_Objects = new Basic_Object[23];
            basic_Objects[1] = new Basic_Object(true, 1, 1, Map, new Vector(0, 30), new Vector(1904, 30), Window_Size, mapToLoad + Collected_Map_Data[4]);
            basic_Objects[0] = new Basic_Object(false, 10, 1, Map, new Vector(20, 40), new Vector(400, 10), Window_Size, mapToLoad + Collected_Map_Data[6]);
            basic_Objects[2] = new Basic_Object(false, 10, 1, Map, new Vector(1904 - 420, 40), new Vector(400, 10), Window_Size, mapToLoad + Collected_Map_Data[6]);
            basic_Objects[3] = new Basic_Object(false, 5, 1, Map, new Vector(30, 50), new Vector(380, 30), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[4] = new Basic_Object(false, 5, 1, Map, new Vector(1904 - 410, 50), new Vector(380, 30), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[5] = new Basic_Object(false, 20, 1, Map, new Vector(150, 90), new Vector(140, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[6] = new Basic_Object(false, 20, 1, Map, new Vector(1904 - 290, 90), new Vector(140, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[7] = new Basic_Object(false, 5, 1, Map, new Vector(30, 80), new Vector(120, 30), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[8] = new Basic_Object(false, 5, 1, Map, new Vector(1904 - 150, 80), new Vector(120, 30), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[9] = new Basic_Object(false, 5, 1, Map, new Vector(290, 80), new Vector(120, 20), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[10] = new Basic_Object(false, 5, 1, Map, new Vector(1904 - 410, 80), new Vector(120, 20), Window_Size, mapToLoad + Collected_Map_Data[5]);
            basic_Objects[11] = new Basic_Object(false, 10, 1, Map, new Vector(150, 100), new Vector(270, 10), Window_Size, mapToLoad + Collected_Map_Data[6]);
            basic_Objects[12] = new Basic_Object(false, 10, 1, Map, new Vector(1904 - 420, 100), new Vector(270, 10), Window_Size, mapToLoad + Collected_Map_Data[6]);
            basic_Objects[13] = new Basic_Object(false, 20, 1, Map, new Vector(20, 110), new Vector(140, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[14] = new Basic_Object(false, 20, 1, Map, new Vector(1904 - 160, 110), new Vector(140, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[15] = new Basic_Object(false, 4, 1, Map, new Vector(200, 80), new Vector(10, 10), Window_Size, mapToLoad + "Core1.bmp");
            basic_Objects[16] = new Basic_Object(false, 4, 1, Map, new Vector(1904 - 210, 80), new Vector(10, 10), Window_Size, mapToLoad + "Core2.bmp");
            basic_Objects[17] = new Basic_Object(false, 20, 1, Map, new Vector(150, 80), new Vector(50, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[18] = new Basic_Object(false, 20, 1, Map, new Vector(1904 - 200, 80), new Vector(50, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[19] = new Basic_Object(false, 20, 1, Map, new Vector(210, 80), new Vector(80, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[20] = new Basic_Object(false, 20, 1, Map, new Vector(1904 - 290, 80), new Vector(80, 10), Window_Size, mapToLoad + Collected_Map_Data[7]);
            basic_Objects[21] = new Basic_Object(true, 1, 1, Map, new Vector(90, 115), new Vector(5, 5), Window_Size, mapToLoad + "ball.bmp");
            basic_Objects[22] = new Basic_Object(true, 1, 1, Map, new Vector(1904 - 95, 115), new Vector(5, 5), Window_Size, mapToLoad + "ball.bmp");

            loadObjects();


            void mapPhysics(string[] Collected_Map_Data)
            {
                Map = new Map_Physics(new Vector(Convert.ToDouble(Collected_Map_Data[0]), Convert.ToDouble(Collected_Map_Data[1])), Convert.ToDouble(Collected_Map_Data[2]), FRAME_TIME, CANVAS_MULTIPLIER, mapToLoad, new Vector(4.5, 5.75), new Vector(95.2 - 4.5, 5.75));
            }

            void background(string background)
            {

                Background = new PictureBox()
                {
                    Size = new Size(Convert.ToInt32(Visualiser.Width), Convert.ToInt32(Visualiser.Height)),
                    Location = new Point(0, 0),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                try
                {
                    Background.Image = Image.FromFile(background);
                }
                catch
                {
                    Background.Image = Properties.Resources.Missing_Texture;
                }
                Visualiser.Controls.Add(Background);
            }

            void loadObjects()
            {
                foreach (Basic_Object obj in basic_Objects)
                {
                    if (obj != null)
                    {
                        if (obj.Return_Invincible() == true)
                        {
                            objects.Add(obj);
                        }
                        else
                        {
                            Vector sizeVector = obj.Return_Size();
                            //int health = Convert.ToInt32(sizeVector.I() * sizeVector.J()) / 100;
                            int health = obj.Return_Health(); // (Convert.ToInt32(sizeVector.I() * sizeVector.J()) / 100);
                            Vector position = obj.Return_Initial_Position();
                            //position.Divide(CANVAS_MULTIPLIER);


                            for (double y = 0; y < sizeVector.J() / 10; y++)
                            {
                                for (double x = 0; x < sizeVector.I() / 10; x++)
                                {
                                    Vector toAdd = new Vector(x * 10, y * 10);
                                    Vector particlePosition = position.Clone();
                                    particlePosition.Add_Vector(toAdd);

                                    objects.Add(new Basic_Object(false, health, obj.Return_Resistance(), Map, particlePosition, PARTICLE_SIZE, Window_Size, obj.Return_Image_Path()));
                                }
                            }
                        }
                    }
                }
            }

            //Display all objects
            foreach (Basic_Object obj in objects)
            {
                int x = Convert.ToInt16(obj.Return_Initial_Position().I() * Window_Size);
                int y = Visualiser.Height - Convert.ToInt16(obj.Return_Initial_Position().J() * Window_Size);
                Visualiser.Controls.Add(obj.Display_Image);

                obj.Display_Image.Location = new Point(x, y);
                obj.Display_Image.Visible = true;
            }

            background(mapToLoad + Collected_Map_Data[3]);
        }

        public void Check_Win()
        {
            foreach (Basic_Object obj in objects)
            {
                string[] Identity = obj.Return_Image_Path().Split('\\');
                string identity = Identity[Identity.Length - 1];
                if (identity == "Core1.bmp")
                {
                    if (obj.Exist() == false)
                    {
                        win = 2;
                        P2Win.Visible = true;
                    }
                }
                else if (identity == "Core2.bmp")
                {
                    if (obj.Exist() == false)
                    {
                        win = 1;
                        P1Win.Visible = true;
                    }
                }
            }

            if (win != 0)
            {

            }
        }

        public void Advance_Frame(Object o)
        {
            int count = Convert.ToInt32(o);
            projectiles[count].Advance_Frame();

            if (projectiles[count].Exist() == true)
            {
                Vector Velocity = projectiles[count].Return_Velocity().Clone();
                Vector Start_Position = projectiles[count].Return_Position().Clone();
                Vector End_Position = Velocity;
                End_Position.Multiply(CANVAS_MULTIPLIER);
                double Speed = Velocity.Magnitude();

                End_Position.Add_Vector(Start_Position);
                foreach (Basic_Object obj in objects)
                {
                    obj.Test_Collision(Start_Position, End_Position, Speed, projectiles[count]);
                }
            }
        }

        public void frameTimer(Object o)
        {
            int count = Convert.ToInt32(o);
            while (projectiles[count].Exist())
            {
                new Thread(new ParameterizedThreadStart(Advance_Frame)).Start(o);
                Thread.Sleep(Convert.ToInt32(1000 * FRAME_TIME));
            }


            bool noneExist = true;
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.Exist())
                {
                    noneExist = false;
                }
            }

            if (noneExist)
            {
                Simulation_Done = true;
            }
        }

        private void draw_projectile()
        {
            g = Visualiser.CreateGraphics();
            Simulation_Done = false;

            foreach (Projectile projectile in projectiles)
            {
                Visualiser.Controls.Add(projectile.Display_Image);
                projectile.Display_Image.BringToFront();
            }
            //Temporary
            objects[0].Display_Image.BringToFront();

            for (int i = 0; i < projectiles.Count(); i++)
            {
                new Thread(new ParameterizedThreadStart(frameTimer)).Start(i);

            }

            while (Simulation_Done == false)
            {
                foreach (Projectile projectile in projectiles)
                {
                    display_vector(projectile);
                    projectile.Visual_Check(Visualiser);
                }
                foreach (Basic_Object obj in objects)
                {
                    obj.Visual_Check(Visualiser);
                }
                Visualiser.Update();
                if (projectiles.Count == 0) Simulation_Done = true;
                Thread.Sleep(25);
            }

            projectiles.Clear();
            Check_Win();

            void display_vector(Projectile projectile)
            {
                int x = Convert.ToInt32(projectile.Return_Position().I() * Window_Size);
                int y = Visualiser.Height - Convert.ToInt32(projectile.Return_Position().J() * Window_Size);
                if (x > 0 && x <= Visualiser.Width && y > 0 && y <= Visualiser.Height)
                {
                    //projectile.Display_Image.
                    projectile.Display_Image.Visible = true;
                    projectile.Display_Image.Location = new Point(x, y);
                }
                else
                {
                    projectile.Display_Image.Visible = false;
                }
            }
        }

        private void Start_Click_1(object sender, EventArgs e)
        {
            double velocity = Double.Parse(Initial_Velocity.Text);
            double angle = Double.Parse(Angle.Text);
            bool WR = true;
            Vector Start;

            if (Server_Info.Return_LAN() == false)
            {
                if (Server_Info.Return_Turn() % 2 == 0)
                {
                    Start = Map.Return_P1_Pos();
                    angle = 90 - angle;
                }
                else
                {
                    Start = Map.Return_P2_Pos();
                    angle = 270 + angle;
                }

                angle = (angle / 180) * Math.PI;
                Server_Info.Increment_Turn();
                Vector Projectile_Velocity_Vector = Create_Vector.Calculate_Vector_From_Bearing(velocity, angle);


                Projectile newProjectile1 = new Projectile(Start.Clone(), 0.5, 10, 0.7854, true, 40, 1, Map, new Vector(5, 5), Window_Size, Map.Return_Map_Path() + "Shell.bmp"); ;
                newProjectile1.Trigger_Movement(Projectile_Velocity_Vector.Clone(), new Vector(0, 0), WR);
                projectiles.Add(newProjectile1);

                draw_projectile();
            } else if (Server_Info.Return_Player() % 2 == Server_Info.Return_Turn() % 2)
            {
                if (Server_Info.Return_Player() == 1)
                {
                    Start = Map.Return_P1_Pos();
                    angle = 90 - angle;
                }
                else
                {
                    Start = Map.Return_P2_Pos();
                    angle = 270 + angle;
                }

                angle = (angle / 180) * Math.PI;
                Server_Info.Increment_Turn();

                Server_Info.Add_Message("<Turn>~" + velocity + "~" + angle);
                Vector Projectile_Velocity_Vector = Create_Vector.Calculate_Vector_From_Bearing(velocity, angle);


                Projectile newProjectile1 = new Projectile(Start.Clone(), 0.5, 10, 0.7854, true, 40, 1, Map, new Vector(5, 5), Window_Size, Map.Return_Map_Path() + "Shell.bmp"); ;
                newProjectile1.Trigger_Movement(Projectile_Velocity_Vector.Clone(), new Vector(0, 0), WR);
                projectiles.Add(newProjectile1);

                draw_projectile();
            } else if (Server_Info.Return_Turn_Given() == true)
            {
                if (Server_Info.Return_Player() == 1)
                {
                    Start = Map.Return_P2_Pos();
                }
                else
                {
                    Start = Map.Return_P1_Pos();
                }

                Server_Info.Increment_Turn();

                Projectile newProjectile1 = new Projectile(Start.Clone(), 0.5, 10, 0.7854, true, 40, 1, Map, new Vector(5, 5), Window_Size, Map.Return_Map_Path() + "Shell.bmp"); ;
                newProjectile1.Trigger_Movement(Server_Info.Return_Given_Vector().Clone(), new Vector(0, 0), WR);
                projectiles.Add(newProjectile1);
                Server_Info.Turn_Used();

                draw_projectile();
            }

        }


        private void display_visual(object sender, EventArgs e)
        {

        }

        private void Size_Changed(object sender, EventArgs e)
        {
            //Visualiser.Refresh();
            //Background.Dispose();
            //foreach (Basic_Object obj in objects)
            //{
            //    obj.Display_Image.Dispose();
            //}
            objects.Clear();
            Visualiser.Controls.Clear();
            Window_Size = Convert.ToDouble(Visualiser.Width) / ORIGINAL_WINDOW_SIZE;
            Load_Map(MapToLoad);
        }

        private void Projectile_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void World_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (World_Selection.SelectedIndex == 2 || World_Selection.SelectedIndex == -1)
            {
                MapToLoad = "Default_Map";
            }
            else if (World_Selection.SelectedIndex == 0)
            {
                MapToLoad = "Mercury";
            }
            else if (World_Selection.SelectedIndex == 1)
            {
                MapToLoad = "Venus";
            }
            else if (World_Selection.SelectedIndex == 3)
            {
                MapToLoad = "Mars";
            }
            else if (World_Selection.SelectedIndex == 4)
            {
                MapToLoad = "Weird_Gravity";
            }
            objects.Clear();
            Visualiser.Controls.Clear();
            Load_Map(MapToLoad);

        }

        private void LAN_Start(object sender, EventArgs e)
        {
            if (Server_Info.Return_Status() == false)
            {
                Server_Info.Change_Status(true);
                Lan_Connection lan_Connection = new Lan_Connection();
                lan_Connection.Show();

                Thread server = new Thread(new ThreadStart(Server));
                server.Start();

                void Server()
                {
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
                            if (Server_Info.connected == false)
                            {
                                Connection();
                            }
                        }
                    }

                    Server_Info.Change_Status(false);

                    static void Connection()
                    {
                        Server_Info.connected = true;
                        WebSocket Peer_Interface = new WebSocket("ws://127.0.0.1:9037/Connection" + Server_Info.Return_Lane());
                        Peer_Interface.OnMessage += Peer_Interface_OnMessage;
                        Peer_Interface.Connect();
                        Peer_Interface.Send("Player " + Server_Info.Return_Player() + " joined");
                        string message = "";

                        do
                        {
                            if (message != "Empty")
                                Peer_Interface.Send(Server_Info.Local().ID + "~" + Server_Info.Return_Player() + "~" + message);
                            Thread.Sleep(100);
                            message = Server_Info.Messages();
                            if (message == null)
                            {
                                message = "Empty";
                            }
                        } while (message.ToLower() != "disconnect");

                        Peer_Interface.Send(Server_Info.Local().ID + "~" + Server_Info.Return_Player() + "~" + "<Disconnect>");
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
                            if (message[2] == "<Turn>")
                            {
                                Server_Info.Other_Turn_Recieved(message);
                            }
                        }
                        
                    }

                    

                    static void Client_Interface_OnMessage(object sender, MessageEventArgs e)
                    {
                        //Console.WriteLine(e.Data);
                        string[] command = e.Data.Split(' ');
                        try
                        {
                            if (command[0] == "ID_Assignment:")
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
                                    User_List.Incoming_Request(command);
                                }
                            }
                            else if (command[0] == "ConnectionAssignment")
                            {
                                bool connected = false;
                                if (Convert.ToInt16(command[1]) == Server_Info.Local().ID)
                                {
                                    Server_Info.Assign_Player(1);
                                    Server_Info.Change_LAN_Status(true);
                                    connected = true;
                                }
                                else if (Convert.ToInt16(command[2]) == Server_Info.Local().ID)
                                {
                                    Server_Info.Assign_Player(2);
                                    Server_Info.Change_LAN_Status(true);
                                    connected = true;
                                }

                                if (connected == true)
                                {
                                    Server_Info.Update_Lane(command[3]);
                                    Connection();
                                }
                            } else if (command[0] == "StartUserList")
                            {
                                Server_Info.Update_User_List(true);
                                User_List.Reset_User_List();
                            } else if (command[0] == "EndUserList")
                            {
                                Server_Info.Update_User_List(false);
                                User_List.Display_User_List();
                            } else if (Server_Info.User_List() == true)
                            {
                                User_List.Add_User_To_List(e.Data);
                            }
                        }
                        catch { }
                    }

                    static void JoinServer()
                    {
                        WebSocket Client_Interface = new WebSocket("ws://127.0.0.1:9037/Connect_To_User");

                        Client_Interface.OnMessage += Client_Interface_OnMessage;
                        Client_Interface.Connect();
                        //Client_Interface.Send("Joined server");

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
                }
            }
        }
    }

    record User(int ID, string name) { } //Record for user

    static class Server_Info
    {
        static private ManualResetEvent Read_Write_Lock = new ManualResetEvent(false);
        static private User local_User = new User(0, "~");
        static private User remote_User = new User(0, "~");
        static private int player = 1;
        static private string lane = "0";
        static private Queue<string> Message_Queue = new Queue<string>();
        static public bool connected = false;
        static private bool started = false;
        static private bool Recieving_User_list = false;
        static private bool LAN = false;
        static private int turn = 1;

        static private bool Turn_Given = false;
        static private Vector Given_Vector;

        static public Vector Return_Given_Vector()
        {
            return Given_Vector;
        }

        static public void Turn_Used()
        {
            Turn_Given = false;
        }

        static public bool Return_Turn_Given()
        {
            return Turn_Given;
        }

        static public void Other_Turn_Recieved(string[] message)
        {
            double velocity = Convert.ToDouble(message[3]);
            double angle = Convert.ToDouble(message[4]);
            
            Given_Vector = Create_Vector.Calculate_Vector_From_Bearing(velocity, angle);

            Turn_Given = true;
        }

        static public void Other_Turn_Used()
        {
            Turn_Given = false;
        }

        static public void Assign_Player(int Player_Num)
        {
            player = Player_Num;
            turn = 1;
        }

        static public int Return_Player()
        {
            return player;
        }

        static public void Increment_Turn()
        {
            turn++;
        }

        static public int Return_Turn()
        {
            return turn;
        }

        static public bool Return_LAN()
        {
            return LAN;
        }

        static public void Change_LAN_Status(bool status)
        {
            LAN = status;
        }

        static public string Messages()
        {
            //Read_Write_Lock.Reset();
            if (Message_Queue.Count == 0)
            {

                return "Empty";
            }
            else
            {
                return Message_Queue.Dequeue();
            }
        }

        static public bool User_List()
        {
            return Recieving_User_list;
        }

        static public void Update_User_List(bool status)
        {
            Recieving_User_list = status;
        }

        static public void Change_Status(bool server_status)
        {
            started = server_status;
        }

        static public bool Return_Status()
        {
            return started;
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
    public class Basic_Object
    {
        protected ManualResetEvent Read_Write_Lock1 = new ManualResetEvent(false);

        protected Map_Physics Map;
        protected bool Physics_Simulated = false;
        protected bool Invincible = true;
        protected int Health = -1;
        protected Vector Initial_Position;
        private Vector Size;
        private string Image_Path;
        public PictureBox Display_Image;
        protected bool Exists = true;
        protected double Resistance;

        private object Exist_Locker = new object();

        //Hitbox
        private Vector Left_Bottom = new Vector(0,0);
        private Vector Right_Bottom = new Vector(0, 0);
        private Vector Right_Top = new Vector(0, 0);

        public Basic_Object(bool invincible, int health, double resistance, Map_Physics map, Vector initial_Position, Vector size, double Window_Size, string image_path)
        {
            Initial_Position = initial_Position;
            Display_Image = new PictureBox()
            {
                Size = new Size(Convert.ToInt32(size.I() * Window_Size), Convert.ToInt32(size.J())),
                Location = new Point(0, 0),//Convert.ToInt(Initial_Position.I()) - Convert.ToInt16(Initial_Position.J())),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false,
                //BackColor = Color.Transparent,
            };
            try
            {
                Display_Image.Image = Image.FromFile(image_path);
            }
            catch
            {
                Display_Image.Image = Properties.Resources.Missing_Texture;
            }
            Invincible = invincible;
            Health = health;
            Resistance = resistance;
            Map = map;
            Size = size;
            Image_Path = image_path;

            Right_Top.Set_Vector(Size.I() + Initial_Position.I(), Initial_Position.J());
            Right_Bottom.Set_Vector(Size.I() + Initial_Position.I(), -Size.J() + Initial_Position.J());
            Left_Bottom.Set_Vector(Initial_Position.I(), -Size.J() + Initial_Position.J());
        }

        public bool Test_Collision(Vector Start, Vector End, double Impact_Force, Projectile Impact_Projectile)
        {
            if (Exists == true)
            {                
                if (Start.I() <= Right_Top.I() && Start.J() >= Left_Bottom.J())
                {
                    if (End.I() >= Left_Bottom.I() && End.J() <= Right_Top.J())
                    {
                        Calculate_Damage(Impact_Projectile);
                        return true;
                    }
                }
                else if (Start.I() >= Left_Bottom.I() && Start.J() <= Right_Top.J())
                {
                    if (End.I() <= Right_Top.I() && End.J() >= Left_Bottom.J())
                    {
                        Calculate_Damage(Impact_Projectile);
                        return true;
                    }
                }
                else if (Start.I() <= Right_Bottom.I() && Start.J() <= Initial_Position.J())
                {
                    if (End.I() >= Initial_Position.I() && End.J() >= Right_Bottom.J())
                    {
                        Calculate_Damage(Impact_Projectile);
                        return true;
                    }
                }
                else if (Start.I() >= Initial_Position.I() && Start.J() >= Right_Bottom.J())
                {

                    if (End.I() <= Right_Bottom.I() && End.J() <= Initial_Position.J())
                    {
                        Calculate_Damage(Impact_Projectile);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Calculate_Damage(Projectile Impact_Projectile)
        {
            //Tests to see the new health of each opbject
            int New_Health = Health - Impact_Projectile.Return_Health();
            Impact_Projectile.Set_Health(-1 * New_Health);
            if (New_Health >= 0) Impact_Projectile.Stop_Exist();
            Health = New_Health;
            if (Health <= 0) Exists = false;
        }

        public void Visual_Check(Panel o)
        {
            if (Invincible == false)
            {
                if (Health <= 0)
                {                 
                    Display_Image.Visible = false;
                    o.Controls.Remove(Display_Image);
                }
            }
        }

        public double Return_Resistance()
        {
            return Resistance;
        }

        public bool Return_Invincible()
        {
            return Invincible;
        }

        public Vector Return_Initial_Position()
        {
            Vector initial_Position = Initial_Position.Clone();
            return initial_Position;
        }

        public Vector Return_Size()
        {
            return Size;
        }

        public string Return_Image_Path()
        {
            return Image_Path;
        }

        public bool Exist()
        {
            Read_Write_Lock1.Reset();
            return Exists;
            Read_Write_Lock1.Set();
        }

        public void Stop_Exist()
        {
            Read_Write_Lock1.Reset();
            Exists = false;
            Read_Write_Lock1.Set();
        }

        public int Return_Health()
        {
            return Health;
        }

        //public void Stop_Exist_Basic()
        //{
        //    Exists = false;
        //}

    }

    public class Projectile : Basic_Object
    {
        private double Drag_Coefficient = 0;
        private double Mass = 1;
        private double Surface_Area = 0;
        Object_Physics Projectile_Motion;

        public Projectile(Vector initial_Position, double drag_coefficient, double mass, double surface_area, bool invincible, int health, double resistance, Map_Physics map, Vector size, double Window_Size, string image_path) : base(invincible, health, resistance, map, initial_Position, size, Window_Size, image_path)
        {
            Drag_Coefficient = drag_coefficient;
            if (mass != 0) Mass = mass;
            Surface_Area = surface_area;
            Physics_Simulated = true;
        }

        public void Trigger_Movement(Vector Initial_Velocity, Vector Initial_Acceleration, bool Wind_Resistance)
        {
            Projectile_Motion = new Object_Physics(Map, Initial_Velocity, Initial_Position, Initial_Acceleration, Drag_Coefficient, Surface_Area, Mass, Wind_Resistance);
        }

        public Vector Return_Position()
        {
            return Projectile_Motion.Return_Position();
        }

        public Vector Return_Velocity()
        {
            return Projectile_Motion.Return_Velocity();
        }

        public Vector Advance_Frame()
        {
            Read_Write_Lock1.Reset();
            Exists = Projectile_Motion.Advance_Frame();
            Read_Write_Lock1.Set();

            return Projectile_Motion.Return_Position();
        }

        public void Set_Health(int New_Value)
        {
            Health = New_Value;
        }
    }

    public class Vector
    {
        protected double i = 0;
        protected double j = 0;

        public Vector(double I, double J)
        {
            i = I;
            j = J;
        }

        public Vector Clone()
        {
            return new Vector(i, j);
        }

        public double I()
        {
            return i;
        }

        public double J()
        {
            return j;
        }

        public void Multiply(double multiplier)
        {
            i *= multiplier;
            j *= multiplier;
        }

        public void Divide(double divider)
        {
            i /= divider;
            j /= divider;
        }

        public double Magnitude()
        {
            return Math.Sqrt(i * i + j * j);
        }

        public void Set_Vector(double I, double J)
        {
            i = I;
            j = J;
        }

        public void Add_Vector(Vector Vector_To_Add)
        {
            i += Vector_To_Add.I();
            j += Vector_To_Add.J();
        }

        public void Subtract_Vector(Vector Vector_To_Subtract)
        {
            i -= Vector_To_Subtract.I();
            j -= Vector_To_Subtract.J();
        }

        public void Square()
        {
            i *= i;
            j *= j;
        }

        public double Bearing()
        {
            double bearing = Math.PI / 2;
            if (i < 0)
            {
                bearing *= 3;
            }
            bearing -= Math.Atan(j / i);
            return bearing;
        }
    }


    public static class Create_Vector
    {
        public static Vector Calculate_Vector_From_Bearing(double Magnitude, double Bearing)
        {
            double J = Math.Cos(Bearing) * Magnitude;
            double I = Math.Sin(Bearing) * Magnitude;

            Vector Created_Vector = new Vector(I,J);
            return Created_Vector;
        }
    }


    public class Map_Physics
    {
        private Vector Gravity = new Vector(0, -9.81);
        private double Air_Density = 1.225;
        private double Frame_Time = 1;
        private double Canvas_Multiplier = 1;
        private string MapPath;
        private Vector P1_Pos;
        private Vector P2_Pos;

        public Map_Physics(Vector gravity, double air_Density, double FRAME_TIME, double canvas_Multiplier, string mapPath ,Vector P1_pos, Vector P2_pos)
        {
            Gravity = gravity;
            Air_Density = air_Density;
            Frame_Time = FRAME_TIME;
            Canvas_Multiplier = canvas_Multiplier;
            MapPath = mapPath;
            P1_Pos = P1_pos;
            P2_Pos = P2_pos;
        }

        public Vector Return_P1_Pos()
        {
            return P1_Pos;
        }

        public Vector Return_P2_Pos()
        {
            return P2_Pos;
        }

        public string Return_Map_Path()
        {
            return MapPath;
        }

        public Vector Map_Gravity()
        {
            return Gravity;
        }

        public double Map_Air_Density()
        {
            return Air_Density;
        }

        public double Return_Frame_Time()
        {
            return Frame_Time;
        }

        public double Return_Canvas_Multiplier()
        {
            return Canvas_Multiplier;
        }
    }

    public class Object_Physics
    {
        private ManualResetEvent Read_Write_Lock = new ManualResetEvent(false);
        private object Frame_Locker = new object();

        private Map_Physics Map;
        private Vector Velocity_Vector = new Vector(0, 0);
        private Vector Position_Vector = new Vector(0, 0);
        private Vector Acceleration_Vector = new Vector(0, 0);
        private double Speed = 0;
        private double Drag_Coefficient = 0.5;
        private double Surface_Area = 1;
        private double Mass = 1;
        private double Frame_Time = 1;
        private double Frame_Time_Squared = 1;
        private double Air_Resistance_Constant = 1;
        private bool Wind_Resistance;

        public Object_Physics(Map_Physics map, Vector Initial_Velocity, Vector Initial_Position, Vector Initial_Acceleration, double drag_coefficient, double surface_area, double mass, bool wind_resistance)
        {
            Map = map;
            Frame_Time = Map.Return_Frame_Time();
            Initial_Velocity.Multiply(Frame_Time);
            Velocity_Vector = Initial_Velocity;
            Initial_Position.Multiply(Map.Return_Canvas_Multiplier());
            Position_Vector = Initial_Position;
            Initial_Acceleration.Add_Vector(Map.Map_Gravity());
            Acceleration_Vector = Initial_Acceleration.Clone();
            Acceleration_Vector.Add_Vector(Map.Map_Gravity());
            Update_Speed();
            if (drag_coefficient != 0) Drag_Coefficient = drag_coefficient;
            if (surface_area != 0) Surface_Area = surface_area;
            if (mass != 0) Mass = mass;

            Air_Resistance_Constant = ((Map.Map_Air_Density() * Drag_Coefficient * Surface_Area) / 2);
            Frame_Time_Squared = Frame_Time * Frame_Time;
            Wind_Resistance = wind_resistance;
        }

        public bool Advance_Frame()
        {
            lock (Frame_Locker)
            {
                Read_Write_Lock.Reset();
                //Update Position_Vector
                double Canvas_Multiplier = Map.Return_Canvas_Multiplier();
                Vector tempVector = Velocity_Vector.Clone();
                tempVector.Multiply(Canvas_Multiplier);
                Position_Vector.Add_Vector(tempVector);
                

                //Update Velocity_Vector
                Vector Temp_Vector_2 = Acceleration_Vector.Clone();
                Temp_Vector_2.Multiply(Frame_Time_Squared);
                Velocity_Vector.Add_Vector(Temp_Vector_2);

                Update_Speed();

                //Update Acceleration_Vector
                Resolve_Forces();

                bool Exists = true;
                if (Position_Vector.J() < 0) Exists = false;

                Read_Write_Lock.Set();

                return Exists;
            }
        }

        private void Resolve_Forces()
        {
            Acceleration_Vector.Set_Vector(0, 0);

            if (Wind_Resistance == true)
            {
                Vector Air_Resistance_Vector = Calculate_Air_Resistance();
                Acceleration_Vector.Add_Vector(Air_Resistance_Vector);
            }

            Acceleration_Vector.Add_Vector(Map.Map_Gravity());
        }

        private Vector Calculate_Air_Resistance()
        {           
            double Air_Resistance = (Air_Resistance_Constant * (Speed * Speed));
            Air_Resistance /= Mass;
            double Resistance_Bearing = (Velocity_Vector.Bearing() + Math.PI) % (2 * Math.PI);
            Vector Air_Resistance_Vector = Create_Vector.Calculate_Vector_From_Bearing(Air_Resistance, Resistance_Bearing);

            return Air_Resistance_Vector;
        }

        private void Update_Speed()
        {
            Speed = Velocity_Vector.Magnitude() /  (2 * Frame_Time);
        }

        public Vector Return_Position()
        {
            Read_Write_Lock.WaitOne();
            return Position_Vector;
        }

        public Vector Return_Velocity()
        {
            return Velocity_Vector;
        }
    }
}
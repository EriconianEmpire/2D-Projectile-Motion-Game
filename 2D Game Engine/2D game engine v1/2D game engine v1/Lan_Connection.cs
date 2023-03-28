using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_game_engine_v1
{
    public partial class Lan_Connection : Form
    {


        public Lan_Connection()
        {
            InitializeComponent();

            /*Thread Update_Check = new Thread(new ThreadStart(Update_Checker));
            Update_Check.Start();*/
        }

        /*void Update_Checker()
        {
            while (true)
            {
                if (User_List.Update_Check() == true)
                {
                    foreach (string user in User_List.Return_User_Array())
                    {
                        string _user = user.Split(' ')[1];
                        Available_User_List.Text = Available_User_List + user + "\n";
                    }
                }
                Thread.Sleep(10000);
            }
        }*/



        /*public Lan_Connection()
        {
            InitializeComponent();
        }*/

        private void Connect_To_Server_Click(object sender, EventArgs e)
        {
            Server_Info.Add_Message("NewUser " + Name_Box.Text);
        }

        private void Join_Game_Click(object sender, EventArgs e)
        {
            int User_List_Index = Available_User_List.SelectedIndex;
            Server_Info.Add_Message("Connect " + User_List.Return_User(User_List_Index).Split(' ')[0] + " " + Server_Info.Local().ID + " " + Server_Info.Local().name);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            Server_Info.Add_Message("Refresh");
            Available_User_List.Items.Clear();
            Thread.Sleep(100);
            if (User_List.Update_Check() == true)
            {
                foreach (string user in User_List.Return_User_Array())
                {
                    Available_User_List.Items.Add(user.Split(' ')[1]);
                }
            }

            if (Incoming_Player.Text.ToString() != User_List.Return_Incoming_Name())
            {
                Incoming_Player.Text = User_List.Return_Incoming_Name();
                Accept.Visible = true;
                Reject.Visible = true;
            }

            if (Server_Info.Return_LAN() == true)
            {
                Disconnect.Visible = true;
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            Server_Info.Add_Message("AcceptConnect " + Server_Info.Remote().ID + " " + Server_Info.Local().ID);
            Accept.Visible = false;
            Reject.Visible = false;
            Disconnect.Visible = true;
            //Server_Info.Change_LAN_Status(true);
        }

        private void Reject_Click(object sender, EventArgs e)
        {
            Server_Info.Add_Message("RejectConnect " + Server_Info.Remote().ID + " " + Server_Info.Local().ID);
            Accept.Visible = false;
            Reject.Visible = false;
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            Server_Info.Add_Message("Disconnect");
            Server_Info.Change_LAN_Status(true);
        }
    }

    static public class User_List
    {
        static private List<string> Available_users = new List<string>();
        static private bool Updated = true;
        static private string Incoming_Name = "";

        static public string Return_Incoming_Name()
        {
            return Incoming_Name;
        }

        static public void Incoming_Request(string[] command)
        {
            Incoming_Name = command[4];
            
        }

        static public string[] Return_User_Array()
        {
            string[] users = new string[Available_users.Count];
            for (int i = 0; i < users.Length; i++)
            {
                users[i] = Available_users[i];
            }
            return users;
        }

        static public string Return_User(int Index)
        {
            return Available_users[Index];
        }

        static public void Add_User_To_List(string user)
        {
            Available_users.Add(user);
        }

        static public void Reset_User_List()
        {
            Available_users.Clear();
        }

        static public void Display_User_List()
        {
            Updated = true;

        }

        static public bool Update_Check()
        {
            bool updated = Updated;
            Updated = false;
            return updated;
        }
    }

}

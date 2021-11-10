using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cards;
using StringEnumClass;
using System.Media;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace My_Math
{

    public partial class FormMain : Form
    {
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public static SettingsClass.Settings Settings = new SettingsClass.Settings(Application.StartupPath);
        Recognize.Class_Table Recognize_Temp = new Recognize.Class_Table();

        public LogicClass.Logic Logic = new LogicClass.Logic();


        private void CustomerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //MessageBox.Show(e.PropertyName + " has been changed.");
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(SettingsClass.Settings.FileName))
            {
                Stream TestFileStream = File.OpenRead(SettingsClass.Settings.FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                Settings = (SettingsClass.Settings)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            Settings.PropertyChanged += this.CustomerPropertyChanged;
            //button_Settings.PerformClick();
        }

        private void button_Load_Screen_Click(object sender, EventArgs e)
        {
            string str = textBox_Num_Screen.Text;
            try
            {
                if(radioButton_Load_Test_Screen.Checked)
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Screen\Test\Test_" + str + ".jpg");
                else
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Screen\Game\Game_" + str + ".jpg");

                textBox_Num_Screen.Text = (Convert.ToInt32(str) + 1).ToString();
                if ((string)panel1.Tag == "init")
                {
                    panel1.HorizontalScroll.Value = panel1.Width;
                    panel1.VerticalScroll.Value = panel1.Height;
                    panel1.Tag = "work";
                }
            }
            catch
            {
                MessageBox.Show("Test_" + str + ".jpg" + " не найден!");
            }
        }

        private void button_Logic_Click(object sender, EventArgs e)
        {
        }

        private void button_Settings_Click(object sender, EventArgs e)
        {
            Form SettFrm = new FormSettings();
            SettFrm.Show(); // отображаем Form2
            //this.Hide(); // скрываем Form1 (this - текущая форма)
        }

        private void button_Save_Screen_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = OperationsWithImage.Capture_Screen();
            string str = textBox_Num_Save_Screen.Text;
            pictureBox1.Image.Save(Application.StartupPath + @"\Screen\Test\Test_" + str + ".jpg");
            textBox_Num_Save_Screen.Text = (Convert.ToInt32(str) + 1).ToString();
        }

        private void radioButton_Debug_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Debug.Checked)
            {
                groupBox_Debug.Visible = true;
            }
            else
            {
                groupBox_Debug.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(timer1.Tag);
            if (++i > 2) { i = 0; timer1.Enabled = false; }
            timer1.Tag = i;
            SystemSounds.Beep.Play();
        }

        private void timer_Screen_Tick(object sender, EventArgs e)
        {
            Game_Action(sender);
        }

        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        private void Display_Info_On_Form(Recognize.Class_Table Table, Game Game)
        {
            Recognize.PlayCards.Card_Struct[] board = Table.PlayCards.Board;
            Recognize.PlayCards.Card_Struct[][] player = Table.PlayCards.Player;
            Recognize.PlayCards.Card_Struct[] my = Table.PlayCards.My;
            Recognize.Finance finance = Table.Finance;
            Color[] suit_color = new Color[5];
            suit_color[0] = Color.White;
            suit_color[1] = Settings.Graphic_Table.Suit_Hearts_Color;
            suit_color[2] = Settings.Graphic_Table.Suit_Diamonds_Color;
            suit_color[3] = Settings.Graphic_Table.Suit_Clubs_Color;
            suit_color[4] = Settings.Graphic_Table.Suit_Spades_Color;
            string str;
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Cards On Board 
            for (int i = 0; i < 5; i++)
            {
                Panel pnl = groupBox_Play_Bot.Controls["panel_Card" + (i + 1).ToString() + "_On_Board_Color"] as Panel;
                pnl.BackColor = suit_color[(int)board[i].card.Suit];
                if (board[i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(board[i].status);
                else str = StringEnum.GetStringValue(board[i].card.Rank);
                pnl.Controls["label_Card" + (i + 1).ToString() + "_On_Board_Rank"].Text = str;
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Cards Players 
            for (int num_plyr = 1; num_plyr < 6; num_plyr++)
                for (int i = 0; i < 2; i++)
                {
                    string card_str = (i + 1).ToString();
                    string player_str = num_plyr.ToString();
                    Panel pnl = groupBox_Play_Bot.Controls["panel_Card" + card_str + "_Player" + player_str + "_Color"] as Panel;
                    pnl.BackColor = suit_color[(int)player[num_plyr][i].card.Suit];
                    if (player[num_plyr][i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(player[num_plyr][i].status);
                    else str = StringEnum.GetStringValue(player[num_plyr][i].card.Rank);
                    pnl.Controls["label_Card" + card_str + "_Player" + player_str + "_Rank"].Text = str;
                }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Cards My 
            for (int i = 0; i < 2; i++)
            {
                Panel pnl = groupBox_Play_Bot.Controls["panel_Card" + (i + 1).ToString() + "_My_Color"] as Panel;
                pnl.BackColor = suit_color[(int)my[i].card.Suit];
                if (my[i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(my[i].status);
                else str = StringEnum.GetStringValue(my[i].card.Rank);
                pnl.Controls["label_Card" + (i + 1).ToString() + "_My_Rank"].Text = str;
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Players Status
            for (int num_plyr = 1; num_plyr < 6; num_plyr++)
            {
                groupBox_Play_Bot.Controls["label_Player" + num_plyr.ToString() + "_Status"].Text = StringEnum.GetStringValue(Game.Players[num_plyr].Status);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Board Status
            groupBox_Play_Bot.Controls["label_Board_Status"].Text = StringEnum.GetStringValue(Game.Board.Status);
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Odds
            if (Game.Status == Game.GAME_STATUS.OK)
            {
                userControl11.Display(Game.My_Odds, Game.Opponents_Odds);
            }
            else
            {
                userControl11.Clear();
            }
            label_Stage_Action.Text = StringEnum.GetStringValue(Game.Stage_Action);
            label_Warning.Text = StringEnum.GetStringValue(Game.Warning_Status);
            label_Fint.Text = StringEnum.GetStringValue(Game.Fint);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        private void Beep(Game game)
        {
            if (game.Status == Game.GAME_STATUS.ERROR)
            {
                SystemSounds.Beep.Play();
                timer1.Enabled = true;
            }
            else if (game.Warning_Status == Game.WARNING_STATUS.GOOD_ODDS)
            {

                SystemSounds.Beep.Play();
                timer1.Enabled = true;
            }
            else if (game.Warning_Status == Game.WARNING_STATUS.OPPONENTS_IN_GAME)
            {
                if (radioButton_Play_Bot.Checked) //if not I play
                {
                    SystemSounds.Beep.Play();
                    timer1.Enabled = true;
                }

            }
        }

        private void button_Start_Stop_Click(object sender, EventArgs e)
        {
            if ((string)button_Start_Stop.Tag == "Start")
            {
                button_Start_Stop.Tag = "Stop";
                button_Start_Stop.Text = "Stop";
                button_Start_Stop.BackColor = Color.Red;
                timer_Screen.Interval = 200;
                timer_Screen.Enabled = true;
                timer_Screen.Tag = "true";
            }
            else
            {
                button_Start_Stop.Tag = "Start";
                button_Start_Stop.Text = "Start";
                button_Start_Stop.BackColor = Color.Green;
                timer_Screen.Enabled = false;
                timer_Screen.Tag = "false";
            }
        }

        public static object CloneObject(object obj)
        {
            if (obj == null) return null;

            Type t1 = obj.GetType();
            object ret = Activator.CreateInstance(t1);

            var properties = t1.GetProperties().ToArray();
            for (int i = 0; i < properties.Length; i++)
            {
                properties[i].SetValue(
                        ret,
                        properties[i].GetValue(obj)
                    );
            }
            return ret;
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        private void Game_Action(object sender)
        {
            string string_status = "";
            Bitmap screen;
            panel_Recognize.BackColor = Color.Red;
            panel_Recognize.Refresh();
            Recognize.Class_Table recognize = new Recognize.Class_Table();
            Game game = new Game();
            Logic.Clear();

            game.Fint = Logic.Fint;


            if (sender as Button == button_Preview) string_status = "PREVIEW";
            if (sender as Button == button_Test) string_status = "TEST";
            if (sender as System.Windows.Forms.Timer == timer_Screen)
            { string_status = "PLAY"; (sender as System.Windows.Forms.Timer).Enabled = false; }


            if (string_status == "PREVIEW" || string_status == "TEST") { screen = (Bitmap)pictureBox1.Image; }
            else if (string_status == "PLAY") { screen = OperationsWithImage.Capture_Screen(); }
            else { MessageBox.Show("ERROR!"); return; }

            Thread Thead_Start(ThreadStart start, string name, ThreadPriority priority)
            {
                Thread thead = new Thread(start);
                thead.Name = name;
                thead.Priority = priority;
                thead.Start();
                return thead;
            }

            void Define_Action()
            {
                game.Set_From_Settings(Settings);
                game.Set_From_Recognize(recognize);

                if (game.Status == Game.GAME_STATUS.OK)
                {
                    game.Get_To_Logic(Logic);
                    if (radioButton_Thread.Checked)
                    {
                        Logic.Evaluation();
                    }
                    else
                    {
                        Logic.Calc_Probabilities();
                    }
                    game.Set_From_Logic(Logic);
                }

                game.Warning(Logic); //even if Game.Status != Game.GAME_STATUS.OK
                Beep(game);
            }

            void Define_Action_PRE_FLOP(LogicClass.STAGE_ACTION act1, LogicClass.STAGE_ACTION act2)
            {
                
                if (recognize.Accelerate_Check != Recognize.MY_COURSE.OK) act2 = LogicClass.STAGE_ACTION.FOLD;
                int position = (int)recognize.Position_Players.Position[0];
                Logic.Hand_Cards_For_Fold = new List<string>();
                for (int i = 0; i < 169; i++)
                {
                    if (Settings.Game.Start_Hands[position].Hand[i].Enable) Logic.Hand_Cards_For_Fold.Add(Settings.Game.Start_Hands[position].Hand[i].Name);
                }

                Logic.Evaluation_Pre_Flop(act1, act2);
                game.Set_From_Logic(Logic);
                game.Warning(Logic);
                //Beep(game);

                string_status = "SEND COMMAND";
                game.Warning_Status = Game.WARNING_STATUS.ACCELERATE_FLOP;
                if (game.Stage_Action == LogicClass.STAGE_ACTION.WAIT)
                {
                    string_status = "WAIT";
                    game.Warning_Status = Game.WARNING_STATUS.GOOD_ODDS;
                    //if I play do beep
                    if (!radioButton_Play_Bot.Checked)
                    {
                        Beep(game);
                    }
                }
            }


            if (string_status == "PREVIEW")
            {
                if (radioButton_Thread.Checked)
                {
                    ThreadStart start = delegate () { Recognize.Recognize_Screen((Bitmap)pictureBox1.Image, recognize); };
                    Thread thead = Thead_Start(start, "Recognize Screen", ThreadPriority.Highest);
                    thead.Join();    //expect until the Thread comes to the end
                }
                else
                {
                    Recognize.Recognize_Screen((Bitmap)pictureBox1.Image, recognize);
                }

                if (radioButton_Debug.Checked)
                {
                    ThreadStart start = delegate () { Recognize.Display_Primitives_On_Screen(Settings.Graphic_Table, recognize, pictureBox1.CreateGraphics()); };
                    Thead_Start(start, "Display Primitives On Screen", ThreadPriority.Highest);
                }
                //--------------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------
                //LOGIC 
                Define_Action();

                Display_Info_On_Form(recognize, game);
                panel_Recognize.BackColor = Color.Black;
                panel_Recognize.Refresh();

                return;
            }

            if (Recognize.Recognize_My_Course.Check_Point_Be(screen, Settings.Graphic_Table.My_Course, value => recognize.My_Course = value))
            {
                label_Warning.Text = "";
                label_Fint.Text = "";
                label_Stage_Action.Text = "RECOGNIZE";
                label_Stage_Action.Refresh();

                timer_Screen.Interval = 1000;
                //Save Screen____________________________________________________________
                string str = textBox_Num_Game_Screen.Text;
                if (string_status != "TEST")
                {
                    screen.Save(Application.StartupPath + @"\Screen\Game\Game_" + str + ".jpg");
                    textBox_Num_Game_Screen.Text = (Convert.ToInt32(str) + 1).ToString();
                }


                //RECOGNIZE____________________________________________________________
                Recognize.Recognize_Screen(screen, recognize);
                //Recognize.Thread_Recognize_Screen(screen, recognize);
                label_Stage_Action.Text = "EVALUATION";
                label_Stage_Action.Refresh();
                //LOGIC____________________________________________________________
                Define_Action();
                if (game.Status == Game.GAME_STATUS.OK)
                {
                    if(radioButton_Play_Bot.Checked) string_status = "SEND COMMAND";  
                    //PRE-FLOP ____________________________________________________________
                    if (game.Stage == LogicClass.STAGE_STATUS.PRE_FLOP)
                    {
                        if (radioButton_Play_Bot.Checked) Define_Action_PRE_FLOP(game.Stage_Action,LogicClass.STAGE_ACTION.FOLD);
                        else Define_Action_PRE_FLOP(LogicClass.STAGE_ACTION.WAIT, LogicClass.STAGE_ACTION.FOLD);
                        //if (!radioButton_Play_Bot.Checked) SystemSounds.Beep.Play();
                    }
                }

                //DISPLAY____________________________________________________________
                Display_Info_On_Form(recognize, game);
            }
            else if (Recognize.Recognize_My_Course.Check_Pre_Flop_Accelerate_CheckFold(screen, recognize))
            {
                //LOGIC____________________________________________________________
                game.Set_From_Settings(Settings);
                game.Set_From_Recognize(recognize);

                if (game.Status == Game.GAME_STATUS.OK)
                {
                    game.Get_To_Logic(Logic);
                    Define_Action_PRE_FLOP(LogicClass.STAGE_ACTION.WAIT, LogicClass.STAGE_ACTION.CHECK);

                    label_Stage_Action.Text = StringEnum.GetStringValue(game.Stage_Action);
                    label_Warning.Text = StringEnum.GetStringValue(game.Warning_Status);
                    label_Fint.Text = StringEnum.GetStringValue(game.Fint);
                    userControl11.Clear();
                }
            }
            else
            {
                string_status = "WAIT";
                label_Stage_Action.Text = "WAIT";
                label_Warning.Text = "WAIT";
                label_Fint.Text = "WAIT";
            }
            switch (string_status)
            {
                case "SEND COMMAND":
                    timer_Screen.Interval = 2000;
                    string str = game.Stage_Action.ToString() + " KEY";
                    int attempt_cnt = 2;
                    while (Send_TCP_Message(str) != "<h1>OK!</h1>" && attempt_cnt>0)
                    {
                        label_Stage_Action.Text = "ERROR!!!" + " " + StringEnum.GetStringValue(game.Stage_Action) + " " + "SEND COMMAND";
                        label_Stage_Action.Refresh();
                        SystemSounds.Beep.Play();
                        attempt_cnt--;
                        if ((string)timer_Screen.Tag == "false") break;
                    }
                    if (attempt_cnt != 0)
                    {
                        label_Stage_Action.Text = StringEnum.GetStringValue(game.Stage_Action) + " " + "SEND COMMAND";
                        label_Stage_Action.Refresh();
                    }
                    else
                    {
                        string_status = "ERROR SEND COMMAND";
                    }
                    break;
                case "WAIT":
                    timer_Screen.Interval = 1000;
                    break;
            }

            if (string_status == "ERROR SEND COMMAND")
            {
                timer_Screen.Enabled = false;
                button_Start_Stop.Tag = "Stop";
                button_Start_Stop_Click(null, null);
            }

            if ((sender as System.Windows.Forms.Timer) == timer_Screen&& (string)timer_Screen.Tag != "false") { timer_Screen.Enabled = true; }

            panel_Recognize.BackColor = Color.Black;
            panel_Recognize.Refresh();
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            string str = "LED ON";
            if ((string)button2.Tag == "LED ON")
            {
                str = "LED OFF";
                button2.Tag = str;
                button2.Text = str;
                button2.BackColor = Color.Red;
                str = "LED ON";
            }
            else
            {
                str = "LED ON";
                button2.Tag = str;
                button2.Text = str;
                button2.BackColor = Color.Green;
                str = "LED OFF";
            }

            string receive_str = await Thread_Send_TCP_MessageAsync(str + " KEY");
            Console.WriteLine("================================");
            Console.WriteLine("RECEIVE:");
            Console.WriteLine(receive_str);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        private static async Task<string> Thread_Send_TCP_MessageAsync(string message)
        {
            Task<string> thr = Task.Run(() => Send_TCP_Message(message));
            string str = await thr;


            //Thread send = new Thread(Send_TCP_Message);
            //send.Name = "Send_TCP_Message: " + message;
            //send.Priority = ThreadPriority.AboveNormal;
            //send.Start();
            //send.Join();    //expect until the Thread comes to the end
            return str;
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        private static string Send_TCP_Message(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            TcpClient client;
            try
            {
                Console.WriteLine("================================");
                Console.WriteLine("=   Connected to the server    =");
                Console.WriteLine("================================");
                client = new System.Net.Sockets.TcpClient(); // Create a new connection   
                client.ReceiveTimeout = 1000;
                client.Connect("192.168.0.199", 8080);
            }
            catch (Exception e) // Catch exceptions   
            {
                Console.WriteLine("================================");
                Console.WriteLine("NO CONNECT");
                Console.WriteLine(e.Message);
                return "NO CONNECT";
            }
            Console.WriteLine("================================");
            Console.WriteLine("SEND:");
            Console.WriteLine(message);

            NetworkStream stream = client.GetStream();
            stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes   

            Console.WriteLine("Waiting for response...");
 
            int bytesRead;
            messageBytes = new byte[100];
            try
            {
                bytesRead = stream.Read(messageBytes, 0, messageBytes.Length);
            }
            catch (Exception e) // Catch exceptions   
            {
                stream.Dispose();
                client.Close();
                Console.WriteLine("================================");
                Console.WriteLine("NO RESPONSE");
                Console.WriteLine(e.Message);
                return "NO RESPONSE";
            }

            // Clean up   
            stream.Dispose();
            client.Close();

            string str = Encoding.ASCII.GetString(messageBytes, 0, bytesRead);
            return str; // Return response   
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button_Preview_Click(object sender, EventArgs e)
        {
            Game_Action(sender);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            Game_Action(sender);
        }

        private void groupBox_Play_Enter(object sender, EventArgs e)
        {

        }
    }
}

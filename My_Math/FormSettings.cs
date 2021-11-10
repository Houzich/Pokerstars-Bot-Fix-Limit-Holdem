using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using StringEnumClass;
using SettingsClass;
using System.Runtime.InteropServices;

namespace My_Math
{
    public partial class FormSettings : Form
    {
        //[DllImport("user64.dll")]
        //static extern bool LockWindowUpdate(IntPtr hWndLock); //lock/allow paint

        private SettingsClass.Settings Settings = FormMain.Settings;
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*Save Settings in File*/
        public void Save_Settings_In_File()
        {
            Stream TestFileStream = File.Create(SettingsClass.Settings.FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, Settings);
            TestFileStream.Close();
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*Actions After Set Settings Variable*/
        public void Actions_After_Set_Settings_Variable()
        {
            Save_Settings_In_File();
            pictureBox1.Refresh();
            Recognize.Class_Table Table = new Recognize.Class_Table();
            Recognize.Recognize_Screen((Bitmap)pictureBox1.Image, Table);
            Recognize.Display_Primitives_On_Screen(Settings.Graphic_Table, Table, pictureBox1.CreateGraphics());
            if (radioButton_Show_Bitmap.Checked)
                Recognize.Display_Text_Area_On_Screen(Settings.Graphic_Table, Table, pictureBox1.CreateGraphics(), (Bitmap)pictureBox1.Image);
            Display_Info_On_Form(Table.PlayCards);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Display_Cards_Name_Monohrome(SettingsClass.Settings.Card[] sett, string type_cards)
        {
            for (int i = 0; i < sett.Length; i++)
            {
                /*!!TEMP!!*/
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, sett[i].Rectangle_Rank);
                int coeff = Settings.Graphic_Table.Rank_Black_White_Coeff;
                string str = "";
                switch (type_cards)
                {
                    case "Board": str = "pictureBox_Card" + (i + 1).ToString() + "_On_Board_Name"; break;
                    case "Player1": str = "pictureBox_Card" + (i + 1).ToString() + "_Player1_Name"; break;
                    case "Player2": str = "pictureBox_Card" + (i + 1).ToString() + "_Player2_Name"; break;
                    case "Player3": str = "pictureBox_Card" + (i + 1).ToString() + "_Player3_Name"; break;
                    case "Player4": str = "pictureBox_Card" + (i + 1).ToString() + "_Player4_Name"; break;
                    case "Player5": str = "pictureBox_Card" + (i + 1).ToString() + "_Player5_Name"; break;
                    case "My": str = "pictureBox_Card" + (i + 1).ToString() + "_My_Name"; break;
                }
                if (str != "") (this.Controls[str] as PictureBox).Image = OperationsWithImage.Bitmap_To_BlackWhite_Monochrome(reg_img, coeff);
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*распознаем все карты на скрине*/
        private void Display_Info_On_Form(Recognize.PlayCards PlayCards)
        {
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
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
                Panel pnl = this.Controls["panel_Card" + (i + 1).ToString() + "_On_Board_Color"] as Panel;
                pnl.BackColor = suit_color[(int)PlayCards.Board[i].card.Suit];
                if (PlayCards.Board[i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(PlayCards.Board[i].status);
                else str = StringEnum.GetStringValue(PlayCards.Board[i].card.Rank);
                pnl.Controls["label_Card" + (i + 1).ToString() + "_On_Board_Rank"].Text = str;
                pnl.Controls["panel_Point" + (i + 1).ToString() + "_On_Board_Color"].BackColor = PlayCards.Board[i].clr_suit;
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Cards Players 
            for (int player = 1; player < 6; player++)
                for (int i = 0; i < 2; i++)
                {
                    string card_str = (i + 1).ToString();
                    string player_str = player.ToString();
                    Panel pnl = this.Controls["panel_Card" + card_str + "_Player" + player_str + "_Color"] as Panel;
                    pnl.BackColor = suit_color[(int)PlayCards.Player[player][i].card.Suit];
                    if (PlayCards.Player[player][i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(PlayCards.Player[player][i].status);
                    else str = StringEnum.GetStringValue(PlayCards.Player[player][i].card.Rank);
                    pnl.Controls["label_Card" + card_str + "_Player" + player_str + "_Rank"].Text = str;
                }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Display Cards My 
            for (int i = 0; i < 2; i++)
            {
                Panel pnl = this.Controls["panel_Card" + (i + 1).ToString() + "_My_Color"] as Panel;
                pnl.BackColor = suit_color[(int)PlayCards.My[i].card.Suit];
                if (PlayCards.My[i].status != Recognize.RECOGNIZE_CARD_STATUS.OK) str = StringEnum.GetStringValue(PlayCards.My[i].status);
                else str = StringEnum.GetStringValue(PlayCards.My[i].card.Rank);
                pnl.Controls["label_Card" + (i + 1).ToString() + "_My_Rank"].Text = str;
            }
        }




        public FormSettings()
        {
            InitializeComponent();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            int ind = comboBox_Num_Card.SelectedIndex;
            int suit = comboBox_Suit_Card.SelectedIndex;
            if ((ind == -1) || (suit == -1)) { return; }
            this.Enabled = false;
            Color clr = ((Bitmap)pictureBox1.Image).GetPixel(
                                                Settings.Graphic_Table.Board.Card[ind].Pixel.Back.Point.X,
                                                Settings.Graphic_Table.Board.Card[ind].Pixel.Back.Point.Y);
            if (suit == 0)
            {
                Settings.Graphic_Table.Suit_Hearts_Color = clr;
                panel_Hearts_Color.BackColor = clr;
            }
            if (suit == 1)
            {
                Settings.Graphic_Table.Suit_Diamonds_Color = clr;
                panel_Diamonds_Color.BackColor = clr;
            }
            if (suit == 2)
            {
                Settings.Graphic_Table.Suit_Clubs_Color = clr;
                panel_Clubs_Color.BackColor = clr;
            }
            if (suit == 3)
            {
                Settings.Graphic_Table.Suit_Spades_Color = clr;
                panel_Spades_Color.BackColor = clr;
            }
            if (suit == 4)
            {
                Settings.Graphic_Table.Suit_None_Color = clr;
                panel_None_Color.BackColor = clr;
            }

            Actions_After_Set_Settings_Variable();
            this.Enabled = true;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            SettingsClass.Settings.Board board = Settings.Graphic_Table.Board;
            SettingsClass.Settings.Player player = new SettingsClass.Settings.Player();
            SettingsClass.Settings.Player my = Settings.Graphic_Table.My;
            SettingsClass.Settings.My_Course my_course = Settings.Graphic_Table.My_Course;
            SettingsClass.Settings.My_Course my_course_call = Settings.Graphic_Table.My_Course_Call;
            SettingsClass.Settings.My_Course accelerate_fold = Settings.Graphic_Table.Accelerate_Fold;
            SettingsClass.Settings.My_Course accelerate_check = Settings.Graphic_Table.Accelerate_Check;
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Primitives Cards
            if (radioButton_Cards_Board.Checked == true)
            {
                int distance = 0;
                for (int i = 0; i < 5; i++)
                {
                    Point p = new Point(e.X + distance, e.Y);
                    board.Card[i].Pixel.Suit.Point = p;
                    board.Card[i].Pixel.Back.Point = new Point(p.X + Settings.Graphic_Table.Card_Back_Color_Distance_X, p.Y + Settings.Graphic_Table.Card_Back_Color_Distance_Y);
                    board.Card[i].Rectangle_Rank = new Rectangle(p.X + Settings.Graphic_Table.Card_Suit_Rank_Distance_X, p.Y + Settings.Graphic_Table.Card_Suit_Rank_Distance_Y, Settings.Graphic_Table.Сard_Rank_Width, Settings.Graphic_Table.Сard_Rank_Height);
                    distance += Settings.Graphic_Table.Сard_On_Board_Distance;
                }
            }

            if (radioButton_Cards_My.Checked == true) player = Settings.Graphic_Table.Player[0];
            if (radioButton_Cards_Player_1.Checked == true) player = Settings.Graphic_Table.Player[1];
            if (radioButton_Cards_Player_2.Checked == true) player = Settings.Graphic_Table.Player[2];
            if (radioButton_Cards_Player_3.Checked == true) player = Settings.Graphic_Table.Player[3];
            if (radioButton_Cards_Player_4.Checked == true) player = Settings.Graphic_Table.Player[4];
            if (radioButton_Cards_Player_5.Checked == true) player = Settings.Graphic_Table.Player[5];

            if (radioButton_Cards_My.Checked == true ||
                radioButton_Cards_Player_1.Checked == true ||
                radioButton_Cards_Player_2.Checked == true ||
                radioButton_Cards_Player_3.Checked == true ||
                radioButton_Cards_Player_4.Checked == true ||
                radioButton_Cards_Player_5.Checked == true)
            {
                int distance = 0;
                for (int i = 0; i < 2; i++)
                {
                    Point p = new Point(e.X + distance, e.Y);
                    player.Card[i].Pixel.Suit.Point = p;
                    player.Card[i].Pixel.Back.Point = new Point(p.X + Settings.Graphic_Table.Card_Back_Color_Distance_X, p.Y + Settings.Graphic_Table.Card_Back_Color_Distance_Y);
                    player.Card[i].Rectangle_Rank = new Rectangle(p.X + Settings.Graphic_Table.Card_Suit_Rank_Distance_X, p.Y + Settings.Graphic_Table.Card_Suit_Rank_Distance_Y, Settings.Graphic_Table.Сard_Rank_Width, Settings.Graphic_Table.Сard_Rank_Height);
                    distance += Settings.Graphic_Table.Card_Player_Distance;
                }
            }

            if (radioButton_None.Checked == false)
            {
                Actions_After_Set_Settings_Variable();
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Button
            player = null;
            if (radioButton_Set_Point_Button_My.Checked == true) player = Settings.Graphic_Table.Player[0];
            if (radioButton_Set_Point_Button_Player_1.Checked == true) player = Settings.Graphic_Table.Player[1];
            if (radioButton_Set_Point_Button_Player_2.Checked == true) player = Settings.Graphic_Table.Player[2];
            if (radioButton_Set_Point_Button_Player_3.Checked == true) player = Settings.Graphic_Table.Player[3];
            if (radioButton_Set_Point_Button_Player_4.Checked == true) player = Settings.Graphic_Table.Player[4];
            if (radioButton_Set_Point_Button_Player_5.Checked == true) player = Settings.Graphic_Table.Player[5];

            if (player != null)
            {
                player.Point_Button = new Point(e.X, e.Y);
                player.Color_Button = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            }

            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Point Stage Action            
            if (radioButton_My_Course.Checked == true)
            {
                my_course.Point = new Point(e.X, e.Y);
                my_course.Color_Action = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Point Stage Action Call_Fold            
            if (radioButton_My_Course_Call.Checked == true)
            {
                my_course_call.Point = new Point(e.X, e.Y);
                my_course_call.Color_Action = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            }           
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Point Accelerate Fold            
            if (radioButton_Accelerate_Fold.Checked == true)
            {
                accelerate_fold.Point = new Point(e.X, e.Y);
                accelerate_fold.Color_Action = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Point Accelerate Check            
            if (radioButton_Accelerate_Check.Checked == true)
            {
                accelerate_check.Point = new Point(e.X, e.Y);
                accelerate_check.Color_Action = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            }

            
            if (radioButton_Set_Point_Button_None.Checked == false)
            {
                Actions_After_Set_Settings_Variable();
            }






            Set_Point_Money(e);

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            panel_Hearts_Color.BackColor = Settings.Graphic_Table.Suit_Hearts_Color;
            panel_Diamonds_Color.BackColor = Settings.Graphic_Table.Suit_Diamonds_Color;
            panel_Clubs_Color.BackColor = Settings.Graphic_Table.Suit_Clubs_Color;
            panel_Spades_Color.BackColor = Settings.Graphic_Table.Suit_Spades_Color;
            panel_None_Color.BackColor = Settings.Graphic_Table.Suit_None_Color;

            label_Hearts_Name.Text = "Hearts";
            label_Diamonds_Name.Text = "Diamonds";
            label_Clubs_Name.Text = "Clubs";
            label_Spades_Name.Text = "Spades";
            label_None_Name.Text = "None";

            propertyGrid1.SelectedObject = Settings;
            comboBox_Set_Point_Money.SelectedIndex = 0;
            comboBox_Set_Start_Hands.SelectedIndex = 0;
        }
        private void Button_save_settings_Click(object sender, EventArgs e)
        {
            Stream TestFileStream = File.Create(SettingsClass.Settings.FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, Settings);
            TestFileStream.Close();
        }
        private void Button_Load_Screen_Click(object sender, EventArgs e)
        {
            string str = textBox_Num_Screen.Text;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Screen\Test\Test_" + str + ".jpg");
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
        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton_Cards_Players_Back_Color.Checked == true)
            {
                for (int player = 1; player < 6; player++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Color_Back = ((Bitmap)pictureBox1.Image).GetPixel(
                                                Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Point.X,
                                                Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Point.Y);
                    }
                }

            }

            if (radioButton_Cards_Players_No_Color.Checked == true)
            {
                for (int player = 1; player < 6; player++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Color_No = ((Bitmap)pictureBox1.Image).GetPixel(
                                                Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Point.X,
                                                Settings.Graphic_Table.Player[player].Card[i].Pixel.Back.Point.Y);
                    }
                }

            }

            if (radioButton_Cards_On_Board_No_Color.Checked == true)
            {
                for (int i = 0; i < 5; i++)
                {

                    Settings.Graphic_Table.Board.Card[i].Pixel.Back.Color_No = ((Bitmap)pictureBox1.Image).GetPixel(
                                            Settings.Graphic_Table.Board.Card[i].Pixel.Back.Point.X,
                                            Settings.Graphic_Table.Board.Card[i].Pixel.Back.Point.Y);
                }

            }

            if (radioButton_Cards_My_No_Color.Checked == true)
            {
                for (int i = 0; i < 2; i++)
                {

                    Settings.Graphic_Table.My.Card[i].Pixel.Back.Color_No = ((Bitmap)pictureBox1.Image).GetPixel(
                                            Settings.Graphic_Table.My.Card[i].Pixel.Back.Point.X,
                                            Settings.Graphic_Table.My.Card[i].Pixel.Back.Point.Y);
                }

            }

            if (radioButton_Set_Point_Color_None.Checked != true)
            {
                Actions_After_Set_Settings_Variable();
            }
        }
        private void button_Recognize_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Recognize Cards
            Recognize.Class_Table Table = new Recognize.Class_Table();
            Graphics gr = pictureBox1.CreateGraphics();
            Bitmap btm = (Bitmap)pictureBox1.Image;
            Recognize.Recognize_Screen(btm, Table);
            Recognize.Display_Primitives_On_Screen(Settings.Graphic_Table, Table, gr);
            if (radioButton_Show_Bitmap.Checked)
                Recognize.Display_Text_Area_On_Screen(Settings.Graphic_Table, Table, gr, btm);


            Display_Info_On_Form(Table.PlayCards);
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Board.Card, "Board");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.My.Card, "My");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Player[1].Card, "Player1");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Player[2].Card, "Player2");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Player[3].Card, "Player3");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Player[4].Card, "Player4");
            Display_Cards_Name_Monohrome(Settings.Graphic_Table.Player[5].Card, "Player5");
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Bet Bitmap
            pictureBox_Bet_Bitmap.Image = Settings.Graphic_Table.Player[0].Bet.Bitmap_Dollar;
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Header
            //Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, Table.Bets.Header.Rectangle);
            //int coeff = Settings.Graphic_Table.Header.Black_White_Coeff;
            //pictureBox_Header.Image = OperationsWithImage.Bitmap_To_BlackWhite_Monochrome(reg_img, coeff);

        }
        private void Set_Point_Money(MouseEventArgs e)
        {
            SettingsClass.Settings.Board board = Settings.Graphic_Table.Board;
            SettingsClass.Settings.Player player = new SettingsClass.Settings.Player();
            SettingsClass.Settings.Player my = Settings.Graphic_Table.My;
            SettingsClass.Settings.Money pot = Settings.Graphic_Table.Pot;
            SettingsClass.Settings.Money call = Settings.Graphic_Table.Call;
            SettingsClass.Settings.Money raise = Settings.Graphic_Table.Raise;
            SettingsClass.Settings.Money header = Settings.Graphic_Table.Header;


            String str = comboBox_Set_Point_Money.Text;
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Bet
            player = null;
            if (str == "Bet My") player = Settings.Graphic_Table.Player[0];
            if (str == "Bet Player 1") player = Settings.Graphic_Table.Player[1];
            if (str == "Bet Player 2") player = Settings.Graphic_Table.Player[2];
            if (str == "Bet Player 3") player = Settings.Graphic_Table.Player[3];
            if (str == "Bet Player 4") player = Settings.Graphic_Table.Player[4];
            if (str == "Bet Player 5") player = Settings.Graphic_Table.Player[5];

            if (player != null)
            {
                int width = player.Bet.Search_Rectangle.Width;
                int heigh = player.Bet.Search_Rectangle.Height;
                player.Bet.Search_Rectangle = new Rectangle(e.X - width, e.Y, width, heigh);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Stack
            player = null;
            if (str == "Stack My") player = Settings.Graphic_Table.Player[0];
            if (str == "Stack Player 1") player = Settings.Graphic_Table.Player[1];
            if (str == "Stack Player 2") player = Settings.Graphic_Table.Player[2];
            if (str == "Stack Player 3") player = Settings.Graphic_Table.Player[3];
            if (str == "Stack Player 4") player = Settings.Graphic_Table.Player[4];
            if (str == "Stack Player 5") player = Settings.Graphic_Table.Player[5];

            if (player != null)
            {
                int width = player.Stack.Search_Rectangle.Width;
                int heigh = player.Stack.Search_Rectangle.Height;
                player.Stack.Search_Rectangle = new Rectangle(e.X, e.Y, width, heigh);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Pot
            if (str == "Pot")
            {
                int w = pot.Search_Rectangle.Width;
                int h = pot.Search_Rectangle.Height;
                pot.Search_Rectangle = new Rectangle(e.X, e.Y, w, h);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Call
            if (str == "Call")
            {
                int w = call.Search_Rectangle.Width;
                int h = call.Search_Rectangle.Height;
                call.Search_Rectangle = new Rectangle(e.X, e.Y, w, h);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Raise
            if (str == "Raise")
            {
                int w = raise.Search_Rectangle.Width;
                int h = raise.Search_Rectangle.Height;
                raise.Search_Rectangle = new Rectangle(e.X, e.Y, w, h);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Set Points Header
            if (str == "Header")
            {
                int w = header.Search_Rectangle.Width;
                int h = header.Search_Rectangle.Height;
                header.Search_Rectangle = new Rectangle(e.X, e.Y, w, h);
            }

            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //set points Dollar Header and save bitmap
            if (str == "Header Dollar Bitmap")
            {
                int w = header.Img_Dollar_Width;
                int h = header.Img_Dollar_Height;
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, new Rectangle(e.X, e.Y, w, h));
                header.Bitmap_Dollar = reg_img;
                reg_img.Save(Application.StartupPath + header.FileName_Dollar_Png);
            }

            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //set points Dollar Pot and save bitmap
            if (str == "Pot Dollar Bitmap")
            {
                int w = pot.Img_Dollar_Width;
                int h = pot.Img_Dollar_Height;
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, new Rectangle(e.X, e.Y, w, h));
                pot.Bitmap_Dollar = reg_img;
                reg_img.Save(Application.StartupPath + pot.FileName_Dollar_Png);
            }

            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //set points Dollar Bet and save bitmap
            if (str == "Bet Dollar Bitmap")
            {
                int w = Settings.Graphic_Table.Player[0].Bet.Img_Dollar_Width;
                int h = Settings.Graphic_Table.Player[0].Bet.Img_Dollar_Height;
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, new Rectangle(e.X, e.Y, w, h));
                reg_img.Save(Application.StartupPath + Settings.Graphic_Table.Player[0].Bet.FileName_Dollar_Png);
                for (int i = 0; i < 6; i++)
                {
                    Settings.Graphic_Table.Player[i].Bet.Bitmap_Dollar = reg_img;
                }
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //set points Dollar Call/Raise and save bitmap
            if (str == "Call/Raise Dollar Bitmap")
            {
                int w = call.Img_Dollar_Width;
                int h = call.Img_Dollar_Height;
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, new Rectangle(e.X, e.Y, w, h));
                call.Bitmap_Dollar = reg_img;
                raise.Bitmap_Dollar = reg_img;
                reg_img.Save(Application.StartupPath + call.FileName_Dollar_Png);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //set points Dollar Stack and save bitmap
            if (str == "Stack Dollar Bitmap")
            {
                int w = Settings.Graphic_Table.Player[0].Stack.Img_Dollar_Width;
                int h = Settings.Graphic_Table.Player[0].Stack.Img_Dollar_Height;
                Bitmap reg_img = OperationsWithImage.Get_Image_Region((Bitmap)pictureBox1.Image, new Rectangle(e.X, e.Y, w, h));
                reg_img.Save(Application.StartupPath + Settings.Graphic_Table.Player[0].Stack.FileName_Dollar_Png);
                for (int i = 0; i < 6; i++)
                {
                    Settings.Graphic_Table.Player[i].Stack.Bitmap_Dollar = reg_img;
                }
            }

            if (str != "None")
            {
                Actions_After_Set_Settings_Variable();
            }

        }

        private void comboBox_Set_Start_Hands_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void Save_Start_Hands(Table_Start_Hands.Table_Start_Hands table_start_hands)
        {
            Settings.Game.Start_Hands[table_start_hands.Start_Hands.Position_Index] = new Settings.Class_Start_Hands(table_start_hands.Start_Hands);
            Save_Settings_In_File();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String str = comboBox_Set_Start_Hands.Text;
            //delete already existing components
                List<Control> del_list = new List<Control>();
            foreach (Control obj in Controls)
                if ((string)obj.Tag == "Table_Start_Hands") del_list.Add(obj);

            foreach (Control obj in del_list) obj.Parent.Controls.Remove(obj);


            if (str == "NONE") { return; }

            Label lbl = new Label();
            lbl.Location = new Point(130, 30);
            lbl.Font = new Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl.Tag = "Table_Start_Hands";
            lbl.AutoSize = true;
            lbl.Text = str;

            Table_Start_Hands.Table_Start_Hands table_start_hands = new Table_Start_Hands.Table_Start_Hands();            
            table_start_hands.Start_Hands = new Settings.Class_Start_Hands(Settings.Game.Start_Hands[comboBox_Set_Start_Hands.SelectedIndex - 1]);
            table_start_hands.Start();
            table_start_hands.Tag = "Table_Start_Hands";
            table_start_hands.Location = new Point(130, 30+ lbl.Height);
            table_start_hands.onSave += new Table_Start_Hands.Table_Start_Hands.SaveDelegate(Save_Start_Hands);

            //LockWindowUpdate(table_start_hands.Handle); //lock paint
            this.Controls.Add(lbl);
            this.Controls.Add(table_start_hands);
            lbl.BringToFront();
            table_start_hands.BringToFront();
            //LockWindowUpdate(IntPtr.Zero); //allow paint
            lbl.Refresh();
            table_start_hands.Refresh();
        }

        private void radioButton_Stage_Action_Color_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using LogicClass;
using Cards;
using mshtml;
using HoldemHand;
using StringEnumClass;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SettingsClass;
using System.Runtime.InteropServices;
using System.Media;

using My_Math;

namespace P_Logic
{
    //public Cards.Deck Deckddd;
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock); //lock/allow paint
        public static SettingsClass.Settings Settings = new SettingsClass.Settings(Application.StartupPath);

        public partial class CardPictureBox : PictureBox
        {
            private Control parent_CardPictureBox;
            private CardClass.Card card;
            public Control Parent_CardPictureBox { get { return parent_CardPictureBox; } set { parent_CardPictureBox = value; } }
            public CardClass.Card Card { get { return card; } set { card = value; } }

            public CardPictureBox()
            {
                this.Card = new CardClass.Card();
            }

        }

        public CardClass.Deck Deck;

        public Form1()
        {
            InitializeComponent();
            Deck = new CardClass.Deck();

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(50, 10);
            Console.SetBufferSize(50, 10);
        }

        private void DropDownDeckImage(object sender, MouseEventArgs e)
        {
            //Получаем ссылку на кнопку, на которую нажали
            PictureBox ClickImg = (PictureBox)sender;
            //Создаем новую кнопку
            DropDownAllClose();
            Panel DropDownPanel = new Panel();
            DropDownPanel.Tag = "DropDownPanel";
            DropDownPanel.AutoSize = false;
            DropDownPanel.Width = (ClickImg.Width + 2) * 4;
            DropDownPanel.Height = 260;
            DropDownPanel.AutoScroll = true;
            DropDownPanel.BackColor = Color.Maroon;
            DropDownPanel.Location = new Point(ClickImg.Location.X, ClickImg.Location.Y + ClickImg.Height + 2);

            foreach (var suit in Enum.GetValues(typeof(CardClass.SUIT)))
            {
                if ((CardClass.SUIT)suit == CardClass.SUIT.None) continue;
                foreach (var rank in Enum.GetValues(typeof(CardClass.RANK)))
                {
                    if ((CardClass.RANK)rank == CardClass.RANK.None) continue;
                    CardClass.Card card = new CardClass.Card((int)rank, (int)suit);
                    int row = (int)card.Rank - (int)CardClass.RANK.Two;
                    int coll = (int)card.Suit - (int)CardClass.SUIT.Hearts;
                    CardPictureBox CardImg = new CardPictureBox();
                    CardImg.SizeMode = PictureBoxSizeMode.AutoSize;

                    var dir = System.IO.Directory.GetCurrentDirectory();
                    string imgCrdPath = dir + @"\Desk_Image\";
                    if (Deck.FindCard(card))
                    {
                        Bitmap btm = new Bitmap(imgCrdPath + card.ToString() + ".gif");
                        CardImg.Image = btm.Clone(new System.Drawing.Rectangle(0, 0, btm.Width, 18), btm.PixelFormat);
                        CardImg.Location = new Point(0 + (CardImg.Width + 2) * coll, 0 + (CardImg.Height + 2) * row);
                        CardImg.MouseClick += new MouseEventHandler(DropDownImageClick);
                    }
                    else
                    {
                        card = new CardClass.Card("None", "None");
                        Bitmap btm = new Bitmap(imgCrdPath + @"None None.gif");
                        CardImg.Image = btm.Clone(new System.Drawing.Rectangle(0, 0, btm.Width, 18), btm.PixelFormat);
                        CardImg.Location = new Point(0 + (CardImg.Width + 2) * coll, 0 + (CardImg.Height + 2) * row);
                        CardImg.MouseClick += new MouseEventHandler(DropDownImageClick);
                    }
                    CardImg.Parent_CardPictureBox = ClickImg;
                    CardImg.Card = card;
                    DropDownPanel.Controls.Add(CardImg);
                }
            }
            
            this.Controls.Add(DropDownPanel);
            this.Controls.SetChildIndex(DropDownPanel, 0);
        }

        void DropDownAllClose()
        {
            foreach (Control dropdown in Controls)
            {
                if ((dropdown is Panel)&&((string)dropdown.Tag == "DropDownPanel"))
                {
                    dropdown.Parent.Controls.Remove(dropdown);
                }
            }
        }

        void DropDownImageClick (object sender, MouseEventArgs e)
        {
            CardPictureBox ClickImg = (CardPictureBox)sender;
            var dir = System.IO.Directory.GetCurrentDirectory();
            string imgCrdPath = dir + @"\Desk_Image\";
            //(ClickImg.Parent_CardPictureBox as CardPictureBox).Image = ClickImg.Image;
            (ClickImg.Parent_CardPictureBox as CardPictureBox).Image = Image.FromFile(imgCrdPath + ClickImg.Card.ToString() + ".gif");
            CardClass.Card par_card = (ClickImg.Parent_CardPictureBox as CardPictureBox).Card;
            CardClass.Card img_card = ClickImg.Card;
            if (par_card.Rank != CardClass.RANK.None)
            {
                Deck.AddCard(par_card);
            }
            (ClickImg.Parent_CardPictureBox as CardPictureBox).Card = ClickImg.Card;
            if (img_card.Rank != CardClass.RANK.None)
            {
                Deck.RemoveCard(img_card);
            }
            ClickImg.Parent.Parent.Controls.Remove(ClickImg.Parent);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DropDownDeckImage(sender, e);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); // в конструкторе
            //Form1 F = new Form1();
            //F.Opacity = 99;
            //pictureBox1.Parent = F;
            //pictureBox1.BackColor = Color.Transparent;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            DropDownAllClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogicClass.Logic logic = new LogicClass.Logic();
            Game game = new Game();
            string string_status = "";

            void Define_Action()
            {
                game.Set_From_Settings(Settings);
                game.Opponents_Stage = 0;
                game.Opponents_Game = 5;


                logic.Bets.Bet = 1;
                logic.Bets.Pot = 1;
                logic.Bets.Call = 1;
                logic.Bets.Raise = 1;
                logic.Opponents_Stage = 0;
                logic.Opponents_Game = 5;
                logic.Hand_Card[0] = my_card1_PictureBox.Card;
                logic.Hand_Card[1] = my_card2_PictureBox.Card;
                logic.Hand_Card[2] = board_card1_PictureBox.Card;
                logic.Hand_Card[3] = board_card2_PictureBox.Card;
                logic.Hand_Card[4] = board_card3_PictureBox.Card;
                logic.Hand_Card[5] = board_card4_PictureBox.Card;
                logic.Hand_Card[6] = board_card5_PictureBox.Card;

                if (logic.Hand_Card[2].Rank == CardClass.RANK.None) logic.Stage = LogicClass.STAGE_STATUS.PRE_FLOP;
                else if (logic.Hand_Card[5].Rank == CardClass.RANK.None) logic.Stage = LogicClass.STAGE_STATUS.FLOP;
                else if (logic.Hand_Card[6].Rank == CardClass.RANK.None) logic.Stage = LogicClass.STAGE_STATUS.TURN;
                else if (logic.Hand_Card[6].Rank != CardClass.RANK.None) logic.Stage = LogicClass.STAGE_STATUS.RIVER;
                logic.Call_Сriterion = game.Call_Сriterion;
                logic.Raise_Criterion = game.Raise_Сriterion;


                ComboBox combo_box = new ComboBox();
                RadioButton radio_button = new RadioButton();
                for (int i = 0; i < 6; i++)
                {
                    foreach (Control cmb in Controls)
                        if ((cmb is ComboBox) && ((string)cmb.Tag == "Player" + i.ToString()))
                            combo_box = (cmb as ComboBox);

                    foreach (Control grb in Controls)
                        if (grb is GroupBox)
                            foreach (Control rdb in grb.Controls)
                                if ((rdb is RadioButton) && ((string)rdb.Tag == "Player" + i.ToString()))
                                    radio_button = (rdb as RadioButton);

                    if (radio_button.Checked)
                    {
                        logic.Bets.Player[i] = Convert.ToDouble(combo_box.Text);
                        logic.Opponents_Stage++;
                        game.Opponents_Stage++;
                    }

                }

                    logic.Calc_Probabilities();
                    game.Set_From_Logic(logic);

                game.Warning(logic); //even if Game.Status != Game.GAME_STATUS.OK
                Beep(game);
            }


            Define_Action();

            label_Stage_Action.Text = StringEnum.GetStringValue(game.Stage_Action);
            label_Warning.Text = StringEnum.GetStringValue(game.Warning_Status);
            label_Fint.Text = StringEnum.GetStringValue(game.Fint);
            userControl11.Display(game.My_Odds, game.Opponents_Odds);
        }


        private void Beep(Game game)
        {
            if (game.Status == Game.GAME_STATUS.ERROR)
            {
                SystemSounds.Beep.Play();
            }
            else if (game.Warning_Status == Game.WARNING_STATUS.GOOD_ODDS)
            {
                SystemSounds.Beep.Play();
            }
            else if (game.Warning_Status == Game.WARNING_STATUS.OPPONENTS_IN_GAME)
            {
                SystemSounds.Beep.Play();
            }
        }
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
        private void Save_Start_Hands(Table_Start_Hands.Table_Start_Hands table_start_hands)
        {
            Settings.Game.Start_Hands[table_start_hands.Start_Hands.Position_Index] = new Settings.Class_Start_Hands(table_start_hands.Start_Hands);
            Save_Settings_In_File();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //delete already existing components
            foreach (Control tbl in Controls)
                if ((tbl is Table_Start_Hands.Table_Start_Hands) && ((string)tbl.Tag == "Table_Start_Hands"))
                    tbl.Parent.Controls.Remove(tbl);


            Table_Start_Hands.Table_Start_Hands table_start_hands = new Table_Start_Hands.Table_Start_Hands();
            table_start_hands.Start_Hands = new Settings.Class_Start_Hands(Settings.Game.Start_Hands[0]);
            table_start_hands.Start();
            table_start_hands.Tag = "Table_Start_Hands";
            table_start_hands.Location = new Point(10, 10);
            table_start_hands.onSave += new Table_Start_Hands.Table_Start_Hands.SaveDelegate(Save_Start_Hands);

            //LockWindowUpdate(table_start_hands.Handle); //lock paint
            this.Controls.Add(table_start_hands);
            table_start_hands.BringToFront();
            //LockWindowUpdate(IntPtr.Zero); //allow paint
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save_Settings_In_File();
        }

        private void comboBox3_DragLeave(object sender, EventArgs e)
        {

        }

        private void Bet_Change(object sender, EventArgs e)
        {
            int sum = 0;
            ComboBox combo_box = new ComboBox();
            RadioButton radio_button = new RadioButton();

            for (int i = 0; i < 6; i++)
            {
                foreach (Control cmb in Controls)
                    if ((cmb is ComboBox) && ((string)cmb.Tag == "Player" + i.ToString()))
                        combo_box = (cmb as ComboBox);

                foreach (Control grb in Controls)
                    if(grb is GroupBox)
                    foreach (Control rdb in grb.Controls)
                        if ((rdb is RadioButton) && ((string)rdb.Tag == "Player" + i.ToString()))
                        radio_button = (rdb as RadioButton);

                if (radio_button.Checked)
                    sum += Convert.ToInt32(combo_box.Text);
            }

            textBox_Pot.Text = sum.ToString();
        }
    }
}

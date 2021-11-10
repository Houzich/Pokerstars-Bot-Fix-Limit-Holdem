using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace SettingsClass
{
    [Serializable()]
    public class Settings : System.ComponentModel.INotifyPropertyChanged
    {
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Class_Start_Hands
        {
            public Class_Start_Hand[] Hand { get; set; } = new Class_Start_Hand[169];
            public int Interval_Scroll { get; set; } = 0;
            public int Position_Index { get; set; } = 0;
            public Class_Start_Hands(Class_Start_Hands other)
            {
                for (int i = 0; i < 169; i++) this.Hand[i] = new Class_Start_Hand(other.Hand[i]);
                this.Interval_Scroll = other.Interval_Scroll;
                this.Position_Index = other.Position_Index;
            }
            public Class_Start_Hands(int index)
            {
                Position_Index = index;

                for (int i = 0; i < 169; i++) Hand[i] = new Class_Start_Hand(i, "");
                int ind = 0;
                Hand[ind++].Name = "32";
                Hand[ind++].Name = "42";
                Hand[ind++].Name = "52";
                Hand[ind++].Name = "62";
                Hand[ind++].Name = "43";
                Hand[ind++].Name = "72";
                Hand[ind++].Name = "32s";
                Hand[ind++].Name = "53";
                Hand[ind++].Name = "63";
                Hand[ind++].Name = "42s";
                Hand[ind++].Name = "73";
                Hand[ind++].Name = "82";
                Hand[ind++].Name = "83";
                Hand[ind++].Name = "62s";
                Hand[ind++].Name = "52s";
                Hand[ind++].Name = "54";
                Hand[ind++].Name = "64";
                Hand[ind++].Name = "43s";
                Hand[ind++].Name = "72s";
                Hand[ind++].Name = "74";
                Hand[ind++].Name = "92";
                Hand[ind++].Name = "53s";
                Hand[ind++].Name = "63s";
                Hand[ind++].Name = "84";
                Hand[ind++].Name = "93";
                Hand[ind++].Name = "73s";
                Hand[ind++].Name = "65";
                Hand[ind++].Name = "82s";
                Hand[ind++].Name = "94";
                Hand[ind++].Name = "83s";
                Hand[ind++].Name = "75";
                Hand[ind++].Name = "54s";
                Hand[ind++].Name = "64s";
                Hand[ind++].Name = "T2";
                Hand[ind++].Name = "85";
                Hand[ind++].Name = "74s";
                Hand[ind++].Name = "92s";
                Hand[ind++].Name = "T3";
                Hand[ind++].Name = "84s";
                Hand[ind++].Name = "76";
                Hand[ind++].Name = "95";
                Hand[ind++].Name = "93s";
                Hand[ind++].Name = "65s";
                Hand[ind++].Name = "T4";
                Hand[ind++].Name = "86";
                Hand[ind++].Name = "94s";
                Hand[ind++].Name = "75s";
                Hand[ind++].Name = "J2";
                Hand[ind++].Name = "T5";
                Hand[ind++].Name = "T2s";
                Hand[ind++].Name = "85s";
                Hand[ind++].Name = "96";
                Hand[ind++].Name = "J3";
                Hand[ind++].Name = "T3s";
                Hand[ind++].Name = "87";
                Hand[ind++].Name = "76s";
                Hand[ind++].Name = "95s";
                Hand[ind++].Name = "J4";
                Hand[ind++].Name = "T6";
                Hand[ind++].Name = "T4s";
                Hand[ind++].Name = "86s";
                Hand[ind++].Name = "97";
                Hand[ind++].Name = "Q2";
                Hand[ind++].Name = "J5";
                Hand[ind++].Name = "J2s";
                Hand[ind++].Name = "T5s";
                Hand[ind++].Name = "96s";
                Hand[ind++].Name = "Q3";
                Hand[ind++].Name = "J6";
                Hand[ind++].Name = "J3s";
                Hand[ind++].Name = "T7";
                Hand[ind++].Name = "87s";
                Hand[ind++].Name = "98";
                Hand[ind++].Name = "Q4";
                Hand[ind++].Name = "J4s";
                Hand[ind++].Name = "T6s";
                Hand[ind++].Name = "97s";
                Hand[ind++].Name = "Q2s";
                Hand[ind++].Name = "J7";
                Hand[ind++].Name = "J5s";
                Hand[ind++].Name = "T8";
                Hand[ind++].Name = "K2";
                Hand[ind++].Name = "Q5";
                Hand[ind++].Name = "22";
                Hand[ind++].Name = "Q3s";
                Hand[ind++].Name = "J6s";
                Hand[ind++].Name = "T7s";
                Hand[ind++].Name = "Q6";
                Hand[ind++].Name = "98s";
                Hand[ind++].Name = "K3";
                Hand[ind++].Name = "Q4s";
                Hand[ind++].Name = "J8";
                Hand[ind++].Name = "T9";
                Hand[ind++].Name = "Q7";
                Hand[ind++].Name = "K4";
                Hand[ind++].Name = "J7s";
                Hand[ind++].Name = "T8s";
                Hand[ind++].Name = "K2s";
                Hand[ind++].Name = "Q5s";
                Hand[ind++].Name = "K5";
                Hand[ind++].Name = "J9";
                Hand[ind++].Name = "33";
                Hand[ind++].Name = "K3s";
                Hand[ind++].Name = "Q8";
                Hand[ind++].Name = "Q6s";
                Hand[ind++].Name = "J8s";
                Hand[ind++].Name = "K6";
                Hand[ind++].Name = "T9s";
                Hand[ind++].Name = "Q7s";
                Hand[ind++].Name = "A2";
                Hand[ind++].Name = "K4s";
                Hand[ind++].Name = "K7";
                Hand[ind++].Name = "JT";
                Hand[ind++].Name = "Q9";
                Hand[ind++].Name = "A3";
                Hand[ind++].Name = "K5s";
                Hand[ind++].Name = "J9s";
                Hand[ind++].Name = "Q8s";
                Hand[ind++].Name = "K8";
                Hand[ind++].Name = "A4";
                Hand[ind++].Name = "K6s";
                Hand[ind++].Name = "A2s";
                Hand[ind++].Name = "44";
                Hand[ind++].Name = "QT";
                Hand[ind++].Name = "JTs";
                Hand[ind++].Name = "A5";
                Hand[ind++].Name = "A6";
                Hand[ind++].Name = "K7s";
                Hand[ind++].Name = "Q9s";
                Hand[ind++].Name = "A3s";
                Hand[ind++].Name = "K9";
                Hand[ind++].Name = "QJ";
                Hand[ind++].Name = "K8s";
                Hand[ind++].Name = "A4s";
                Hand[ind++].Name = "A7";
                Hand[ind++].Name = "QTs";
                Hand[ind++].Name = "A5s";
                Hand[ind++].Name = "KT";
                Hand[ind++].Name = "A6s";
                Hand[ind++].Name = "K9s";
                Hand[ind++].Name = "A8";
                Hand[ind++].Name = "QJs";
                Hand[ind++].Name = "55";
                Hand[ind++].Name = "KJ";
                Hand[ind++].Name = "A9";
                Hand[ind++].Name = "A7s";
                Hand[ind++].Name = "KQ";
                Hand[ind++].Name = "KTs";
                Hand[ind++].Name = "A8s";
                Hand[ind++].Name = "KJs";
                Hand[ind++].Name = "AT";
                Hand[ind++].Name = "A9s";
                Hand[ind++].Name = "66";
                Hand[ind++].Name = "KQs";
                Hand[ind++].Name = "AJ";
                Hand[ind++].Name = "AQ";
                Hand[ind++].Name = "ATs";
                Hand[ind++].Name = "AK";
                Hand[ind++].Name = "AJs";
                Hand[ind++].Name = "AQs";
                Hand[ind++].Name = "77";
                Hand[ind++].Name = "AKs";
                Hand[ind++].Name = "88";
                Hand[ind++].Name = "99";
                Hand[ind++].Name = "TT";
                Hand[ind++].Name = "JJ";
                Hand[ind++].Name = "QQ";
                Hand[ind++].Name = "KK";
                Hand[ind++].Name = "AA";
            }
            [Serializable()]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            [EditorBrowsable(EditorBrowsableState.Always)]
            public class Class_Start_Hand
            {
                bool enabled_scroll = false;
                bool enabled_user  = false;
                bool disabled_user = false;

                public bool Enable { get; set; } = false;
                public int Index { get; set; } = 0;
                public string Name { get; set; } = "";

                public bool Enabled_Scroll {
                    get { return enabled_scroll; }
                    set { enabled_scroll = value; Check_Enable(); }
                }

                public bool Enabled_User {
                    get { return enabled_user; }
                    set { enabled_user = value; Check_Enable(); }
                }

                public bool Disabled_User
                {
                    get { return disabled_user; }
                    set { disabled_user = value; Check_Enable(); }
                }

                void Check_Enable()
                {
                    if (disabled_user) { Enable = false; }
                    else if (enabled_user) { Enable = true; }
                    else if (enabled_scroll) { Enable = true; }
                    else { Enable = false; }
                }
                
                public Class_Start_Hand(Class_Start_Hand other)
                {
                    this.Index = other.Index;
                    this.Name = other.Name;
                    this.Enabled_Scroll = other.Enabled_Scroll;
                    this.Enabled_User = other.Enabled_User;
                    this.Disabled_User = other.Disabled_User;
                    this.Enable = other.Enable;
                }
                public Class_Start_Hand(int i, string str)
                {
                    Index = i;
                    Name = str;
                }
                public void Clear()
                {
                    Enabled_Scroll = false;
                    Enabled_User = false;
                }
            }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class My_Course
        {
            public Point Point { get; set; } = new Point(100,100);
            public Color Color_Action { get; set; } = new Color();
            public override string ToString() { return "Point"; }
        }

        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class _Pixel
        {
            public Point Point { get; set; }
            public Color Color_Back { get; set; }
            public Color Color_No { get; set; }
            
            public _Pixel()
            {
                Point = new Point();
                Color_Back = new Color();
                Color_No = new Color();
            }
            public override string ToString() { return Point.ToString(); }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Pixel
        {
            public _Pixel Suit { get; set; }
            public _Pixel Back { get; set; }

            public Pixel()
            {
                Suit = new _Pixel();
                Back = new _Pixel();
            }
            public override string ToString() { return "Pixel"; }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Card
        {
            public Pixel Pixel { get; set; } = new Pixel();
            public Rectangle Rectangle_Rank { get; set; } = new Rectangle();
            public Card()
            {

            }
            public override string ToString() { return "Card"; }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Player
        {
            public Card[] Card { get; set; } = new Card[2];
            public Color Color_Button { get; set; } = new Color();
            public Point Point_Button { get; set; } = new Point();
            public Money Bet { get; set; } = new Money();
            public Money Stack { get; set; } = new Money();
            public Player()
            {
                for (int i = 0; i < 2; i++)
                {
                    Card[i] = new Card();
                }

            }
            public override string ToString() { return "Player"; }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Board
        {
            public Card[] Card { get; set; } = new Card[5];
            public Board()
            {
                for (int i = 0; i < 5; i++)
                {
                    Card[i] = new Card();
                }

            }
            public override string ToString() { return "Board"; }
        }


        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class Money
        {
            public int Black_White_Coeff { get; set; } = new int();
            public Bitmap Bitmap_Dollar { get; set; } = new Bitmap(1, 1);
            public int Text_Rectangle_Width { get; set; } = new int();
            public int Text_Rectangle_Height { get; set; } = new int();
            public int Img_Dollar_Width { get; set; } = new int();
            public int Img_Dollar_Height { get; set; } = new int();
            public Rectangle Search_Rectangle { get; set; } = new Rectangle();
            public string FileName_Dollar_Png { get; set; }

            public Money()
            {
            }
            public override string ToString() { return "Money"; }
        }

        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class _Game
        {
            public double Call_Сriterion { get; set; } = new double();
            public double Raise_Сriterion { get; set; } = new double();
            public Class_Start_Hands[] Start_Hands { get; set; } = new Class_Start_Hands[6];
            public _Game()
            {
                for (int i = 0; i < 6; i++) Start_Hands[i] = new Class_Start_Hands(i);
            }
            public override string ToString() { return "Game"; }
        }
        [Serializable()]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public class _Graphic_Table
        {
            public Player[] Player { get; set; } = new Player[6];
            public Player My;
            public Board Board { get; set; } = new Board();
            public Money Pot { get; set; } = new Money();
            public Money Call { get; set; } = new Money();
            public Money Raise { get; set; } = new Money();
            public Money Header { get; set; } = new Money();
            public My_Course My_Course { get; set; } = new My_Course();
            public My_Course My_Course_Call { get; set; } = new My_Course();
            public My_Course Accelerate_Fold { get; set; } = new My_Course();
            public My_Course Accelerate_Check { get; set; } = new My_Course();
            public int Сard_On_Board_Distance { get; set; } = new int();
            public int Card_Player_Distance { get; set; } = new int();
            public int Card_Suit_Rank_Distance_X { get; set; } = new int();
            public int Card_Suit_Rank_Distance_Y { get; set; } = new int();
            public int Сard_Rank_Width { get; set; } = new int();
            public int Сard_Rank_Height { get; set; } = new int();
            public int Card_Back_Color_Distance_X { get; set; } = new int();
            public int Card_Back_Color_Distance_Y { get; set; } = new int();
            public Color Suit_Hearts_Color { get; set; } = new Color();
            public Color Suit_Diamonds_Color { get; set; } = new Color();
            public Color Suit_Clubs_Color { get; set; } = new Color();
            public Color Suit_Spades_Color { get; set; } = new Color();
            public Color Suit_None_Color { get; set; } = new Color();
            public int Rank_Black_White_Coeff { get; set; } = new int();

            public _Graphic_Table()
            {
                for (int i = 0; i < 6; i++)
                {
                    Player[i] = new Player();
                }
                My = Player[0];
            }
            public override string ToString() { return "Graphic Table"; }

            //public _Graphic_Table()
            //{ }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public _Graphic_Table Graphic_Table { get; set; } = new _Graphic_Table();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public _Game Game { get; set; } = new _Game();

        [field: NonSerialized()]
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public const string FileName = @"..\..\SavedSettings.bin";
        public const string Stack_Dollar_Png_FileName = @"\Bitmap\Stack_Dollar.png";
        public const string Bet_Dollar_Png_FileName = @"\Bitmap\Bet_Dollar.png";
        public const string Header_Dollar_Png_FileName = @"\Bitmap\Header_Dollar.png";
        public const string Call_Raise_Dollar_Png_FileName = @"\Bitmap\Call_Raise_Dollar.png";
        public const string Pot_Dollar_Png_FileName = @"\Bitmap\Pot_Dollar.png";

        public Settings(string path)
        {
            Game.Call_Сriterion = 0;
            Game.Raise_Сriterion = 5;

            Game.Start_Hands[0].Interval_Scroll = 35;
            Game.Start_Hands[1].Interval_Scroll = 35;
            Game.Start_Hands[2].Interval_Scroll = 35;
            Game.Start_Hands[3].Interval_Scroll = 35;
            Game.Start_Hands[4].Interval_Scroll = 35;
            Game.Start_Hands[5].Interval_Scroll = 35;
            for (int i = 0; i < 6; i++)
                for (int j = 169 - 1; j >= (169 - 1) - Game.Start_Hands[i].Interval_Scroll; j--)
                {
                    Game.Start_Hands[i].Hand[j].Enable = true;
                    Game.Start_Hands[i].Hand[j].Enabled_Scroll = true;
                }

            Graphic_Table.Player[1].Card[0].Pixel.Back.Color_Back = Color.FromArgb(140, 140, 140);
            Graphic_Table.Player[2].Card[0].Pixel.Back.Color_Back = Color.FromArgb(138, 138, 138);
            Graphic_Table.Player[3].Card[0].Pixel.Back.Color_Back = Color.FromArgb(137, 137, 137);
            Graphic_Table.Player[4].Card[0].Pixel.Back.Color_Back = Color.FromArgb(138, 138, 138);
            Graphic_Table.Player[5].Card[0].Pixel.Back.Color_Back = Color.FromArgb(140, 140, 140);

            Graphic_Table.My.Card[0].Pixel.Back.Color_No = Color.FromArgb(81, 81, 81);
            Graphic_Table.Player[1].Card[0].Pixel.Back.Color_No = Color.FromArgb(31, 32, 37);
            Graphic_Table.Player[2].Card[0].Pixel.Back.Color_No = Color.FromArgb(39, 39, 41);
            Graphic_Table.Player[3].Card[0].Pixel.Back.Color_No = Color.FromArgb(44, 44, 46);
            Graphic_Table.Player[4].Card[0].Pixel.Back.Color_No = Color.FromArgb(44, 45, 49);
            Graphic_Table.Player[5].Card[0].Pixel.Back.Color_No = Color.FromArgb(2, 119, 39);

            Graphic_Table.Board.Card[0].Pixel.Back.Color_No = Color.FromArgb(2, 122, 40);
            Graphic_Table.Board.Card[1].Pixel.Back.Color_No = Color.FromArgb(27, 139, 62);
            Graphic_Table.Board.Card[2].Pixel.Back.Color_No = Color.FromArgb(38, 79, 29);
            Graphic_Table.Board.Card[3].Pixel.Back.Color_No = Color.FromArgb(28, 137, 62);
            Graphic_Table.Board.Card[4].Pixel.Back.Color_No = Color.FromArgb(27, 140, 62);

            Graphic_Table.Suit_Hearts_Color = Color.FromArgb(Convert.ToInt32("ff9e3737", 16));
            Graphic_Table.Suit_Diamonds_Color = Color.FromArgb(Convert.ToInt32("ff2b6288", 16));
            Graphic_Table.Suit_Clubs_Color = Color.FromArgb(Convert.ToInt32("ff3b6b2a", 16));
            Graphic_Table.Suit_Spades_Color = Color.FromArgb(Convert.ToInt32("ff565656", 16));
            Graphic_Table.Suit_None_Color = Color.FromArgb(Convert.ToInt32("ff037b29", 16));

            Graphic_Table.Rank_Black_White_Coeff = 205;

            Graphic_Table.Сard_Rank_Width = 18;
            Graphic_Table.Сard_Rank_Height = 19;
            Graphic_Table.Card_Suit_Rank_Distance_X = 0;
            Graphic_Table.Card_Suit_Rank_Distance_Y = -33;
            Graphic_Table.Сard_On_Board_Distance = 54;
            Graphic_Table.Card_Back_Color_Distance_X = 3;
            Graphic_Table.Card_Back_Color_Distance_Y = -5;
            Graphic_Table.Card_Player_Distance = 51;

            Graphic_Table.My.Card[0].Pixel.Suit.Point = new Point(913, 546);
            Graphic_Table.Player[1].Card[0].Pixel.Suit.Point = new Point(618, 450);
            Graphic_Table.Player[2].Card[0].Pixel.Suit.Point = new Point(618, 280);
            Graphic_Table.Player[3].Card[0].Pixel.Suit.Point = new Point(915, 216);
            Graphic_Table.Player[4].Card[0].Pixel.Suit.Point = new Point(1213, 280);
            Graphic_Table.Player[5].Card[0].Pixel.Suit.Point = new Point(1213, 450);
            Graphic_Table.Board.Card[0].Pixel.Suit.Point = new Point(831, 387);
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Stage Action
            Graphic_Table.My_Course.Point = new Point(1236, 676);
            Graphic_Table.My_Course.Color_Action = Color.FromArgb(114, 19, 10);
            //Stage Action Call_Fold
            Graphic_Table.My_Course_Call.Point = new Point(1093, 674);
            Graphic_Table.My_Course_Call.Color_Action = Color.FromArgb(120, 21, 12);            
            //Accelerate Fold
            Graphic_Table.Accelerate_Fold.Point = new Point(982, 690);
            Graphic_Table.Accelerate_Fold.Color_Action = Color.FromArgb(178, 178, 178);
            //Accelerate Check
            Graphic_Table.Accelerate_Check.Point = new Point(1110, 645);
            Graphic_Table.Accelerate_Check.Color_Action = Color.FromArgb(216, 216, 216);
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Pot
            Graphic_Table.Pot.Text_Rectangle_Width = 38;
            Graphic_Table.Pot.Text_Rectangle_Height = 19;
            Graphic_Table.Pot.Img_Dollar_Width = 8;
            Graphic_Table.Pot.Img_Dollar_Height = 10;
            Graphic_Table.Pot.Black_White_Coeff = 150;
            Graphic_Table.Pot.Search_Rectangle = new Rectangle(933, 322, 84, 19);
            Graphic_Table.Pot.FileName_Dollar_Png = Pot_Dollar_Png_FileName;
            try
            {
                Graphic_Table.Pot.Bitmap_Dollar = new Bitmap(Image.FromFile(path+Pot_Dollar_Png_FileName));
            }
            catch
            {
                Graphic_Table.Pot.Bitmap_Dollar = new Bitmap(Graphic_Table.Pot.Img_Dollar_Width, Graphic_Table.Pot.Img_Dollar_Height);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Call
            Graphic_Table.Call.Text_Rectangle_Width = 50;
            Graphic_Table.Call.Text_Rectangle_Height = 19;
            Graphic_Table.Call.Img_Dollar_Width = 8;
            Graphic_Table.Call.Img_Dollar_Height = 10;
            Graphic_Table.Call.Black_White_Coeff = 230;
            Graphic_Table.Call.Search_Rectangle = new Rectangle(1108, 690, 80, 19);
            Graphic_Table.Call.FileName_Dollar_Png = Call_Raise_Dollar_Png_FileName;
            try
            {
                Graphic_Table.Call.Bitmap_Dollar = new Bitmap(Image.FromFile(path+Call_Raise_Dollar_Png_FileName));
            }
            catch
            {
                Graphic_Table.Call.Bitmap_Dollar = new Bitmap(Graphic_Table.Call.Img_Dollar_Width, Graphic_Table.Call.Img_Dollar_Height);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Raise
            Graphic_Table.Raise.Text_Rectangle_Width = 50;
            Graphic_Table.Raise.Text_Rectangle_Height = 19;
            Graphic_Table.Raise.Img_Dollar_Width = 8;
            Graphic_Table.Raise.Img_Dollar_Height = 10;
            Graphic_Table.Raise.Black_White_Coeff = 230;
            Graphic_Table.Raise.Search_Rectangle = new Rectangle(1245, 690, 80, 19);
            Graphic_Table.Raise.FileName_Dollar_Png = Call_Raise_Dollar_Png_FileName;
            try
            {
                Graphic_Table.Raise.Bitmap_Dollar = new Bitmap(Image.FromFile(path + Call_Raise_Dollar_Png_FileName));
            }
            catch
            {
                Graphic_Table.Raise.Bitmap_Dollar = new Bitmap(Graphic_Table.Raise.Img_Dollar_Width, Graphic_Table.Raise.Img_Dollar_Height);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Header
            Graphic_Table.Header.Text_Rectangle_Width = 22;
            Graphic_Table.Header.Text_Rectangle_Height = 13;
            Graphic_Table.Header.Img_Dollar_Width = 8;
            Graphic_Table.Header.Img_Dollar_Height = 10;
            Graphic_Table.Header.Black_White_Coeff = 150;
            Graphic_Table.Header.Search_Rectangle = new Rectangle(602, 152, 150, 13);
            Graphic_Table.Header.FileName_Dollar_Png = Header_Dollar_Png_FileName;
            try
            {
                Graphic_Table.Header.Bitmap_Dollar = new Bitmap(Image.FromFile(path + Header_Dollar_Png_FileName));
            }
            catch
            {
                Graphic_Table.Header.Bitmap_Dollar = new Bitmap(Graphic_Table.Header.Img_Dollar_Width, Graphic_Table.Header.Img_Dollar_Height);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Players
            Graphic_Table.Player[0].Color_Button = Color.FromArgb(245, 0, 10);
            Graphic_Table.Player[1].Color_Button = Color.FromArgb(245, 0, 10);
            Graphic_Table.Player[2].Color_Button = Color.FromArgb(209, 215, 212);
            Graphic_Table.Player[3].Color_Button = Color.FromArgb(213, 218, 215);
            Graphic_Table.Player[4].Color_Button = Color.FromArgb(212, 218, 215);
            Graphic_Table.Player[5].Color_Button = Color.FromArgb(228, 233, 230);

            Graphic_Table.Player[0].Point_Button = new Point(1035, 511);
            Graphic_Table.Player[1].Point_Button = new Point(774, 475);
            Graphic_Table.Player[2].Point_Button = new Point(735, 345);
            Graphic_Table.Player[3].Point_Button = new Point(896, 294);
            Graphic_Table.Player[4].Point_Button = new Point(1193, 354);
            Graphic_Table.Player[5].Point_Button = new Point(1147, 480);
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Cards On Board 
            for (int i = 0; i < 5; i++)
            {
                Board board = Graphic_Table.Board;
                Point p = new Point(board.Card[0].Pixel.Suit.Point.X + Graphic_Table.Сard_On_Board_Distance * i, board.Card[0].Pixel.Suit.Point.Y);
                board.Card[i].Pixel.Suit.Point = p;
                board.Card[i].Pixel.Back.Point = new Point(p.X + Graphic_Table.Card_Back_Color_Distance_X, p.Y + Graphic_Table.Card_Back_Color_Distance_Y);
                board.Card[i].Rectangle_Rank = new Rectangle(p.X + Graphic_Table.Card_Suit_Rank_Distance_X, p.Y + Graphic_Table.Card_Suit_Rank_Distance_Y, Graphic_Table.Сard_Rank_Width, Graphic_Table.Сard_Rank_Height);
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //Cards Players 
            Player[] player = Graphic_Table.Player;
            for (int i = 0; i < 6; i++)
                for (int ii = 0; ii < 2; ii++)
                {
                    Point p = new Point(player[i].Card[0].Pixel.Suit.Point.X + Graphic_Table.Card_Player_Distance * ii, player[i].Card[0].Pixel.Suit.Point.Y);
                    player[i].Card[ii].Pixel.Suit.Point = p;
                    player[i].Card[ii].Pixel.Back.Point = new Point(p.X + Graphic_Table.Card_Back_Color_Distance_X, p.Y + Graphic_Table.Card_Back_Color_Distance_Y);
                    player[i].Card[ii].Rectangle_Rank = new Rectangle(p.X + Graphic_Table.Card_Suit_Rank_Distance_X, p.Y + Graphic_Table.Card_Suit_Rank_Distance_Y, Graphic_Table.Сard_Rank_Width, Graphic_Table.Сard_Rank_Height);
                }
            //Bets Players
            for (int i = 0; i < 6; i++)
            {
                player[i].Bet.Img_Dollar_Width = 8;
                player[i].Bet.Img_Dollar_Height = 10;
                player[i].Bet.Text_Rectangle_Width = 31;
                player[i].Bet.Text_Rectangle_Height = 15;
                player[i].Bet.Black_White_Coeff = 120;
                player[i].Bet.FileName_Dollar_Png = Bet_Dollar_Png_FileName;
                try
                {
                    player[i].Bet.Bitmap_Dollar = new Bitmap(Image.FromFile(path + Bet_Dollar_Png_FileName));
                }
                catch
                {
                    player[i].Bet.Bitmap_Dollar = new Bitmap(1, 1);
                }
            }
            player[0].Bet.Search_Rectangle = new Rectangle(877, 472, 120, 15);
            player[1].Bet.Search_Rectangle = new Rectangle(757, 443, 120, 15);
            player[2].Bet.Search_Rectangle = new Rectangle(779, 319, 120, 15);
            player[3].Bet.Search_Rectangle = new Rectangle(943, 286, 120, 15);
            player[4].Bet.Search_Rectangle = new Rectangle(1028, 319, 120, 15);
            player[5].Bet.Search_Rectangle = new Rectangle(1044, 444, 120, 15);
            
            //Stack Players
            for (int i = 0; i < 6; i++)
            {
                player[i].Stack.Img_Dollar_Width = 8;
                player[i].Stack.Img_Dollar_Height = 10;
                player[i].Stack.Black_White_Coeff = 100;
                player[i].Stack.FileName_Dollar_Png = Stack_Dollar_Png_FileName;
                try
                {
                    player[i].Stack.Bitmap_Dollar = new Bitmap(Image.FromFile(path + Stack_Dollar_Png_FileName));
                }
                catch
                {
                    player[i].Stack.Bitmap_Dollar = new Bitmap(1, 1);
                }
            }
            player[0].Stack.Search_Rectangle = new Rectangle(948, 579, 70, 16);
            player[1].Stack.Search_Rectangle = new Rectangle(610, 484, 70, 16);
            player[2].Stack.Search_Rectangle = new Rectangle(610, 312, 70, 16);
            player[3].Stack.Search_Rectangle = new Rectangle(908, 246, 70, 16);
            player[4].Stack.Search_Rectangle = new Rectangle(1246, 312, 70, 16);
            player[5].Stack.Search_Rectangle = new Rectangle(1246, 484, 70, 16);
        }
    }
}
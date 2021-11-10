using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using System.Drawing.Drawing2D;
using Cards;
using StringEnumClass;
using Emgu.CV;
//using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Threading;
using System.Diagnostics;

//using System.Reflection;
//using System.Linq.Expressions;





namespace My_Math
{
    class Recognize
    {
        private static SettingsClass.Settings Settings = FormMain.Settings;
        //private static Recognize_Cards rec_cards = new Recognize_Cards();
        //private static Recognize_Button rec_button = new Recognize_Button();
        //private static Recognize_Finance rec_finance = new Recognize_Finance();
        static object locker = new object();

        public enum RECOGNIZE_CARD_STATUS
        {
            [StringValue("ER")] ERROR,
            [StringValue("OK")] OK,
            [StringValue("NO")] NO,
            [StringValue("CL")] CLOSE,
            [StringValue("OP")] OPEN,
            [StringValue("IN")] INIT
        };
        public enum RECOGNIZE_BET_STATUS
        {
            [StringValue("ER")] ERROR,
            [StringValue("OK")] OK,
            [StringValue("NO")] NO,
            [StringValue("IN")] INIT
        };
        public enum RECOGNIZE_BUTTON_STATUS
        {
            [StringValue("MY")] MY = 0,
            [StringValue("01")] PLAYER1 = 1,
            [StringValue("02")] PLAYER2 = 2,
            [StringValue("03")] PLAYER3 = 3,
            [StringValue("04")] PLAYER4 = 4,
            [StringValue("05")] PLAYER5 = 5,
            [StringValue("ER")] ERROR = 6,
            [StringValue("OK")] OK = 7,
            [StringValue("NO")] NO = 8,
            [StringValue("IN")] INIT = 9
        };

        public enum POSITION
        {
            [StringValue("BUTTON")] BUTTON = 0,                     //BUTTON
            [StringValue("SMALL BLIND")] SMALL_BLIND = 1,           //SMALL BLIND
            [StringValue("BIG BLIND")] BIG_BLIND = 2,               //BIG BLIND
            [StringValue("UNDER THE GUN")] UNDER_THE_GUN = 3,       //UNDER THE GUN
            [StringValue("MIDDLE POSITION")] MIDDLE_POSITION = 4,   //MIDDLE POSITION
            [StringValue("CUT OFF")] CUT_OFF = 5,                   //CUT OFF
            [StringValue("ERROR")] ERROR = 6,                       //ERROR
            [StringValue("INIT")] INIT = 7,                         //INIT
        };
        public enum MY_COURSE
        {
            [StringValue("WAIT")] WAIT = 5,
            [StringValue("OK")] OK = 6,
            [StringValue("INIT")] INIT = 7
        };

        public class Finance
        {
            public Players_Finance Stacks { get; set; }
            public Players_Finance Bets { get; set; }
            public Money Pot { get; set; } = new Money();
            public Money Call { get; set; } = new Money();
            public Money Raise { get; set; } = new Money();
            public Money Header { get; set; } = new Money();
            public Finance(int player)
            {
                Stacks = new Players_Finance(player);
                Bets = new Players_Finance(player);
            }

            public override bool Equals(Object obj)
            {
                if (obj == null || !(obj is Finance))
                    return false;
                Finance o = (Finance)obj;
                if (!Object.Equals(this.Stacks, o.Stacks)) return false;
                if (!Object.Equals(this.Bets, o.Bets)) return false;
                if (!Object.Equals(this.Pot, o.Pot)) return false;
                if (!Object.Equals(this.Call, o.Call)) return false;
                if (!Object.Equals(this.Raise, o.Raise)) return false;
                if (!Object.Equals(this.Header, o.Header)) return false;
                return true;
            }

            public override int GetHashCode()
            {
                return this.GetHashCode();
            }


            public class Players_Finance
            {
                public Money[] Player { get; set; }
                public Money My { get; set; }
                public Players_Finance(int player)
                {
                    Player = new Money[player];
                    for (int i = 0; i < player; i++) Player[i] = new Money();
                    My = Player[0];
                }

                public override bool Equals(Object obj)
                {
                    if (obj == null || !(obj is Players_Finance))
                        return false;
                    Players_Finance o = (Players_Finance)obj;
                    for (int i = 0; i < o.Player.Length; i++)
                    {
                        if (this.Player[i].Float != o.Player[i].Float) return false;
                        if (this.Player[i].Status != o.Player[i].Status) return false;
                    }

                    return true;
                }

                public override int GetHashCode()
                {
                    return this.GetHashCode();
                }

            }
            public class Money
            {
                public Rectangle Rectangle { get; set; } = new Rectangle();
                public double Float { get; set; } = default(float);
                public RECOGNIZE_BET_STATUS Status { get; set; } = RECOGNIZE_BET_STATUS.INIT;

                public override bool Equals(Object obj)
                {
                    if (obj == null || !(obj is Money))
                        return false;
                    else
                        return (this.Float == ((Money)obj).Float) & (this.Status == ((Money)obj).Status);
                }

                public override int GetHashCode()
                {
                    return this.GetHashCode();
                }

            };

        }

        public class PlayCards
        {
            public Card_Struct[][] Player { get; set; }
            public Card_Struct[] Board { get; set; }
            public Card_Struct[] My { get; set; }

            public PlayCards(int player)
            {
                Player = new Card_Struct[player][];
                for (int i = 0; i < player; i++)
                {
                    Player[i] = new Card_Struct[2];
                    Player[i][0] = new Card_Struct();
                    Player[i][1] = new Card_Struct();
                }
                My = Player[0];
                Board = new Card_Struct[5];
                for (int i = 0; i < 5; i++) Board[i] = new Card_Struct();
            }

            public class Card_Struct
            {
                public CardClass.Card card { get; set; } = new CardClass.Card();
                public RECOGNIZE_CARD_STATUS status { get; set; } = RECOGNIZE_CARD_STATUS.INIT;
                public Color clr_suit { get; set; } = new Color();
                public Color clr_back { get; set; }
            };

        }

        public class Button
        {
            public int Position_Button { get; set; } = -1;
            public RECOGNIZE_BUTTON_STATUS Status { get; set; } = RECOGNIZE_BUTTON_STATUS.INIT;
        }

        public class Position_Players
        {
            public POSITION[] Position { get; set; }
            public Position_Players(int player)
            {
                Position = new POSITION[player];
                for (int i = 0; i < player; i++)
                {
                    Position[i] = POSITION.INIT;
                }
            }
        }

        public class Class_Table
        {
            public PlayCards PlayCards { get; set; } = new PlayCards(6);
            public Finance Finance { get; set; } = new Finance(6);
            public Button Button { get; set; } = new Button();
            public Position_Players Position_Players { get; set; } = new Position_Players(6);
            public MY_COURSE My_Course { get; set; } = MY_COURSE.INIT;
            public MY_COURSE My_Course_Call { get; set; } = MY_COURSE.INIT;
            public MY_COURSE Accelerate_Fold { get; set; } = MY_COURSE.INIT;
            public MY_COURSE Accelerate_Check { get; set; } = MY_COURSE.INIT;
            public int Opponents_Num { get; set; } = 0;
        }

        public static Class_Table CloneObject(Class_Table obj)
        {
            if (obj == null) return null;

            Type t1 = obj.GetType();
            Class_Table ret = (Class_Table)Activator.CreateInstance(t1);

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
        /**/
        public static void Display_Text_Area_On_Screen(SettingsClass.Settings._Graphic_Table sett, Class_Table table, Graphics gr, Bitmap btm)
        {
            //Bets_______________________________________________________________
            for (int i = 0; i < 6; i++)
                Display_Text_Area(sett.Player[i].Bet, table.Finance.Bets.Player[i], gr, btm);
            //Stacks_______________________________________________________________
            for (int i = 0; i < 6; i++)
                Display_Text_Area(sett.Player[i].Stack, table.Finance.Stacks.Player[i], gr, btm);
            //Call _______________________________________________________________
            Display_Text_Area(sett.Call, table.Finance.Call, gr, btm);
            //Raise _______________________________________________________________
            Display_Text_Area(sett.Raise, table.Finance.Raise, gr, btm);
            ////Pot_______________________________________________________________
            Display_Text_Area(sett.Pot, table.Finance.Pot, gr, btm);
            //Header_______________________________________________________________
            Display_Text_Area(sett.Header, table.Finance.Header, gr, btm);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public static void Display_Text_Area(SettingsClass.Settings.Money sett, Finance.Money text, Graphics gr, Bitmap btm)
        {
            //int destination_height = 18;
            //Pot_______________________________________________________________
            if (text.Status != RECOGNIZE_BET_STATUS.NO && text.Status != RECOGNIZE_BET_STATUS.ERROR)
            {
                Bitmap image = OperationsWithImage.Region_Bitmap_To_BlackWhite_Monochrome(btm, text.Rectangle, sett.Black_White_Coeff);
                gr.DrawImage(image, new PointF(text.Rectangle.X, text.Rectangle.Bottom + 4));
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public static void Display_Primitives_On_Screen(SettingsClass.Settings._Graphic_Table sett, Class_Table table, Graphics gr)
        {
            Font fnt = new Font("Arial", 8, FontStyle.Bold);
            //Position_______________________________________________________________
            if (table.Button.Status == RECOGNIZE_BUTTON_STATUS.NO)
                Draw_String("NO BUTTON!!!", Color.Blue, sett.My.Point_Button, 0, fnt.Height);
            else
                for (int i = 0; i < 6; i++)
                    Draw_String(StringEnum.GetStringValue(table.Position_Players.Position[i]), Color.Blue, sett.Player[i].Point_Button, 0, fnt.Height);

            Display_Primitives_Cards(sett.Board.Card);
            Display_Primitives_Cards(sett.My.Card);
            Display_Primitives_Cards(sett.Player[1].Card);
            Display_Primitives_Cards(sett.Player[2].Card);
            Display_Primitives_Cards(sett.Player[3].Card);
            Display_Primitives_Cards(sett.Player[4].Card);
            Display_Primitives_Cards(sett.Player[5].Card);

            fnt = new Font("Arial", 12, FontStyle.Bold);
            //Players_______________________________________________________________
            for (int i = 0; i < 6; i++)
            {
                Finance.Money player_stack = table.Finance.Stacks.Player[i];
                Finance.Money player_bets = table.Finance.Bets.Player[i];
                Draw_Сircle(Color.Yellow, sett.Player[i].Point_Button);
                Draw_Rectangle(Color.Red, sett.Player[i].Bet.Search_Rectangle);
                Draw_Rectangle(Color.Red, sett.Player[i].Stack.Search_Rectangle);
                //Stacks
                Draw_String_Double(player_stack.Float, Color.Red, player_stack.Rectangle.Location, 0, fnt.Height);
                Draw_Rectangle(Color.Yellow, player_stack.Rectangle);
                //Bets
                Draw_String_Double(player_bets.Float, Color.Yellow, player_bets.Rectangle.Location, 0, -fnt.Height);
                Draw_Rectangle(Color.Yellow, player_bets.Rectangle);
            }
            //Pot_______________________________________________________________
            Draw_Rectangle(Color.Red, sett.Pot.Search_Rectangle);
            Draw_String_Double(table.Finance.Pot.Float, Color.Black, table.Finance.Pot.Rectangle.Location, 0, -fnt.Height);
            Draw_Rectangle(Color.Yellow, table.Finance.Pot.Rectangle);
            //Call_______________________________________________________________
            Draw_Rectangle(Color.Yellow, sett.Call.Search_Rectangle);
            Draw_String_Double(table.Finance.Call.Float, Color.Black, table.Finance.Call.Rectangle.Location, 0, -(fnt.Height + 40));
            Draw_Rectangle(Color.Green, table.Finance.Call.Rectangle);
            //Raise_______________________________________________________________
            Draw_Rectangle(Color.Yellow, sett.Raise.Search_Rectangle);
            Draw_String_Double(table.Finance.Raise.Float, Color.Black, table.Finance.Raise.Rectangle.Location, 0, -(fnt.Height + 40));
            Draw_Rectangle(Color.Green, table.Finance.Raise.Rectangle);
            //Header_______________________________________________________________
            Draw_Rectangle(Color.Yellow, sett.Header.Search_Rectangle);
            Draw_String_Double(table.Finance.Header.Float, Color.Black, table.Finance.Header.Rectangle.Location, 0, fnt.Height);
            Draw_Rectangle(Color.Green, table.Finance.Header.Rectangle);
            //My Course_______________________________________________________________
            Draw_Сircle(Color.Yellow, sett.My_Course.Point);
            if (table.My_Course == MY_COURSE.OK)
                Draw_String("COURSE", Color.Red, sett.My_Course.Point, 0, -fnt.Height);
            //My Course Call/Fold_______________________________________________________________
            Draw_Сircle(Color.Yellow, sett.My_Course_Call.Point);
            if (table.My_Course_Call != MY_COURSE.OK)
                Draw_String("FOLD/CALL", Color.Red, sett.My_Course_Call.Point, 0, -fnt.Height);
            //Accelerate Fold_______________________________________________________________
            Draw_Сircle(Color.Yellow, sett.Accelerate_Fold.Point);
            if (table.Accelerate_Fold == MY_COURSE.OK)
                Draw_String("ACCELERATE FOLD", Color.Red, sett.Accelerate_Fold.Point, 0, -fnt.Height);
            //Accelerate Check_______________________________________________________________
            Draw_Сircle(Color.Yellow, sett.Accelerate_Check.Point);
            if (table.Accelerate_Check == MY_COURSE.OK)
                Draw_String("ACCELERATE CHECK/FOLD", Color.Red, sett.Accelerate_Check.Point, 0, -fnt.Height);

            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /**/
            void Display_Primitives_Cards(SettingsClass.Settings.Card[] sett_batch_cards)
            {
                //Graphics gr = pictureBox1.CreateGraphics();
                for (int i = 0; i < sett_batch_cards.Length; i++)
                {
                    Draw_Сircle(Color.Red, sett_batch_cards[i].Pixel.Suit.Point);
                    Draw_Сircle(Color.Yellow, sett_batch_cards[i].Pixel.Back.Point);
                    Draw_Rectangle(Color.Red, sett_batch_cards[i].Rectangle_Rank);
                }

            }
            void Draw_Сircle(Color clr, Point p)
            {
                gr.DrawEllipse(new Pen(clr), p.X - 2, p.Y - 2, 4, 4);
            }
            void Draw_Rectangle(Color clr, Rectangle rect)
            {
                gr.DrawRectangle(new Pen(clr), rect);
            }
            void Draw_String(string str, Color clr, Point p, int offset_x, int offset_y)
            {
                gr.DrawString(str, fnt, new SolidBrush(clr), new PointF(p.X + offset_x, p.Y + offset_y));
            }
            void Draw_String_Double(double dbl, Color clr, Point p, int offset_x, int offset_y)
            {
                gr.DrawString(dbl.ToString("F2"), fnt, new SolidBrush(Color.Red), new PointF(p.X + offset_x, p.Y + offset_y));
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*recognize cards on board*/
        static class Recognize_Cards
        {
            static public OperationsWithImage Operations_With_Image { get; set; } = new OperationsWithImage();

            static public void Recognize_Granch_Cards(SettingsClass.Settings.Card[] sett, PlayCards.Card_Struct[] card, Bitmap btm)
            {
                for (int i = 0; i < card.Length; i++)
                {
                    card[i].clr_back = (btm).GetPixel(sett[i].Pixel.Back.Point.X, sett[i].Pixel.Back.Point.Y);
                    card[i].clr_suit = (btm).GetPixel(sett[i].Pixel.Suit.Point.X, sett[i].Pixel.Suit.Point.Y);
                    if (card[i].clr_back == sett[i].Pixel.Back.Color_No) card[i].status = RECOGNIZE_CARD_STATUS.NO;
                    else if (card[i].clr_back == sett[i].Pixel.Back.Color_Back) card[0].status = RECOGNIZE_CARD_STATUS.CLOSE;
                    else card[i].status = RECOGNIZE_CARD_STATUS.OPEN;

                    if (card[0].status == RECOGNIZE_CARD_STATUS.NO || card[0].status == RECOGNIZE_CARD_STATUS.CLOSE)
                    {
                        foreach (PlayCards.Card_Struct c in card)
                        {
                            c.status = card[0].status;
                            c.card.Suit = CardClass.SUIT.None;
                            c.card.Rank = CardClass.RANK.None;
                        }
                        break;
                    }

                    if (card[i].status == RECOGNIZE_CARD_STATUS.NO)
                    {
                        card[i].card.Rank = CardClass.RANK.None;
                        card[i].card.Suit = CardClass.SUIT.None;
                        for (int x = i; x < card.Length; x++)
                        {
                            card[x].card.Rank = CardClass.RANK.None;
                            card[x].card.Suit = CardClass.SUIT.None;
                            card[x].status = RECOGNIZE_CARD_STATUS.NO;
                        }
                        break;
                    }


                    Color[] palette_color = new Color[5];
                    palette_color[0] = Settings.Graphic_Table.Suit_Hearts_Color;
                    palette_color[1] = Settings.Graphic_Table.Suit_Diamonds_Color;
                    palette_color[2] = Settings.Graphic_Table.Suit_Clubs_Color;
                    palette_color[3] = Settings.Graphic_Table.Suit_Spades_Color;
                    int clr_num = Operations_With_Image.Find_Color_In_Palette(palette_color, card[i].clr_suit, 4);

                    if (clr_num == -1)
                    {
                        card[i].card.Rank = CardClass.RANK.None;
                        card[i].card.Suit = CardClass.SUIT.None;
                        card[i].status = RECOGNIZE_CARD_STATUS.ERROR;
                        continue;
                    }
                    switch (clr_num)
                    {
                        case 0: card[i].card.Suit = CardClass.SUIT.Hearts; break;
                        case 1: card[i].card.Suit = CardClass.SUIT.Diamonds; break;
                        case 2: card[i].card.Suit = CardClass.SUIT.Clubs; break;
                        case 3: card[i].card.Suit = CardClass.SUIT.Spades; break;
                    }

                    string str = Operations_With_Image.Get_Text_From_Bitmap(btm, sett[i].Rectangle_Rank, "1234567890JQKA", Settings.Graphic_Table.Rank_Black_White_Coeff);

                    switch (str)
                    {
                        case "2": card[i].card.Rank = CardClass.RANK.Two; break;
                        case "3": card[i].card.Rank = CardClass.RANK.Three; break;
                        case "4": card[i].card.Rank = CardClass.RANK.Four; break;
                        case "5": card[i].card.Rank = CardClass.RANK.Five; break;
                        case "6": card[i].card.Rank = CardClass.RANK.Six; break;
                        case "7": card[i].card.Rank = CardClass.RANK.Seven; break;
                        case "8": card[i].card.Rank = CardClass.RANK.Eight; break;
                        case "9": card[i].card.Rank = CardClass.RANK.Nine; break;
                        case "10": card[i].card.Rank = CardClass.RANK.Ten; break;
                        case "J": card[i].card.Rank = CardClass.RANK.Jack; break;
                        case "Q": card[i].card.Rank = CardClass.RANK.Queen; break;
                        case "K": card[i].card.Rank = CardClass.RANK.King; break;
                        case "A": card[i].card.Rank = CardClass.RANK.Ace; break;
                        default:
                            card[i].card.Rank = CardClass.RANK.None;
                            card[i].card.Suit = CardClass.SUIT.None;
                            card[i].status = RECOGNIZE_CARD_STATUS.ERROR;
                            break;
                    }

                    if (card[i].status == RECOGNIZE_CARD_STATUS.ERROR) continue;
                    card[i].status = RECOGNIZE_CARD_STATUS.OK;
                }
            }

            /*распознаем все карты на скрине*/
            static public void Recognize(Bitmap btm, PlayCards PlayCards)
            {
                lock (locker)
                {
                    Recognize_Granch_Cards(Settings.Graphic_Table.Board.Card, PlayCards.Board, btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.My.Card, PlayCards.My, btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.Player[1].Card, PlayCards.Player[1], btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.Player[2].Card, PlayCards.Player[2], btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.Player[3].Card, PlayCards.Player[3], btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.Player[4].Card, PlayCards.Player[4], btm);
                    Recognize_Granch_Cards(Settings.Graphic_Table.Player[5].Card, PlayCards.Player[5], btm);
                }
            }
            static public void Recognize_My_Cards(Bitmap btm, PlayCards PlayCards)
            {

                Recognize_Granch_Cards(Settings.Graphic_Table.My.Card, PlayCards.My, btm);
            }
        }

        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        static public class Recognize_My_Course
        {
            static public bool Check_Point_Be(Bitmap btm, SettingsClass.Settings.My_Course sett, Action<MY_COURSE> course)
            {
                Color clr_pnt = (btm).GetPixel(sett.Point.X, sett.Point.Y);
                Color cl_act = sett.Color_Action;
                if (cl_act == clr_pnt) course(MY_COURSE.OK);
                else course(MY_COURSE.WAIT);
                if (cl_act == clr_pnt) return true;
                else return false;
            }

            static public bool Check_Pre_Flop(Bitmap btm)
            {
                Color clr_pnt = (btm).GetPixel(Settings.Graphic_Table.Board.Card[0].Pixel.Back.Point.X, Settings.Graphic_Table.Board.Card[0].Pixel.Back.Point.Y);
                Color cl_act = Settings.Graphic_Table.Board.Card[0].Pixel.Back.Color_No;
                if (cl_act == clr_pnt) return true;
                else return false;
            }

            static public bool Check_Pre_Flop_Accelerate_CheckFold(Bitmap btm, Class_Table Table)
            {
                if (!Check_Pre_Flop(btm)) return false;
                if (!Check_Point_Be(btm, Settings.Graphic_Table.Accelerate_Fold, value => Table.Accelerate_Fold = value) && !Check_Point_Be(btm, Settings.Graphic_Table.Accelerate_Check, value => Table.Accelerate_Check = value)) return false;
                Recognize_Cards.Recognize_My_Cards(btm, Table.PlayCards);
                Recognize_Button.Recognize(btm, Table);
                return true;
            }
        }

        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        static class Recognize_Button
        {
            static public void Recognize(Bitmap btm, Class_Table Table)
            {
                Table.Button.Position_Button = -1;
                Table.Button.Status = RECOGNIZE_BUTTON_STATUS.NO;

                for (int i = 0; i < 6; i++)
                {
                    Color clr_butt = Settings.Graphic_Table.Player[i].Color_Button;
                    Point pnt = Settings.Graphic_Table.Player[i].Point_Button;
                    Color clr_pnt = (btm).GetPixel(pnt.X, pnt.Y);
                    if (clr_butt == clr_pnt)
                    {
                        Table.Button.Position_Button = i;
                        Table.Button.Status = (RECOGNIZE_BUTTON_STATUS)i;
                        break;
                    }
                }

                if (Table.Button.Position_Button == -1)
                {
                    Table.Button.Status = RECOGNIZE_BUTTON_STATUS.NO;
                    for (int i = 0; i < 6; i++) Table.Position_Players.Position[i] = POSITION.ERROR;
                    return;
                }

                for (int i = 0, j = Table.Button.Position_Button; i < 6; i++, j = (++j == 6) ? 0 : j)
                    Table.Position_Players.Position[j] = (POSITION)i;
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        static class Recognize_Finance
        {
            static public OperationsWithImage Operations_With_Image { get; set; } = new OperationsWithImage();

            static public void Recognize(Bitmap btm, Finance fin)
            {
                lock (locker)
                {
                    SettingsClass.Settings._Graphic_Table sett = Settings.Graphic_Table;
                    //--------------------------------------------------------------------------------------
                    //--------------------------------------------------------------------------------------
                    //--------------------------------------------------------------------------------------
                    //Bets Players 
                    for (int i = 0; i < 6; i++)
                    {
                        Recognize_Money(sett.Player[i].Bet, fin.Bets.Player[i], btm, "static text area");
                        Recognize_Money(sett.Player[i].Stack, fin.Stacks.Player[i], btm, "dinamic text area");
                    }
                    //Bet Pot 
                    Recognize_Money(sett.Pot, fin.Pot, btm, "dinamic text area");
                    //Bet Call 
                    Recognize_Money(sett.Call, fin.Call, btm, "dinamic text area");
                    //Bet Raise 
                    Recognize_Money(sett.Raise, fin.Raise, btm, "dinamic text area");
                    //Bet Header 
                    Recognize_Money(sett.Header, fin.Header, btm, "static text area");
                    if (fin.Header.Float != 0.02
                        && fin.Header.Float != 0.05
                        && fin.Header.Float != 0.10
                        && fin.Header.Float != 0.25)
                    {
                        fin.Header.Status = RECOGNIZE_BET_STATUS.ERROR;
                    }
                }
            }
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*recognize cards on board*/
            static public void Recognize_Money(SettingsClass.Settings.Money sett, Finance.Money money, Bitmap btm, string recognize_text_area)
            {
                Rectangle location = OperationsWithImage.Search_Bitmap(sett.Bitmap_Dollar, OperationsWithImage.Get_Image_Region(btm, sett.Search_Rectangle), 0.2);
                if (location.Width != 0)
                {
                    int btm_width = sett.Img_Dollar_Width;
                    int x = location.X - 1 + sett.Search_Rectangle.X + btm_width;
                    int y = sett.Search_Rectangle.Y;
                    int width,height;

                        if (recognize_text_area == "static text area")
                        {
                            width = sett.Text_Rectangle_Width;
                        height = sett.Text_Rectangle_Height;
                        }
                        else
                        {
                            width = sett.Search_Rectangle.Right - x;
                        height = sett.Search_Rectangle.Bottom - y;
                        }
                    


                    money.Rectangle = new Rectangle(x, y, width, height);
                    string str = Operations_With_Image.Get_Text_From_Bitmap(btm, money.Rectangle, "1234567890.", sett.Black_White_Coeff);
#if DEBUG
                    Console.WriteLine(str);
#endif
                    try
                    {
                        money.Float = double.Parse(str);
                        money.Status = RECOGNIZE_BET_STATUS.OK;
                    }
                    catch (Exception)
                    {
                        money.Float = default(double);
                        money.Status = RECOGNIZE_BET_STATUS.ERROR;
                    }
                    //Console.WriteLine("Player {0} bet YES", i);
                }
                else
                {
                    money.Float = default(double);
                    money.Status = RECOGNIZE_BET_STATUS.NO;
                    //Console.WriteLine("Player {0} bet NO", i);
                }


            }


        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        public static void Thread_Recognize_Screen(Bitmap btm, Class_Table recognize)
        {
            Thread Thread_Recognize_Screen = new Thread(delegate () { Recognize.Recognize_Screen(btm, recognize); });
            Thread_Recognize_Screen.Name = "Recognize Screen";
            Thread_Recognize_Screen.Priority = ThreadPriority.Highest;
            Thread_Recognize_Screen.Start();
            Thread_Recognize_Screen.Join();    //expect until the Thread comes to the end
        }
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            public static void Recognize_Screen(Bitmap btm, Class_Table Table)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            Recognize_Cards.Recognize(btm, Table.PlayCards);
            Recognize_Finance.Recognize(btm, Table.Finance);
            Recognize_Button.Recognize(btm, Table);
            Recognize_My_Course.Check_Point_Be(btm, Settings.Graphic_Table.My_Course, value => Table.My_Course = value);
            Recognize_My_Course.Check_Point_Be(btm, Settings.Graphic_Table.My_Course_Call, value => Table.My_Course_Call = value);
            if (Table.My_Course_Call != MY_COURSE.OK) {
                Table.Finance.Call = Table.Finance.Raise;
            }
            Recognize_My_Course.Check_Point_Be(btm, Settings.Graphic_Table.Accelerate_Fold, value => Table.Accelerate_Fold = value);
            Recognize_My_Course.Check_Point_Be(btm, Settings.Graphic_Table.Accelerate_Check, value => Table.Accelerate_Check = value);
        }









    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Collections;
using HoldemHand;
using Cards;
using StringEnumClass;

namespace LogicClass
{
    public enum STAGE_STATUS
    {
        [StringValue("ERROR")] ERROR = 0,
        [StringValue("INIT")] INIT = 1,
        [StringValue("PRE-FLOP")] PRE_FLOP = 2,
        [StringValue("FLOP")] FLOP = 3,
        [StringValue("TURN")] TURN = 4,
        [StringValue("RIVER")] RIVER = 5,
    };


    public enum FINT_STATUS
    {
        [StringValue("NO")] INIT
            , [StringValue("FLOP HIDE NUTS")] FLOP_HIDE_NUTS
            , [StringValue("TURN CHECK RAISE")] TURN_CHECK_RAISE

    };

    public enum BET_STATUS
    {
        [StringValue("ERROR")] ERROR,
        [StringValue("TEMP ERROR")] TEMP_ERROR,
        [StringValue("INIT")] INIT,
        [StringValue("OK")] OK
    };
    public enum POSITION
    {
        [StringValue("BU")] BUTTON = 0,             //BUTTON
        [StringValue("SB")] SMALL_BLIND = 1,        //SMALL BLIND
        [StringValue("BB")] BIG_BLIND = 2,          //BIG BLIND
        [StringValue("UTG")] UNDER_THE_GUN = 3,     //UNDER THE GUN
        [StringValue("MP")] MIDDLE_POSITION = 4,    //MIDDLE POSITION
        [StringValue("CO")] CUT_OFF = 5,            //CUT OFF
        [StringValue("ER")] ERROR = 6,              //ERROR
        [StringValue("INT")] INIT = 7,              //INIT
    };
    public enum PLAYER_STATUS
    {
        [StringValue("ERROR")] ERROR,
        [StringValue("INIT")] INIT,
        [StringValue("NO")] NO,
        [StringValue("UTG")] UTG,
        [StringValue("FOLD")] FOLD,
        [StringValue("CALL")] CALL,
        [StringValue("CHECK")] CHECK,
        [StringValue("RAISE")] RAISE,
        [StringValue("RE-RAISE")] RE_RAISE
    };

    public enum STAGE_ACTION
    {
        [StringValue("FOLD")] FOLD = 0,
        [StringValue("CALL")] CALL = 1,
        [StringValue("CHECK")] CHECK = 2,
        [StringValue("RAISE")] RAISE = 3,
        [StringValue("RE-RAISE")] RE_RAISE = 4,
        [StringValue("ERROR")] ERROR = 6,
        [StringValue("WAIT")] WAIT = 7,
        [StringValue("OK")] OK = 8,
        [StringValue("INIT")] INIT = 9
    };

    public class PlayerClass
    {
        public CardClass.Card[] Cards { get; set; } = new CardClass.Card[2];
        public PLAYER_STATUS Status { get; set; } = PLAYER_STATUS.INIT;
        public POSITION Position { get; set; } = POSITION.INIT;
        public PLAYER_STATUS[] Action_History { get; set; } = new PLAYER_STATUS[4];
        public PlayerClass()
        {
            Cards[0] = new CardClass.Card();
            Cards[1] = new CardClass.Card();
            for (int i = 0; i < 4; i++) Action_History[i] = PLAYER_STATUS.INIT;         
        }

        public void Clear()
        {
           Status = PLAYER_STATUS.INIT;
           Position = POSITION.INIT;
           Cards[0].Clear();
           Cards[1].Clear();
        }
        public void Clear_Action_History()
        {
            for (int i = 0; i < 4; i++) Action_History[i] = PLAYER_STATUS.INIT;
        }

        public static PlayerClass CloneObject(PlayerClass obj)
        {
            if (obj == null) return null;

            Type t1 = obj.GetType();
            PlayerClass ret = (PlayerClass)Activator.CreateInstance(t1);

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
    }

    public class BetsClass
    {
        public double[] Player { get; set; } = new double[6] { 0, 0, 0, 0, 0, 0 };
        public double Pot { get; set; } = 0;
        public double Call { get; set; } = 0;
        public double Raise { get; set; } = 0;
        public double Bet { get; set; } = 0;
        public BET_STATUS Status = BET_STATUS.INIT;
        public BetsClass()
        {
        }
        public static BetsClass CloneObject(BetsClass obj)
        {
            if (obj == null) return null;

            Type t1 = obj.GetType();
            BetsClass ret = (BetsClass)Activator.CreateInstance(t1);

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
    }


    public class Logic
    {
        public PlayerClass[] Players { get; set; } = new PlayerClass[6];
        public CardClass.Card[] Hand_Card { get; set; } = new CardClass.Card[7];
        public double[] My_Odds { get; set; } = new double[12];
        public double[] Opponents_Odds { get; set; } = new double[12];
        public int Opponents_Stage { get; set; }
        public int Opponents_Game { get; set; }
        public BetsClass Bets { get; set; } = new BetsClass();
        public STAGE_STATUS Stage { get; set; } = STAGE_STATUS.INIT;
        public STAGE_ACTION Stage_Action { get; set; } = STAGE_ACTION.INIT;
        public FINT_STATUS Fint { get; set; } = FINT_STATUS.INIT;

        public double Call_Сriterion { get; set; }
        public double Raise_Criterion { get; set; }

        public List<string> Hand_Cards_For_Fold = new List<string>();

        public Logic()
        {
            for (int i = 0; i < 7; i++) Hand_Card[i] = new CardClass.Card();
            for (int i = 0; i < 6; i++) Players[i] = new PlayerClass();
        }

        public void Clear()
        {
            for (int i = 0; i < 7; i++) Hand_Card[i] = new CardClass.Card();

            My_Odds = new double[12];
            Opponents_Odds = new double[12];
            Opponents_Stage = 0;
            Opponents_Game = 0;
            Bets = new BetsClass();
            Stage = STAGE_STATUS.INIT;
            Stage_Action = STAGE_ACTION.INIT;           
            Call_Сriterion = 0;
            Raise_Criterion = 0;

            //Saved
            for (int i = 0; i < 6; i++) Players[i].Clear();
            Fint = Fint;
        }
        public void Clear_Action_History()
        {
            for (int i = 0; i < 6; i++) Players[i].Clear_Action_History();
            Fint = FINT_STATUS.INIT;
        }
        
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Evaluation()
        {
            Thread Thread_Probabilities = new Thread(delegate () { Calc_Probabilities(); });
            Thread_Probabilities.Name = "probabilities";
            Thread_Probabilities.Priority = ThreadPriority.Highest;
            Thread_Probabilities.Start();
            //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread_Probabilities.Join();    //expect until the Thread comes to the end
        }
        public void Calc_Probabilities()
        {
            string[] str_crd = new string[5];
            str_crd[0] = Hand_Card[0].CardToStrDeck() == "NN" ? "" : (Hand_Card[0].CardToStrDeck() + " ");
            str_crd[1] = Hand_Card[1].CardToStrDeck() == "NN" ? "" : Hand_Card[1].CardToStrDeck();
            string Pocket = str_crd[0] + str_crd[1];
            str_crd[0] = Hand_Card[2].CardToStrDeck() == "NN" ? "" : (Hand_Card[2].CardToStrDeck() + " ");
            str_crd[1] = Hand_Card[3].CardToStrDeck() == "NN" ? "" : (Hand_Card[3].CardToStrDeck() + " ");
            str_crd[2] = Hand_Card[4].CardToStrDeck() == "NN" ? "" : (Hand_Card[4].CardToStrDeck() + " ");
            str_crd[3] = Hand_Card[5].CardToStrDeck() == "NN" ? "" : (Hand_Card[5].CardToStrDeck() + " ");
            str_crd[4] = Hand_Card[6].CardToStrDeck() == "NN" ? "" : Hand_Card[6].CardToStrDeck();
            string Board = str_crd[0] + str_crd[1] + str_crd[2] + str_crd[3] + str_crd[4]; //Board = "" or Board = "AH AS"(for example)

            Hand.HandWinOdds(Hand.ParseHand(Pocket), Hand.ParseHand(Board), out double[] my_odds, out double[] opponents_odds, Opponents_Stage + 2, 0.1);

            this.My_Odds[9] = 0;
            this.Opponents_Odds[9] = 0;
            for (int i = 0; i < 9; i++)
            {
                this.My_Odds[9] += my_odds[i];
                this.Opponents_Odds[9] += opponents_odds[i];
                this.My_Odds[i] = my_odds[i];
                this.Opponents_Odds[i] = opponents_odds[i];
            }

            int stage_int = 0;
            double correction = 0.0;
            if (Stage == STAGE_STATUS.PRE_FLOP) { stage_int = 3; correction = 0.3; }
            if (Stage == STAGE_STATUS.FLOP) { stage_int = 2; correction = 0.4; }
            if (Stage == STAGE_STATUS.TURN) { stage_int = 1; correction = 0.4; }
            if (Stage == STAGE_STATUS.RIVER) { stage_int = 4; correction = 0.7; }
            this.My_Odds[10] = (Bets.Call + stage_int * Bets.Bet * correction) / ((Bets.Pot + Bets.Call) + stage_int * Bets.Bet * Opponents_Stage);
            this.My_Odds[11] = (Bets.Raise + stage_int * Bets.Bet * correction) / ((Bets.Pot + Bets.Raise) + stage_int * Bets.Bet * Opponents_Stage);

            if (Stage == STAGE_STATUS.PRE_FLOP)
            {
                stage_int = 3; correction = 0.05;
                this.My_Odds[10] = (Bets.Call + stage_int * Bets.Bet * correction) / ((Bets.Call) + stage_int * Bets.Bet * Opponents_Stage);
                stage_int = 3; correction = 0.3;
                this.My_Odds[11] = (Bets.Raise + stage_int * Bets.Bet * correction) / ((Bets.Raise) + stage_int * Bets.Bet * Opponents_Stage);
            }

            if (Stage == STAGE_STATUS.RIVER)
            {
                this.My_Odds[10] = Bets.Call / (Bets.Pot + Bets.Call);
            }
            if (Bets.Call == 0) { this.My_Odds[10] = 0; }


            if ((this.My_Odds[9] - this.My_Odds[11]) * 100 > Raise_Criterion) Stage_Action = STAGE_ACTION.RAISE;
            else if (Bets.Call == 0) Stage_Action = STAGE_ACTION.CHECK;
            else if ((this.My_Odds[9] - this.My_Odds[10]) * 100 > Call_Сriterion) Stage_Action = STAGE_ACTION.CALL;
            else Stage_Action = STAGE_ACTION.FOLD;


            // Logic _______________________________________________________________________        
            if (Stage == STAGE_STATUS.PRE_FLOP)
            {

                if (this.My_Odds[9] < 0.20) Stage_Action = STAGE_ACTION.FOLD;

            }
            if (Stage == STAGE_STATUS.FLOP)
            {
                if (this.My_Odds[9] < 0.6 && (Bets.Player[0] != 0))
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                    }
                }
                if (this.My_Odds[9] < 0.4)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (Players[i].Status == PLAYER_STATUS.RAISE)
                            if (Stage_Action == STAGE_ACTION.RAISE)
                                Stage_Action = STAGE_ACTION.CALL;
                    }

                }
            }
            if (Stage == STAGE_STATUS.TURN)
            {
                if (this.My_Odds[9] < 0.6 && (Bets.Player[0] != 0))
                {

                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                    }
                }
                else if (this.My_Odds[9] < 0.4)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (Players[i].Status == PLAYER_STATUS.RAISE)
                            if (Stage_Action == STAGE_ACTION.RAISE)
                                Stage_Action = STAGE_ACTION.CALL;
                    }

                }
                else if (If_Flush_Draw_On_Board(Hand_Card, Stage) && !If_Flush_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Flush Draw On Board -- Replase RAISE on CALL");
                    }
                }
                else if (If_Gutshot_On_Board(Hand_Card, Stage) && !If_Straight_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Gutshot On Board -- CALL");
                    }
                }
                else if (If_Straight_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.FOLD)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Straight -- Replase FOLD on CALL");
                    }
                }
                else if (!If_Flush_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.FOLD)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Flush -- Replase FOLD on CALL");
                    }
                }
                else if (this.My_Odds[9] < 0.20)
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        //Console.WriteLine("Flush Draw On Board -- Replase RAISE on CALL");
                    }
                }
                Console.WriteLine("Gutshot On Board:  " + If_Gutshot_On_Board(Hand_Card, Stage));
                Console.WriteLine(If_Straight(Hand_Card, Stage));
                Console.WriteLine("Flush Draw On Board:  " + If_Flush_Draw_On_Board(Hand_Card, Stage));
                Console.WriteLine(If_Flush(Hand_Card, Stage));
            }
            if (Stage == STAGE_STATUS.RIVER)
            {
                if (this.My_Odds[9] < 0.45 && (Bets.Player[0] != 0))
                {
                    Stage_Action = STAGE_ACTION.CALL;
                }
                else if (this.My_Odds[9] < 0.4)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (Players[i].Status == PLAYER_STATUS.RAISE)
                            if (Stage_Action == STAGE_ACTION.RAISE)
                                Stage_Action = STAGE_ACTION.CALL;
                    }

                }
                else if (If_Flush_Draw_On_Board(Hand_Card, Stage) && !If_Flush_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Flush Draw On Board -- Replase RAISE on CALL");
                    }
                }
                else if (If_Gutshot_On_Board(Hand_Card, Stage) && !If_Straight_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Gutshot On Board -- CALL");
                    } 
                }
                else if (If_Straight_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.FOLD)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Straight -- Replase FOLD on CALL");
                    }
                }
                else if (!If_Flush_Bool(Hand_Card, Stage))
                {
                    if (Stage_Action == STAGE_ACTION.FOLD)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        Console.WriteLine("Flush -- Replase FOLD on CALL");
                    }
                }
                else if (this.My_Odds[9] < 0.20)
                {
                    if (Stage_Action == STAGE_ACTION.RAISE)
                    {
                        Stage_Action = STAGE_ACTION.CALL;
                        //Console.WriteLine("Flush Draw On Board -- Replase RAISE on CALL");
                    }
                }
                Console.WriteLine("Gutshot On Board:  " + If_Gutshot_On_Board(Hand_Card, Stage));
                Console.WriteLine(If_Straight(Hand_Card, Stage));
                Console.WriteLine("Flush Draw On Board:  " + If_Flush_Draw_On_Board(Hand_Card, Stage));
                Console.WriteLine(If_Flush(Hand_Card, Stage));
            }

            // FINT _______________________________________________________________________
            if (Stage == STAGE_STATUS.PRE_FLOP)
            {

                this.Fint = FINT_STATUS.INIT;

            }

            if (Stage == STAGE_STATUS.FLOP)
            {
                if (My_Odds[9] > 0.6)
                {
                    this.Fint = FINT_STATUS.FLOP_HIDE_NUTS;
                }
                else
                {
                    this.Fint = FINT_STATUS.INIT;
                }
            }

            if (Stage == STAGE_STATUS.TURN)
            {
                if (My_Odds[9] > 0.6 && this.Fint != FINT_STATUS.TURN_CHECK_RAISE)
                {
                    //this.Fint = FINT_STATUS.TURN_CHECK_RAISE;
                }
                else
                {
                    this.Fint = FINT_STATUS.INIT;
                }
            }

            if (Stage == STAGE_STATUS.RIVER)
            {
                this.Fint = FINT_STATUS.INIT;
            }


            //if (this.Fint == FINT_STATUS.FLOP_HIDE_NUTS) Stage_Action = STAGE_ACTION.CALL;

            //if (this.Fint == FINT_STATUS.TURN_CHECK_RAISE) Stage_Action = STAGE_ACTION.CALL;


            // HISTORY _______________________________________________________________________
            if (Stage == STAGE_STATUS.PRE_FLOP)
            {
                this.Clear_Action_History();
            }

            int stg = (int)Stage - (int)STAGE_STATUS.PRE_FLOP;
            for (int i = 1; i < 6; i++) this.Players[i].Action_History[stg] = this.Players[i].Status;
            PLAYER_STATUS act = PLAYER_STATUS.INIT;
            switch (Stage_Action)
            {
                case STAGE_ACTION.FOLD: act = PLAYER_STATUS.FOLD; break;

                case STAGE_ACTION.CHECK: 
                case STAGE_ACTION.CALL: act = PLAYER_STATUS.CALL; break;

                case STAGE_ACTION.RAISE:
                case STAGE_ACTION.RE_RAISE: act = PLAYER_STATUS.RAISE; break;
            }
            this.Players[0].Action_History[stg] = act;
        }


        public void Evaluation_Pre_Flop(STAGE_ACTION act1, STAGE_ACTION act2)
        {
            string str;
            if (Hand_Card[1].Rank > Hand_Card[0].Rank) str = CardClass.Card.RankToStr(Hand_Card[1].Rank) + CardClass.Card.RankToStr(Hand_Card[0].Rank);
            else str = CardClass.Card.RankToStr(Hand_Card[0].Rank) + CardClass.Card.RankToStr(Hand_Card[1].Rank);

            if (Hand_Card[1].Suit == Hand_Card[0].Suit) str += "s";

            if (Hand_Cards_For_Fold.Find(item => item == str) != null) Stage_Action = act1;
            else Stage_Action = act2;
        }



        // flush is when all of the suits are the same
        static public bool If_Gutshot_On_Board(CardClass.Card[] Hand_Card, STAGE_STATUS stage)
        {
            int amount_card = 3 + (int)stage - (int)STAGE_STATUS.FLOP;

            int[] rank = new int[amount_card];
            for (int i = 0; i < amount_card; i++)
                rank[i] = (int)Hand_Card[2 + i].Rank;

            var unique = rank.Distinct(); //только уникальные числа
            if (unique.Count() < 4) return false;
            rank = new int[unique.Count()];
            int t = 0; foreach (int i in unique) rank[t++] = i;

            Array.Sort(rank);

            List<int[]> straight_list = new List<int[]>();
            straight_list.Add(new int[] { (int)CardClass.RANK.Ace, (int)CardClass.RANK.Two, (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five });
            straight_list.Add(new int[] { (int)CardClass.RANK.Two, (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six });
            straight_list.Add(new int[] { (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven });
            straight_list.Add(new int[] { (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight });
            straight_list.Add(new int[] { (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine });
            straight_list.Add(new int[] { (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten });
            straight_list.Add(new int[] { (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack });
            straight_list.Add(new int[] { (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen });
            straight_list.Add(new int[] { (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen, (int)CardClass.RANK.King });
            straight_list.Add(new int[] { (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen, (int)CardClass.RANK.King, (int)CardClass.RANK.Ace });


            int amount_batch_4card = rank.Length - 4 + 1;
            for (int i = 0; i < amount_batch_4card; i++)
                foreach (var item in straight_list)
                {
                    if (Array.IndexOf(item, rank[i + 0]) != -1 &&
                        Array.IndexOf(item, rank[i + 1]) != -1 &&
                        Array.IndexOf(item, rank[i + 2]) != -1 &&
                        Array.IndexOf(item, rank[i + 3]) != -1
                        ) return true;

                };
            return false;
        }

        static public bool If_Flush_Draw_On_Board(CardClass.Card[] cards, STAGE_STATUS stage)
        {
            int amount_card_on_board = 3 + (int)stage - (int)STAGE_STATUS.FLOP; //+3 карты флопа
            int amount_batch_4card = amount_card_on_board - 4; //кол-во пачек по 5 карт

            CardClass.Card[] board_cards = new CardClass.Card[amount_card_on_board];
            for (int i = 0; i < amount_card_on_board; i++) board_cards[i] = cards[i + 2];

            int num_clubs = board_cards.Where(x => x.Suit == CardClass.SUIT.Clubs).Count();
            int num_diamonds = board_cards.Where(x => x.Suit == CardClass.SUIT.Diamonds).Count();
            int num_hearts = board_cards.Where(x => x.Suit == CardClass.SUIT.Hearts).Count();
            int num_spades = board_cards.Where(x => x.Suit == CardClass.SUIT.Spades).Count();

            if (num_clubs >= 4 || num_diamonds >= 4 || num_hearts >= 4 || num_spades >= 4) return true;
            return false;
        }

        public string If_Flush(CardClass.Card[] cards, STAGE_STATUS stage)
        {

            int amount_card_on_board = 3 + (int)stage - (int)STAGE_STATUS.FLOP; //+3 карты флопа
            int amount_card = amount_card_on_board + 2;

            CardClass.Card[] hand_cards = new CardClass.Card[amount_card];
            for (int i = 0; i < amount_card; i++) hand_cards[i] = cards[i];

            CardClass.Card[] flush_cards;
            int num_clubs = hand_cards.Where(x => x.Suit == CardClass.SUIT.Clubs).Count();
            int num_diamonds = hand_cards.Where(x => x.Suit == CardClass.SUIT.Diamonds).Count();
            int num_hearts = hand_cards.Where(x => x.Suit == CardClass.SUIT.Hearts).Count();
            int num_spades = hand_cards.Where(x => x.Suit == CardClass.SUIT.Spades).Count();

            if (num_clubs > 4) flush_cards = hand_cards.Where(x => x.Suit == CardClass.SUIT.Clubs).ToArray();
            else if (num_diamonds > 4) flush_cards = hand_cards.Where(x => x.Suit == CardClass.SUIT.Diamonds).ToArray();
            else if (num_hearts > 4) flush_cards = hand_cards.Where(x => x.Suit == CardClass.SUIT.Hearts).ToArray();
            else if (num_spades > 4) flush_cards = hand_cards.Where(x => x.Suit == CardClass.SUIT.Spades).ToArray();
            else return "No Flush"; //нет flush полюбому

            Array.Sort((CardClass.Card[])flush_cards, CardClass.Card.Sort_By_SuitRank_Decrease());

            bool my_cards_be = false;
            for (int j = 0; j < 5; j++) if (flush_cards[j] == cards[0] || flush_cards[j] == cards[1]) my_cards_be = true;

            if (!my_cards_be) return "Flush On Board Not My"; //Flush есть, но не мой


            string str = "";
            //Array.Sort((CardClass.Card[])flush_cards, CardClass.Card.Sort_By_SuitRank_Increase());
            for (int j = 0; j < 5; j++)
            {
                if (cards[0].Equals(flush_cards[j]) && (cards[0].Rank >= CardClass.RANK.King) && (j == 0)) { str = " High"; break; }
                if (cards[1].Equals(flush_cards[j]) && (cards[1].Rank >= CardClass.RANK.King) && (j == 0)) { str = " High"; break; }

                if((cards[0].Rank == CardClass.RANK.King || cards[1].Rank == CardClass.RANK.King) &&(flush_cards[0].Rank == CardClass.RANK.Ace && j == 1)){
                    if (cards[0].Equals(flush_cards[j])) { str = " High"; break; }
                    if (cards[1].Equals(flush_cards[j])) { str = " High"; break; }

                }

                if ((cards[0].Rank == CardClass.RANK.Queen || cards[1].Rank == CardClass.RANK.Queen) && 
                    (flush_cards[0].Rank == CardClass.RANK.Ace &&
                    flush_cards[1].Rank == CardClass.RANK.King &&
                    j == 2)
                    )
                {
                    if (cards[0].Equals(flush_cards[j])) { str = " High"; break; }
                    if (cards[1].Equals(flush_cards[j])) { str = " High"; break; }

                }

                if ((cards[0].Rank == CardClass.RANK.Jack || cards[1].Rank == CardClass.RANK.Jack) &&
                (flush_cards[0].Rank == CardClass.RANK.Ace &&
                flush_cards[1].Rank == CardClass.RANK.King &&
                flush_cards[2].Rank == CardClass.RANK.Queen &&
                j == 3)
                )
                {
                    if (cards[0].Equals(flush_cards[j])) { str = " High"; break; }
                    if (cards[1].Equals(flush_cards[j])) { str = " High"; break; }

                }

                if ((cards[0].Rank == CardClass.RANK.Ten || cards[1].Rank == CardClass.RANK.Ten) &&
                (flush_cards[0].Rank == CardClass.RANK.Ace &&
                flush_cards[1].Rank == CardClass.RANK.King &&
                flush_cards[2].Rank == CardClass.RANK.Queen &&
                flush_cards[3].Rank == CardClass.RANK.Jack &&
                j == 4)
                )
                {
                    if (cards[0].Equals(flush_cards[j])) { str = " High"; break; }
                    if (cards[1].Equals(flush_cards[j])) { str = " High"; break; }
                }

                if (j == 5) {
                    str = "";
                    break;
                }

                if (cards[0].Equals(flush_cards[j]) && (cards[0].Rank <= CardClass.RANK.Jack)) { str = " Low";}
                if (cards[1].Equals(flush_cards[j]) && (cards[1].Rank <= CardClass.RANK.Jack)) { str = " Low"; }
            }
            if (str != "")
            {
                string message = "Flush";
                if (str == " High" || str == " Low") message += str;
                return message;
            }
            //когда у меня нижние карты и на доске Flush
            if (str == "") return "Flush On Board Not My"; //Flush есть, но не мой

            return "No Flush";
        }

        public bool If_Straight_Bool(CardClass.Card[] cards, STAGE_STATUS stage)
        {
            string str = If_Straight(cards, stage);
            if (str == "Straight" || str == "Straight High Two Card" || str == "Straight High One Card")
                return true;

            return false;
        }
        public bool If_Flush_Bool(CardClass.Card[] cards, STAGE_STATUS stage)
        {
            string str = If_Flush(cards, stage);
            if (str != "No Flush")
                return true;

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="stage"></param>
        /// <returns>
        /// "No Straight"
        /// "Straight"
        /// "Straight High Two Card"
        /// "Straight High One Card"
        /// "Straight Low Two Card"
        /// "Straight Low One Card"
        /// "Straight On Board"
        /// </returns>
        public string If_Straight(CardClass.Card[] cards, STAGE_STATUS stage)
        {

            int amount_card_on_board = 3 + (int)stage - (int)STAGE_STATUS.FLOP; //+3 карты флопа
            int amount_card = amount_card_on_board + 2;

            HashSet<CardClass.Card> unique_cards = new HashSet<CardClass.Card>(cards, new CardClass.Card.FilterByRank()); // Все значения но по одному разу
            unique_cards.RemoveWhere(item => item.Rank == CardClass.RANK.None); //delate cards None
            if (unique_cards.Count < 5) return "No Straight"; //нет стрита полюбому

            int[][] straight_arr = new int[10][];
            straight_arr[0] = new int[5] { (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen, (int)CardClass.RANK.King, (int)CardClass.RANK.Ace };
            straight_arr[1] = new int[5] { (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen, (int)CardClass.RANK.King };
            straight_arr[2] = new int[5] { (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack, (int)CardClass.RANK.Queen };
            straight_arr[3] = new int[5] { (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten, (int)CardClass.RANK.Jack };
            straight_arr[4] = new int[5] { (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine, (int)CardClass.RANK.Ten };
            straight_arr[5] = new int[5] { (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight, (int)CardClass.RANK.Nine };
            straight_arr[6] = new int[5] { (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven, (int)CardClass.RANK.Eight };
            straight_arr[7] = new int[5] { (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six, (int)CardClass.RANK.Seven };
            straight_arr[8] = new int[5] { (int)CardClass.RANK.Two, (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five, (int)CardClass.RANK.Six };
            straight_arr[9] = new int[5] { (int)CardClass.RANK.Ace, (int)CardClass.RANK.Two, (int)CardClass.RANK.Three, (int)CardClass.RANK.Four, (int)CardClass.RANK.Five };

            int[] straight_arr_be = new int[5];
            int[] hand_card = new int[amount_card];
            for (int i = 0; i < amount_card; i++) hand_card[i] = (int)cards[i].Rank;
            
            for (int i = 0; i < straight_arr.Length; i++)
            {
                bool straight_be = true;
                for (int j = 0; j < 5; j++)
                    if (Array.IndexOf(hand_card, straight_arr[i][j]) == -1)
                    {
                        straight_be = false;
                        break;
                    }
                if (straight_be) { straight_arr_be = straight_arr[i]; break; }
                }

            if (straight_arr_be[0] == 0) return "No Straight"; //нет стрита

            string str = "";
            int pos_card0 = Array.IndexOf(straight_arr_be, (int)cards[0].Rank);
            int pos_card1 = Array.IndexOf(straight_arr_be, (int)cards[1].Rank);

            if (pos_card0 != -1 && Array.Find(cards, p => (int)p.Rank == (int)cards[0].Rank && p != cards[0] && p != cards[1]) == null) //и нет на доске такой же карты
                str += (pos_card0 + 1).ToString();
            if (pos_card1 != -1 && Array.Find(cards, p => (int)p.Rank == (int)cards[1].Rank && p != cards[0] && p != cards[1]) == null) //и нет на доске такой же карты
                str += (pos_card1 + 1).ToString();

            if (str != "")
            {
                string message = "";

                message += "Straight";
                if (str == "54" || str == "45") message += " High Two Card";
                if (str == "35" || str == "53") message += " High One Card";
                if (str == "25" || str == "52") message += " High One Card";
                if (str == "15" || str == "51") message += " High One Card";

                if (str == "5" || str == "55") message += " High One Card";
                if (str == "4" || str == "44") message += " High One Card";
                if (str == "3" || str == "33") message += " High One Card";
                if (str == "2" || str == "22") message += " High One Card";

                if (str == "12" || str == "21") message += " Low Two Cards";
                if (str == "1" || str == "11") message += " Low One Card";
                return message;
            }
            else if (straight_arr_be[0] != 0) { return "Straight On Board"; }

            return "No Straight";
        }




        //public string If_Straight(CardClass.Card[] cards, STAGE_STATUS stage)
        //{

        //    int amount_card_on_board = 3 + (int)stage - (int)STAGE_STATUS.FLOP; //+3 карты флопа
        //    int amount_card = amount_card_on_board + 2;

        //    HashSet<CardClass.Card> unique_cards = new HashSet<CardClass.Card>(cards, new CardClass.Card.FilterByRank()); // Все значения но по одному разу
        //    unique_cards.RemoveWhere(item => item.Rank == CardClass.RANK.None); //delate cards None

        //    if (unique_cards.Count < 5) return "No Straight"; //нет стрита полюбому

        //    CardClass.Card[] straight_batch = new CardClass.Card[unique_cards.Count()];

        //    int cnt = 0; bool my_cards_be = false;
        //    foreach (CardClass.Card card in unique_cards)
        //    {
        //        if (cards[0].Rank == card.Rank) { straight_batch[cnt++] = cards[0]; my_cards_be = true; }
        //        else if (cards[1].Rank == card.Rank) { straight_batch[cnt++] = cards[1]; my_cards_be = true; }
        //        else straight_batch[cnt++] = card;
        //    }
        //    int amount_batch_5card = cnt - 5; //кол-во пачек по 5 карт
        //    Array.Sort((CardClass.Card[])straight_batch, CardClass.Card.Sort_By_Rank_Increase());


        //    bool[] straight_be = new bool[4];
        //    CardClass.Card[][] straight_arr = new CardClass.Card[4][];
        //    straight_arr[0] = new CardClass.Card[5];
        //    straight_arr[1] = new CardClass.Card[5];
        //    straight_arr[2] = new CardClass.Card[5];
        //    straight_arr[3] = new CardClass.Card[5];
        //    if (
        //        straight_batch[0].Rank == CardClass.RANK.Two &&
        //        straight_batch[1].Rank == CardClass.RANK.Three &&
        //        straight_batch[2].Rank == CardClass.RANK.Four &&
        //        straight_batch[3].Rank == CardClass.RANK.Five &&
        //        straight_batch[straight_batch.Length - 1].Rank == CardClass.RANK.Ace
        //        )
        //    {
        //        straight_arr[0][0] = straight_batch[straight_batch.Length-1];
        //        straight_arr[0][1] = straight_batch[0];
        //        straight_arr[0][2] = straight_batch[1];
        //        straight_arr[0][3] = straight_batch[2];
        //        straight_arr[0][4] = straight_batch[3];
        //        straight_be[0] = true;
        //    }

        //    for (int i = 0; i < amount_batch_5card; i++)
        //    {
        //        int sum = 0;
        //        for (int j = 0; j < 5; j++)
        //        {
        //          sum += (int)straight_batch[j + i].Rank - (int)straight_batch[i].Rank;
        //          straight_arr[i+1][j] = straight_batch[j + i];
        //        }               
        //        if (sum == 10) straight_be[i] = true;               
        //    }

        //    if (Array.IndexOf(straight_be, true) != -1 && !my_cards_be) return "Straight On Board Not My"; //стрит есть, но не мой

        //    if (Array.IndexOf(straight_be, true) != -1)//Если стрит есть
        //    {
        //        bool first_straight_flash = false;
        //        for (int i = 4 - 1; i >= 1; i--)
        //        {
        //            if (straight_be[i] == true && !first_straight_flash)
        //            {
        //                string str = "";
        //                first_straight_flash = true;
        //                for (int j = 0; j < 5; j++)
        //                {
        //                    if (cards[0].Equals(straight_arr[i][j])) str += (j + 1).ToString();
        //                    if (cards[1].Equals(straight_arr[i][j])) str += (j + 1).ToString();
        //                }
        //                if (str != ""&&
        //                    (
        //                    straight_arr[i][0].Suit == straight_arr[i][1].Suit &&
        //                    straight_arr[i][0].Suit == straight_arr[i][2].Suit &&
        //                    straight_arr[i][0].Suit == straight_arr[i][3].Suit &&
        //                    straight_arr[i][0].Suit == straight_arr[i][4].Suit
        //                    )
        //                    )
        //                {
        //                    string message = "";

        //                    message += "StraightFlush";
        //                    if (str == "45") message += " High Two Cards";
        //                    if (str == "35") message += " High One Card";
        //                    if (str == "25") message += " High One Card";
        //                    if (str == "15") message += " High One Card";
        //                    if (str == "5") message += " High One Card";

        //                    if (str == "12") message += " Low Two Cards";
        //                    if (str == "1") message += " Low One Card";
        //                    return message;
        //                }
        //            }
        //        }


        //        bool first_straight = false;
        //        for (int i = 4 - 1; i >= 1; i--)
        //        {
        //            if (straight_be[i] == true && !first_straight)
        //            {
        //                string str = "";
        //                first_straight = true;
        //                for (int j = 0; j < 5; j++)
        //                {
        //                    if (cards[0].Equals(straight_arr[i][j])) str += (j+1).ToString();
        //                    if (cards[1].Equals(straight_arr[i][j])) str += (j+1).ToString();
        //                }
        //                if (str != "")
        //                {
        //                    string message = "";

        //                    message += "Straight";
        //                    if (str == "45") message += " High Two Cards";
        //                    if (str == "35") message += " High One Card";
        //                    if (str == "25") message += " High One Card";
        //                    if (str == "15") message += " High One Card";
        //                    if (str == "5") message += " High One Card";

        //                    if (str == "12") message += " Low Two Cards";
        //                    if (str == "1") message += " Low One Card";
        //                    return message;
        //                }
        //                //когда у меня нижние карты и на доске стрит
        //                if (str == "") return "Straight On Board Not My"; //стрит есть, но не мой

        //            }
        //        }
        //    }

        //    return "No Straight";
        //}

    }
}

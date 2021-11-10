using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using StringEnumClass;
using LogicClass;

namespace My_Math
{
    public class Game
    {
        public enum GAME_STATUS
        {
            [StringValue("ERROR")] ERROR,
            [StringValue("TEMP ERROR")] TEMP_ERROR,
            [StringValue("INIT")] INIT,
            [StringValue("OK")] OK
        };

        public enum BOARD_STATUS
        {
            [StringValue("ERROR")] ERROR,
            [StringValue("TEMP ERROR")] TEMP_ERROR,
            [StringValue("INIT")] INIT,
            [StringValue("OK")] OK
        };

        public enum WARNING_STATUS
        {
            [StringValue("INIT")] INIT,
            [StringValue("OK")] OK,
            [StringValue("RECOGNIZE ERROR!!!")] RECOGNIZE_ERROR,
            [StringValue("HEADER!!!")] HEADER,
            [StringValue("GOOD ODDS!!!")] GOOD_ODDS,
            [StringValue("ACCELERATE FLOP")] ACCELERATE_FLOP,
            [StringValue("OPPONENTS IN GAME!!!")] OPPONENTS_IN_GAME
        };

        public LogicClass.STAGE_STATUS STAGE_STATUS;
        public LogicClass.STAGE_ACTION STAGE_ACTION;
        public LogicClass.FINT_STATUS FINT_STATUS;
        public LogicClass.POSITION POSITION;
        public LogicClass.PLAYER_STATUS PLAYER_STATUS;


        public LogicClass.PlayerClass[] Players { get; set; } = new LogicClass.PlayerClass[6];
        public _Board Board;
        public LogicClass.PlayerClass My;
        public LogicClass.BetsClass Bets { get; set; } = new LogicClass.BetsClass();
        public GAME_STATUS Status { get; set; } = GAME_STATUS.INIT;
        public STAGE_STATUS Stage = STAGE_STATUS.INIT;
        public STAGE_ACTION Stage_Action { get; set; } = STAGE_ACTION.INIT;
        public FINT_STATUS Fint { get; set; } = FINT_STATUS.INIT;
        
        public WARNING_STATUS Warning_Status { get; set; } = WARNING_STATUS.INIT;
        public int Opponents_Stage = 0;
        public int Opponents_Game = 0;
        public double Call_Сriterion = new double();
        public double Raise_Сriterion = new double();
        public double[] My_Odds = new double[12];
        public double[] Opponents_Odds = new double[12];
        public Game()
        {
            Players[0] = new LogicClass.PlayerClass();
            Players[1] = new LogicClass.PlayerClass();
            Players[2] = new LogicClass.PlayerClass();
            Players[3] = new LogicClass.PlayerClass();
            Players[4] = new LogicClass.PlayerClass();
            Players[5] = new LogicClass.PlayerClass();
            Board = new _Board();
            My = Players[0];
        }




        public class _Board
        {
            public CardClass.Card[] Cards;
            public BOARD_STATUS Status;
            public _Board()
            {
                Cards = new CardClass.Card[5];
                Cards[0] = new CardClass.Card();
                Cards[1] = new CardClass.Card();
                Cards[2] = new CardClass.Card();
                Cards[3] = new CardClass.Card();
                Cards[4] = new CardClass.Card();
                Status = BOARD_STATUS.INIT;
            }
        }


        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Set_From_Settings(SettingsClass.Settings sett)
        {
           Call_Сriterion = sett.Game.Call_Сriterion;
           Raise_Сriterion = sett.Game.Raise_Сriterion;
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Set_From_Logic(LogicClass.Logic logic)
        {
            Stage_Action = (STAGE_ACTION)logic.Stage_Action;
            Fint = (FINT_STATUS)logic.Fint;
            for (int i = 0; i < logic.My_Odds.Length; i++)
            {
                My_Odds[i] = logic.My_Odds[i];
                Opponents_Odds[i] = logic.Opponents_Odds[i];
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Get_To_Logic(LogicClass.Logic logic)
        {
            logic.Bets= LogicClass.BetsClass.CloneObject(Bets);

            logic.Hand_Card[0] = My.Cards[0];
            logic.Hand_Card[1] = My.Cards[1];
            logic.Hand_Card[2] = Board.Cards[0];
            logic.Hand_Card[3] = Board.Cards[1];
            logic.Hand_Card[4] = Board.Cards[2];
            logic.Hand_Card[5] = Board.Cards[3];
            logic.Hand_Card[6] = Board.Cards[4];
            logic.Opponents_Stage = Opponents_Stage;
            logic.Opponents_Game = Opponents_Game;

            logic.Call_Сriterion = Call_Сriterion;
            logic.Raise_Criterion = Raise_Сriterion;
            logic.Stage = Stage;

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                logic.Players[num_plyr] = LogicClass.PlayerClass.CloneObject(Players[num_plyr]);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /**/
        public void Warning(LogicClass.Logic logic)
        {
            if (Status == GAME_STATUS.ERROR)
            {
                Warning_Status = WARNING_STATUS.RECOGNIZE_ERROR;
            }
            else if (My_Odds[9] > 0.8)
            {
                Warning_Status = WARNING_STATUS.GOOD_ODDS;
            }
            else if (Opponents_Game < 3)
            {
                Warning_Status = WARNING_STATUS.OPPONENTS_IN_GAME;
            }           
        }
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /**/
            public void Set_From_Recognize(object o)
        {
            Recognize.Class_Table recognize = (Recognize.Class_Table)o;

            Recognize.PlayCards.Card_Struct[] board = recognize.PlayCards.Board;
            Recognize.PlayCards.Card_Struct[][] player = recognize.PlayCards.Player;
            Recognize.PlayCards.Card_Struct[] my = recognize.PlayCards.My;
            Recognize.Finance finance = recognize.Finance;

            //Set players status from cards
            for (int num_plyr = 1; num_plyr < 6; num_plyr++)
            {
                if (player[num_plyr][0].status == Recognize.RECOGNIZE_CARD_STATUS.ERROR) Players[num_plyr].Status = PLAYER_STATUS.ERROR;
                if (player[num_plyr][0].status == Recognize.RECOGNIZE_CARD_STATUS.NO) Players[num_plyr].Status = PLAYER_STATUS.FOLD;
                if (player[num_plyr][0].status == Recognize.RECOGNIZE_CARD_STATUS.CLOSE) Players[num_plyr].Status = PLAYER_STATUS.UTG;
            }
            //______________!!
            if (my[0].status == Recognize.RECOGNIZE_CARD_STATUS.ERROR) Players[0].Status = PLAYER_STATUS.ERROR;
            if (my[0].status == Recognize.RECOGNIZE_CARD_STATUS.NO) Players[0].Status = PLAYER_STATUS.FOLD;
            if (my[0].status == Recognize.RECOGNIZE_CARD_STATUS.OK) Players[0].Status = PLAYER_STATUS.UTG;

            //Set cards
            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
            {
                Players[num_plyr].Cards[0] = player[num_plyr][0].card;
                Players[num_plyr].Cards[1] = player[num_plyr][1].card;
            }


            //calculate number of the opponents participating in a game stage
            Opponents_Stage = 0;
            for (int num_plyr = 1; num_plyr < 6; num_plyr++)
                if (Players[num_plyr].Status == PLAYER_STATUS.UTG) Opponents_Stage++;

            Opponents_Game = 0;
            for (int num_plyr = 1; num_plyr < 6; num_plyr++)
                if (finance.Stacks.Player[num_plyr].Status == Recognize.RECOGNIZE_BET_STATUS.OK) Opponents_Game++;



            //Set status on a board
            int cnt = 0;
            for (int i = 0; i < 5; i++)
            {
                if (board[i].status == Recognize.RECOGNIZE_CARD_STATUS.ERROR)
                {
                    Board.Status = BOARD_STATUS.ERROR;
                    cnt = -1;
                    break;
                }
                if (board[i].status == Recognize.RECOGNIZE_CARD_STATUS.OK) cnt++;
                if (board[0].status == Recognize.RECOGNIZE_CARD_STATUS.NO) cnt = 0;
            }
            if (cnt == 0) { Stage = STAGE_STATUS.PRE_FLOP; }
            if (cnt == 3) { Stage = STAGE_STATUS.FLOP; }
            if (cnt == 4) { Stage = STAGE_STATUS.TURN; }
            if (cnt == 5) { Stage = STAGE_STATUS.RIVER; }

            for (int i = 0; i < 5; i++)
            {
                Board.Cards[i] = board[i].card;
            }

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
            {
                Bets.Player[num_plyr] = finance.Bets.Player[num_plyr].Float;
            }
            Bets.Pot = finance.Pot.Float;
            Bets.Call = finance.Call.Float;
            Bets.Raise = finance.Raise.Float;
            Bets.Bet = finance.Header.Float;

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                if (finance.Bets.Player[num_plyr].Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Bets.Status = BET_STATUS.ERROR;
            if (finance.Pot.Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Bets.Status = BET_STATUS.ERROR;
            if (finance.Call.Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Bets.Status = BET_STATUS.ERROR;
            if (finance.Raise.Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Bets.Status = BET_STATUS.ERROR;
            if (finance.Header.Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Bets.Status = BET_STATUS.ERROR;
            //POSITION_________________________________________________________________________
            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                Players[num_plyr].Position = (POSITION)recognize.Position_Players.Position[num_plyr];

            if (Stage != STAGE_STATUS.PRE_FLOP)
            {
                double curr_bet = Bets.Player[0];
                for (int i = 0; i < 6; i++)
                {
                    POSITION posit = (POSITION)((int)POSITION.SMALL_BLIND + i);
                    if (posit > POSITION.CUT_OFF) { posit = POSITION.BUTTON; }

                    for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                        if (Players[num_plyr].Status == PLAYER_STATUS.UTG && Players[num_plyr].Position == posit)
                        {
                            if (Bets.Player[num_plyr] == curr_bet)
                                Players[num_plyr].Status = PLAYER_STATUS.CALL;
                            if (Bets.Player[num_plyr] > curr_bet)
                            {
                                Players[num_plyr].Status = PLAYER_STATUS.RAISE;
                                curr_bet = Bets.Player[num_plyr];
                            }
                        }
                }
            }
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
            //CHECK ERROR
            if (recognize.My_Course == Recognize.MY_COURSE.OK && recognize.Finance.Header.Status != Recognize.RECOGNIZE_BET_STATUS.OK)
            {
                Status = GAME_STATUS.ERROR;
            }

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                if (Players[num_plyr].Status == PLAYER_STATUS.ERROR) Status = GAME_STATUS.ERROR;

            if (Board.Status == BOARD_STATUS.ERROR) Status = GAME_STATUS.ERROR;

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)
                if (Players[num_plyr].Position == POSITION.ERROR) Status = GAME_STATUS.ERROR;

            for (int num_plyr = 0; num_plyr < 6; num_plyr++)//!!!!!!!!!!! Change on Game
                if (recognize.Finance.Bets.Player[num_plyr].Status == Recognize.RECOGNIZE_BET_STATUS.ERROR) Status = GAME_STATUS.ERROR;

            if (My.Status == PLAYER_STATUS.FOLD) Status = GAME_STATUS.TEMP_ERROR;

            if (Status == GAME_STATUS.INIT) Status = GAME_STATUS.OK;


        }
    }
}

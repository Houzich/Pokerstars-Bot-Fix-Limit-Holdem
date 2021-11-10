using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using HoldemHand;

namespace MultiOddsGrid
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private string pocket = "";
        private string board = "";
        private int opponents = 1;

        /// <summary>
        /// 
        /// </summary>
        public int Opponents
        {
            get
            {
                return opponents;
            }

            set { 
                opponents = value;
                UpdateContents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Pocket
        {
            get { return pocket; }
            set { 
                pocket = value;
                UpdateContents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Board
        {
            get { return board; }
            set { 
                board = value;
                UpdateContents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string FormatPercent(double v)
        {
            if (v != 0.0)
            {
                if (v * 100.0 >= 1.0)
                    return string.Format("{0:##0.0}%", v * 100.0);
                else
                    return "<1%";
            }
            return "n/a";
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateContents()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void Display(double[] player, double[] opponent)
        {
            if (!this.DesignMode)
            {
                double playerwins = 0.0;
                double opponentwins = 0.0;
                bool montecarlo = true;

                for (int i = 0; i < 9; i++)
                {
                    switch (i)
                    {
                        case 0:
                            PlayerHighCard.Text = FormatPercent(player[i]);
                            OpponentHighCard.Text = FormatPercent(opponent[i]);
                            break;
                        case 1:
                            PlayerPair.Text = FormatPercent(player[i]);
                            OpponentPair.Text = FormatPercent(opponent[i]);
                            break;
                        case 2:
                            PlayerTwoPair.Text = FormatPercent(player[i]);
                            OpponentTwoPair.Text = FormatPercent(opponent[i]);
                            break;
                        case 3:
                            Player3ofaKind.Text = FormatPercent(player[i]);
                            Opponent3ofaKind.Text = FormatPercent(opponent[i]);
                            break;
                        case 4:
                            PlayerStraight.Text = FormatPercent(player[i]);
                            OpponentStraight.Text = FormatPercent(opponent[i]);
                            break;
                        case 5:
                            PlayerFlush.Text = FormatPercent(player[i]);
                            OpponentFlush.Text = FormatPercent(opponent[i]);
                            break;
                        case 6:
                            PlayerFullhouse.Text = FormatPercent(player[i]);
                            OpponentFullhouse.Text = FormatPercent(opponent[i]);
                            break;
                        case 7:
                            Player4ofaKind.Text = FormatPercent(player[i]);
                            Opponent4ofaKind.Text = FormatPercent(opponent[i]);
                            break;
                        case 8:
                            PlayerStraightFlush.Text = FormatPercent(player[i]);
                            OpponentStraightFlush.Text = FormatPercent(opponent[i]);
                            break;
                    }
                    playerwins += player[i] * 100.0;
                    opponentwins += opponent[i] * 100.0;
                }

                PlayerWin.Text = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", playerwins);
                OpponentWin.Text = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", opponentwins);

                //My_Win.Text = FormatPercent(player[9]);
                //Pot_Odds_Call.Text = FormatPercent(player[10]);
                //Pot_Odds_Raise.Text = FormatPercent(player[11]);
                My_Win.Text = string.Format("{0:##0.0}%", player[9] * 100.0);
                Pot_Odds_Call.Text = string.Format("{0:##0.0}%", player[10] * 100.0);
                Pot_Odds_Raise.Text = string.Format("{0:##0.0}%", player[11] * 100.0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            PlayerHighCard.Text = "";
            OpponentHighCard.Text = "";
            PlayerPair.Text = "";
            OpponentPair.Text = "";
            PlayerTwoPair.Text = "";
            OpponentTwoPair.Text = "";
            Player3ofaKind.Text = "";
            Opponent3ofaKind.Text = "";
            PlayerStraight.Text = "";
            OpponentStraight.Text = "";
            PlayerFlush.Text = "";
            OpponentFlush.Text = "";
            PlayerFullhouse.Text = "";
            OpponentFullhouse.Text = "";
            Player4ofaKind.Text = "";
            Opponent4ofaKind.Text = "";
            PlayerStraightFlush.Text = "";
            OpponentStraightFlush.Text = "";
            PlayerWin.Text = "";
            OpponentWin.Text = "";


            My_Win.Text = "";
            Pot_Odds_Call.Text = "";
            Pot_Odds_Raise.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
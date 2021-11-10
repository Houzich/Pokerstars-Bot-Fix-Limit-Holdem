using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Collections;

using StringEnumClass;

namespace Cards
{
    public class CardClass
    {
        public enum START_HAND
        {

            [StringValue("32")] start_hand_32,
            [StringValue("42")] start_hand_42,
            [StringValue("52")] start_hand_52,
            [StringValue("62")] start_hand_62,
            [StringValue("43")] start_hand_43,
            [StringValue("72")] start_hand_72,
            [StringValue("32s")] start_hand_32s,
            [StringValue("53")] start_hand_53,
            [StringValue("63")] start_hand_63,
            [StringValue("42s")] start_hand_42s,
            [StringValue("73")] start_hand_73,
            [StringValue("82")] start_hand_82,
            [StringValue("83")] start_hand_83,
            [StringValue("62s")] start_hand_62s,
            [StringValue("52s")] start_hand_52s,
            [StringValue("54")] start_hand_54,
            [StringValue("64")] start_hand_64,
            [StringValue("43s")] start_hand_43s,
            [StringValue("72s")] start_hand_72s,
            [StringValue("74")] start_hand_74,
            [StringValue("92")] start_hand_92,
            [StringValue("53s")] start_hand_53s,
            [StringValue("63s")] start_hand_63s,
            [StringValue("84")] start_hand_84,
            [StringValue("93")] start_hand_93,
            [StringValue("73s")] start_hand_73s,
            [StringValue("65")] start_hand_65,
            [StringValue("82s")] start_hand_82s,
            [StringValue("94")] start_hand_94,
            [StringValue("83s")] start_hand_83s,
            [StringValue("75")] start_hand_75,
            [StringValue("54s")] start_hand_54s,
            [StringValue("64s")] start_hand_64s,
            [StringValue("T2")] start_hand_T2,
            [StringValue("85")] start_hand_85,
            [StringValue("74s")] start_hand_74s,
            [StringValue("92s")] start_hand_92s,
            [StringValue("T3")] start_hand_T3,
            [StringValue("84s")] start_hand_84s,
            [StringValue("76")] start_hand_76,
            [StringValue("95")] start_hand_95,
            [StringValue("93s")] start_hand_93s,
            [StringValue("65s")] start_hand_65s,
            [StringValue("T4")] start_hand_T4,
            [StringValue("86")] start_hand_86,
            [StringValue("94s")] start_hand_94s,
            [StringValue("75s")] start_hand_75s,
            [StringValue("J2")] start_hand_J2,
            [StringValue("T5")] start_hand_T5,
            [StringValue("T2s")] start_hand_T2s,
            [StringValue("85s")] start_hand_85s,
            [StringValue("96")] start_hand_96,
            [StringValue("J3")] start_hand_J3,
            [StringValue("T3s")] start_hand_T3s,
            [StringValue("87")] start_hand_87,
            [StringValue("76s")] start_hand_76s,
            [StringValue("95s")] start_hand_95s,
            [StringValue("J4")] start_hand_J4,
            [StringValue("T6")] start_hand_T6,
            [StringValue("T4s")] start_hand_T4s,
            [StringValue("86s")] start_hand_86s,
            [StringValue("97")] start_hand_97,
            [StringValue("Q2")] start_hand_Q2,
            [StringValue("J5")] start_hand_J5,
            [StringValue("J2s")] start_hand_J2s,
            [StringValue("T5s")] start_hand_T5s,
            [StringValue("96s")] start_hand_96s,
            [StringValue("Q3")] start_hand_Q3,
            [StringValue("J6")] start_hand_J6,
            [StringValue("J3s")] start_hand_J3s,
            [StringValue("T7")] start_hand_T7,
            [StringValue("87s")] start_hand_87s,
            [StringValue("98")] start_hand_98,
            [StringValue("Q4")] start_hand_Q4,
            [StringValue("J4s")] start_hand_J4s,
            [StringValue("T6s")] start_hand_T6s,
            [StringValue("97s")] start_hand_97s,
            [StringValue("Q2s")] start_hand_Q2s,
            [StringValue("J7")] start_hand_J7,
            [StringValue("J5s")] start_hand_J5s,
            [StringValue("T8")] start_hand_T8,
            [StringValue("K2")] start_hand_K2,
            [StringValue("Q5")] start_hand_Q5,
            [StringValue("22")] start_hand_22,
            [StringValue("Q3s")] start_hand_Q3s,
            [StringValue("J6s")] start_hand_J6s,
            [StringValue("T7s")] start_hand_T7s,
            [StringValue("Q6")] start_hand_Q6,
            [StringValue("98s")] start_hand_98s,
            [StringValue("K3")] start_hand_K3,
            [StringValue("Q4s")] start_hand_Q4s,
            [StringValue("J8")] start_hand_J8,
            [StringValue("T9")] start_hand_T9,
            [StringValue("Q7")] start_hand_Q7,
            [StringValue("K4")] start_hand_K4,
            [StringValue("J7s")] start_hand_J7s,
            [StringValue("T8s")] start_hand_T8s,
            [StringValue("K2s")] start_hand_K2s,
            [StringValue("Q5s")] start_hand_Q5s,
            [StringValue("K5")] start_hand_K5,
            [StringValue("J9")] start_hand_J9,
            [StringValue("33")] start_hand_33,
            [StringValue("K3s")] start_hand_K3s,
            [StringValue("Q8")] start_hand_Q8,
            [StringValue("Q6s")] start_hand_Q6s,
            [StringValue("J8s")] start_hand_J8s,
            [StringValue("K6")] start_hand_K6,
            [StringValue("T9s")] start_hand_T9s,
            [StringValue("Q7s")] start_hand_Q7s,
            [StringValue("A2")] start_hand_A2,
            [StringValue("K4s")] start_hand_K4s,
            [StringValue("K7")] start_hand_K7,
            [StringValue("JT")] start_hand_JT,
            [StringValue("Q9")] start_hand_Q9,
            [StringValue("A3")] start_hand_A3,
            [StringValue("K5s")] start_hand_K5s,
            [StringValue("J9s")] start_hand_J9s,
            [StringValue("Q8s")] start_hand_Q8s,
            [StringValue("K8")] start_hand_K8,
            [StringValue("A4")] start_hand_A4,
            [StringValue("K6s")] start_hand_K6s,
            [StringValue("A2s")] start_hand_A2s,
            [StringValue("44")] start_hand_44,
            [StringValue("QT")] start_hand_QT,
            [StringValue("JTs")] start_hand_JTs,
            [StringValue("A5")] start_hand_A5,
            [StringValue("A6")] start_hand_A6,
            [StringValue("K7s")] start_hand_K7s,
            [StringValue("Q9s")] start_hand_Q9s,
            [StringValue("A3s")] start_hand_A3s,
            [StringValue("K9")] start_hand_K9,
            [StringValue("QJ")] start_hand_QJ,
            [StringValue("K8s")] start_hand_K8s,
            [StringValue("A4s")] start_hand_A4s,
            [StringValue("A7")] start_hand_A7,
            [StringValue("QTs")] start_hand_QTs,
            [StringValue("A5s")] start_hand_A5s,
            [StringValue("KT")] start_hand_KT,
            [StringValue("A6s")] start_hand_A6s,
            [StringValue("K9s")] start_hand_K9s,
            [StringValue("A8")] start_hand_A8,
            [StringValue("QJs")] start_hand_QJs,
            [StringValue("55")] start_hand_55,
            [StringValue("KJ")] start_hand_KJ,
            [StringValue("A9")] start_hand_A9,
            [StringValue("A7s")] start_hand_A7s,
            [StringValue("KQ")] start_hand_KQ,
            [StringValue("KTs")] start_hand_KTs,
            [StringValue("A8s")] start_hand_A8s,
            [StringValue("KJs")] start_hand_KJs,
            [StringValue("AT")] start_hand_AT,
            [StringValue("A9s")] start_hand_A9s,
            [StringValue("66")] start_hand_66,
            [StringValue("KQs")] start_hand_KQs,
            [StringValue("AJ")] start_hand_AJ,
            [StringValue("AQ")] start_hand_AQ,
            [StringValue("ATs")] start_hand_ATs,
            [StringValue("AK")] start_hand_AK,
            [StringValue("AJs")] start_hand_AJs,
            [StringValue("AQs")] start_hand_AQs,
            [StringValue("77")] start_hand_77,
            [StringValue("AKs")] start_hand_AKs,
            [StringValue("88")] start_hand_88,
            [StringValue("99")] start_hand_99,
            [StringValue("TT")] start_hand_TT,
            [StringValue("JJ")] start_hand_JJ,
            [StringValue("QQ")] start_hand_QQ,
            [StringValue("KK")] start_hand_KK,
            [StringValue("AA")] start_hand_AA,
        }

        public enum DECK
        {
            [StringValue("2H")] H2 = 0,
            [StringValue("3H")] H3 = 1,
            [StringValue("4H")] H4 = 2,
            [StringValue("5H")] H5 = 3,
            [StringValue("6H")] H6 = 4,
            [StringValue("7H")] H7 = 5,
            [StringValue("8H")] H8 = 6,
            [StringValue("9H")] H9 = 7,
            [StringValue("TH")] HT = 8,
            [StringValue("JH")] HJ = 9,
            [StringValue("QH")] HQ = 10,
            [StringValue("KH")] HK = 11,
            [StringValue("AH")] HA = 12,
            [StringValue("2D")] D2 = 13,
            [StringValue("3D")] D3 = 14,
            [StringValue("4D")] D4 = 15,
            [StringValue("5D")] D5 = 16,
            [StringValue("6D")] D6 = 17,
            [StringValue("7D")] D7 = 18,
            [StringValue("8D")] D8 = 19,
            [StringValue("9D")] D9 = 20,
            [StringValue("TD")] DT = 21,
            [StringValue("JD")] DJ = 22,
            [StringValue("QD")] DQ = 23,
            [StringValue("KD")] DK = 24,
            [StringValue("AD")] DA = 25,
            [StringValue("2C")] C2 = 26,
            [StringValue("3C")] C3 = 27,
            [StringValue("4C")] C4 = 28,
            [StringValue("5C")] C5 = 29,
            [StringValue("6C")] C6 = 30,
            [StringValue("7C")] C7 = 31,
            [StringValue("8C")] C8 = 32,
            [StringValue("9C")] C9 = 33,
            [StringValue("TC")] CT = 34,
            [StringValue("JC")] CJ = 35,
            [StringValue("QC")] CQ = 36,
            [StringValue("KC")] CK = 37,
            [StringValue("AC")] CA = 38,
            [StringValue("2S")] S2 = 39,
            [StringValue("3S")] S3 = 40,
            [StringValue("4S")] S4 = 41,
            [StringValue("5S")] S5 = 42,
            [StringValue("6S")] S6 = 43,
            [StringValue("7S")] S7 = 44,
            [StringValue("8S")] S8 = 45,
            [StringValue("9S")] S9 = 46,
            [StringValue("TS")] ST = 47,
            [StringValue("JS")] SJ = 48,
            [StringValue("QS")] SQ = 49,
            [StringValue("KS")] SK = 50,
            [StringValue("AS")] SA = 51
        }

        public enum SUIT
        {
            [StringValue("None")] None = 0,
            [StringValue("Hearts")] Hearts = 1, //червы
            [StringValue("Diamonds")] Diamonds = 2, //бубны
            [StringValue("Clubs")] Clubs = 3,   //трефы
            [StringValue("Spades")] Spades = 4, //пики
        }

        public enum RANK
        {
            [StringValue("None")] None = 0,
            [StringValue("A")] Ace = 14,
            [StringValue("K")] King = 13,
            [StringValue("Q")] Queen = 12,
            [StringValue("J")] Jack = 11,
            [StringValue("T")] Ten = 10,
            [StringValue("9")] Nine = 9,
            [StringValue("8")] Eight = 8,
            [StringValue("7")] Seven = 7,
            [StringValue("6")] Six = 6,
            [StringValue("5")] Five = 5,
            [StringValue("4")] Four = 4,
            [StringValue("3")] Three = 3,
            [StringValue("2")] Two = 2
        }

        public enum POKERSCORE
        {
            None, JacksOrBetter, TwoPair, ThreeOfAKind,
            Straight, Flush, FullHouse, FourOfAKind, StraightFlush,
            RoyalFlush
        }


        //IComparable 
        public class Card : IComparable
        {
            public RANK Rank { get; set; }
            public SUIT Suit { get; set; }


            public class FilterByRank : EqualityComparer<Card>
            {
                public override bool Equals(Card x, Card y)
                {
                    return (x.Rank == y.Rank);
                }

                public override int GetHashCode(Card card)
                {
                    return card.Rank.GetHashCode();
                }
            }


            public override int GetHashCode()
            {
                return this.Rank.GetHashCode();
            }

            // IComparable interface method
            public int CompareTo(object o)
            {
                Card c = (Card)o;
                if (Rank > c.Rank) return 1;
                if (Rank < c.Rank) return -1;
                else return 0;
            }

            private class sortRankDecreaseHelper : IComparer
            {
                int IComparer.Compare(object a, object b)
                {
                    Card c1 = (Card)a;
                    Card c2 = (Card)b;

                    if (c1.Rank > c2.Rank) return -1;
                    else if (c1.Rank < c2.Rank) return 1;
                    else return 0;
                }
            }

            //________________________________________________________________
            private class SortByRankIncreaseClass : IComparer
            {
                int IComparer.Compare(object a, object b)
                {
                    Card c1 = (Card)a;
                    Card c2 = (Card)b;

                    if (c1.Rank < c2.Rank) return -1;
                    else if (c1.Rank > c2.Rank) return 1;
                    else return 0;
                }
            }
            public static IComparer Sort_By_Rank_Increase()
            {
                return (IComparer)new SortByRankIncreaseClass();
            }

            private class SortBySuitRankDecreaseClass : IComparer
            {
                int IComparer.Compare(object a, object b)
                {
                    Card c1 = (Card)a;
                    Card c2 = (Card)b;

                    if (c1.Rank > c2.Rank && c1.Suit >= c2.Suit) return -1;
                    else if (c1.Rank >= c2.Rank && c1.Suit < c2.Suit) return 1;
                    else if (c1.Rank <= c2.Rank && c1.Suit > c2.Suit) return -1;
                    else if (c1.Rank < c2.Rank && c1.Suit <= c2.Suit) return 1;
                    else return 0;
                }
            }
            public static IComparer Sort_By_SuitRank_Decrease()
            {
                return (IComparer)new SortBySuitRankDecreaseClass();
            }
            private class SortBySuitRankIncreaseClass : IComparer
            {
                int IComparer.Compare(object a, object b)
                {
                    Card c1 = (Card)a;
                    Card c2 = (Card)b;

                    if (c1.Rank < c2.Rank && c1.Suit <= c2.Suit) return -1;
                    else if (c1.Rank <= c2.Rank && c1.Suit > c2.Suit) return 1;
                    else if (c1.Rank >= c2.Rank && c1.Suit < c2.Suit) return -1;
                    else if (c1.Rank > c2.Rank && c1.Suit >= c2.Suit) return 1;
                    else return 0;
                }
            }
            public static IComparer Sort_By_SuitRank_Increase()
            {
                return (IComparer)new SortBySuitRankIncreaseClass();
            }
            //________________________________________________________________

            public static IComparer sortRankDecrease()
            {
                return (IComparer)new sortRankDecreaseHelper();
            }


            public Card(string rank, string suit)
            {

                this.Rank = (RANK)StringEnum.Parse(typeof(RANK), rank, true);
                this.Suit = (SUIT)StringEnum.Parse(typeof(SUIT), suit, true);
            }

            public Card(int rank, int suit)
            {
                this.Rank = (RANK)rank;
                this.Suit = (SUIT)suit;
            }


            public Card() : this(StringEnum.GetStringValue(RANK.None), StringEnum.GetStringValue(SUIT.None))
            {
            }

            public void Clear()
            {
                this.Rank = RANK.None;
                this.Suit = SUIT.None;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Card c = (Card)obj;
                return (this.Rank == c.Rank) && (this.Suit == c.Suit);
            }


            public override string ToString()
            {
                return StringEnum.GetStringValue(this.Rank) + " " + StringEnum.GetStringValue(this.Suit);
            }

            public int CardToInt()
            {
                return ((int)this.Suit - (int)SUIT.Hearts) * 13 + ((int)this.Rank - (int)RANK.Two);
            }

            public string CardToStrDeck()
            {
                if (this.Rank == RANK.None || this.Suit == SUIT.None) { return "NN"; }
                string str = StringEnum.GetStringValue((DECK)this.CardToInt());
                return str;
            }

            static public string RankToStr(RANK rank)
            {
                return StringEnum.GetStringValue(rank);
            }

            public bool IsJacksOrBetter()
            {
                if (this.Rank >= RANK.Jack)
                    return true;
                return false;
            }

        }



        public static Card IntToCard(int i)
        {
            return new Card((int)i % 13 + (int)RANK.Two, (int)i / 13 + (int)SUIT.Hearts);
        }


        public class Deck
        {
            // array of Card of object (the real deck)
            public Card[] Card;

            public Deck()
            {
                Init();
            }

            public Deck(string str)
            {
                if (str == "Empty")
                {
                    Card = new Card[0];
                }
            }
            private void Init()
            {
                Card = new Card[52];
                int counter = 0;
                // nice way to initialize the Deck, using
                // builtin functionality of Enum
                foreach (SUIT s in Enum.GetValues(typeof(SUIT)))
                    foreach (RANK r in Enum.GetValues(typeof(RANK)))
                        if (r != RANK.None && s != SUIT.None)
                            Card[counter++] = new Card(StringEnum.GetStringValue(r), StringEnum.GetStringValue(s));
            }

            public void RemoveCard(Card c)
            {
                Card = Card.Where(item => !item.Equals(c)).ToArray();
            }

            public void AddCard(Card c)
            {
                Card[] temp = new Card[] { c };
                Card = Card.Concat(temp).ToArray();
            }

            public int Size
            {
                get { return Card.Length; }
            }

            public bool FindCard(Card c)
            {
                foreach (Card card in this.Card)
                {
                    if (card.Equals(c)) { return true; }
                }
                return false;
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsClass;
using System.Runtime.InteropServices;

namespace Table_Start_Hands
{
    public partial class Table_Start_Hands : UserControl
    {
        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock); //lock/allow paint

        private static Label[][] Label_Array { get; set; } = new Label[13][];
        public Settings.Class_Start_Hands Start_Hands { get; set; } = new Settings.Class_Start_Hands(0);
           // = new Settings.Class_Start_Hands(0);
        private static Class_Rank[] Rank_Array { get; set; } = new Class_Rank[13];


        public delegate void SaveDelegate(Table_Start_Hands sender);
        public event SaveDelegate onSave;

        private class Class_Rank
        {
            public int Int_Rank { get; set; } = 0;
            public string Str_Rank { get; set; } = "";
            public Class_Rank(int i, string str)
            {
                Int_Rank = i;
                Str_Rank = str;
            }
        }

        public Table_Start_Hands()
        {
            for (int i = 0; i < 13; i++)
            {
                Rank_Array[i] = new Class_Rank(i, "");
            }
            Rank_Array[0].Str_Rank = "2";
            Rank_Array[1].Str_Rank = "3";
            Rank_Array[2].Str_Rank = "4";
            Rank_Array[3].Str_Rank = "5";
            Rank_Array[4].Str_Rank = "6";
            Rank_Array[5].Str_Rank = "7";
            Rank_Array[6].Str_Rank = "8";
            Rank_Array[7].Str_Rank = "9";
            Rank_Array[8].Str_Rank = "T";
            Rank_Array[9].Str_Rank = "J";
            Rank_Array[10].Str_Rank = "Q";
            Rank_Array[11].Str_Rank = "K";
            Rank_Array[12].Str_Rank = "A";

            string str;
            string str_name;
            Color color;

            for (int row = 0; row < 13; row++)
            {
                Label_Array[row] = new Label[13];
                for (int column = 0; column < 13; column++)
                {
                    Label_Array[row][column] = new Label();
                    if (column > row)
                    {
                        str = "s";
                        str_name = "s";
                        color = Color.Violet;
                    }
                    else if (column < row)
                    {
                        str = "o";
                        str_name = "";
                        color = Color.LightGreen;
                    }
                    else
                    {
                        str = "";
                        str_name = "";
                        color = Color.Turquoise;
                    }

                    if (Rank_Array[12 - row].Int_Rank > Rank_Array[12 - column].Int_Rank)
                    {
                        str_name = "Label_" + Rank_Array[12 - row].Str_Rank + Rank_Array[12 - column].Str_Rank + str_name;
                        str = Rank_Array[12 - row].Str_Rank + Rank_Array[12 - column].Str_Rank + str;
                    }
                    else
                    {
                        str_name = "Label_" + Rank_Array[12 - column].Str_Rank + Rank_Array[12 - row].Str_Rank + str_name;
                        str = Rank_Array[12 - column].Str_Rank + Rank_Array[12 - row].Str_Rank + str;
                    }

                    Label lbl = Label_Array[row][column];

                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Font = new Font("Arial Narrow", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    lbl.Margin = new Padding(0);
                    lbl.Size = new Size(30, 30);
                    lbl.TabIndex = 4;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;

                    lbl.Location = new Point((lbl.Width + 1) * row, (lbl.Height + 1) * column);
                    lbl.Text = str;
                    lbl.Name = str_name;
                    lbl.Tag = color;
                    lbl.BackColor = color;

                    lbl.MouseClick += new MouseEventHandler(Cell_Click);
                    lbl.MouseDoubleClick += new MouseEventHandler(Cell_DoubleClick);
                    Controls.Add(lbl);
                }
            }
            //LockWindowUpdate(main_pnl.Handle); //lock paint  
            //LockWindowUpdate(IntPtr.Zero); //allow paint        
            InitializeComponent();

        }

        private void Table_Start_Hands_Load(object sender, EventArgs e)
        {
        }

        private void trackBar_Start_Hands_Scroll(object sender, EventArgs e)
        {
            int num = -trackBar_Start_Hands.Value;
            Start_Hands.Interval_Scroll = num;
            label_Percent_Start_Hands.Text = ((((double)num) / 169.0) * 100.0).ToString("#.#") + "%";
            textBox_Start_Hands.Text = num.ToString();
            for (int i = 0; i < 169; i++) Start_Hands.Hand[i].Enabled_Scroll = false;
            for (int i = 169 - 1; i >= 169 - num; i--) Start_Hands.Hand[i].Enabled_Scroll = true;

            foreach (Control lbl in Controls)
                if (lbl is Label) Fill_Label_Hand((Label)lbl);

        }


        private void textBox_Start_Hands_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackBar_Start_Hands.Value = -Convert.ToInt32(textBox_Start_Hands.Text);
                Start_Hands.Interval_Scroll = -Convert.ToInt32(textBox_Start_Hands.Text);
                trackBar_Start_Hands_Scroll(sender, e);
            }
            catch
            {
                MessageBox.Show("ERROR TEXT");
                textBox_Start_Hands.Text = "0";
            }
        }


        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button_SAVE_Click(object sender, EventArgs e)
        {
            onSave(this);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        public void Start()
        {
            textBox_Start_Hands.Text = Start_Hands.Interval_Scroll.ToString();
            textBox_Start_Hands_TextChanged(null, null);
            foreach (Control lbl in Controls)
                if (lbl is Label) Fill_Label_Hand((Label)lbl);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        public void Clear()
        {
            textBox_Start_Hands.Text = "0";
            trackBar_Start_Hands.Value = 0;
            foreach (Settings.Class_Start_Hands.Class_Start_Hand start_hand in Start_Hands.Hand)
                start_hand.Clear();

            foreach (Control lbl in Controls)
                if (lbl is Label) Fill_Label_Hand((Label)lbl);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        void Fill_Label_Hand(Label lbl)
        {
            foreach (Settings.Class_Start_Hands.Class_Start_Hand start_hand in Start_Hands.Hand)
            {
                if ("Label_" + start_hand.Name == lbl.Name)
                {
                    if (start_hand.Disabled_User)
                    {
                        lbl.BackColor = Color.White;
                    }
                    else if (start_hand.Enabled_User)
                    {
                        lbl.BackColor = Color.DarkRed;
                    }
                    else if (start_hand.Enabled_Scroll)
                    {
                        lbl.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        lbl.BackColor = (Color)lbl.Tag;
                    }
                }
            }
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        void Cell_Click(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            //IEnumerable<_Start_Hand> start_hand = Start_Hand.Where<_Start_Hand>(item => item.Name == (string)(lbl.Name).Replace("Label_", ""));
            for (int i = 0; i < 169; i++)
                if ("Label_" + Start_Hands.Hand[i].Name == lbl.Name)
                {
                    if (Start_Hands.Hand[i].Disabled_User == true && Start_Hands.Hand[i].Enabled_Scroll == true)
                    {
                        Start_Hands.Hand[i].Disabled_User = false;
                        Start_Hands.Hand[i].Enabled_User = false;
                    }
                    else if(Start_Hands.Hand[i].Disabled_User == true)
                    {
                        Start_Hands.Hand[i].Disabled_User = false;
                        Start_Hands.Hand[i].Enabled_User = !Start_Hands.Hand[i].Enabled_User;
                    }
                    else
                    {
                        Start_Hands.Hand[i].Enabled_User = !Start_Hands.Hand[i].Enabled_User;
                    }                   
                }
                    
            Fill_Label_Hand(lbl);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        void Cell_DoubleClick(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            //IEnumerable<_Start_Hand> start_hand = Start_Hand.Where<_Start_Hand>(item => item.Name == (string)(lbl.Name).Replace("Label_", ""));
            for (int i = 0; i < 169; i++)
                if ("Label_" + Start_Hands.Hand[i].Name == lbl.Name)
                {
                    Start_Hands.Hand[i].Disabled_User = true;
                    Start_Hands.Hand[i].Enabled_User = false;
                }
                    
            Fill_Label_Hand(lbl);
        }
        
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/

    }
}

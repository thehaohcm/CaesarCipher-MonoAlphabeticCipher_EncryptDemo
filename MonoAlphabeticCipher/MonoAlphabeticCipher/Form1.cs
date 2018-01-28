using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoAlphabeticCipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("Bạn vui lòng nhập vào chuỗi cần mã hoá","Xin nhập vào chuỗi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textBox1.Clear();
                return;
            }
            try {
                string str = textBox1.Text;
                string arr = "";
                string newstr = "";
                //string arr_stand = "";
                char[] alphabetp = new char[26];
                char[] alphabet = new char[26];
                Random rd = new Random();
                int alpha_count = 97;
                for (int i = 0; i < 26; i++)
                {
                    alphabetp[i] = alphabet[i] = Convert.ToChar(alpha_count);
                    alpha_count++;
                }
                //arr_stand = Convert.ToString(alphabetp);
                int count = 26;
                while (count > 0)
                {
                    int order = rd.Next(0, count);
                    char t = Convert.ToChar(alphabet[order]);
                    if (count > 0) {
                        for (int i = order + 1; i < count; i++)
                        {
                            alphabet[i - 1] = alphabet[i];
                        }
                    }
                    count--;
                    arr += t;
                }
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] >= 65 && str[i] <= 90)
                    {
                        newstr += (char)((int)arr[(int)(str[i] - 65)] - 32);
                    }
                    else if (str[i] >= 97 && str[i] <= 122)
                    {
                        newstr += arr[(int)(str[i] - 97)];
                    }
                    else {
                        newstr += str[i];
                    }
                }
                textBox2.Text = arr;
                Console.WriteLine("length: " + arr.Length);
                textBox3.Text = newstr;
            }catch(Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra, chương trình không thể mã hoá nôi dung", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkKey(string key)
        {
            char[] alphabet = new char[26];
            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = Convert.ToChar(97+i);
            }
            for (int i = 0; i < 26; i++)
            {
                bool flag = false;
                int j = 0;
                char t;
                for(j= 0; j < 26-i; j++)
                {
                    t = key[i];
                    if (key[i].Equals(alphabet[j]))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    return false;
                if (j >= 0&&j<26)
                {
                    for(int k= j;k<26-i-1; k++)
                    {
                        alphabet[k] = alphabet[k+1];
                    }
                }

            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length < 26 || checkKey(textBox5.Text) == false)
            {
                MessageBox.Show("Bạn vui lòng nhập dãi key dài 26 ký tự, trong đó các ký tự không được phép trùng nhau", "Vui lòng nhập lại key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string str = textBox4.Text;
            string newstr = "";
            string key = textBox5.Text;
            try {
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] >= 65 && str[i] <= 90)
                    {
                        newstr += (char)(key.IndexOf((char)(str[i] + 32)) + 97 - 32);
                    }
                    else if (str[i] >= 97 && str[i] <= 122)
                    {
                        newstr += (char)(key.IndexOf(str[i]) + 97);
                    }
                    else
                    {
                        newstr += str[i];
                    }
                }
                textBox6.Text = newstr;
            }catch(Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra, chương trình không thể giải mã được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = textBox3.Text;
            textBox5.Text = textBox2.Text;
            tabControl1.SelectedIndex = 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!textBox1.Text.Trim().Equals(""))
            {
                button1.Enabled = true;
                button3.Enabled = true;

            }
            else
            {
                button1.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void textBox1_TextAlignChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Trim().Equals("")&&!textBox5.Text.Trim().Equals(""))
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Trim().Equals("") && !textBox5.Text.Trim().Equals(""))
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' '||e.KeyChar<'a'||e.KeyChar>'z')
                e.Handled = true;

        }
    }
}

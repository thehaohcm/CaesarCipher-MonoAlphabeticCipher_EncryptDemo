using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CaesarCipherDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string encryp1t(string str, int num)
        {
            if (num == 0)
                return str;
            char[] buffer = str.ToCharArray();
            char letter;
            char let;
            int temp = num;
            for (int i = 0; i < buffer.Length; i++)
            {
                
                letter = buffer[i];
                let = letter;
                if (!letter.Equals(' ')){
                    if (let > '9')
                    {
                        if (num < 0)
                        {
                            num = 26 + num;
                        }
                        letter = (char)(letter + num);
                        if (letter > 'z')
                        {
                            letter = (char)(letter - 26);
                        }
                        else if ((letter > 'Z') && (let < 'a'))
                        {
                            letter = (char)(letter - 26);
                        }
                        num = temp;
                    }
                    else
                    {
                        if (num < 0)
                        {
                            num = 10 + num;
                            let = (char)(let + num);
                        }
                        else if (num >= 10)
                        {
                            //let = (char)(letter - num);
                            let = (char)(let + (num % 10)-1);
                        }
                        else
                        {
                            let = (char)(let + num);
                        }

                        if (let > '9')
                        {
                            let = (char)(let - 10);
                        }
                        letter = let;
                        num = temp;
                    }

                    buffer[i] = letter;
                    System.Console.WriteLine(buffer[i]);
                }
            }

            return new string(buffer);
        }

        private string encrypt(string str, int num)
        {
            if (num == 0)
                return str;
            if(num<0)
            {
                num = 26 + num;
            }
            char[] buffer = str.ToCharArray();
            char letter;
            int temp = num;
                for (int i = 0; i < buffer.Length; i++)
                {

                    letter = buffer[i];
                    if (!letter.Equals(' '))
                    {
                        int a = 0, mod = 0, size = 0, key = 0, tong = 0, du = 0, thuong = 0;
                        if (letter >= 48 && letter <= 57)
                        {
                            mod = 57;
                            size = 10 - 1;
                        }
                        else if (letter >= 65 && letter <= 90)
                        {
                            mod = 90;
                            size = 26 - 1;
                        }
                        else if (letter >= 97 && letter <= 122)
                        {
                            mod = 122;
                            size = 26 - 1;
                        }
                        key = num % (size + 1);
                        tong = letter + key;
                        du = tong % mod;
                        thuong = tong / mod;
                        if (du == 0)
                        {
                            a = tong;
                        }
                        else
                        {
                            if (thuong == 1)
                            {
                                a = mod - size + du - 1;
                            }
                            else
                            {
                                a = du;
                            }
                        }
                        letter = (char)a;
                        buffer[i] = letter;
                        //System.Console.WriteLine(buffer[i]);
                    }
                }

                return new string(buffer);
        }

        private string decrypt(string str,int num)
        {
            if (num == 0)
                return str;
            char[] buffer = str.ToCharArray();
            char letter;
            int temp = num;
                for (int i = 0; i < buffer.Length; i++)
                {

                    letter = buffer[i];
                    if (!letter.Equals(' '))
                    {
                        int a = 0, mod = 0, size = 0, key = 0, tong = 0, du = 0, thuong = 0;
                        if (letter >= 32 && letter <= 64)
                        {
                            mod = 64;
                            size = 32 - 1;
                        }
                        else if (letter >= 65 && letter <= 90)
                        {
                            mod = 90;
                            size = 26 - 1;
                        }
                        else if (letter >= 97 && letter <= 122)
                        {
                            mod = 122;
                            size = 26 - 1;
                        }
                        key = num % (size + 1);
                        tong = letter + size - key + 1;
                        du = tong % mod;
                        thuong = tong / mod;
                        if (du == 0)
                        {
                            a = tong;
                        }
                        else
                        {
                            if (thuong == 1)
                            {
                                a = mod - size + du - 1;
                            }
                            else
                            {
                                a = du;
                            }
                        }
                        letter = (char)a;
                        buffer[i] = letter;
                        //System.Console.WriteLine(buffer[i]);
                    }
                }
                return new string(buffer);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                resultTxt.Text = encrypt(inputTxt.Text, (int)numPad.Value);
            }catch(Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra, chương trình không thể mã hoá được chuỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decryptRichTxt.Clear();
            string str = inputDecryptTxt.Text;
            try
            {
                for (int i = 0; i <= 25; i++)
                { 
                    string nd = decrypt(str, i);
                    decryptRichTxt.AppendText(nd + " " + " với độ dịch: " + (int)(i) + " hoac " +(int)(i-26) + "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra, chương trình không thể phá mã tiếp được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inputTxt_TextChanged(object sender, EventArgs e)
        {
            if (!inputTxt.Text.Trim().Equals(""))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void inputTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void inputDecryptTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button2.PerformClick();
        }
    }
}

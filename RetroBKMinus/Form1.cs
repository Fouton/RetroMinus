using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RetroMinus
{
    public partial class Form1 : Form
    {
        public string romName;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.DefaultExt = "sfc";
            of.Filter = "ALTTP RANDOMIZER ROM|*.sfc";
            if (of.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.ReadWrite);
                byte[] ROM_DATA = new byte[fs.Length];
                fs.Read(ROM_DATA, 0, (int)fs.Length);
                fs.Position = 0;
                int position = 0xE96E;
                bool retroROM = (false);

                if (ROM_DATA[0x180172] == 0x01 || ROM_DATA[0x180175] == 0x01 || ROM_DATA[0x180176] == 0x0A || ROM_DATA[0x180178] == 0x32)
                    retroROM = true;
                else
                {
                    for (int i = 0; i < 168; i++)
                    {// scan all chest locations
                        if (ROM_DATA[position] == 0xAF)
                        {//if an item's content is a universal key
                            retroROM = true;//the rom is a retro ROM
                        }
                        position += 3;
                    }
                }
                if (retroROM)
                {
                    position = 0xE96E;
                    for (int i = 0; i < 168; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position += 3;
                    }
                    //see tables.asm to know what each of these following changes are
                    position = 0x180000;
                    for (int i = 0; i < 7; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position ++;
                    }
                    position = 0x180010;
                    for (int i = 0; i < 8; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position++;
                    }
                    position = 0x180028;
                    for (int i = 0; i < 2; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position += 2;
                    }
                    position = 0x289B0;
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x180140;
                    for (int i = 0; i < 11; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position++;
                    }
                    position = 0x180150;
                    for (int i = 0; i < 10; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position++;
                    }
                    position = 0x180160;
                    for (int i = 0; i < 3; i++)
                    {// for all chest locations
                        if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                        {//if an item's content is a big key
                            ROM_DATA[position] = 0xAF;//change it into a universal small key
                        }
                        position ++;
                    }
                    //this is the stuff that wasnt included in tables.asm. Im 95% sure these are item locations as theyve confirmed to swap with multiple changes I made.
                    position = 0xEDA8; //DIGGING OR CHEST
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x2DF45; //Uncle Item
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x2EB18; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x2F1FC; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x330C7; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x339CF; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x33D68; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0x33E7D; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0xEE185; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    position = 0xEE1C3; //???
                    if ((ROM_DATA[position] >= 0x92 && ROM_DATA[position] <= 0x9F) || ROM_DATA[position] == 0x32)
                    {//if an item's content is a big key
                        ROM_DATA[position] = 0xAF;//change it into a universal small key
                    }
                    
                    //making all big chests contents into small chests contents
                    position = 0xE96D;
                    for (int i = 0; i < 168; i++)
                    {//for all chest locations
                        if ((ROM_DATA[position] & 0x80) == 0x80)
                        {//if an item is a big chest item
                            ROM_DATA[position] ^= 0x80; //turn it into a regular chest item
                        }
                        position += 3;
                    }

                    //This code is required for any change below to work (dont know why)
                    position = 0xCBA2;
                    ROM_DATA[position] = 0xCD;
                    ROM_DATA[position + 1] = 0xF1;

                    //removing the lock on Zelda's cell
                    position = 0x523F5;
                    ROM_DATA[position] = 0x38;
                    ROM_DATA[position + 1] = 0xBC;

                    //removing the locks in Blind's cells
                    position = 0xFDC7F;
                    ROM_DATA[position] = 0x48;
                    ROM_DATA[position + 1] = 0xBC;
                    ROM_DATA[position + 3] = 0x78;
                    ROM_DATA[position + 4] = 0xBC;
                    ROM_DATA[position + 6] = 0xA8;
                    ROM_DATA[position + 7] = 0xBC;

                    //Eastern Door
                    position = 0x519CE;
                    ROM_DATA[position] = 0x1C;
                    //Eastern useless Door
                    position = 0x51B76;
                    ROM_DATA[position] = 0x00;
                    //Desert Door
                    position = 0xF87F9;
                    ROM_DATA[position] = 0x1C;
                    //Hera Door
                    position = 0xFCF50;
                    ROM_DATA[position] = 0x1C;
                    //PoD Door
                    position = 0xFA7C8;
                    ROM_DATA[position] = 0x1C;
                    //TT Door
                    position = 0xFD784;
                    ROM_DATA[position] = 0x1C;
                    //Ice Door
                    position = 0xFC8CB;
                    ROM_DATA[position] = 0x1C;
                    //MM Vitty Door
                    position = 0xFBA9B;
                    ROM_DATA[position] = 0x00;
                    //MM Main Door
                    position = 0xFB4AE;
                    ROM_DATA[position] = 0x1C;
                    //MM Stupid Door
                    position = 0xFB3B8;
                    ROM_DATA[position] = 0x1C;
                    //TR Trinexx Door
                    position = 0xFE808;
                    ROM_DATA[position] = 0x00;
                    //TR Main Door
                    position = 0xFE6EF;
                    ROM_DATA[position] = 0x1C;
                    //GT Main Door
                    position = 0xFF822;
                    ROM_DATA[position] = 0x1C;
                    //GT Aga2 Door
                    position = 0xFFF1A;
                    ROM_DATA[position] = 0x00;

                    //EP Big Chest
                    position = 0x51974;
                    ROM_DATA[position] = 0x7D;
                    ROM_DATA[position + 1] = 0x6E;
                    ROM_DATA[position + 2] = 0xF9;
                    //DP Big Chest
                    position = 0xF891B;
                    ROM_DATA[position] = 0x3D;
                    ROM_DATA[position + 1] = 0x3E;
                    ROM_DATA[position + 2] = 0xF9;
                    //TH Big Chest
                    position = 0xFCDC2;
                    ROM_DATA[position] = 0x7D;
                    ROM_DATA[position + 1] = 0x2A;
                    ROM_DATA[position + 2] = 0xF9;
                    //PD Big Chest
                    position = 0xFA62B;
                    ROM_DATA[position] = 0x21;
                    ROM_DATA[position + 1] = 0x82;
                    ROM_DATA[position + 2] = 0xF9;
                    //TT Big Chest
                    position = 0xFDBC1;
                    ROM_DATA[position] = 0x3D;
                    ROM_DATA[position + 1] = 0xBE;
                    ROM_DATA[position + 2] = 0xF9;
                    //SW Big Chest
                    position = 0xFBC6C;
                    ROM_DATA[position] = 0x3D;
                    ROM_DATA[position + 1] = 0xB2;
                    ROM_DATA[position + 2] = 0xF9;
                    //SP Big Chest
                    position = 0xF965B;
                    ROM_DATA[position] = 0x79;
                    ROM_DATA[position + 1] = 0x72;
                    ROM_DATA[position + 2] = 0xF9;
                    //IP Big Chest
                    position = 0xFC8A1;
                    ROM_DATA[position] = 0x1D;
                    ROM_DATA[position + 1] = 0xA6;
                    ROM_DATA[position + 2] = 0xF9;
                    //MM Big Chest
                    position = 0xFB4FA;
                    ROM_DATA[position] = 0xBD;
                    ROM_DATA[position + 1] = 0x22;
                    ROM_DATA[position + 2] = 0xF9;
                    //TR Big Chest
                    position = 0xFE6B2;
                    ROM_DATA[position] = 0xBD;
                    ROM_DATA[position + 1] = 0xB2;
                    ROM_DATA[position + 2] = 0xF9;
                    //GT Big Chest
                    position = 0xFF39E;
                    ROM_DATA[position] = 0x3D;
                    ROM_DATA[position + 1] = 0xC2;
                    ROM_DATA[position + 2] = 0xF9;
                    
                    //write changes
                    fs.Write(ROM_DATA, 0, (int)fs.Length);
                    label2.Text = "Patch Applied!";
                }
                else
                {//not a retro rom (or somehow all universal keys landed outside of chests)
                    label2.Text = "Error:\nNot a Retro ROM";
                }
                fs.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}

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
        public Byte ItemFix(Byte romData)
        {
            if ((romData >= 0x92 && romData <= 0x9F) || romData == 0x32 || romData == 0x24 || (romData >= 0x92 && romData <= 0x9F)) //turn any key into generic small keys
                return 0xAF;
            return romData;
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
                byte[,] shopData = new byte[11,8];
                int shopCount = 0;
                int takeCount = 0;
                fs.Read(ROM_DATA, 0, (int)fs.Length);
                fs.Position = 0;
                int position = 0xE96E;
                int lampCount = 0;
                bool retroROM = (false);
                int swordCave = 0x00;
                int[] costs = new int[2]; costs[0] = 0; costs[1] = 0;

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
                if (retroROM && (ROM_DATA[0x180211] & 0x10) != 0x10)
                {
                    ROM_DATA[0x180211] = Convert.ToByte(ROM_DATA[0x180211] + 0x10);// ;PC 0x180211 ;---f ridn   ;f - fouton's retrominus
                    if (checkBox1.Checked || checkBox6.Checked)
                    {
                        //This code is required for any big key changes below to work (dont know why)
                        position = 0xCBA2;
                        ROM_DATA[position] = 0xCD;
                        ROM_DATA[position + 1] = 0xF1;
                    }
                    if (checkBox6.Checked)
                    {//no big doors
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
                    }
                    if (checkBox1.Checked) //No Big Chests
                    {
                        //Rom Edits for making all big chests contents into small chests contents
                        position = 0xE96D;
                        for (int i = 0; i < 168; i++)
                        {//for all chest locations
                            if ((ROM_DATA[position] & 0x80) == 0x80)
                            {//if an item is a big chest item
                                ROM_DATA[position] ^= 0x80; //turn it into a regular chest item
                            }
                            position += 3;
                        }

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
                    }
                    if (checkBox1.Checked && checkBox6.Checked)
                    {//If remove big keys
                        position = 0xE96E;
                        for (int i = 0; i < 168; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position += 3;
                        }
                        //see tables.asm to know what each of these following changes are
                        position = 0x180000;
                        for (int i = 0; i < 7; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position++;
                        }
                        position = 0x180010;
                        for (int i = 0; i < 8; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position++;
                        }
                        position = 0x180028;
                        for (int i = 0; i < 2; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position += 2;
                        }
                        ROM_DATA[0x289B0] = ItemFix(ROM_DATA[0x289B0]);
                        position = 0x180140;
                        for (int i = 0; i < 11; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position++;
                        }
                        position = 0x180150;
                        for (int i = 0; i < 10; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position++;
                        }
                        position = 0x180160;
                        for (int i = 0; i < 3; i++)
                        {// for all chest locations
                            ROM_DATA[position] = ItemFix(ROM_DATA[position]);
                            position++;
                        }
                        //this is the stuff that wasnt included in tables.asm. Im 95% sure these are item locations as theyve confirmed to swap with multiple changes I made.
                        ROM_DATA[0xEDA8] = ItemFix(ROM_DATA[0xEDA8]);
                        ROM_DATA[0x2DF45] = ItemFix(ROM_DATA[0x2DF45]);
                        ROM_DATA[0x2EB18] = ItemFix(ROM_DATA[0x2EB18]);
                        ROM_DATA[0x2F1FC] = ItemFix(ROM_DATA[0x2F1FC]);
                        ROM_DATA[0x330C7] = ItemFix(ROM_DATA[0x330C7]);
                        ROM_DATA[0x339CF] = ItemFix(ROM_DATA[0x339CF]);
                        ROM_DATA[0x33D68] = ItemFix(ROM_DATA[0x33D68]);
                        ROM_DATA[0x33E7D] = ItemFix(ROM_DATA[0x33E7D]);
                        ROM_DATA[0xEE185] = ItemFix(ROM_DATA[0xEE185]);
                        ROM_DATA[0xEE1C3] = ItemFix(ROM_DATA[0xEE1C3]);
                    }
                    
                    //shops 
                    if ((ROM_DATA[0x180211] & 0x02) != 0x02)
                    {//if not entrance rando
                        //rearrange the shop info so that the following changes dont break anything
                        position = 0x184800;
                        for (int i = 0; i < 8; i++)
                            shopData[10, i] = 0x00;

                        for (int i=0; i < 11; i++)
                        {
                            if (i == 10 && ROM_DATA[0x184850] == 0x00)
                                break;
                            if ((ROM_DATA[position + 5] & 0x80) == 0x80)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (ROM_DATA[0x184850] == 0x00)
                                        shopData[takeCount + 5, j] = ROM_DATA[position];
                                    else
                                        shopData[takeCount + 6, j] = ROM_DATA[position];
                                    position++;
                                }
                                takeCount++;
                            }
                            else if (ROM_DATA[position + 3] == 0x57 && ROM_DATA[0x184850] != 0x00)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    shopData[5,j] = ROM_DATA[position];
                                    position++;
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    shopData[shopCount, j] = ROM_DATA[position];
                                    position++;
                                }
                                shopCount++;
                            }
                        }
                        shopData[0 ,7] = 0x00;
                        shopData[1, 7] = 0x03;
                        shopData[2, 7] = 0x06;
                        shopData[3, 7] = 0x09;
                        shopData[4, 7] = 0x0C;
                        if (ROM_DATA[0x184850] != 0x00)
                        {
                            shopData[5, 7] = 0x0F;
                            shopData[6, 7] = 0x12;
                            shopData[7, 7] = 0x13;
                            shopData[8, 7] = 0x14;
                            shopData[9, 7] = 0x15;
                            shopData[10, 7] = 0x16;
                        }
                        else
                        {
                            shopData[5, 7] = 0x0F;
                            shopData[6, 7] = 0x10;
                            shopData[7, 7] = 0x11;
                            shopData[8, 7] = 0x12;
                            shopData[9, 7] = 0x13;
                        }

                        for (int i = 0; i < 9; i++)//reorder their shop IDs
                            shopData[i, 0] = Convert.ToByte(i);
                        shopData[9, 0] = 0xFF;
                        if (ROM_DATA[0x184850] != 0x00)
                        {
                            shopData[9, 0] = 0x09;
                            shopData[10, 0] = 0xFF;
                        }
                        for (int i = 0; i < 11; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                ROM_DATA[0x184800 + j + i * 8] = shopData[i,j];
                            }
                        }
                        
                        if (checkBox2.Checked)
                        {//things that need to be done now instead of at the normal spot
                            for (int i = 0; i < 25; i++)
                            {
                                if (ROM_DATA[0x178000 + ROM_DATA[0x1780AF + i * 7]] != 0xFF)
                                {
                                    swordCave = 5 + ((ROM_DATA[0x178000 + ROM_DATA[0x1780AF + i * 7]] * 7 / 3) % 5);
                                }
                            }
                            position = 0x184828;
                            if (ROM_DATA[0x184850] != 0x00)
                            {
                                swordCave += 1;
                                position += 8;
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                if (ROM_DATA[position + 5 + i * 8] == 0x81)
                                    ROM_DATA[position + 5 + i * 8] = 0x82;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {//scan shop info to find which takeany has the sword
                                if (ROM_DATA[0x184805 + i * 8] == 0x81)
                                {
                                    swordCave = Convert.ToInt32(ROM_DATA[0x184800 + i * 8]);
                                    if (swordCave == 0xFF && ROM_DATA[0x184850] != 0x00)
                                        swordCave = 10;
                                    else if (swordCave == 0xFF && ROM_DATA[0x184850] == 0x00)
                                        swordCave = 9;
                                    break;
                                }
                            }
                        }

                        position = 0x184900;
                        for (int i = 0; i < 27; i++)
                        {
                            if (i < 24 || ROM_DATA[0x184850] != 0x00)
                                ROM_DATA[position + 3 + i * 8] = 0x00;

                        }
                        for (int i = 0; i < 5; i++)
                        {
                            //fill in the regular shop contents
                            ROM_DATA[position + i * 24] = Convert.ToByte(i); ROM_DATA[position + 8 + i * 24] = Convert.ToByte(i); ROM_DATA[position + 16 + i * 24] = Convert.ToByte(i);
                            ROM_DATA[position + 1 + i * 24] = 0x43; ROM_DATA[position + 9 + i * 24] = 0xAF; ROM_DATA[position + 17 + i * 24] = 0x31;
                            ROM_DATA[position + 2 + i * 24] = 0x50; ROM_DATA[position + 10 + i * 24] = 0x64; ROM_DATA[position + 18 + i * 24] = 0x32;
                        }
                        position = 0x184978;
                        if (ROM_DATA[0x184850] != 0x00)
                        {
                            ROM_DATA[0x184978] = 0x05; ROM_DATA[0x184980] = 0x05; ROM_DATA[0x184988] = 0x05;
                            ROM_DATA[0x184979] = 0x05; ROM_DATA[0x18497A] = 0xF4; ROM_DATA[0x18497B] = 0x01;
                            ROM_DATA[0x184981] = 0x0E; ROM_DATA[0x184982] = 0x0A;
                            ROM_DATA[0x184989] = 0x43; ROM_DATA[0x18498A] = 0x50;
                            position = 0x184990;
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            if (ROM_DATA[0x184850] != 0x00)
                            {
                                if (i+6 == swordCave)
                                {
                                    if (i + 6 == 10)
                                        ROM_DATA[position] = 0xFF;
                                    else
                                        ROM_DATA[position] = Convert.ToByte(i + 6);
                                    if (ROM_DATA[0x180041] != 0x00)
                                        ROM_DATA[position + 1] = 0x46;
                                    else
                                        ROM_DATA[position + 1] = 0x5E;
                                    ROM_DATA[position + 2] = 0x00;
                                    position += 8;
                                }
                                else
                                {
                                    if (i + 6 == 10)
                                    {
                                        ROM_DATA[position] = 0xFF; ROM_DATA[position + 8] = 0xFF;
                                    }
                                    else
                                    {
                                        ROM_DATA[position] = Convert.ToByte(i + 6); ROM_DATA[position + 8] = Convert.ToByte(i + 6);
                                    }
                                    ROM_DATA[position + 1] = 0x30; ROM_DATA[position + 9] = 0x3E;
                                    ROM_DATA[position + 2] = 0x00; ROM_DATA[position + 10] = 0x00;
                                    position += 16;
                                }
                            }
                            else
                            {
                                if (i+5 == swordCave)
                                {
                                    if (i + 5 == 10)
                                        ROM_DATA[position] = 0xFF;
                                    else
                                        ROM_DATA[position] = Convert.ToByte(i + 5);
                                    if (ROM_DATA[0x180041] != 0x00)
                                        ROM_DATA[position + 1] = 0x46;
                                    else
                                        ROM_DATA[position + 1] = 0x5E;
                                    ROM_DATA[position + 2] = 0x00;
                                    position += 8;
                                }
                                else
                                {
                                    if (i + 5 == 10)
                                    {
                                        ROM_DATA[position] = 0xFF; ROM_DATA[position + 8] = 0xFF;
                                    }
                                    else
                                    {
                                        ROM_DATA[position] = Convert.ToByte(i + 5); ROM_DATA[position + 8] = Convert.ToByte(i + 5);
                                    }
                                    ROM_DATA[position + 1] = 0x30; ROM_DATA[position + 9] = 0x3E;
                                    ROM_DATA[position + 2] = 0x00; ROM_DATA[position + 10] = 0x00;
                                    position += 16;
                                }
                            }
                        }

                        if (checkBox2.Checked)
                        {//function that sets the take anys to set locations
                            //Set some entrances to original
                            ROM_DATA[0xDBBCF] = 0x5D; ROM_DATA[0xDBBAE] = 0x3C; ROM_DATA[0xDBBD7] = 0x65; ROM_DATA[0xDBBB6] = 0x44; ROM_DATA[0xDBBB4] = 0x42;
                            ROM_DATA[0xDBBBC] = 0x4A; ROM_DATA[0xDBBC7] = 0x55; ROM_DATA[0xDBBDD] = 0x56; ROM_DATA[0xDBBED] = 0x6F; ROM_DATA[0xDBBEB] = 0x6D;
                            ROM_DATA[0xDBBDB] = 0x69; ROM_DATA[0xDBBDA] = 0x68; ROM_DATA[0xDBBEE] = 0x70; ROM_DATA[0xDBBDC] = 0x6A; ROM_DATA[0xDBBCC] = 0x5A;
                            ROM_DATA[0xDBBD8] = 0x66; ROM_DATA[0xDBBCB] = 0x59; ROM_DATA[0xDBBD4] = 0x62; ROM_DATA[0xDBBE8] = 0x6B; ROM_DATA[0xDBBD9] = 0x67;
                            ROM_DATA[0xDBBE9] = 0x71; ROM_DATA[0xDBBEA] = 0x71; ROM_DATA[0xDBBC2] = 0x50; ROM_DATA[0xDBBD0] = 0x5E; ROM_DATA[0xDBBDE] = 0x5E;
                            ROM_DATA[0xDBBE4] = 0x5E; ROM_DATA[0xDBBDF] = 0x5E; ROM_DATA[0xDBBF3] = 0x5E; ROM_DATA[0xDBBC8] = 0x5E; ROM_DATA[0xDBBE2] = 0x5E;
                            ROM_DATA[0xDBBE6] = 0x60; ROM_DATA[0xDBBCA] = 0x58; ROM_DATA[0xDBBB8] = 0x46; ROM_DATA[0xDBBBE] = 0x4C; ROM_DATA[0xDBBE0] = 0x58;
                            ROM_DATA[0xDBBE7] = 0x57; ROM_DATA[0xDBBD2] = 0x60; ROM_DATA[0xDBBC9] = 0x60; ROM_DATA[0xDBBE1] = 0x60;

                            //take anys - Desert Fairy, 20 Rup Cave, DWDM Fairy, Mire Hint, DW IceRod Hint
                            ROM_DATA[0xDBBE4] = 0x58; ROM_DATA[0xDBBED] = 0x58; ROM_DATA[0xDBBE2] = 0x58; ROM_DATA[0xDBBDC] = 0x58; ROM_DATA[0xDBBD4] = 0x58;
                            position = 0x184828;
                            if (ROM_DATA[0x184850] != 0x00)
                                position += 8;
                            ROM_DATA[0x18482D + (swordCave - 5) * 8] = 0x81;
                            ROM_DATA[position] = Convert.ToByte(5 + ((position - 0x184828) / 8)); ROM_DATA[position+1] = 0x12; ROM_DATA[position+2] = 0x01; ROM_DATA[position+3] = 0x72; ROM_DATA[position+4] = 0x00; ROM_DATA[position+6] = 0xE2; ROM_DATA[position+7] = Convert.ToByte(15 + (((position - 0x184828)*3)/8));
                            ROM_DATA[position+8] = Convert.ToByte(6 + ((position - 0x184828) / 8)); ROM_DATA[position+9] = 0x12; ROM_DATA[position+10] = 0x01; ROM_DATA[position+11] = 0x7B; ROM_DATA[position+12] = 0x00; ROM_DATA[position+14] = 0xE2; ROM_DATA[position+15] = Convert.ToByte(16 + (((position - 0x184828) * 3) / 8));
                            ROM_DATA[position+16] = Convert.ToByte(7 + ((position - 0x184828) / 8)); ROM_DATA[position + 17] = 0x12; ROM_DATA[position+18] = 0x01; ROM_DATA[position+19] = 0x70; ROM_DATA[position+20] = 0x00; ROM_DATA[position+22] = 0xE2; ROM_DATA[position+23] = Convert.ToByte(17 + (((position - 0x184828) * 3) / 8));
                            ROM_DATA[position+24] = Convert.ToByte(8 + ((position - 0x184828) / 8)); ROM_DATA[position + 25] = 0x12; ROM_DATA[position+26] = 0x01; ROM_DATA[position+27] = 0x62; ROM_DATA[position+28] = 0x00; ROM_DATA[position+30] = 0xE2; ROM_DATA[position+31] = Convert.ToByte(18 + (((position - 0x184828) * 3) / 8));
                            ROM_DATA[position+32] = 0xFF; ROM_DATA[position + 33] = 0x12; ROM_DATA[position+34] = 0x01; ROM_DATA[position+35] = 0x6A; ROM_DATA[position+36] = 0x00; ROM_DATA[position+38] = 0xE2; ROM_DATA[position+39] = Convert.ToByte(19 + (((position - 0x184828) * 3) / 8));


                        }

                        if (checkBox4.Checked)
                        {//function that sets the shops to set locations
                            if (ROM_DATA[0x184850] == 0x00)
                            {
                                for (int i = 0; i < 80; i++)
                                {//if there was no fire shield shop, add space for it
                                    ROM_DATA[0x184857 - i] = ROM_DATA[0x18484F - i];
                                }
                                for (int i = 0; i < 72; i++)
                                {//if there was no fire shield shop, add space for its contents
                                    ROM_DATA[0x1849D7 - i] = ROM_DATA[0x1849BF - i];
                                }
                                ROM_DATA[0x184830] = 0x06; ROM_DATA[0x184838] = 0x07; ROM_DATA[0x184840] = 0x08; ROM_DATA[0x184848] = 0x09;
                                position = 0x184990;
                                for (int i = 0; i < 4; i++)
                                {
                                    ROM_DATA[position] = Convert.ToByte(6 + i);
                                    if (ROM_DATA[position + 1] != 0x5E && ROM_DATA[position + 1] != 0x46)
                                    {
                                        position += 8;
                                        ROM_DATA[position] = Convert.ToByte(6 + i);
                                    }
                                    position += 8;
                                }
                                ROM_DATA[position] = 0xFF; ROM_DATA[0x1849D0] = 0xFF;
                            }
                            //shops - Lake Hylia, Paradox Cave, DW Lumberjack, Hammershop, DW Lake Hylia, Red Shield
                            ROM_DATA[0x184800] = 0x00; ROM_DATA[0x184801] = 0x12; ROM_DATA[0x184802] = 0x01; ROM_DATA[0x184803] = 0x58; ROM_DATA[0x184804] = 0x00; ROM_DATA[0x184805] = 0x03; ROM_DATA[0x184806] = 0xA0; ROM_DATA[0x184807] = 0x00;
                            ROM_DATA[0x184808] = 0x01; ROM_DATA[0x184809] = 0xFF; ROM_DATA[0x18480A] = 0x00; ROM_DATA[0x18480B] = 0x00; ROM_DATA[0x18480C] = 0x00; ROM_DATA[0x18480D] = 0x43; ROM_DATA[0x18480E] = 0xA0; ROM_DATA[0x18480F] = 0x03;
                            ROM_DATA[0x184810] = 0x02; ROM_DATA[0x184811] = 0x0F; ROM_DATA[0x184812] = 0x01; ROM_DATA[0x184813] = 0x57; ROM_DATA[0x184814] = 0x00; ROM_DATA[0x184815] = 0x03; ROM_DATA[0x184816] = 0xC1; ROM_DATA[0x184817] = 0x06;
                            ROM_DATA[0x184818] = 0x03; ROM_DATA[0x184819] = 0x0F; ROM_DATA[0x18481A] = 0x01; ROM_DATA[0x18481B] = 0x60; ROM_DATA[0x18481C] = 0x00; ROM_DATA[0x18481D] = 0x03; ROM_DATA[0x18481E] = 0xC1; ROM_DATA[0x18481F] = 0x09;
                            ROM_DATA[0x184820] = 0x04; ROM_DATA[0x184821] = 0x0F; ROM_DATA[0x184822] = 0x01; ROM_DATA[0x184823] = 0x74; ROM_DATA[0x184824] = 0x00; ROM_DATA[0x184825] = 0x03; ROM_DATA[0x184826] = 0xC1; ROM_DATA[0x184827] = 0x0C;
                            ROM_DATA[0x184828] = 0x05; ROM_DATA[0x184829] = 0x10; ROM_DATA[0x18482A] = 0x01; ROM_DATA[0x18482B] = 0x75; ROM_DATA[0x18482C] = 0x00; ROM_DATA[0x18482D] = 0x03; ROM_DATA[0x18482E] = 0xC1; ROM_DATA[0x18482F] = 0x0F;
                            //take any sram values realigned
                            ROM_DATA[0x184837] = 0x12; ROM_DATA[0x18483F] = 0x13; ROM_DATA[0x184847] = 0x14; ROM_DATA[0x18484F] = 0x15; ROM_DATA[0x184857] = 0x16;

                            position = 0x184900;
                            for (int i = 0; i < 5; i++)
                            {//fill in the regular shop contents
                                ROM_DATA[position + 1 + i * 24] = 0x43; ROM_DATA[position + 9 + i * 24] = 0xAF; ROM_DATA[position + 17 + i * 24] = 0x31;
                                ROM_DATA[position + 2 + i * 24] = 0x50; ROM_DATA[position + 10 + i * 24] = 0x64; ROM_DATA[position + 18 + i * 24] = 0x32;
                            }//red shield
                            ROM_DATA[position + 120] = 0x05; ROM_DATA[position + 128] = 0x05; ROM_DATA[position + 136] = 0x05;
                            ROM_DATA[position + 121] = 0x05; ROM_DATA[position + 129] = 0x0E; ROM_DATA[position + 137] = 0x43;
                            ROM_DATA[position + 122] = 0xF4; ROM_DATA[position + 130] = 0x0A; ROM_DATA[position + 138] = 0x50;
                            ROM_DATA[position + 123] = 0x01;

                            for (int i = 0; i < 8; i++)
                            {//set the end portion
                                ROM_DATA[0x1849D8 + i] = 0xFF;
                            }
                        }
                    }

                    if (checkBox3.Checked)
                    {//This function looks into randomizing the costs of arrows and keys. It bases itself off the RNG of the seed so that the value is always the same for each seed.
                     //If you'd like to avoid learning this as to not ruin how the RNG works, do not read into this function at all, as it isnt encrypted or anything fancy.
                        for (int i = 0; i < 255; i++)
                        {
                            if (ROM_DATA[0x178030 + i + Convert.ToInt32(ROM_DATA[0x178010]) + Convert.ToInt32(ROM_DATA[0x178203])] > 0x3B && ROM_DATA[0x178030 + i + Convert.ToInt32(ROM_DATA[0x178010]) + Convert.ToInt32(ROM_DATA[0x178203])] < 0x65)
                            {
                                costs[0] = Convert.ToInt32(ROM_DATA[0x178030 + i + Convert.ToInt32(ROM_DATA[0x178010]) + Convert.ToInt32(ROM_DATA[0x178203])]);
                                break;
                            }
                            if (i == 254)
                            {// if somehow we didnt get a value, default to be safe
                                costs[0] = 80;
                            }
                        }
                        for (int i = 0; i < 255; i++)
                        {
                            if (ROM_DATA[0x1783C0 - i - Convert.ToInt32(ROM_DATA[0x1782BA]) - Convert.ToInt32(ROM_DATA[0x17812F])] > 0x4A && ROM_DATA[0x1783C0 - i - Convert.ToInt32(ROM_DATA[0x1782BA]) - Convert.ToInt32(ROM_DATA[0x17812F])] < 0x7E)
                            {
                                costs[1] = Convert.ToInt32(ROM_DATA[0x1783C0 - i - Convert.ToInt32(ROM_DATA[0x1782BA]) - Convert.ToInt32(ROM_DATA[0x17812F])]);
                                break;
                            }
                            if (i == 254)
                            {// if somehow we didnt get a value, default to be safe
                                costs[1] = 100;
                            }
                        }
                        for (int i = 0; i < 28; i++)
                        {
                            if (ROM_DATA[0x184901 + i * 8] == 0x43)
                                ROM_DATA[0x184902 + i * 8] = Convert.ToByte(costs[0]);
                            if (ROM_DATA[0x184901 + i * 8] == 0xAF)
                                ROM_DATA[0x184902 + i * 8] = Convert.ToByte(costs[1]);
                        }
                    }

                    if (checkBox5.Checked)
                    {//function that adds the sword vs lamp option
                        for (int i = 0; i < 11; i++)
                        {//for all shop IDs
                            if (ROM_DATA[0x184805 + i * 8] == 0x81)
                            {//if its the sword take any ID, make it pick between 2 items
                                ROM_DATA[0x184805 + i * 8] = 0x82;
                                break;
                            }
                        }
                        for (int i = 0; i < 27; i++)
                        {//for all shop contents 
                            if (ROM_DATA[0x184901 + i * 8] == 0x5E || ROM_DATA[0x184901 + i * 8] == 0x46)
                            {//if its the sword item block
                                for (int j = 0; j < 0x1849DF - (0x184907 + i*8); j++)
                                {//shift everything beyond that block over by 8 bytes
                                    ROM_DATA[0x1849E7 - j] = ROM_DATA[0x1849DF - j];
                                }//add the lamp into the newly created space
                                ROM_DATA[0x184908 + i * 8] = ROM_DATA[0x184900 + i * 8];
                                ROM_DATA[0x184909 + i * 8] = 0x12;
                                ROM_DATA[0x18490A + i * 8] = 0x00;
                                ROM_DATA[0x18490B + i * 8] = 0x00;
                                ROM_DATA[0x18490C + i * 8] = 0x00;
                                ROM_DATA[0x18490D + i * 8] = 0xFF;
                                ROM_DATA[0x18490E + i * 8] = 0x00;
                                ROM_DATA[0x18490F + i * 8] = 0x00;
                            }
                        }//Count number of lamps
                        position = 0xE96E;
                        for (int i = 0; i < 168; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position += 3;
                        }
                        //see tables.asm to know what each of these following changes are
                        position = 0x180000;
                        for (int i = 0; i < 7; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position++;
                        }
                        position = 0x180010;
                        for (int i = 0; i < 8; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position++;
                        }
                        position = 0x180028;
                        for (int i = 0; i < 2; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position += 2;
                        }
                        if (ROM_DATA[0x289B0] == 0x12)
                            lampCount += 1;
                        position = 0x180140;
                        for (int i = 0; i < 11; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position++;
                        }
                        position = 0x180150;
                        for (int i = 0; i < 10; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position++;
                        }
                        position = 0x180160;
                        for (int i = 0; i < 3; i++)
                        {// for all chest locations
                            if (ROM_DATA[position] == 0x12)
                                lampCount += 1;
                            position++;
                        }
                        //this is the stuff that wasnt included in tables.asm. Im 95% sure these are item locations as theyve confirmed to swap with multiple changes I made.
                        if (ROM_DATA[0xEDA8] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x2DF45] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x2EB18] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x2F1FC] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x330C7] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x339CF] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x33D68] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0x33E7D] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0xEE185] == 0x12)
                            lampCount += 1;
                        if (ROM_DATA[0xEE1C3] == 0x12)
                            lampCount += 1;
                        //if its not easy mode
                        if (lampCount == 1)
                        {
                            if (ROM_DATA[0x180041] != 0x00)
                                ROM_DATA[0x184002] = 0x47;//become twenty rupees #2
                            else
                                ROM_DATA[0x184002] = 0x5E;//become progressive sword

                            
                        }
                        if (lampCount > 1)
                            ROM_DATA[0x184002] = 0x3E;//become boss heart
                    }
                    
                    //write changes
                    fs.Write(ROM_DATA, 0, (int)fs.Length);
                    label2.Text = "Patch Applied!";
                }
                else if (!retroROM)
                {//not a retro rom (or somehow all universal keys landed outside of chests)
                    label2.Text = "Error:\nNot a Retro ROM";
                }
                else
                {//already been retro minus'd
                    label2.Text = "Error:\nAlready a Retro Minus ROM";
                }
                fs.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}

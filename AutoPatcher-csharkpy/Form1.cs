using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SConsole;
using System.Threading;

namespace AutoPatcher_csharkpy
{
    public partial class GTAPATCHERSPEEDRUN : Form
    {
        MConsole sconsole = new MConsole();
        public GTAPATCHERSPEEDRUN()
        {
            InitializeComponent();
        }
        static string checkversion()
        {
            string vs;
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\Grand Theft Auto V");
            vs = key.GetValue("InstallFolder").ToString();
            key.Close();
            return vs;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Please Read The README It's IMPORTANT\nDo You Want To Open it Now?\nYou Can Open It With A Button In The Program\nOr Just Open It In The Folder Later","ATTENTION",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            sconsole.Init("LOG");
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("README.txt");
                sconsole.LOG("Opened README");
            }
            else
            {
                sconsole.LOG("Didn't Open README", Color.Red);
            }

            GTAPATH.Text = checkversion();
            if (GTAPATH.Text.Length != 0)
            {
                MessageBox.Show("GTA Folder Found sucessfully");
                sconsole.LOG("GTA Folder found successfully");
            }
            else
            {
                sconsole.DEBUGMSG("ERROR: No GTA Folder was Found in Registry");
            }

        }

        private void BTN_AUTOPATCHER_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Make sure the game runs and all first-time setup programs have been completed, before executing this script\n Start Patcher?","Attention",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            //If Messagebox input = yes run the autopatcher
            if(rs == DialogResult.Yes)
            {
                sconsole.LOG("AutoPatcher Started:");
                //do whatever autopatcher does :D
                if (File.Exists(GTAPATH.Text + @"\steam_api64.dll"))
                {
                    sconsole.LOG("Found Steam Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    if(File.Exists(GTAPATH.Text + @"\GTA5.exe") &&
                        File.Exists(GTAPATH.Text + @"\PlayGTAV.exe") &&
                        File.Exists(GTAPATH.Text + @"\GTAVLauncher.exe") &&
                        File.Exists(GTAPATH.Text + @"\steam_api64.dll") &&
                        File.Exists(GTAPATH.Text + @"\update\update.rpf") )
                    {
                        sconsole.LOG("sry one of these files were not found\nGTA5.exe\nPlayGTAV.exe\nGTAVLauncher.exe\nsteam_api64.dll\nupdate.rpf");
                        return;
                    }
                    sconsole.LOG("Backing Up Files");//Backing Up all important files,
                    File.Move(GTAPATH.Text + @"\GTA5.exe", s + @"\Backup\Steam\GTA5.exe");
                    File.Move(GTAPATH.Text + @"\PlayGTAV.exe", s + @"\Backup\Steam\PlayGTAV.exe");
                    File.Move(GTAPATH.Text + @"\GTAVLauncher.exe", s + @"\Backup\Steam\GTAVLauncher.exe");
                    File.Move(GTAPATH.Text + @"\steam_api64.dll", s + @"\Backup\Steam\steam_api64.dll");
                    File.Move(GTAPATH.Text + @"\update\update.rpf", s + @"\Backup\Steam\update.rpf");


                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                    sconsole.LOG("Found Steam Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    if (!File.Exists(GTAPATH.Text + @"\GTA5.exe") &&
                        !File.Exists(GTAPATH.Text + @"\PlayGTAV.exe") &&
                        !File.Exists(GTAPATH.Text + @"\GTAVLauncher.exe") &&
                        !File.Exists(GTAPATH.Text + @"\update\update.rp6"))
                    {
                        sconsole.LOG("sry one of these files were not found\nGTA5.exe\nPlayGTAV.exe\nGTAVLauncher.exe\nupdate.rpf");
                        goto test;
                    }
                    else
                    {
                        sconsole.LOG("Backing Up Files");
                        File.Move(GTAPATH.Text + @"\GTA5.exe", s + @"\Backup\Steam\GTA5.exe");
                    }



                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }
        //sconsole.LOG("Sucessfully clicked a button",Color.Red);
        test:
            MessageBox.Show("Please look at the LOG as we couldnt find a specific file");

        }

        private void RestoreBackup_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Do you really want to restore the Backup?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //If Messagebox input = yes run the autopatcher
            if (rs == DialogResult.Yes)
            {
                sconsole.LOG("Restoring Started:");
                //do whatever autopatcher does :D
                if (File.Exists(GTAPATH.Text + @"\steam_api64.dll"))
                {
                    sconsole.LOG("Found Steam Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    sconsole.LOG("Restoring Files");
                    File.Move(s + @"\Backup\Steam\GTA5.exe", GTAPATH.Text + @"\GTA5.exe");


                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    sconsole.LOG("Restoring Files");
                    File.Move(s + @"\Backup\Steam\GTA5.exe", GTAPATH.Text + @"\GTA5.exe");
                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }
        }
    }
}

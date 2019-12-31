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
            sconsole.Init("LOG");
            sconsole.DEBUGMSG("Hey Just click this console window to copy the text to your clipboard");
            DialogResult dialogResult = MessageBox.Show("Please Read The README It's IMPORTANT\nDo You Want To Open it Now?\nYou Can Open It With A Button In The Program\nOr Just Open It In The Folder Later","ATTENTION",MessageBoxButtons.YesNo,MessageBoxIcon.Error);

            if(dialogResult == DialogResult.Yes)
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
                MessageBox.Show("1");
                sconsole.LOG("GTA Folder found successfully");
            }
            else
            {
                sconsole.DEBUGMSG("ERROR: No GTA Folder was Found in Registry");
            }

        }

        private void button1_Click(object sender, EventArgs e)
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
                    File.Move(GTAPATH.Text + @"\GTA5.exe", s + @"\Backup\Steam\GTA5.exe");


                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }
            //sconsole.LOG("Sucessfully clicked a button",Color.Red);

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
                    File.Move(s + @"\Backup\Steam\GTA5.exe", GTAPATH.Text + @"\GTA5.exe");


                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }
        }
    }
}

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
        string checkversion()
        {
            string vs;
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\Grand Theft Auto V");
            vs = key.GetValue("InstallFolder").ToString();
            
            if(vs.Length == 0)
            {
                RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\GTAV");
                vs = key2.GetValue("InstallFolderSteam").ToString();
                sconsole.LOG("Found GTA Steam Folder");
                key2.Close();
                return vs;            
            }
            else
            {
                sconsole.LOG("Found GTA Rockstar Folder");
                key.Close();
                return vs;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Please Read The README It's IMPORTANT\nDo You Want To Open it Now?\nYou Can Open It With A Button In The Program\nOr Just Open It In The Folder Later","ATTENTION",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            sconsole.Init("LOG");
            sconsole.writetofile = true;
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
                MessageBox.Show("GTA Folder Found successfully");
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
                    if(!File.Exists(GTAPATH.Text + @"\GTA5.exe") ||
                        !File.Exists(GTAPATH.Text + @"\GTAVLauncher.exe") ||
                        !File.Exists(GTAPATH.Text + @"\steam_api64.dll") ||
                        !File.Exists(GTAPATH.Text + @"\update\update.rpf") )
                    {
                        sconsole.LOG("sry one of these files were not found\nGTA5.exe\nPlayGTAV.exe\nGTAVLauncher.exe\nsteam_api64.dll\nupdate.rpf");
                        MessageBox.Show("Please look at the LOG as we couldnt find a specific file");
                    }
                    else
                    {
                        sconsole.LOG("Backing Up Files");//Backing Up all important files,
                        File.Copy(GTAPATH.Text + @"\GTA5.exe", s + @"\Backup\Steam\GTA5.exe",true);
                        File.Copy(GTAPATH.Text + @"\GTAVLauncher.exe", s + @"\Backup\Steam\GTAVLauncher.exe", true);
                        File.Copy(GTAPATH.Text + @"\steam_api64.dll", s + @"\Backup\Steam\steam_api64.dll", true);
                        File.Copy(GTAPATH.Text + @"\update\update.rpf", s + @"\Backup\Steam\update.rpf", true);

                        Process p = new Process();
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\Rockstar Games Social Club");
                        key.SetValue("Version", "1.0.0.0");
                        key.Close();

                        p.StartInfo.FileName = s + @"\common\uninstall.exe";
                        p.Start();
                        while(true)
                        {
                            if(p.HasExited)
                            {
                                sconsole.LOG("Rockstar Games Launcher probably has been sucessfully uninstalled");
                                break;
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                        p.StartInfo.FileName = s + @"\Steam\Social-Club-v1.1.7.8-Setup.exe";
                        p.Start();
                        while(true)
                        {
                            if(p.HasExited)
                            {
                                sconsole.LOG("Social Club has probably been installed");
                                break;
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                        Thread.Sleep(500);
                        sconsole.LOG("STEAM: Placing 1.27 patch files into GTA Directory");
                        if (File.Exists(GTAPATH.Text + "GTA5.exe")) { File.Delete(GTAPATH.Text + "GTA5.exe"); }
                        if (File.Exists(GTAPATH.Text + "GTAVLauncher.exe")) { File.Delete(GTAPATH.Text + "GTAVLauncher.exe"); }
                        if (File.Exists(GTAPATH.Text + "steam_api64.dll")) { File.Delete(GTAPATH.Text + "steam_api64.dll"); }
                        if (File.Exists(GTAPATH.Text + @"\update\update.rpf")) { File.Delete(GTAPATH.Text + @"\update\update.rpf"); }
                        File.Copy(s + @"\Steam\GTA5.exe", GTAPATH.Text + "GTA5.exe");
                        File.Copy(s + @"\Steam\GTAVLauncher.exe", GTAPATH.Text + "GTAVLauncher.exe");
                        File.Copy(s + @"\Steam\steam_api64.dll", GTAPATH.Text + "steam_api64.dll");

                        File.Copy(s + @"\Common\update.rpf", GTAPATH.Text + @"\update\update.rpf");

                        sconsole.LOG("STEAM: Downgrade should have been successful");
                        MessageBox.Show("STEAM: Downgrade should have been successful");
                    }



                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    if (!File.Exists(GTAPATH.Text + @"\GTA5.exe") ||
                        !File.Exists(GTAPATH.Text + @"\GTAVLauncher.exe") ||
                        !File.Exists(GTAPATH.Text + @"\update\update.rpf"))
                    {
                        sconsole.LOG("sry one of these files were not found\nGTA5.exe\nPlayGTAV.exe\nGTAVLauncher.exe\nupdate.rpf");
                        MessageBox.Show("Please look at the LOG as we couldnt find a specific file\nYou should probably verify your Game!");
                    }
                    else
                    {
                        sconsole.LOG("Backing Up Files");
                        File.Copy(GTAPATH.Text + @"\GTA5.exe", s + @"\Backup\Rockstar\GTA5.exe",true);
                        Thread.Sleep(300);
                        File.Copy(GTAPATH.Text + @"\GTAVLauncher.exe", s + @"\Backup\Rockstar\GTAVLauncher.exe", true);
                        Thread.Sleep(300);
                        File.Copy(GTAPATH.Text + @"\update\update.rpf", s + @"\Backup\Rockstar\update.rpf", true);
                        Thread.Sleep(300);
                        Process p = new Process();
                        //sconsole.LOG("Uninstalling Social Club...");
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games", true);
                        key.DeleteSubKey("Rockstar Games Social Club");

                        key.OpenSubKey("Version", RegistryKeyPermissionCheck.ReadWriteSubTree);
                        key.SetValue("Version", "1.0.0.0",RegistryValueKind.String);
                        key.Close();

                        p.StartInfo.FileName = s + @"\common\uninstall.exe";
                        p.Start();
                        Thread.Sleep(1000);
                        while (!p.HasExited)
                        {
                            if (p.HasExited)
                            {
                                sconsole.LOG("Rockstar Games Launcher probably has been sucessfully uninstalled");
                                break;
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                        p.WaitForExit(500000);
                        p.StartInfo.FileName = s + @"\Rockstar\Social-Club-v1.1.6.0-Setup.exe";
                        p.Start();
                        Thread.Sleep(1000);
                        while (!p.HasExited)
                        {
                            if (p.HasExited)
                            {
                                sconsole.LOG("Social Club has probably been installed");
                                break;
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                        Thread.Sleep(500);
                        sconsole.LOG("ROCKSTAR: Placing 1.27 patch files into GTA Directory");
                        if (File.Exists(GTAPATH.Text + "GTA5.exe")) { File.Delete(GTAPATH.Text+ "GTA5.exe"); }
                        if (File.Exists(GTAPATH.Text + "GTAVLauncher.exe")){ File.Delete(GTAPATH.Text +"GTAVLauncher.exe"); }
                        if (File.Exists(GTAPATH.Text + "x64a.rpf")) { File.Delete(GTAPATH.Text+"x64a.rpf"); }
                        if (File.Exists(GTAPATH.Text + @"\update\update.rpf")) { File.Delete(GTAPATH.Text + @"\update\update.rpf"); }
                        File.Copy(s + @"\Rockstar\GTA5.exe",GTAPATH.Text +"GTA5.exe",true);
                        File.Copy(s + @"\Rockstar\GTAVLauncher.exe", GTAPATH.Text + "GTAVLauncher.exe", true);
                        File.Copy(s + @"\Rockstar\x64a.rpf", GTAPATH.Text + "x64a.rpf", true);
                        if (File.Exists(GTAPATH.Text + "GFSDK_ShadowLib.win64.dll")) { File.Delete(GTAPATH.Text + "GFSDK_ShadowLib.win64.dll"); }
                        File.Copy(s + @"\Rockstar\GFSDK_ShadowLib.win64.dll", GTAPATH.Text + "GFSDK_ShadowLib.win64.dll", true);

                        File.Copy(s + @"\Common\update.rpf", GTAPATH.Text + @"\update\update.rpf", true);

                        sconsole.LOG("ROCKSTAR: Downgrade should have been successful");
                        MessageBox.Show("ROCKSTAR: Downgrade should have been successful");
                    }

                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }

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
                    try
                    {
                        if (File.Exists(GTAPATH.Text + "GTA5.exe")) { File.Delete(GTAPATH.Text + "GTA5.exe"); }
                        if (File.Exists(GTAPATH.Text + "GTAVLauncher.exe")) { File.Delete(GTAPATH.Text + "GTAVLauncher.exe"); }
                        if (File.Exists(GTAPATH.Text + "steam_api64.dll")) { File.Delete(GTAPATH.Text + "steam_api64.dll"); }
                        if (File.Exists(GTAPATH.Text + @"\update\update.rpf")) { File.Delete(GTAPATH.Text + @"\update\update.rpf"); }
                        File.Copy(s + @"\Backup\Steam\GTA5.exe", GTAPATH.Text + @"\GTA5.exe");
                        //File.Move(s + @"\Backup\Steam\PlayGTAV.exe",GTAPATH.Text + @"\PlayGTAV.exe");
                        File.Copy(s + @"\Backup\Steam\GTAVLauncher.exe", GTAPATH.Text + @"\GTAVLauncher.exe", true);
                        File.Copy(s + @"\Backup\Steam\steam_api64.dll", GTAPATH.Text + @"\steam_api64.dll", true);
                        File.Copy(s + @"\Backup\Steam\update.rpf", GTAPATH.Text + @"\update\update.rpf", true);
                        MessageBox.Show("Everything should be sucessfully restored if not please post a github issue\n with the log of the program");
                    }
                    catch(Exception f)
                    {
                        MessageBox.Show(f.Message.ToString(), "ERROR");
                        sconsole.LOG(f.Message.ToString());
                    }
                }
                else
                {
                    sconsole.LOG("Found Rockstar Version");
                    Thread.Sleep(200);
                    string s = Directory.GetCurrentDirectory();
                    sconsole.LOG("Restoring Files");
                    try
                    {
                        if (File.Exists(GTAPATH.Text + "GTA5.exe")) { File.Delete(GTAPATH.Text + "GTA5.exe"); }
                        if (File.Exists(GTAPATH.Text + "GTAVLauncher.exe")) { File.Delete(GTAPATH.Text + "GTAVLauncher.exe"); }
                        if (File.Exists(GTAPATH.Text + "x64a.rpf")) { File.Delete(GTAPATH.Text + "x64a.rpf"); }
                        if (File.Exists(GTAPATH.Text + @"\update\update.rpf")) { File.Delete(GTAPATH.Text + @"\update\update.rpf"); }
                        File.Copy(s + @"\Backup\Rockstar\GTA5.exe", GTAPATH.Text + @"\GTA5.exe",true); 
                        File.Copy(s + @"\Backup\Rockstar\GTAVLauncher.exe", GTAPATH.Text + @"\GTAVLauncher.exe",true);
                        File.Copy(s + @"\Backup\Rockstar\update.rpf", GTAPATH.Text + @"\update\update.rpf", true);
                        MessageBox.Show("Everything should be sucessfully restored if not please post a github issue\n with the log of the program");
                    }
                    catch(Exception f)
                    {
                        MessageBox.Show(f.Message.ToString(),"ERROR");
                        sconsole.LOG(f.Message.ToString());
                    }
                }


            }
            else //if Messagebox input = no/closed do nothing and give an console error
            {
                sconsole.LOG("Messagebox Got A No Or Got Closed");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Advanced_NoClip
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 18; i++)
            {
                dataGridView1.RowCount = 18;
                dataGridView1.Rows[i].Cells[0].Value = i;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PS3.Connect(); PS3.Attach();
            RPC.Enable();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Functions.Kickgod();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Aimbot [ OFF ]")
            {
                Functions.NoRecoil();
                Functions.InfAmmo((int)numericUpDown1.Value);
                Functions.redBoxes((int)numericUpDown1.Value);
                Functions.allPerks((int)numericUpDown1.Value);
                Aimbot.Start();
                button3.Text = "Aimbot [ ON ]";
            }
            else if (button3.Text == "Aimbot [ ON ]")
            {
                Aimbot.Stop();
                button3.Text = "Aimbot [ OFF ]";
            }
        }

        private void Aimbot_Tick(object sender, EventArgs e)
        {
            if (Functions.Aimbot_and_ForgeMode.ReadFloat(0x110a5f8 + ((uint)numericUpDown1.Value * 0x3980)) > 0)
            {
                Functions.Aimbot_and_ForgeMode.Aimbot((uint)numericUpDown1.Value, Functions.Aimbot_and_ForgeMode.FindClosestEnemy((uint)numericUpDown1.Value));
                Functions.AutoShoot((int)numericUpDown1.Value);
            }
            else if (Functions.Aimbot_and_ForgeMode.ReadInt(0xFCA41D + ((uint)numericUpDown1.Value * 0x280)) == 0)
            {
                System.Threading.Thread.Sleep(3000);
                Functions.InfAmmo((int)numericUpDown1.Value);
                Functions.redBoxes((int)numericUpDown1.Value);
                Functions.allPerks((int)numericUpDown1.Value);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Advanced No-Clip [ OFF ]")
            {
                Functions.allPerks((int)numericUpDown2.Value);
                Advanced_NoClip.Start();
                button5.Text = "Advanced No-Clip [ ON ]";
                Functions.iPrintln((int)numericUpDown2.Value, "^1Advanced NoClip By ^:Kiwi_modz ^2ON");
                System.Threading.Thread.Sleep(1500);
                Functions.iPrintln((int)numericUpDown2.Value, "^1 Press And Hold [{+gostand}] To Go Up");
                System.Threading.Thread.Sleep(1500);
                Functions.iPrintln((int)numericUpDown2.Value, "^1 Press And Hold [{+melee}] To Go Down");
            }
            else if (button5.Text == "Advanced No-Clip [ ON ]")
            {
                Advanced_NoClip.Stop();
                button5.Text = "Advanced No-Clip [ OFF ]";
                Functions.iPrintln((int)numericUpDown2.Value, "^1Advanced NoClip By ^:Kiwi_modz ^1OFF");
                System.Threading.Thread.Sleep(1500);
            }
           
            
        }
        private void AdvancedNoClip_Tick(object sender, EventArgs e)
        {
            Functions.AdvancedNoClip((int)numericUpDown2.Value);

            if (Functions.Aimbot_and_ForgeMode.ReadInt(0xFCA41D + ((uint)numericUpDown2.Value * 0x280)) == 0)
            {
               System.Threading.Thread.Sleep(1500);
               Functions.allPerks((int)numericUpDown2.Value);
               Functions.iPrintln((int)numericUpDown2.Value, "^1Advanced NoClip By ^:Kiwi_modz");
            }
        }
        public static string GetName(int Client)
        {
            byte[] buffer = new byte[16];
            PS3.GetMemory(0x0110D694 + 0x3980 * (uint)Client, ref buffer);
            string names = Encoding.ASCII.GetString(buffer);
            names = names.Replace("\0", "");
            return names;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HOST.Text = Functions.ServerInfo.getHostName();
            MAP.Text = Functions.ServerInfo.getMapName();
            MODE.Text = Functions.ServerInfo.getGameMode();
            for (int i = 0; i < 18; i++)
            {
                dataGridView1.Enabled = true;
                dataGridView1.RowCount = 18;
                dataGridView1.Rows[i].Cells[0].Value = i;
                dataGridView1.Rows[i].Cells[1].Value = GetName(i);
                Application.DoEvents();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            numericUpDown1.Value = dataGridView1.CurrentRow.Index;
            numericUpDown2.Value = dataGridView1.CurrentRow.Index;
            numericUpDown3.Value = dataGridView1.CurrentRow.Index;
        }

        private void Forge_Mode_Tick(object sender, EventArgs e)
        {
            uint Enemy = Functions.Aimbot_and_ForgeMode.FindClosestEnemy((uint)numericUpDown3.Value);
            if (Functions.Aimbot_and_ForgeMode.ReadFloat(0x110a5f8 + ((uint)numericUpDown3.Value * 0x3980)) > 0)
            {
                Functions.Aimbot_and_ForgeMode.Freeze(Enemy, true);
                Functions.Aimbot_and_ForgeMode.ForgeMode((uint)numericUpDown3.Value, Enemy);
            }
            else
            {
                Functions.Aimbot_and_ForgeMode.Freeze(Enemy, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "ForgeMode [ OFF ]")
            {
                Forge_Mode.Start();
                button4.Text = "ForgeMode [ ON ]";
            }
            else if (button4.Text == "ForgeMode [ ON ]")
            {
                Forge_Mode.Stop();
                button4.Text = "ForgeMode [ OFF ]";
            }
        }
    }
}






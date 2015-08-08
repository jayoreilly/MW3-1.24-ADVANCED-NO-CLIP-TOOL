using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Advanced_NoClip
{
    class Functions
    {
        #region AllPerks
        public static void allPerks(Int32 client)
        {

            PS3.SetMemory(0x110A76D + (0x3980 * (UInt32)client), new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
        }
        #endregion
        #region InfAmmo
        public static void InfAmmo(Int32 client)
        {//Credits To BLB < for this
            byte[] PEmptyN = new byte[] { 0x0f, 0xff, 0xff, 0xff };
            byte[] PEmptyN1 = new byte[] { 0x0f, 0xff, 0xff };
            PS3.SetMemory(0x0110a6ab + (0x3980 * (UInt32)client), PEmptyN);
            PS3.SetMemory(0x0110a629 + (0x3980 * (UInt32)client), PEmptyN1);
            byte[] PEmptyN2 = new byte[] { 0x0f, 0xff, 0xff, 0xff };
            byte[] PEmptyN3 = new byte[] { 0x0f, 0xff, 0xff };
            PS3.SetMemory(0x0110a691 + (0x3980 * (UInt32)client), PEmptyN2);
            PS3.SetMemory(0x0110a619 + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a690 + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a6a8 + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a6cd + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a6c0 + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a69c + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a6c0 + (0x3980 * (UInt32)client), PEmptyN3);
            PS3.SetMemory(0x0110a6b4 + (0x3980 * (UInt32)client), PEmptyN3);
        }
        #endregion
        #region ServerInFo
        public static class ServerInfo
        {//credits to Seb5594 for this
            public static String ReturnInfos(Int32 Index)
            {

                return Encoding.ASCII.GetString(PS3.ReadBytes(0x8360d5, 0x100)).Replace(@"\", "|").Split(new char[] { '|' })[Index];

            }
            public static String getHostName()
            {
                String str = ReturnInfos(0x10);
                switch (str)
                {
                    case "Modern Warfare 3":
                        return "Dedicated Server (No Player is Host)";
                    case "":
                        return "You are not In-Game";
                }
                return str;
            }
            public static String getGameMode()
            {
                switch (ReturnInfos(2))
                {
                    case "war":
                        return "Team Deathmatch";
                    case "dm":
                        return "Free for All";
                    case "sd":
                        return "Search and Destroy";
                    case "dom":
                        return "Domination";
                    case "conf":
                        return "Kill Confirmed";
                    case "sab":
                        return "Sabotage";
                    case "koth":
                        return "Head Quartes";
                    case "ctf":
                        return "Capture The Flag";
                    case "infect":
                        return "Infected";
                    case "sotf":
                        return "Hunted";
                    case "dd":
                        return "Demolition";
                    case "grnd":
                        return "Drop Zone";
                    case "tdef":
                        return "Team Defender";
                    case "tjugg":
                        return "Team Juggernaut";
                    case "jugg":
                        return "Juggernaut";
                    case "gun":
                        return "Gun Game";
                    case "oic":
                        return "One In The Chamber";
                }
                return "Unknown Gametype";
            }
            public static String getMapName()
            {
                switch (ReturnInfos(6))
                {
                    case "mp_alpha":
                        return "Lockdown";
                    case "mp_bootleg":
                        return "Bootleg";
                    case "mp_bravo":
                        return "Mission";
                    case "mp_carbon":
                        return "Carbon";
                    case "mp_dome":
                        return "Dome";
                    case "mp_exchange":
                        return "Downturn";
                    case "mp_hardhat":
                        return "Hardhat";
                    case "mp_interchange":
                        return "Interchange";
                    case "mp_lambeth":
                        return "Fallen";
                    case "mp_mogadishu":
                        return "Bakaara";
                    case "mp_paris":
                        return "Resistance";
                    case "mp_plaza2":
                        return "Arkaden";
                    case "mp_radar":
                        return "Outpost";
                    case "mp_seatown":
                        return "Seatown";
                    case "mp_underground":
                        return "Underground";
                    case "mp_village":
                        return "Village";
                    case "mp_aground_ss":
                        return "Aground";
                    case "mp_aqueduct_ss":
                        return "Aqueduct";
                    case "mp_cement":
                        return "Foundation";
                    case "mp_hillside_ss":
                        return "Getaway";
                    case "mp_italy":
                        return "Piazza";
                    case "mp_meteora":
                        return "Sanctuary";
                    case "mp_morningwood":
                        return "Black Box";
                    case "mp_overwatch":
                        return "Overwatch";
                    case "mp_park":
                        return "Liberation";
                    case "mp_qadeem":
                        return "Oasis";
                    case "mp_restrepo_ss":
                        return "Lookout";

                    case "mp_terminal_cls":
                        return "Terminal";
                }
                return "Unknown Map";
            }
        }
        #endregion
        #region KickPlayer
        public static void KickPlayer(uint client, String Message)
        {//Credits To SC58< for this function
            RPC.Call(0x00223BD0, new Object[] { client, Message });
        }
        #endregion
        #region SV_GameSendServerCommand
        public static void SV_GameSendServerCommand(Int32 client, String command)
        {//Credits To SC58< for this function
            RPC.Call(Offsets.SV_GameSendServerCommand, new Object[] { client, 0, command });
        }
        #endregion
        #region godMode
        public static void godMode(Int32 client)
        {//Credits To INSAN3LY_D34TH For Offest
            PS3.WriteByte(0x0FCA41E + ((UInt32)client * 0x280), 0xFF);
        }
        public static void godMode1(Int32 client)
        {//Credits To Sticky INSAN3LY_D34TH For Offest
            PS3.WriteByte(0x0FCA41E + ((UInt32)client * 0x280), 0x64);
        }
        #endregion
        #region RedBoxes
        public static void redBoxes(Int32 client)
        {//Credits To INSAN3LY_D34TH For Offest
            PS3.WriteByte(0x0110a293 + ((UInt32)client * 0x3980), 0x10);
        }
        #endregion
        #region iPrintl
        public static void iPrintln(int client, string Text)
        {
            SV_GameSendServerCommand(client, "c \"" + Text + "\"");
            Thread.Sleep(20);
        }
        #endregion
        #region AutoShoot
        public static void AutoShoot(Int32 client)
        {
            if (Buttons.DetectButton(client) == Buttons.L1 + Buttons.R1)

                PS3.WriteByte(0x110a4c7 + ((UInt32)client * 0x3980), 0x01);
            //Credits to Mango_Knife for AutoShoot OffSet :)
        }
        #endregion
        #region Ghetto Advanced No CLip
        public static void IsAirborn(Int32 client)
        {//Credits To kiwi_modz
            float Velocity = PS3.ReadFloat(Offsets.Funcs.G_Client(client, 0x30));
            Velocity = 5;
            PS3.WriteFloat(Offsets.Funcs.G_Client(client, 0x30), Velocity);
        }
        public static void MoveUp(Int32 client)
        {//Credits To Sticky < for this function that was originally called "double jump"
            //He Ported this function from se7ensins.com so credits to that aswell.
            //I made Ghetto Shit with this Func to start with :)

            float Velocity = PS3.ReadFloat(Offsets.Funcs.G_Client(client, 0x30));
            Velocity += 50;
            PS3.WriteFloat(Offsets.Funcs.G_Client(client, 0x30), Velocity);
        }
        public static void MoveDown(Int32 client)
        {//Credits To kiwi_modz
            System.Threading.Thread.Sleep(20);
            float Velocity = PS3.ReadFloat(Offsets.Funcs.G_Client(client, 0x30));
            Velocity -= 550;
            PS3.WriteFloat(Offsets.Funcs.G_Client(client, 0x30), Velocity);
        }

        public static void AdvancedNoClip(Int32 client)
        {//Credits To kiwi_modz
            if (Buttons.DetectButton(client) == Buttons.X)
            {
                MoveUp(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.X + Buttons.L1)
            {
                MoveUp(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.R3)
            {
                MoveDown(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.NoButtonPressed)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.R1)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L1)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L1 + Buttons.L3)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L1 + Buttons.L3 + Buttons.R1)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L1 + Buttons.R1)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.Square)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L3)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.R3)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.L2)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.R2)
            {
                IsAirborn(client);
            }
            else if (Buttons.DetectButton(client) == Buttons.O)
            {
                IsAirborn(client);
            }
        }
        #endregion
        #region NoReCoil
        public static void NoRecoil()
        {
            PS3.SetMemory(0xBE6D0, new Byte[] { 0x60, 0x00, 0x00, 0x00 });
            //Credit to Mango_Knife for the OffSet :)
        }
        #endregion
        #region KickGod
        public static void Kickgod()
        {
            for (uint i = 0; i < 18; i++)
            {
                byte[] antigod1 = new byte[1];
                byte[] antigod2 = new byte[1];
                byte[] antigod3 = new byte[1];
                PS3.GetMemory(0x110a280 + 0x27b + (i * 0x3980), ref antigod1);
                PS3.GetMemory(0x110a280 + 0x283 + (i * 0x3980), ref antigod2);
                PS3.GetMemory(0x110a280 + 0x27f + (i * 0x3980), ref antigod3);
                if (antigod1[0] != 0x00 && antigod2[0] == 0x00 && antigod3[0] == 0x00)
                {
                    System.Threading.Thread.Sleep(1);
                    KickPlayer(i, "Kicked ^1For Using Godmode class");
                }
            }
        }
        #endregion
        #region Aimbot_and_ForgeMode
        public static class Aimbot_and_ForgeMode
        {
            //Credits to
            //xCSBKx: Making this class the Aimbot and the ForgeMode Function
            //VezahModz: AnglestoForward Function
            //iMCSx and Seb5594 Read+Write Function
            #region Read + Write
            public static int ReadInt(uint Offset)
            {
                byte[] buffer = new byte[4];
                PS3.GetMemory(Offset, ref buffer);
                Array.Reverse(buffer);
                int Value = BitConverter.ToInt32(buffer, 0);
                return Value;
            }
            public static byte ReadByte(uint Offset)
            {
                byte[] buffer = new byte[1];
                PS3.GetMemory(Offset, ref buffer);
                return buffer[0];
            }
            public static float[] ReadFloatLength(uint Offset, int Length)
            {
                byte[] buffer = new byte[Length * 4];
                PS3.GetMemory(Offset, ref buffer);
                PS3.ReverseBytes(buffer);
                float[] Array = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    Array[i] = BitConverter.ToSingle(buffer, (Length - 1 - i) * 4);
                }
                return Array;
            }
            public static void WriteFloatArray(uint Offset, float[] Array)
            {
                byte[] buffer = new byte[Array.Length * 4];
                for (int Lenght = 0; Lenght < Array.Length; Lenght++)
                {
                    PS3.ReverseBytes(BitConverter.GetBytes(Array[Lenght])).CopyTo(buffer, Lenght * 4);
                }
                PS3.SetMemory(Offset, buffer);
            }
            public static float ReadFloat(uint Offset)
            {
                byte[] buffer = new byte[4];
                PS3.GetMemory(Offset, ref buffer);
                Array.Reverse(buffer, 0, 4);
                return BitConverter.ToSingle(buffer, 0);
            }
            #endregion
            public static void Freeze(uint Client, bool State)
            {
                if (State == true)
                {
                    PS3.SetMemory(0x0110d87f + (Client * 0x3980), new byte[] { 0x04 });
                }
                else
                {
                    PS3.SetMemory(0x0110d87f + (Client * 0x3980), new byte[] { 0x00 });
                }
            }
            public static void Aimbot(uint Client, uint Target)
            {
                #region Check if Dead

                if (ReadInt(0xFCA41D + (Target * 0x280)) > 0)
                {
                    #region Stance
                    byte StanceByte = ReadByte(0x110d88a + (Target * 0x3980));
                    float Stance = 0;
                    if (StanceByte == 2)
                        Stance = 21;
                    else if (StanceByte == 1)
                        Stance = 47;
                    else
                        Stance = 0;
                    #endregion
                    #region Origin
                    float[] O1 = ReadFloatLength(0x110a29c + (Client * 0x3980), 3);
                    float[] O2 = ReadFloatLength(0x110a29c + (Target * 0x3980), 3);
                    O2[2] = O2[2] - Stance;
                    #endregion
                    #region VectoAngles
                    float[] value1 = new float[] { O2[0] - O1[0], O2[1] - O1[1], O2[2] - O1[2] };
                    float yaw, pitch;
                    float[] angles = new float[3];
                    if ((value1[1] == 0f) && (value1[0] == 0f))
                    {
                        yaw = 0f;
                        pitch = 0f;
                        if (value1[2] > 0f)
                            pitch = 90f;
                        else
                            pitch = 270f;
                    }
                    else
                    {
                        if (value1[0] != -1f)
                            yaw = (float)((Math.Atan2(value1[1], value1[0]) * 180) / Math.PI);
                        else if (value1[1] > 0f)
                            yaw = 90f;
                        else
                            yaw = 270f;
                        if (yaw < 0f)
                            yaw += 360f;
                        float forward = (float)Math.Sqrt(((value1[0] * value1[0]) + (value1[1] * value1[1])));
                        pitch = (float)((Math.Atan2(value1[2], (double)forward) * 180.0) / Math.PI);
                        if (pitch < 0f)
                            pitch += 360f;
                    }
                    angles[0] = -pitch;
                    angles[1] = yaw;
                    angles[2] = 0;
                    #endregion
                    #region SetViewAngles
                    WriteFloatArray(0x1000000, angles);
                    RPC.Call(0x1767E0, 0xFCA280 + (0x280 * Client), 0x1000000);
                    #endregion
                }
                #endregion
            }
            public static void ForgeMode(uint Client, uint Target, uint Distance_in_Meters = 6)
            {
                #region Get Angles and Origion
                float[] Angles = ReadFloatLength(0x110a3d8 + (Client * 0x3980), 2);
                float[] Origin = ReadFloatLength(0x110a29c + (Client * 0x3980), 3);
                #endregion
                #region AnglestoForward
                float diff = Distance_in_Meters * 40;
                float num = ((float)Math.Sin((Angles[0] * Math.PI) / 180)) * diff;
                float num1 = (float)Math.Sqrt(((diff * diff) - (num * num)));
                float num2 = ((float)Math.Sin((Angles[1] * Math.PI) / 180)) * num1;
                float num3 = ((float)Math.Cos((Angles[1] * Math.PI) / 180)) * num1;
                float[] forward = new float[] { Origin[0] + num3, Origin[1] + num2, Origin[2] - num };
                #endregion
                #region Set Target Origin
                Freeze(Target, true);
                WriteFloatArray(0x110a29c + (Target * 0x3980), forward);
                #endregion
            }
            public static float[] distances = new float[18];
            public static uint FindClosestEnemy(uint Attacker)
            {
                #region Check if Alive and Get Origin
                for (uint i = 0; i < 18; i++)
                {
                    if ((ReadInt(0xFCA41D + (i * 0x280)) > 0))
                    {
                        #region Attacker Origin
                        float[] O1 = ReadFloatLength(0x110a29c + ((uint)Attacker * 0x3980), 3);
                        #endregion
                        #region Client Origin
                        float[] O2 = ReadFloatLength(0x110a29c + (i * 0x3980), 3);
                        #endregion
                        #region Get Distance
                        distances[i] = (float)(Math.Sqrt(
                        ((O2[0] - O1[0]) * (O2[0] - O1[0])) +
                        ((O2[1] - O1[1]) * (O2[1] - O1[1])) +
                        ((O2[2] - O1[2]) * (O2[2] - O1[2]))
                        ));
                        #endregion
                    }
                    else
                    {
                        #region Dead Players get Max Value
                        distances[i] = float.MaxValue;
                        #endregion
                    }
                }
                #endregion
                #region Copy Distances
                float[] newDistances = new float[18];
                Array.Copy(distances, newDistances, distances.Length);
                #endregion
                #region Sort Distances and return Client
                Array.Sort(newDistances);
                // Array.Sort(distances);
                for (uint i = 0; i < 18; i++)
                {
                    if (distances[i] == newDistances[1])
                    //if (distances[i] == distances[1])
                    {
                        return i;
                    }
                }
                #endregion
                #region Failed
                int Failed = -1;
                return (uint)Failed;
                #endregion
            }
        }
    }
}
        #endregion
        

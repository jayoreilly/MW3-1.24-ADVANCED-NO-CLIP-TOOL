﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3TMAPI_NET;

namespace Advanced_NoClip
{
    class PS3
    {
        private static uint processID;
        private static uint GetProcessID()
        {
            uint[] array;
            PS3TMAPI.GetProcessList(0, out array);
            return array[0];
        }
        public static Int32 Target = 0;
        public static String GetTargetName()
        {
            if ((Parameters.ConsoleName == null) || (Parameters.ConsoleName == string.Empty))
            {
                PS3TMAPI.InitTargetComms();
                PS3TMAPI.TargetInfo targetInfo = new PS3TMAPI.TargetInfo
                {
                    Flags = PS3TMAPI.TargetInfoFlag.TargetID,
                    Target = Target
                };
                PS3TMAPI.GetTargetInfo(ref targetInfo);
                Parameters.ConsoleName = targetInfo.Name;
            }
            return Parameters.ConsoleName;
        }
        public static UInt32 ProcessID()
        {
            return Parameters.ProcessID;
        }
        public class Parameters
        {
            public static PS3TMAPI.ConnectStatus connectStatus;
            public static string ConsoleName;
            public static string info;
            public static string MemStatus;
            public static uint ProcessID;
            public static uint[] processIDs;
            public static byte[] Retour;
            public static string snresult;
            public static string Status;
            public static string usage;
        }
        public enum ResetTarget
        {
            Hard,
            Quick,
            ResetEx,
            Soft
        }
        public static Boolean Attach()
        {
            Boolean flag = false;
            PS3TMAPI.GetProcessList((Int32)Target, out Parameters.processIDs);
            if (Parameters.processIDs.Length > 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            if (flag)
            {
                ulong num = Parameters.processIDs[0];
                Parameters.ProcessID = Convert.ToUInt32(num);
                PS3TMAPI.ProcessAttach((Int32)Target, PS3TMAPI.UnitType.PPU, Parameters.ProcessID);
                PS3TMAPI.ProcessContinue((Int32)Target, Parameters.ProcessID);
                Parameters.info = "The Process 0x" + Parameters.ProcessID.ToString("X8") + " Has Been Attached !";
            }
            return flag;
        }
        public static Boolean Connect(Int32 TargetInPS3 = 0)
        {
            Boolean flag = false;
            Target = TargetInPS3;
            flag = PS3TMAPI.SUCCEEDED(PS3TMAPI.InitTargetComms());
            return PS3TMAPI.SUCCEEDED(PS3TMAPI.Connect(TargetInPS3, null));
        }
        public static void GetMemory(uint addr, ref byte[] Buffer)
        {
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0, addr, ref Buffer);
        }
        public static void SetMemory(UInt32 Address, Byte[] bytes)
        {
            PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0L, (ulong)Address, bytes);
        }
        public static Byte[] GetMem(UInt32 Address, Int32 Length)
        {
            Byte[] buff = new Byte[Length];
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0, Address, ref buff);
            return buff;
        }
        public static Byte[] SetMem(UInt32 Address, Int32 Length)
        {
            Byte[] bytes = new Byte[Length];
            PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0L, (ulong)Address, bytes);
            return bytes;
        }
        public static float ReadFloat(UInt32 offset)
        {
            byte[] myBuffer = PS3.GetMem(offset, 4);
            Array.Reverse(myBuffer, 0, 4);
            return BitConverter.ToSingle(myBuffer, 0);
        }
        public static void WriteFloat(UInt32 offset, float input)
        {
            byte[] array = new byte[4];
            BitConverter.GetBytes(input).CopyTo(array, 0);
            Array.Reverse(array, 0, 4);
            PS3.SetMemory(offset, array);
        }
       
        public static Byte ReadByte(UInt32 address)
        {
            return PS3.GetMem(address, 1)[0];
        }

        public static Byte[] ReadBytes(UInt32 address, Int32 length)
        {
            return PS3.GetMem(address, length);
        }

        public static Int32 ReadInt32(UInt32 address)
        {
            Byte[] memory = PS3.GetMem(address, 4);
            Array.Reverse(memory, 0, 4);
            return BitConverter.ToInt32(memory, 0);
        }
                             
        public static float ReadSingle(UInt32 address)
        {
            Byte[] memory = PS3.GetMem(address, 4);
            Array.Reverse(memory, 0, 4);
            return BitConverter.ToSingle(memory, 0);
        }

        public static float[] ReadSingle(UInt32 address, Int32 length)
        {
            Byte[] memory = PS3.GetMem(address, length * 4);
            ReverseBytes(memory);
            float[] numArray = new float[length];
            for (Int32 i = 0; i < length; i++)
            {
                numArray[i] = BitConverter.ToSingle(memory, ((length - 1) - i) * 4);
            }
            return numArray;
        }

        public static string ReadString(UInt32 address)
        {
            Int32 length = 40;
            Int32 num2 = 0;
            string source = "";
            do
            {
                Byte[] memory = PS3.GetMem(address + ((UInt32)num2), length);
                source = source + Encoding.UTF8.GetString(memory);
                num2 += length;
            }
            while (!source.Contains<char>('\0'));
            Int32 inPS3 = source.IndexOf('\0');
            string str2 = source.Substring(0, inPS3);
            source = string.Empty;
            return str2;
        }

        public static Byte[] ReverseBytes(Byte[] toReverse)
        {
            Array.Reverse(toReverse);
            return toReverse;
        }

        public static void WriteByte(UInt32 address, Byte input)
        {
            PS3.SetMemory(address, new Byte[] { input });
        }

        public static void WriteBytes(UInt32 address, Byte[] input)
        {
            PS3.SetMemory(address, input);
        }

        public static bool WriteBytesToggle(uint Offset, Byte[] On, Byte[] Off)
        {
            bool flag = ReadByte(Offset) == On[0];
            WriteBytes(Offset, !flag ? On : Off);
            return flag;
        }

        public static void WriteInt16(UInt32 address, short input)
        {
            Byte[] array = new Byte[2];
            ReverseBytes(BitConverter.GetBytes(input)).CopyTo(array, 0);
            PS3.SetMemory(address, array);
        }

        public static void WriteInt32(UInt32 address, Int32 input)
        {
            Byte[] array = new Byte[4];
            ReverseBytes(BitConverter.GetBytes(input)).CopyTo(array, 0);
            PS3.SetMemory(address, array);
        }
      
        public static void WriteSingle(UInt32 address, float input)
        {
            Byte[] array = new Byte[4];
            BitConverter.GetBytes(input).CopyTo(array, 0);
            Array.Reverse(array, 0, 4);
            PS3.SetMemory(address, array);
        }

        public static void WriteSingle(UInt32 address, float[] input)
        {
            Int32 length = input.Length;
            Byte[] array = new Byte[length * 4];
            for (Int32 i = 0; i < length; i++)
            {
                ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (Int32)(i * 4));
            }
            PS3.SetMemory(address, array);
        }

        public static void WriteString(UInt32 address, String input)
        {
            Byte[] Bytes = Encoding.UTF8.GetBytes(input);
            Array.Resize<byte>(ref Bytes, Bytes.Length + 1);
            PS3.SetMemory(address, Bytes);
        }

        public static void WriteUInt16(UInt32 address, ushort input)
        {
            Byte[] array = new Byte[2];
            BitConverter.GetBytes(input).CopyTo(array, 0);
            Array.Reverse(array, 0, 2);
            PS3.SetMemory(address, array);
        }
                
        public static void WriteUInt32(UInt32 address, UInt32 input)
        {
            Byte[] array = new Byte[4];
            BitConverter.GetBytes(input).CopyTo(array, 0);
            Array.Reverse(array, 0, 4);
            PS3.SetMemory(address, array);
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Advanced_NoClip
{
    public class RPC
    {
        #region Vezah's Amazing FPS RPC
        public static uint func_address = 0x0277208; //MW3 1.24
        /*
         * MW3 FPS RPC by Vezah!
         * This function get written at the FPS Offset!
         */
        #region Enable RPC
        public static void Enable()
        { // Credits to kiwi_modz For Check RPC :)
            byte[] CheckRPC = new byte[1];
            PS3.GetMemory(0x27720C, ref CheckRPC);
            if (CheckRPC[0] == 0x80)
            {
                byte[] PPC = new byte[] {0x3F,0x80,0x10,0x05,0x81,0x9C,0x00,0x48,0x2C,0x0C,0x00,0x00,0x41,0x82,0x00,0x78,
                                        0x80,0x7C,0x00,0x00,0x80,0x9C,0x00,0x04,0x80,0xBC,0x00,0x08,0x80,0xDC,0x00,0x0C,
                                        0x80,0xFC,0x00,0x10,0x81,0x1C,0x00,0x14,0x81,0x3C,0x00,0x18,0x81,0x5C,0x00,0x1C,
                                        0x81,0x7C,0x00,0x20,0xC0,0x3C,0x00,0x24,0xC0,0x5C,0x00,0x28,0xC0,0x7C,0x00,0x2C,
                                        0xC0,0x9C,0x00,0x30,0xC0,0xBC,0x00,0x34,0xC0,0xDC,0x00,0x38,0xC0,0xFC,0x00,0x3C,
                                        0xC1,0x1C,0x00,0x40,0xC1,0x3C,0x00,0x44,0x7D,0x89,0x03,0xA6,0x4E,0x80,0x04,0x21,
                                        0x38,0x80,0x00,0x00,0x90,0x9C,0x00,0x48,0x90,0x7C,0x00,0x4C,0xD0,0x3C,0x00,0x50,
                                        0x48,0x00,0x00,0x14};
                PS3.SetMemory(func_address, new byte[] { 0x41 });
                PS3.SetMemory(func_address + 4, PPC);
                PS3.SetMemory(func_address, new byte[] { 0x40 });
            }
            else if (CheckRPC[0] == 0x3F)
            {
                MessageBox.Show("RPC Is Already Enabled\nWe Don't Want You To Freeze Now Do We ", "Advanced No Clip By kiwi_modz");
            }
        }
        #endregion
        public static Int32 Call(UInt32 address, params Object[] parameters)
        {
            Int32 length = parameters.Length;
            Int32 index = 0;
            UInt32 count = 0;
            UInt32 Strings = 0;
            UInt32 Single = 0;
            UInt32 Array = 0;
            while (index < length)
            {
                if (parameters[index] is Int32)
                {
                    PS3.WriteInt32(0x10050000 + (count * 4), (Int32)parameters[index]);
                    count++;
                }
                else if (parameters[index] is UInt32)
                {
                    PS3.WriteUInt32(0x10050000 + (count * 4), (UInt32)parameters[index]);
                    count++;
                }
                else
                {
                    UInt32 pointer;
                    if (parameters[index] is String)
                    {
                        pointer = 0x10052000 + (Strings * 0x400);
                        PS3.WriteString(pointer, Convert.ToString(parameters[index]));
                        PS3.WriteUInt32(0x10050000 + (count * 4), pointer);
                        count++;
                        Strings++;
                    }
                    else if (parameters[index] is float)
                    {
                        PS3.WriteSingle(0x10050024 + (Single * 4), (Single)parameters[index]);
                        Single++;
                    }
                    else if (parameters[index] is float[])
                    {
                        Single[] Args = (Single[])parameters[index];
                        pointer = 0x10051000 + Array * 4;
                        PS3.WriteSingle(address, Args);
                        PS3.WriteUInt32(0x10050000 + count * 4, address);
                        count++;
                        Array += (UInt32)Args.Length;
                    }

                }
                index++;
            }
            PS3.WriteUInt32(0x10050048, address);
            Thread.Sleep(20);
            return PS3.ReadInt32(0x1005004c);
        }
    }
}
        #endregion
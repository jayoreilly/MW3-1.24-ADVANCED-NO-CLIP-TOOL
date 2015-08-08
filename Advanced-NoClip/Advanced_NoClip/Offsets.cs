using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_NoClip
{
    class Offsets
    {
        public static UInt32 FuncAddr = 0x277208;
        public static UInt32 G_Client = 0x110a280;
        public static UInt32 G_ClientSize = 0x3980;
        public static UInt32 G_Entity = 0xfca280;
        public static UInt32 G_EntitySize = 640;
        public static UInt32 SV_GameSendServerCommand = 0x228fa8;
        public class Funcs
        {
            public static UInt32 G_Client(Int32 clientIndex, UInt32 Mod = 0)
            {
                return ((Offsets.G_Client + Mod) + ((UInt32)(Offsets.G_ClientSize * clientIndex)));
            }

            public static UInt32 G_Entity(Int32 clientIndex, UInt32 Mod = 0)
            {
                return ((Offsets.G_Entity + Mod) + ((UInt32)(Offsets.G_EntitySize * clientIndex)));
            }
        }
    }
}
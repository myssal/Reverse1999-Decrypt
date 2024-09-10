using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace LuaDecryptor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lua lua = new Lua(args[0]);
            lua.DecryptLuabytes();
        }

        
    }
}

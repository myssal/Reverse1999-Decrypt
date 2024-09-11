using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace LuaDecryptor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lua lua = new Lua("");
            //lua.DecryptLuabytes();
            //lua.DecompileLua();
            //lua.restoreLuaName();
            //lua.checkHardCodeLuaName();
            //lua.checkHash();
            Console.WriteLine("Done decrypting and restore lua file names");
            Console.ReadLine();
        }

        
    }
}

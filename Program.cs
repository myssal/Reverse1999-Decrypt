using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace LuaDecryptor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static void LuaDecrypt(string[] args)
        {
            if (args.Length == 0)
                Console.WriteLine("Usage: LuaDecryptor.exe <Luabytes folder> [options]" +
                                  "default: decrypt luabytes, decompile lua and restore file name" +
                                  "-l: decrypte luabytes bundle" +
                                  "-r: restore file name." +
                                  "-d: decompile compiled lua to lua source code.");
            Lua lua = new Lua(args[0]);
            if (args.Length == 1)
            {
                lua.DecryptLuabytes();
                lua.DecompileLua();
                lua.RestoreLuaName();
            }
            if (args.Contains("-l")) lua.DecryptLuabytes();
            if (args.Contains("-d")) lua.DecompileLua();
            if (args.Contains("-r")) lua.RestoreLuaName();
            Console.WriteLine($"Done decrypting {lua.totalFile} lua files, with {lua.successRestore} lua files succeeded in restoring original name.");
            Console.ReadLine();
        }
    }
}

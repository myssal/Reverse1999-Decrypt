using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LuaDecryptor
{
    public class Lua
    {
        public string luabytes {  get; set; }
        public string luaCompiled { get; set; }
        
        public Lua(string luabytes) 
        {
            this.luabytes = luabytes;
            luaCompiled = "Compiled_Lua";
        }
        public void DecryptLuabytes()
        {
            var files = Directory.GetFiles(luabytes, "*.dat", SearchOption.AllDirectories);
            Console.WriteLine($"Find {files.Length} files.");
            foreach (var file in files)
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine($"Decrypting {filename}");
                var reader = new BinaryReader(File.Open(file, FileMode.Open));
                reader.ReadBytes(48);
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    var key = reader.ReadBytes(reader.ReadChar());
                    var str = Encoding.UTF8.GetString(key).ToLower();
                    var data = reader.ReadBytes(reader.ReadInt32());
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= key[i % key.Length];
                    }

                    using (var stream = new MemoryStream(data))
                    using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                    using (var decompressed = new MemoryStream())
                    {
                        gzip.CopyTo(decompressed);
                        data = decompressed.ToArray();
                    }

                    Directory.CreateDirectory(Path.Combine(luaCompiled, filename));
                    File.WriteAllBytes(Path.Combine(luaCompiled, filename, str), data);
                }
            }
        }

        public void DecompileLua()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuaData")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LuaData"));
            ProcessStartInfo info = new ProcessStartInfo("luajit-decompiler-v2.exe");
            info.Arguments = $"{luaCompiled} -s -o \"LuaData\"";
            info.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = info;
            process.Start();
            process.WaitForExit();
        }
    }
}

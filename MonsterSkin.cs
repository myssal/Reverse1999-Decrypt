using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaDecryptor
{
    public class MonsterSkin
    {
        public static void KeyNameSort()
        {
            string monster_skin = "monster_skin.txt";
            List<string> keynames = File.ReadAllLines(monster_skin).ToList();
            Dictionary<int, string> keyVal = new Dictionary<int, string>();
            for (int i = 0; i < keynames.Count; i++)
            {
                string[] content = keynames[i].Split(" ");
                keyVal.Add(int.Parse(content[1]), content[0]);
            }
            var sorted = keyVal.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            keynames.Clear();
            foreach (var key in sorted)
            {
                keynames.Add($"{key.Key} - {key.Value}");
            }
            File.WriteAllLines(monster_skin, keynames.ToArray());
        }

        public static void ParseLuaKeyNames()
        {
            string luaKeyNames = @"LuaData\modules\configs\excel2json";
            List<string> ex2JsonList = Directory.GetFiles(luaKeyNames, "*.lua*", SearchOption.AllDirectories).ToList();

        }
    }
}

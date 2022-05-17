using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace StringDumper {
    class Program {

        static string letters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789 ";
        static string extra = ",.;:-()[]{}!\'#%`\t";


        static void Main(string[] args) {
            if (args.Length == 0) DumpStrings(@"E:\Extracted\PathOfExile\3.18.Sentinel\PathOfExile.exe");
            else DumpStrings(args[0]);
        }

        static void DumpStrings(string path, int minSize = 4) {
            HashSet<char> l = new HashSet<char>(letters.ToCharArray());
            HashSet<char> e = new HashSet<char>(extra.ToCharArray());
            HashSet<string> outputStrings = new HashSet<string>();

            StringBuilder b = new StringBuilder();
            int letterCount = 0;

            

            using (BinaryReader reader = new BinaryReader(File.OpenRead(path), Encoding.ASCII)) {

                long filesize = reader.BaseStream.Length;
                long percentSize = filesize / 100;
                Console.WriteLine("--------10--------20--------30--------40--------50--------60--------70--------80--------90-------100");

                while (reader.PeekChar() != -1) {
                    char c = reader.ReadChar();
                    if (l.Contains(c)) {
                        letterCount++;
                        b.Append(c);
                    } else if (e.Contains(c)) {
                        b.Append(c);
                    } else {
                        if (b.Length >= minSize && (letterCount * 10) >= (b.Length * 6)) outputStrings.Add(b.ToString());
                        letterCount = 0;
                        b.Clear();
                    }
                    if (reader.BaseStream.Position % percentSize == 0) Console.Write("*");
                }
            }
            Console.WriteLine();
            using (TextWriter output = new StreamWriter(File.Open(Path.GetFileNameWithoutExtension(path) + ".txt", FileMode.Create))) {
                foreach (string str in outputStrings) output.WriteLine(str);
            }
        }
    }
}

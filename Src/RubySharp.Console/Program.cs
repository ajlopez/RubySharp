namespace RubySharp.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            Machine machine = new Machine();

            foreach (var arg in args)
                machine.ExecuteFile(arg);

            if (args.Length == 0)
                machine.ExecuteReader(Console.In);
        }
    }
}

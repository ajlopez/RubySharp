namespace RubySharp.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core;

    class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();

            foreach (var arg in args)
                machine.ExecuteFile(arg);
        }
    }
}

namespace RubySharp.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;

    public class Program
    {
        public static void Main(string[] args)
        {
            Machine machine = new Machine();

            foreach (var arg in args)
                machine.ExecuteFile(arg);

            if (args.Length == 0)
            {
                Parser parser = new Parser(Console.In);

                while (true)
                    try
                    {
                        IExpression expr = parser.ParseCommand();
                        var result = expr.Evaluate(machine.RootContext);
                        var text = result == null ? "nil" : result.ToString();
                        Console.WriteLine(string.Format("=> {0}", text));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
            }
        }
    }
}

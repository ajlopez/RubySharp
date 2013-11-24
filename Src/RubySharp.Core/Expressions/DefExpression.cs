namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DefExpression : IExpression
    {
        private string name;
        private IList<string> parameters;
        private IExpression command;

        public DefExpression(string name, IList<string> parameters, IExpression command)
        {
            this.name = name;
            this.parameters = parameters;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            var result = new DefinedFunction(this.command, this.parameters, context);

            if (context.Module != null)
                context.Module.SetInstanceMethod(this.name, result);
            else
                context.Self.Class.SetInstanceMethod(this.name, result);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DefExpression)
            {
                var cmd = (DefExpression)obj;

                if (this.parameters.Count != cmd.parameters.Count)
                    return false;

                for (int k = 0; k < this.parameters.Count; k++)
                    if (this.parameters[k] != cmd.parameters[k])
                        return false;

                return this.name == cmd.name && this.command.Equals(cmd.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = this.name.GetHashCode() + this.command.GetHashCode();

            foreach (var parameter in this.parameters)
                result += parameter.GetHashCode();

            return result;
        }
    }
}

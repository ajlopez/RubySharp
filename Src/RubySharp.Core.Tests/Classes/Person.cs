namespace RubySharp.Core.Tests.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public delegate int MyEvent(string n);

    public delegate int MyIntEvent();

    public class Person
    {
        public Person()
        {
        }

        public Person(string firstname, string lastname)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public event MyEvent NameEvent;

        public event MyIntEvent IntEvent;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetName()
        {
            if (this.NameEvent != null)
                this.NameEvent.Invoke(this.FirstName);

            if (this.IntEvent != null)
                this.IntEvent.Invoke();

            return this.LastName + ", " + this.FirstName;
        }
    }
}

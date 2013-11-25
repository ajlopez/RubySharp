namespace MyLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MyClass
    {
        private string name;
        private int age;

        public MyClass()
        {
        }

        public MyClass(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name { get { return this.name; } }

        public int Age { get { return this.age; } }
    }
}

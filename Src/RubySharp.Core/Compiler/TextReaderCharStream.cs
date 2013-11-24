namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TextReaderCharStream : ICharStream
    {
        TextReader reader;
        private char[] buffer = new char[1024];
        private int length;
        private int position;

        public TextReaderCharStream(TextReader reader) 
        {
            this.reader = reader;
        }

        public int NextChar()
        {
            while (this.position >= this.length)
            {
                this.length = this.reader.Read(this.buffer, 0, this.buffer.Length);

                if (this.length == 0)
                    return -1;

                this.position = 0;
            }

            return this.buffer[this.position++];
        }

        public void BackChar()
        {
            if (this.position > 0 && this.position <= this.length)
                this.position--;
        }
    }
}


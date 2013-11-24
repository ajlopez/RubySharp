namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public class Range : IEnumerable<int>
    {
        private int from;
        private int to;

        public Range(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new RangeEnumerator(this.from, this.to);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RangeEnumerator(this.from, this.to);
        }

        public override string ToString()
        {
            return string.Format("{0}..{1}", this.from, this.to);
        }

        private class RangeEnumerator : IEnumerator<int>
        {
            private int from;
            private int to;
            private int current;

            public RangeEnumerator(int from, int to)
            {
                this.from = from;
                this.to = to;
                this.current = from - 1;
            }

            public object Current
            {
                get { return this.current; }
            }

            public bool MoveNext()
            {
                this.current++;

                return this.current >= this.from && this.current <= this.to;
            }

            public void Reset()
            {
                this.current = this.from - 1;
            }

            int IEnumerator<int>.Current
            {
                get { return this.current; }
            }

            public void Dispose()
            {
            }
        }
    }
}

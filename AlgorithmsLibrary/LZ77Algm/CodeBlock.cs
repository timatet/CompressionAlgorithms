namespace AlgorithmsLibrary
{
    public class CodeBlock
    {
        public int offset { get; private set; }
        public int length { get; private set; }
        public char next { get; private set; }

        public CodeBlock(int offset, int length, char next)
        {
            this.offset = offset;
            this.length = length;
            this.next = next;
        }
    }
}

namespace AzDoc.Indexing.Expressions
{
    public abstract class BinaryExpression : Expression
    {
        private readonly string _left;
        private readonly string _right;

        public BinaryExpression(string left, string right)
        {
            _left = left;
            _right = right;
        }

        public string Right
        {
            get { return _right; }
        }

        public string Left
        {
            get { return _left; }
        }
    }
}

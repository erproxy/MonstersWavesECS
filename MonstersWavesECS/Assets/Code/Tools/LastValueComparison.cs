namespace Code.Tools
{
    public class LastValueComparison <T>
    {
        public T Value { get; private set; }

        public LastValueComparison(T initializeValue)
        {
            Value = initializeValue;
        }

        public bool IsNotEqualsLastValue(T value)
        {
            if (Value.Equals(value)) return false;
            
            Value = value;
            return true;
        }
    }
}
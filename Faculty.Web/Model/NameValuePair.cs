namespace Faculty.Web.Model
{
    public class NameValuePair<TValue>
    {
        public string Name { get; set; }
        public TValue Value { get; set; }

        public NameValuePair(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        public NameValuePair(TValue value) : this(null, value)
        { }

        public NameValuePair() : this(null, default)
        { }
    }
}
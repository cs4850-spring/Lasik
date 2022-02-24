namespace generation.Java.Nodes.Statements
{
    public abstract class Statement : Node
    {
         [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }

        [JsonProperty("selector")]
        public Selector Selector { get; set; }

        [JsonProperty("comment")]
        public Comment Comment { get; set; }


    }
}
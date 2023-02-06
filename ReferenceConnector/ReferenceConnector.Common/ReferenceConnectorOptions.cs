namespace ReferenceConnector.Common
{
    public class ReferenceConnectorOptions
    {
        public const string SectionName = "ReferenceConnector";

        public int NewChannelLikelihood { get; set; } = 1000;
        public int RemoveChannelLikelihood { get; set; } = 10000;
        public int NewRecordLikelihood { get; set; } = 5;
        public int NewAggregationLikelihood { get; set; } = 20;
        public int MaxNewRecords { get; set; } = 5;
        public int NewRecordVersionLikelihood { get; set; } = 20;

        public List<string> RecordAuthors { get; set; } = new()
        {
            "Alexandra Kerferd",
            "Jack Earp",
            "Hugo Kitamura",
            "Poppy Croft",
            "Kiara Sherrard",
            "Chloe Corlette",
            "Noah Merrylees",
            "Stella Crookes",
            "Archie Oakley",
            "Bailey Thorpe"
        };
    }
}

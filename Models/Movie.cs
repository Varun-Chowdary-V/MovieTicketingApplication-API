using System.Text.Json;

namespace MovieTicketingApplication.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Lang { get; set; }
        public long Duration {  get; set; }
        public string Poster {  get; set; }
        public string Trailer { get; set; }
        public string[] Location {  get; set; }

        public string LocationSerialized
        {
            get => JsonSerializer.Serialize(Location);
            set => Location = JsonSerializer.Deserialize<string[]>(value);
        }
    }
}

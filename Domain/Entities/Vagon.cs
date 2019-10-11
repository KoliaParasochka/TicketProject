
namespace Domain.Entities
{
    public class Vagon
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Places { get; set; }
        public decimal PlacePrice { get; set; }
        public string Type { get; set; }

        public Train Train { get; set; }
    }
}

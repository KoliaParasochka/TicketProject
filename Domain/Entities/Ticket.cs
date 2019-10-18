namespace Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Place { get; set; }

        public int? VagonId { get; set; }
        public virtual Vagon Vagon { get; set; }

        public int? MyUserId { get; set; }
        public virtual MyUser User { get; set; }
    }
}
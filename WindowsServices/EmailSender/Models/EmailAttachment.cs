namespace EmailSender.Models
{
    public class EmailAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public int? EmailId { get; set; }
        public Email Email { get; set; }
    }
}

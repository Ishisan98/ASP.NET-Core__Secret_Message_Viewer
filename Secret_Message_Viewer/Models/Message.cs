namespace Secret_Message_Viewer.Models
{
    public class Message
    {
        public Message()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }

        public string Text { get; set; }

        public virtual MsgAttribute MsgAttribute { get; set; }
    }
}

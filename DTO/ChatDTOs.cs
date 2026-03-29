using System;

namespace doctors.DTO
{
    public class SendMessageDTO
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
    }

    public class MessageDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
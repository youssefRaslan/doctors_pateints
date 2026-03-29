using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace doctors.services.impelemtion
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendMessageAsync(SendMessageDTO model)
        {
            // Verify relationship exists
            var isDoctorSender = await _context.PatientDoctors
                .AnyAsync(pd => pd.DoctorId == model.SenderId && pd.PatientId == model.ReceiverId && pd.IsActive);
                
            var isPatientSender = await _context.PatientDoctors
                .AnyAsync(pd => pd.PatientId == model.SenderId && pd.DoctorId == model.ReceiverId && pd.IsActive);

            if (!isDoctorSender && !isPatientSender)
                return false; // Relationship does not exist or isn't active

            var message = new Message
            {
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
                Content = model.Content,
                FileUrl = model.FileUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync(int doctorId, int patientId)
        {
            return await _context.Messages
                .Where(m => (m.SenderId == doctorId && m.ReceiverId == patientId) ||
                            (m.SenderId == patientId && m.ReceiverId == doctorId))
                .OrderBy(m => m.CreatedAt)
                .Select(m => new MessageDTO
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Content = m.Content,
                    FileUrl = m.FileUrl,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();
        }
    }
}
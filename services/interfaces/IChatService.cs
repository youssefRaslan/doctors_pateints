using System.Collections.Generic;
using System.Threading.Tasks;
using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface IChatService
    {
        Task<bool> SendMessageAsync(SendMessageDTO model);
        Task<IEnumerable<MessageDTO>> GetMessagesAsync(int doctorId, int patientId);
    }
}
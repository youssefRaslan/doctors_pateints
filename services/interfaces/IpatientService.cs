using System.Collections.Generic;
using System.Threading.Tasks;
using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface IpatientService
    {
        Task<PatientDetailsDTO?> GetByIdAsync(int id);
        Task<IEnumerable<PatientDetailsDTO>> GetAllAsync(int page, int pageSize);
        Task<bool> UpdateAsync(int id, UpdatePatientDTO updateDto);
        Task<bool> RemoveAsync(int id);
    }
}

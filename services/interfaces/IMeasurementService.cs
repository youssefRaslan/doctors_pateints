using System.Collections.Generic;
using System.Threading.Tasks;
using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface IMeasurementService
    {
        Task<bool> AddMeasurementAsync(AddMeasurementDTO model);
        Task<IEnumerable<MeasurementDTO>> GetPatientMeasurementsAsync(int patientId);
    }
}
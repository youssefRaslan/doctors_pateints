using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace doctors.services.impelemtion
{
    public class MeasurementService : IMeasurementService
    {
        private readonly AppDbContext _context;

        public MeasurementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMeasurementAsync(AddMeasurementDTO model)
        {
            var patientExists = await _context.patients.AnyAsync(p => p.Id == model.PatientId);
            if (!patientExists) return false;

            var measurement = new Measurement
            {
                PatientId = model.PatientId,
                SugarLevel = model.SugarLevel,
                BloodPressure = model.BloodPressure,
                Date = model.Date
            };

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MeasurementDTO>> GetPatientMeasurementsAsync(int patientId)
        {
            return await _context.Measurements
                .Where(m => m.PatientId == patientId)
                .OrderByDescending(m => m.Date)
                .Select(m => new MeasurementDTO
                {
                    Id = m.Id,
                    PatientId = m.PatientId,
                    SugarLevel = m.SugarLevel,
                    BloodPressure = m.BloodPressure,
                    Date = m.Date
                })
                .ToListAsync();
        }
    }
}
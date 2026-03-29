using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace doctors.services.impelemtion
{
    public class PatientService : IpatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PatientDetailsDTO?> GetByIdAsync(int id)
        {
            var p = await _context.patients.FindAsync(id);
            if (p == null) return null;

            return new PatientDetailsDTO
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                ImageUrl = p.ImageUrl,
                BirthDate = p.BirthDate,
                Age = p.Age
            };
        }

        public async Task<IEnumerable<PatientDetailsDTO>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _context.patients
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PatientDetailsDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    Address = p.Address,
                    ImageUrl = p.ImageUrl,
                    BirthDate = p.BirthDate,
                    Age = p.Age
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdatePatientDTO updateDto)
        {
            var patient = await _context.patients.FindAsync(id);
            if (patient == null) return false;

            patient.Name = updateDto.Name;
            patient.PhoneNumber = updateDto.PhoneNumber;
            patient.Address = updateDto.Address;
            patient.BirthDate = updateDto.BirthDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var patient = await _context.patients.FindAsync(id);
            if (patient == null) return false;

            _context.patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
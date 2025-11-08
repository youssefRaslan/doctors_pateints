using doctors.data;

namespace doctors.services.interfaces
{
    public interface IpatientService
    {
        Patient? GetById(int id);
        List<Patient> GetAll();
        void Add(Patient doctor);
        void Remove(int id);
        void Update(Patient doctor);

    }
}

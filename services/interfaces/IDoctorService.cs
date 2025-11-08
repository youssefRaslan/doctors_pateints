using doctors.data;
using doctors.DTO;
using System.Collections.Generic;

namespace doctors.services.interfaces
{
    public interface IDoctorService
    {
     Task<reqstgmailDoctorDTO> Getgmail(getgmailDoctorDTO getgmail );
        List<GetallDoctorDTO> GetAll();
        Task<requstDoctorDTO> Add(AddDoctorDTO doctor);
        Task<removeDoctorDTO> Remove(getgmailDoctorDTO removeDOC);
        Task <updatDoctorDTO> Update(rqstupdatDoctorDTO id,updatDoctorDTO updatDOC);

    }
}

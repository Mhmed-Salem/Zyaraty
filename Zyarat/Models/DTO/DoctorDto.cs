namespace Zyarat.Models.DTO
{
    public class DoctorDto
    {
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public CityDto City { set; get; }
        public MedicalSpecializationDto MedicalSpecialized { set; get; }

    }
}
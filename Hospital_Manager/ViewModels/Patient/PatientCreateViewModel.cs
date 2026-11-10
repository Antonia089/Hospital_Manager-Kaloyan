using System.ComponentModel.DataAnnotations;

namespace Hospital_Manager.ViewModels.Patient
{
    public class PatientCreateViewModel
    {
        [Required(ErrorMessage = "Първото име е задължително.")]
        [MaxLength(30, ErrorMessage = "Първото име не може да бъде по-дълго от 30 символа.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Фамилното име е задължително.")]
        [MaxLength(30, ErrorMessage = "Фамилното име не може да бъде по-дълго от 30 символа.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Моля, изберете лекар.")]
        public List<int> DoctorIds { get; set; } = new List<int>();
    }
}

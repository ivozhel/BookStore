namespace BookStore.Models.Models.Users
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string NationalIDNumber { get; set; }
        public string EmployeeName { get; set; }
        public string LoginID { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int VacationHours  { get; set; }
        public int SickLeaveHours { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BirthPlace { get; set; }
        //public DateOnly BirthDate { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string IdCard { get; set; }
        //public DateOnly IdCardValidity { get; set; }
        public string CardAuthority { get; set; }
        //public string BSN { get; set; }
        public string IBAN { get; set; }
        //public string BIC { get; set; }
        public string BankDetails { get; set; }
        public int WorkExperienceInDays { get; set; }
        public DateTime StartDate { get; set; }
        public int? WorkExperienceInCompanyInDays { get; private set; }
        public int VacationHours { get; set; }
        public string Office { get; set; }
        public string Discipline { get; set; }
        //public string Function { get; set; }
        //public string Team { get; set; }
    }
}

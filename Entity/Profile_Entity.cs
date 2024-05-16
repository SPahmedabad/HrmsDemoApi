using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserDetails
    {
        public int id { get; set; }
        public String Email_id { get; set; }
        public String Password { get; set; }
        public int role { get; set; }
    }
    public class Api_Resoponse_Entity
    {
        public string status { get; set; }
        public string message { get; set; }
        public DataSet ArrayOfResponse { get; set; }
    }
    public class EmployeeMainData
    {
      
        public string Employee_Name { get; set; }
        public string DOJ { get; set; }
        public int Employee_Code { get; set; }
        public string Desigination { get; set; }
        public string Working_Status { get; set; }
        public string Reporting_Manager { get; set; }
        public string Location { get; set; }
        public string Total_Experience { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Blood_Group { get; set; }
        public string FatherHusband_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Highest_Qualification { get; set; }
        public string Emerg_ConatactNumber { get; set; }
        public string Emerg_ConatactPerson { get; set; }
        public string Email_id { get; set; }
        public string Matitual_Status { get; set; }
        public string PanCard_No { get; set; }
        public string Adhar_No { get; set; }
        public string Curr_Address { get; set; }
        public string Perma_Addresss { get; set; }
        public string Official_EmaildID { get; set; }
        public string UAN_Number { get; set; }
        public string ESIC_Number { get; set; }
        public string Mediclaim_ID { get; set; }
        public int role { get; set; }
        public string Image { get; set; }
        public int Department { get; set; }
        public string password { get; set; }
        public string flag { get; set; }
        public string Updated_Image { get; set; }
        public int dept_id { get; set; }
    }
}

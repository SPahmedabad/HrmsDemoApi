using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FeedbackFormEntity
    {
        public int Trainee_Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ddlModuleName { get; set; }

        public int ddlDepartment { get; set; }

        public int ddlTrainer { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public char ModeOfTraining { get; set; }

        public string EnjoyMostAboutTraining1 { get; set; }

        public string EnjoyMostAboutTraining2 { get; set; }

        public string EnjoyMostAboutTraining3 { get; set; }

        public string ImproveTraining { get; set; }

        public string SkillsLearn { get; set; }

        public string ddl_ques1 { get; set; }                //Question1 in Ratings

        public string ddl_ques2 { get; set; }

        public string ddl_ques3 { get; set; }

        public string ddl_ques4 { get; set; }

        public string ddl_ques5 { get; set; }
        public string ddl_ques6 { get; set; }

        public string ddl_ques7 { get; set; }

        public string ddl_ques8 { get; set; }

        public string ddl_ques9 { get; set; }

        public string ddl_ques10 { get; set; }

        public string Comments { get; set; }

        public string Answers { get; set; }

        //added by Deeksha
        //public int mod_id { get; set; }

        //public string mod_name { get; set; }

        //public int dept_id { get; set; }

        //public string dept_name { get; set; }


        //Author -> Saddam Shaikh -> Navigation Property
        public DepartmentEntity Department_Entity = new DepartmentEntity();

        public ModuleEntity Module_Entity = new ModuleEntity();
      

        public int mail_log_id { get; set; }


    }
}

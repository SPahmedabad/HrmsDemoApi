using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GrievanceForm
    {
        public int id { get; set; }
        public string flag { get; set; }
        public int VictimId { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string TimeOfIncident { get; set; }
        public string LocationOfIncident { get; set; }
        public string ComplaintAgainst { get; set; }
        public string IncidentDetails { get; set; }
        public string WitnessEmployeeName { get; set; }
        public Double witnessContactNumber { get; set; }
        public string WitnessEmailId { get; set; }
        public string FirstTimeIssues { get; set; }
        public string SuggestionsForResolve { get; set; }
    }
}

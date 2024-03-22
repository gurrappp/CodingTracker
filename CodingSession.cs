using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class CodingSession
    {

        public int Id { get; set; }
        public string? Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool HasStarted { get; set; } = false;
    }
}

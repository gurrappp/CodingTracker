using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class UserInput
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime StartSession(string time)
        {

            if (!DateTime.TryParse(time.ToString(), out var _))
            {
                time = DateTime.Today.ToString();
            }

            return time;

        }


    }
}

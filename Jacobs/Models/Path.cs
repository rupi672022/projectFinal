using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jacobs.Models
{
    public class Path
    {
        double duration;
        double distance;

        public   Path()
            {}

        public Path(double duration, double distance)
        {
            this.Duration = duration;
            this.Distance = distance;
        }

        public double Duration { get => duration; set => duration = value; }
        public double Distance { get => distance; set => distance = value; }
    }

}
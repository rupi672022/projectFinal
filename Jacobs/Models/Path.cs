using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace GoogleApi.Test.Maps.DistanceMatrix
{
    public class Path
    {
        int distance;
        int duration;

        public int Distance { get => distance; set => distance = value; }
        public int Duration { get => duration; set => duration = value; }

        public Path(int distance, int duration)
        {
            this.Distance = distance;
            this.Duration = duration;
        }

        public Path() { }
    }
}
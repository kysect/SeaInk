using System;
using System.IO;

namespace SeaInk.Core.Models
{
    public class StudyAssignment
    {
        public int LocalId { get; set; }
        public string Title { get; set; }
        public bool IsMilestone { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public float MinPoints { get; private set; }
        public float MaxPoints { get; private set; }

        //Задаёт новую разбаловку, при нарушении инварианта 
        //кидает эксепшн
        public void SetScoring(float minPoints, float maxPoints)
        {
            if (minPoints <= maxPoints)
            {
                MinPoints = minPoints;
                MaxPoints = maxPoints;
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}
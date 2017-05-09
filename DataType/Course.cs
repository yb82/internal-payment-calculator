using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NatCalculator
{
    public class Course
    {
        private String _courseName;
        private int _duration;
        private int _enrolment;
        private int _material;
        private int _tuition;
        public int specialflag { get; set; }
        private List<string> _startDate;

        public Course()
        {
            this._startDate = new List<string>();
        }
        public List<string> StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public int Tuition
        {
            get { return _tuition; }
            set { _tuition = value; }
        }

        public int Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public int Enrolment
        {
            get { return _enrolment; }
            set { _enrolment = value; }
        }
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public String CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }
    }
}

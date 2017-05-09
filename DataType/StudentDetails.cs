using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NatCalculator.DataType
{
    class StudentDetails
    {
        public static List<string> TitleType = new List<string>( new string[] {"Mr.","Ms" ,"Miss", "Mrs"});
        public enum GenderType { Male, Female };
        public static List<string> LevelOfEng = new List<string>(new string[] { "Beginner", "Elementary", "Pre-Intermediate", "Intermediate", "Upper-Intermediate", "Advanced" });
        public static List<string> Month = new List<string>(new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" });
        
        private string _title;
        private string _fName;
        private string _lName;
        private string _gender;
        private string _dateOfBirth;
        private string _nation;
        private string _lvlOfEng;
        private string _passportNo;
        private string _currentAddr;
        private string _contact;
        private string _email;
        private bool _insurance;
        private string _insuranceType;
        private string _accommodationType;
        private int _numberOfWeek;
        private string _startDate;
        private string _roomtype;
        private string _windowType;

        public StudentDetails()
        {
           // titleType = new List<string>();

            //titleType.Add();

        }

        public string WindowType
        {
            get { return _windowType; }
            set { _windowType = value; }
        }

        public string Roomtype
        {
            get { return _roomtype; }
            set { _roomtype = value; }
        }

        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }


        public int NumberOfWeek
        {
            get { return _numberOfWeek; }
            set { _numberOfWeek = value; }
        }
        

        public string AccommodationType
        {
            get { return _accommodationType; }
            set { _accommodationType = value; }
        }

        public string InsuranceType
        {
            get { return _insuranceType; }
            set { _insuranceType = value; }
        }
        

        public bool Insurance
        {
            get { return _insurance; }
            set { _insurance = value; }
        }


        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }


        public string CurrentAddr
        {
            get { return _currentAddr; }
            set { _currentAddr = value; }
        }


        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value; }
        }


        public string LvlOfEng
        {
            get { return _lvlOfEng; }
            set { _lvlOfEng = value; }
        }


        public string Nation
        {
            get { return _nation; }
            set { _nation = value; }
        }


        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }


        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }


        public string LName
        {
            get { return _lName; }
            set { _lName = value; }
        }


        public string FName
        {
            get { return _fName; }
            set { _fName = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

    }
}

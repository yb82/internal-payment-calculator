using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NatCalculator
{
    public class OutputSet
    {
        private string _itemDescription;

        public string ItemDescription
        {
            get { return _itemDescription; }
            set { _itemDescription = value; }
        }
        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private string _dueDate;

        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        private string _courseName;

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }
        public OutputSet(string courseName, string dueDate, string itemDescription, string amount)
        {
            this._courseName = courseName;
            this._dueDate = dueDate;
            this._itemDescription = itemDescription;
            this._amount = amount;

        }
     }
}

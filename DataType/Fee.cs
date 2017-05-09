using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NatCalculator
{
    class Fee
    {
        private int _single;
        private int _package;
        private int _package1;
        //private int _package3;
        private int _paymentPlanFee;
        public int PaymentPlanFee
        {
            get { return _paymentPlanFee; }
            set { _paymentPlanFee = value; }
        }
        public int Package
        {
            get { return _package; }
            set { _package = value; }
        }
        public int Package1
        {
            get { return _package1; }
            set { _package1 = value; }
        }

        //public int Package3
        //{
        //    get { return _package3; }
        //    set { _package3 = value; }
        //}
        public int Single
        {
            get { return _single; }
            set { _single = value; }
        }
    }
}

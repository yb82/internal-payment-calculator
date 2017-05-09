/*
 *  Author: Young Bo KIM
 *  Date : 09/07/2015
 *  Description : This class creates payment plan as marketing team request. Each payment does not have in common, so it needs to be calculated by all source code.
 *  
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Globalization;


namespace NatCalculator
{

    public class Calculator
    {

        public const string TUITION = "TUITION FEE";
        private string FORMAT = "dd/MMM/yyyy"; // Output format.

        private DataHandler _dHandler;     // All course information such as price, start date, duration and so on.
        private List<String> _courseList;  // Selected Course name
        private int _option;               // Payment Option
        ArrayList _result;                 // Result set for print out
        private List<DateTime> _startDate; // Start date list.
        private ArrayList _courseData;  // Store "Course for figuring out payment dates.
        private Fee _fees;              // This fee represent the discount fee for package and single course. 



        private string today;               // store today's date as string
        private const int TENWEEKS = 67;
        private const int SECONDPAYMENTDATE = -17;
        private const int FOURWEEKS = 28;
        private const int FULLENROLMENT = 195;
        private const int FULLMATERIAL = 100;
        private CultureInfo auInfo = new CultureInfo("en-AU");
        private Boolean fStudent = false;
        private Boolean fEnrolmentFee = false;
        private Boolean fMat = false;
        private Boolean fPaymentPlanFee = false;
        private int enrolPercent =0;
        private int matPercent=0;
        private double firstCourseEnrolmentFee;
        private DataHandler dHandler;
        private List<string> coursename;
        private int paymentOption;
        private List<DateTime> startDate;
        private int enrolmentPercent;
        //public Calculator(DataHandler dHandler, List<String> selectedCourseList, int option, List<DateTime> startDate)//, bool fStudent, bool fEnrolmentFee,bool fMat, bool fPaymentPlanFee)
        public Calculator(DataHandler dHandler, List<String> selectedCourseList, int option, List<DateTime> startDate, bool fStudent, bool fEnrolmentFee, bool fMat, bool fPaymentPlanFee)
        {
            this._dHandler = dHandler;
            this._courseList = selectedCourseList;
            this._option = option;
            this._result = new ArrayList();
            this._startDate = startDate;
            this._fees = dHandler.Fees;
            DateTime date = DateTime.Now;
            this.fStudent = fStudent;
            this.fEnrolmentFee = fEnrolmentFee;
            this.fMat = fMat;
            this.fPaymentPlanFee = fPaymentPlanFee;
            this.today = date.ToString(FORMAT, auInfo);

        }
        public Calculator(DataHandler dHandler, List<String> selectedCourseList, int option, List<DateTime> startDate, bool fStudent, bool fEnrolmentFee, bool fMat, bool fPaymentPlanFee, int enrolPercent, int matPercent)
        {
            this._dHandler = dHandler;
            this._courseList = selectedCourseList;
            this._option = option;
            this._result = new ArrayList();
            this._startDate = startDate;
            this._fees = dHandler.Fees;
            DateTime date = DateTime.Now;
            this.fStudent = fStudent;
            this.fEnrolmentFee = fEnrolmentFee;
            this.fMat = fMat;
            this.fPaymentPlanFee = fPaymentPlanFee;
            this.today = date.ToString(FORMAT, auInfo);
            this.enrolPercent = enrolPercent;
            this.matPercent = matPercent;
        }

        

        /*
         *   getResult()
         *   create output dataset and pass "Output" type of result to Form class.
         *  
         */

        public ArrayList getResult()
        {
            _courseData = new ArrayList();
            foreach (string c in this._courseList)
            {
                Course course = this.findCourseByName(c);
                if (course != null)
                {
                    _courseData.Add(course);
                }

            }
            Course firstCourse = (Course)_courseData[0];
            this.firstCourseEnrolmentFee = firstCourse.Enrolment;
            // check course count
            this.checkMatEnrolFee();
            if (_courseList.Count() == 1)
                if (this._option != 4)
                    this.calculateSingleOption();
                else this.calculatePackageOption4();

            else
            {

                // In order to reduce code, check enrolment and material fee before calculating.



                switch (this._option)
                {
                    case 1:
                        this.calculatePackageOption1();
                        break;
                    case 2:
                        this.calculatePackageOption2();
                        break;
                    case 3:
                        this.calculatePackageOption3();
                        break;
                    case 4:
                        this.calculatePackageOption4();
                        break;
                }

            }
            return _result;


        }
        /*
         * checkMatEnrolFee
         * 
         * as saved course list, this will find the fee, create and save Outputset, 
         */

        private void checkMatEnrolFee()
        {
            //bool flag = false;

            //foreach (Course c in this._courseData)
            //{
            //    if (c.CourseName.Contains("Business")) // if there is business course, waive the fee even though there is fee.
            //    {
            //        flag = true;
            //    }
            //}
            OutputSet outputRecord;
            string courseName = "";
            int courseCnt = 0;
            double materialFee = 0;


            /* 17/11/2016
             * Rule changed, 
             * if cert IV course is, waive enrolment fee.
             * and if 2 more course, $95
             * 
            */
            bool checkEnrolment = false;
            bool moreThan2Courses = false;
            Course b = (Course)this._courseData[0];
            courseName = b.CourseName;
            foreach (Course c in this._courseData)
            {

                if (c.Enrolment == 0)
                {
                    checkEnrolment = true;
                }

            }
            
            moreThan2Courses = (this._courseData.Count > 1) ? true : false;

            if (enrolPercent == 0)
            {
             
            // any Cert IV or former student or Enrolment fee special waive Enrolment fee
            if (checkEnrolment || fStudent || fEnrolmentFee)
            {
                outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee", "0");
            }
            else if (moreThan2Courses)
            {
                outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee", "95");
            }
            else
                outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee", this.firstCourseEnrolmentFee.ToString());


            }
            else
            {
                if (checkEnrolment || fStudent || fEnrolmentFee)
                {
                    outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee", "0");
                }
                else if (moreThan2Courses)
                {
                    outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee", (95*this.enrolPercent/100.0).ToString());
                }
                else
                    outputRecord = new OutputSet(courseName, this.today, "Enrolment Fee",(this.firstCourseEnrolmentFee*enrolPercent/100.0).ToString()) ;

            }

            this._result.Add(outputRecord);

            foreach (Course c in this._courseData)
            {
                //    if (!flag)
                //        {


                //            if (c.Enrolment != 0 && courseCnt == 0 )
                //            {
                //                int enrolment = c.Enrolment;
                //                if (fStudent || fEnrolmentFee)
                //                {
                //                    enrolment = 0;
                //                }
                //                outputRecord = new OutputSet(c.CourseName, this.today, "Enrolment Fee", enrolment.ToString());
                //                this._result.Add(outputRecord);
                //                courseName = c.CourseName;

                //            }
                if (c.Material != 0)
                {

                    materialFee += c.Material;
                    //outputRecord = new OutputSet(c.CourseName, this.today, "Material Fee", c.Material.ToString());
                    //this._result.Add(outputRecord);

                }
                courseCnt++;
                //return; // we charge this fee only one time.

            }
            
            if (fMat)
            {
                materialFee = 0;
            }
            else if (fStudent)
            {
                materialFee = 100;
            }
            else if (this.matPercent != 0)
            {
                materialFee = materialFee * this.matPercent / 100.0;
            }
            outputRecord = new OutputSet(courseName, this.today, "Material Fee", materialFee.ToString());
            this._result.Add(outputRecord);

            int paymentPlanFee = this._fees.PaymentPlanFee;
            if (fPaymentPlanFee)
                paymentPlanFee = 0;
            outputRecord = new OutputSet(courseName, this.today, "Payment Plan Fee", paymentPlanFee.ToString());

            this._result.Add(outputRecord);

        }

        private void calculatePackageOption4()
        {

            int counter = 0;
            int courseCnt = 1 ;
            OutputSet record;
            DateTime tempdate = DateTime.Now;


            DateTime startDateOfFirstCourse = this._startDate[0];
            int aPayment = getNextPayment();
            string paymentDate = "";
            
            foreach (Course c in this._courseData)
            {
                int tuition = c.Tuition;

                string courseName = c.CourseName;

                //discount tuition fee on last course tuition fee.

                if (courseCnt == this._courseList.Count && this._courseData.Count == 2)
                {
                    tuition -= this._fees.Package;
                }
                else if (courseCnt == this._courseList.Count && this._courseData.Count >= 3)
                {
                    tuition = c.Tuition - this._fees.Package1;
                }
                else tuition = c.Tuition;


                //paymentDate = tempdate.Date.ToString(FORMAT, auInfo);
                if (courseCnt == 1) { 
                paymentDate = this.today;
                tuition -= aPayment;
                record = new OutputSet(courseName, paymentDate, TUITION, aPayment.ToString());
                this._result.Add(record);
                }
                if (tuition > aPayment)
                {

                    while (tuition > 0)
                    {
                        if (counter == 0)
                        {
                            tempdate = startDateOfFirstCourse;
                            tempdate = tempdate.AddDays(-10);
                            paymentDate = tempdate.Date.ToString(FORMAT, auInfo);
                            
                        } else 
                        {
                            tempdate = startDateOfFirstCourse;
                            tempdate = tempdate.AddDays(counter * FOURWEEKS);

                        }
                        paymentDate = tempdate.Date.ToString(FORMAT, auInfo);

                        tuition -= aPayment;
                        record = new OutputSet(courseName, paymentDate, TUITION, aPayment.ToString());
                        this._result.Add(record);
                        counter++;

                        aPayment = getNextPayment();
                        if (tuition < aPayment)
                        {
                            if (tuition != 0)
                            {
                                tempdate = startDateOfFirstCourse;
                                tempdate = tempdate.AddDays(counter * FOURWEEKS);

                            
                            paymentDate = tempdate.Date.ToString(FORMAT, auInfo);


                            record = new OutputSet(courseName, paymentDate, TUITION, tuition.ToString());
                            aPayment -= tuition;
                            this._result.Add(record);
                            }
                            break;
                        }
                        

                    }
                    
                

                }
                
                  









                //DateTime tmpDate = this._startDate[counter].Date.AddDays(TENWEEKS);
                //record = new OutputSet(courseName, tmpDate.Date.ToString(FORMAT, auInfo), TUITION, amount);
                //this._result.Add(record);

                courseCnt++;
            }
            // throw new NotImplementedException();
        }
        private int getNextPayment()
        {
            return 500;
        }
        private void calculatePackageOption3()
        {

            int counter = 0;
            int courseCounter = this._courseData.Count;
            foreach (Course c in this._courseData)
            {
                int tuition = c.Tuition;
                int reminder = tuition;
                string courseName = c.CourseName;

                //discount tuition fee on last course tuition fee.

                if (counter == courseCounter - 1 && courseCounter == 2)
                {
                    reminder -= this._fees.Package;
                }
                else if ((courseCounter - 1 == counter && courseCounter >= 3))
                {
                    reminder = c.Tuition - this._fees.Package1;
                }
                else reminder = c.Tuition;

                string amount = (reminder * 0.5).ToString();
                OutputSet record;
                record = new OutputSet(courseName, today, TUITION, amount);
                this._result.Add(record);

                DateTime tmpDate = this._startDate[counter].Date.AddDays(TENWEEKS);
                record = new OutputSet(courseName, tmpDate.Date.ToString(FORMAT, auInfo), TUITION, amount);
                this._result.Add(record);

                counter++;
            }
            // throw new NotImplementedException();
        }

        /*
         *  Option 2 Payment plan 
         */
        private void calculatePackageOption2()
        {

            int courseCounter = this._courseData.Count;
            DateTime startdateOfFirstCourse = this._startDate[0];

            DateTime tmpDate = new DateTime();
            int counter = 0;
            foreach (Course c in this._courseData)
            {
                int tuition = 0;


                if (courseCounter - 1 == counter && courseCounter==2)
                {
                    tuition = c.Tuition - this._fees.Package;
                }
                else if ((courseCounter - 1 == counter && courseCounter >= 3))
                {
                    tuition = c.Tuition - this._fees.Package1;
                }
                else tuition = c.Tuition;


            
                int reminder = tuition;
                string courseName = c.CourseName;

                OutputSet record;
              
                double[] amount = new double[4];
                amount[0] = amount[1] = reminder * 0.2;
                amount[2] = amount[3] = reminder * 0.3;

                tmpDate = new DateTime();
                for (int i = 0; i < 4; i++)
                {

                    if (i == 0)
                        record = new OutputSet(courseName, today, TUITION, (amount[i]).ToString());
                    else if (i == 1)
                    {
                        tmpDate = _startDate[counter].AddDays(SECONDPAYMENTDATE);
                        record = new OutputSet(courseName, tmpDate.Date.ToString(FORMAT, auInfo), TUITION, (amount[i]).ToString());

                    }
                    else if (i == 2)
                    {
                        tmpDate = _startDate[counter].AddDays(TENWEEKS);
                        record = new OutputSet(courseName, tmpDate.Date.ToString(FORMAT, auInfo), TUITION, (amount[i]).ToString());
                    }
                    else
                    {
                        tmpDate = tmpDate.AddDays(FOURWEEKS);
                        record = new OutputSet(courseName, tmpDate.Date.ToString(FORMAT, auInfo), TUITION, (amount[i]).ToString());

                    }
                    this._result.Add(record);
                }
                counter++;
            }


        }
        /*
         *  Option 1 payment plan.
         */

        //private void calculatePackageOption1()
        //{


        //    int courseCounter = this._courseData.Count;
        //    DateTime startdateOfFirstCourse = this._startDate[0];

        //    DateTime tempDate = new DateTime();
        //    int counter = 0;
        //    foreach (Course c in this._courseData)
        //    {
        //        int tuition = 0;
        //        if (courseCounter - 1 == counter)
        //        {
        //            tuition = c.Tuition - this._fees.Package;
        //        }
        //        else tuition = c.Tuition;

        //        int remainder = tuition;
        //        string courseName = c.CourseName;
        //        OutputSet records, firstRow;

        //        /*
        //         * if there are 4 selected courses, both the first and second payment($1000) has to devided into $250
        //         * if 3, $500,$250,$250 in total $1000 
        //         * if 2, $500, $500
        //         */


        //        if (counter == 0 && courseCounter == 4)
        //        {
        //            firstRow = new OutputSet(courseName, today, TUITION, "250");    
        //            remainder -= 250;
        //        }

        //        else if (counter != 0 && (courseCounter == 3 || courseCounter == 4))
        //        {
        //            firstRow = new OutputSet(courseName, today, TUITION, "250");
        //            remainder -= 250;

        //        }
        //        else
        //        {
        //            firstRow = new OutputSet(courseName, today, TUITION, "500");
        //            remainder -= 500;
        //        }


        //        this._result.Add(firstRow);



        //        tempDate = startdateOfFirstCourse.Date.AddDays(SECONDPAYMENTDATE);
        //        if (counter == 0 && courseCounter == 4)
        //        {
        //            records = new OutputSet(courseName, tempDate.ToString(FORMAT,auInfo), TUITION, "250");
        //            remainder -= 250;
        //        }
        //        else if (counter != 0 && (courseCounter == 3 || courseCounter == 4))
        //        {
        //            records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "250");
        //            remainder -= 250;
        //        }
        //        else
        //        {
        //            records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "500");
        //            remainder -= 500;
        //        }
        //        this._result.Add(records);

        //        /*
        //         * for the third payment plan, it depends on the start date.
        //         * conter shows the course order, the  of first course will be 0 then 1, 2 and 3.
        //         * order represents payment order, after 1st and 2nd payment, the order will be 0 then so on.
        //         * 
        //         *
        //         */

        //        int order = 0;
        //        do
        //        {
        //            if (order == 0 && counter == 0)
        //            {

        //                tempDate = startdateOfFirstCourse.Date.AddDays(TENWEEKS);
        //            }
        //            else if (order == 0 && counter != 0)
        //            {

        //                tempDate = this._startDate[counter].AddDays(TENWEEKS);
        //            }
        //            else tempDate = tempDate.AddDays(FOURWEEKS);


        //            // Even in the third, it is going to be less than $1000 due to discount fee.(the cheapest tuition fee 1800 -500 -500 = $800)
        //            // if this current remainder under $1000 break immediately, otherwise it goes negative.

        //            if (remainder < 1000)
        //            {
        //                break;
        //            }
        //            records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "1000");
        //            remainder -= 1000;
        //            this._result.Add(records);


        //            order++;

        //        }
        //        while (remainder - 1000 > 0);


        //        if (remainder > 0)
        //        {
        //            tempDate = tempDate.AddDays(FOURWEEKS);
        //            OutputSet lastRow = new OutputSet(courseName, tempDate.Date.ToString(FORMAT, auInfo), TUITION, remainder.ToString());
        //            //lastReminder = reminder;
        //            this._result.Add(lastRow);

        //        }
        //        //else lastReminder = 0;
        //        counter++;
        //    }

        //}

        private void calculatePackageOption1()
        {


            int courseCounter = this._courseData.Count;
            DateTime startdateOfFirstCourse = this._startDate[0];

            DateTime tempDate = new DateTime();
            int counter = 0;
            foreach (Course c in this._courseData)
            {
                int tuition = 0;


                if (courseCounter - 1 == counter && courseCounter==2)
                {
                    tuition = c.Tuition - this._fees.Package;
                }
                else if ((courseCounter - 1 == counter && courseCounter >= 3))
                {
                    tuition = c.Tuition - this._fees.Package1;
                }
                else tuition = c.Tuition;

                int remainder = tuition;
                string courseName = c.CourseName;
                OutputSet records, firstRow;

                /*
                 * if there are 4 selected courses, both the first and second payment($1000) has to devided into $250
                 * if 3, $500,$250,$250 in total $1000 
                 * if 2, $500, $500
                 */
                /*
                 *  01/01/2017 changes
                 *  change the eBecas payment rule!
                 *  deduct from the first payment.
                 */

                //if (counter == 0 && courseCounter == 4)
                //{
                //    firstRow = new OutputSet(courseName, today, TUITION, "250");
                //    remainder -= 250;
                //}

                //else if (counter != 0 && (courseCounter == 3 || courseCounter == 4))
                //{
                //    firstRow = new OutputSet(courseName, today, TUITION, "250");
                //    remainder -= 250;

                //}
                //else
                //{
                //    firstRow = new OutputSet(courseName, today, TUITION, "500");
                //    remainder -= 500;
                //}
                switch (courseCounter)
                {
                    case 2:
                        firstRow = new OutputSet(courseName, today, TUITION, "500");
                        remainder -= 500;
                        break;
                    case 3:
                        if (counter != 0)
                        {
                            firstRow = new OutputSet(courseName, today, TUITION, "250");
                            remainder -= 250;
                        }
                        else
                        {
                            firstRow = new OutputSet(courseName, today, TUITION, "500");
                            remainder -= 500;
                        }
                        break;
                    case 4:
                        firstRow = new OutputSet(courseName, today, TUITION, "250");
                        remainder -= 250;
                        break;
                    case 5:
                        firstRow = new OutputSet(courseName, today, TUITION, "200");
                        remainder -= 200;

                        break;
                    case 6:
                        if (counter != 0 && counter != 1)
                        {
                            firstRow = new OutputSet(courseName, today, TUITION, "150");
                            remainder -= 150;
                        }
                        else
                        {
                            firstRow = new OutputSet(courseName, today, TUITION, "200");
                            remainder -= 200;
                        }
                        break;

                    default:
                        // default has to be set up.
                        firstRow = new OutputSet(courseName, today, TUITION, "1000");
                        break;
                }


                this._result.Add(firstRow);



                tempDate = startdateOfFirstCourse.Date.AddDays(SECONDPAYMENTDATE);
                string stringTempdate = tempDate.ToString(FORMAT, auInfo);
                //records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "1000");
                // remainder -= 1000;
                switch (courseCounter)
                {
                    case 2:
                        records = new OutputSet(courseName, stringTempdate, TUITION, "500");
                        remainder -= 500;
                        break;
                    case 3:
                        if (counter != 0)
                        {
                            records = new OutputSet(courseName, stringTempdate, TUITION, "250");
                            remainder -= 250;
                        }
                        else
                        {
                            records = new OutputSet(courseName, stringTempdate, TUITION, "500");
                            remainder -= 500;
                        }
                        break;
                    case 4:
                        records = new OutputSet(courseName, stringTempdate, TUITION, "250");
                        remainder -= 250;
                        break;
                    case 5:
                        records = new OutputSet(courseName, stringTempdate, TUITION, "200");
                        remainder -= 200;
                        break;
                    case 6:
                        if (counter != 0 && counter != 1)
                        {
                            records = new OutputSet(courseName, stringTempdate, TUITION, "150");
                            remainder -= 150;
                        }
                        else
                        {
                            records = new OutputSet(courseName, stringTempdate, TUITION, "200");
                            remainder -= 200;
                        }
                        break;

                    default:
                        // default has to be set up.
                        records = new OutputSet(courseName, stringTempdate, TUITION, "1000");
                        break;
                }



                //if (counter == 0 && courseCounter == 4)
                //{
                //    records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "250");
                //    remainder -= 250;
                //}
                //else if (counter != 0 && (courseCounter == 3 || courseCounter == 4))
                //{
                //    records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "250");
                //    remainder -= 250;
                //}
                //else
                //{
                //    records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "500");
                //    remainder -= 500;
                //}
                this._result.Add(records);

                /*
                 * for the third payment plan, it depends on the start date.
                 * conter shows the course order, the  of first course will be 0 then 1, 2 and 3.
                 * order represents payment order, after 1st and 2nd payment, the order will be 0 then so on.
                 * 
                 *
                 */

                int order = 0;
                do
                {
                    if (order == 0 && counter == 0)
                    {

                        tempDate = startdateOfFirstCourse.Date.AddDays(TENWEEKS);
                    }
                    else if (order == 0 && counter != 0)
                    {

                        tempDate = this._startDate[counter].AddDays(TENWEEKS);
                    }
                    else tempDate = tempDate.AddDays(FOURWEEKS);


                    // Even in the third, it is going to be less than $1000 due to discount fee.(the cheapest tuition fee 1800 -500 -500 = $800)
                    // if this current remainder under $1000 break immediately, otherwise it goes negative.

                    if (remainder < 1000)
                    {
                        break;
                    }
                    records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "1000");
                    remainder -= 1000;
                    this._result.Add(records);


                    order++;

                }
                while (remainder - 1000 > 0);


                if (remainder > 0)
                {
                    tempDate = tempDate.AddDays(FOURWEEKS);
                    OutputSet lastRow = new OutputSet(courseName, tempDate.Date.ToString(FORMAT, auInfo), TUITION, remainder.ToString());
                    //lastReminder = reminder;
                    this._result.Add(lastRow);

                }
                //else lastReminder = 0;
                counter++;
            }

        }
        /*
         * findCourseByName
         * 
         * From datahandler which contains all course information, this function finds a course.
         */
        private Course findCourseByName(string courseName)
        {
            foreach (Course c in this._dHandler.Courses)
            {

                if (c.CourseName == courseName)
                {
                    return c;
                }

            }
            return null;
        }


        /*
         * CalculateSingleOption
         * 
         */

        private void calculateSingleOption()
        {
            foreach (Course c in this._dHandler.Courses)
            {
                if (c.CourseName == this._courseList[0])
                {
                    c.Tuition -= this._fees.Single;
                    this.createSinglePaymentPlan(c);
                    break;
                }

            }
        }
        private int checkSpecialEnrolment(Course course)
        {
            int special = course.specialflag;
            if (special != 0)
                return 0;
            else return FULLENROLMENT;
        }
        private int checkSpecialMaterial(Course course)
        {
            int special = course.specialflag;
            if (special != 0)
                return 0;
            else return FULLENROLMENT;
        }

        public void createSinglePaymentPlan(Course c)
        {
            int tuition = c.Tuition;
            int reminder = tuition;

            string courseName = c.CourseName;

            if (this._option == 1)
            {
                int enrolment = c.Enrolment;
                enrolment = this.checkSpecialEnrolment(c);
                if (this.fEnrolmentFee || this.fStudent)
                {
                    enrolment = 0;
                }


                //int tempTuition = (500 - enrolment - c.Material); // $500 includes everything.
                //reminder -= tempTuition;
                reminder -= 300; // 04/11/2016 changed the Single course fee, fixed $300 tution.


                //if (c.Enrolment != 0)
                //{
                //    OutputSet secondRow = new OutputSet(courseName, today, "Enrolment Fee", enrolment.ToString());
                //    this._result.Add(secondRow);
                //}
                //if (c.Material != 0)
                //{
                //    OutputSet thirdRow = new OutputSet(courseName, today, "Material Fee", c.Material.ToString());
                //    this._result.Add(thirdRow);
                //}
                OutputSet firstRow = new OutputSet(courseName, today, TUITION, "300");

                this._result.Add(firstRow);




                // Student pay $500 in total include enrolment and material fee;



                DateTime tempDate = this._startDate[0].Date.AddDays(SECONDPAYMENTDATE);
                int counter = 0;
                do
                {

                    OutputSet records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, "1000");
                    reminder -= 1000;
                    if (counter == 0)
                    {
                        tempDate = this._startDate[0].AddDays(TENWEEKS);
                    }
                    else tempDate = tempDate.AddDays(FOURWEEKS);
                    counter++;
                    this._result.Add(records);

                }
                while (reminder - 1000 > 0);
                if (reminder > 0)
                {
                    OutputSet lastRow = new OutputSet(courseName, tempDate.Date.ToString(FORMAT, auInfo), TUITION, reminder.ToString());
                    this._result.Add(lastRow);
                }


            }

            else if (this._option == 2)
            {
                double[] amount = new double[4];
                amount[0] = amount[2] = reminder * 0.3;
                amount[1] = amount[3] = reminder * 0.2;


                //if (c.Enrolment != 0)
                //{
                //    OutputSet secondRow = new OutputSet(courseName, today, "Enrolment Fee", c.Enrolment.ToString());
                //    this._result.Add(secondRow);
                //}
                //if (c.Material != 0)
                //{
                //    OutputSet thirdRow = new OutputSet(courseName, today, "Material Fee", c.Material.ToString());
                //    this._result.Add(thirdRow);
                //}

                OutputSet firstRow = new OutputSet(courseName, today, TUITION, amount[0].ToString());
                this._result.Add(firstRow);

                DateTime tempDate = this._startDate[0].Date.AddDays(SECONDPAYMENTDATE);
                int counter = 0;
                for (int i = 1; i < amount.Length; i++)
                {


                    OutputSet records = new OutputSet(courseName, tempDate.ToString(FORMAT, auInfo), TUITION, amount[i].ToString());
                    if (counter == 0)
                    {
                        tempDate = this._startDate[0].AddDays(TENWEEKS);
                    }
                    else tempDate = tempDate.AddDays(FOURWEEKS);
                    counter++;


                    this._result.Add(records);

                }



            }
        }
    }

}


/*
 * Preprocess Class.
 * 
 * This class reads course details from XML file and create relevant classes.
 * This class contains all courses details such as price and duration.
 * Before 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NatCalculator
{

    public class DataHandler
    {
        private ArrayList courses = new ArrayList();
        private DateTime today = DateTime.Now;
        //private string format = "dd/MMM/yyyy";

        public ArrayList Courses
        {
            get { return courses; }
            set { courses = value; }
        }
        private Fee fees = new Fee();

        internal Fee Fees
        {
            get { return fees; }
            set { fees = value; }
        }




        private XElement xmlFile;

        public XElement XmlFile
        {
            get { return xmlFile; }
            set { xmlFile = value; }
        }
        public DataHandler()
        {
            try
            {

                this.ReadData();
            }
            catch (Exception e)
            {
                //throw (e.Message);
            }
            //xmlFile.Save("courseproperty.xml");

        }
        private void ReadData()
        {

            DateTime startDate;
            try
            {
                xmlFile = XElement.Load("courseproperties.xml");


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            var query = from c in xmlFile.Elements()
                        select c;

            foreach (XElement el in query)
            {
                //Console.WriteLine(el.Name);

                //Console.WriteLine( el.Attribute("name"));
                if (el.Name == "Course")
                {
                    Course course = new Course();
                    course.CourseName = el.Attribute("name").Value;
                    course.Duration = int.Parse(el.Attribute("duration").Value);
                    course.Enrolment = int.Parse(el.Attribute("enrolment").Value);
                    course.Material = int.Parse(el.Attribute("material").Value);
                    course.Tuition = int.Parse(el.Attribute("tuition").Value);
                    //course.specialflag = int.Parse(el.Attribute("specialtag").Value);



                    try
                    {
                        string startdate = el.Attribute("startdate").Value.Replace(" ", "");
                        var startdatelist = startdate.Split(',');
                        foreach (string a in startdatelist)
                        {
                            startDate = changeStringToDate(a);
                            if (startDate >= today)
                            {
                                course.StartDate.Add(a);
                            }
                        }
                        string newStartList = string.Join(",", course.StartDate.ToArray());
                        el.Attribute("startdate").Value = newStartList;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                    Courses.Add(course);
                }
                else if (el.Name == "Discount")
                {
                    this.fees.Single = int.Parse(el.Attribute("single").Value);
                    this.fees.Package = int.Parse(el.Attribute("package").Value);
                    this.fees.Package1 = int.Parse(el.Attribute("package1").Value);
                }
                else if (el.Name == "Fee")
                {
                    this.fees.PaymentPlanFee = int.Parse(el.Attribute("paymentplanfee").Value);
                }

            }
            saveXml();
        }
        public static DateTime changeStringToDate(string s)
        {
            var culture = new CultureInfo("en-AU");
            return DateTime.ParseExact(s, "dd/MM/yyyy", culture);
        }
        public void saveXml()
        {
            xmlFile.Save("courseproperties.xml");
            //do something to save data.
        }

    }
}

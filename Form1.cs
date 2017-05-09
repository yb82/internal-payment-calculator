using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;
using NatCalculator.DataType;

namespace NatCalculator
{

    public partial class resultBox : Form
    {

        private ArrayList calculatedResult;
        private DataHandler dHandler;
        public const string FORMAT = "dd/MM/yyyy";
        DateTime now = DateTime.Now;
        protected List<string> coursename;
        protected List<DateTime> startDate;
        private string today;
        private static string oldCourse1 = null, oldCourse2 = null, oldCourse3 = null, oldCourse4 = null;
        private int paymentOption;
        //private static string newCourse1 = null, newCourse2 = null, newCourse3 = null, newCourse4 = null;
        //  private static List<string> newCourse = new List<string>();
        //private static string oldFinishedDate1 = null, oldFinishedDate2 = null, oldFinishedDate3 = null, oldFinishedDate4 = null;
        //private static string newFinishedDate1 = null, newFinishedDate2 = null, newFinishedDate3 = null, newFinishedDate4 = null;

        internal DataHandler DHandler
        {
            get { return dHandler; }
            set { dHandler = value; }
        }
        const int MaxCourse = 6;
        public resultBox()
        {

            InitializeComponent();
            //MessageBox.Show("");
            dHandler = new DataHandler();

            this.addItemToSelectView();

            courseLists.View = View.List;
            selectedList.View = View.List;

            courseLists.MultiSelect = false;
            selectedList.MultiSelect = false;

            //courseLists.Padding.Top=10;

            tabControl1.TabPages[0].Text = "Paymentplan Calculator";
            option1.Checked = true;



            resultView.View = View.Details;


            ColumnHeader due_date = new ColumnHeader();
            ColumnHeader item_description = new ColumnHeader();
            ColumnHeader amount = new ColumnHeader();
            ColumnHeader courseNameHeader = new ColumnHeader();

            due_date.Width = 350;
            due_date.Text = "Course";

            item_description.Width = 100;
            item_description.Text = "Start Date";

            amount.Text = "Finish Date";
            amount.Width = 100;

            


            resultView.Columns.AddRange(new ColumnHeader[] { due_date, item_description, amount});

            //string[] strArrGroups = new string[3] { "FIRST", "SECOND", "THIRD" };
            //string[] strArrItems = new string[4] { "uno", "dos", "twa", "quad" };
            //for (int i = 0; i < strArrGroups.Length; i++)
            //{
            //    int groupIndex = resultView.Groups.Add(new ListViewGroup(strArrGroups[i], HorizontalAlignment.Left));
            //    for (int j = 0; j < strArrItems.Length; j++)
            //    {
            //        ListViewItem lvi = new ListViewItem(strArrItems[j]);
            //        ListViewItem lvi1 = new ListViewItem(strArrItems[j]+"hello");
            //        lvi.SubItems.Add("Hasta la Vista, Mon Cherri!");
            //        resultView.Items.Add(lvi);
            //        resultView.Items.Add(lvi1);
            //        resultView.Groups[i].Items.Add(lvi);
            //    }
            //} 
            //ListViewGroup group1 = new ListViewGroup("hello");

            //lvi.SubItems.Add("hello2123213123123");
            //lvi.SubItems.Add("hlloo11111111111111111111111111");
            //resultView.Groups.Add(lvg);
            //resultView.Groups[0].Items.Insert(0,lvi);

            //resultView.Items.Add(lvi);
            this.today = this.now.ToString(FORMAT);
            //studentInformation = new StudentDetails() ;


        }


        private void addItemToSelectView()
        {
            var countCourseLists = courseLists.Items.Count;
            var countSeletedLists = selectedList.Items.Count;

            if (countCourseLists > 0)
                courseLists.Clear();
            if (selectedList.Items.Count > 0)
                selectedList.Clear();

            foreach (Course course in dHandler.Courses)
            {
                var lvi = new ListViewItem(course.CourseName);

                courseLists.Items.Add(lvi);

            }

        }
        #region UI Control
        private void button_Print_Click(object sender, EventArgs e)
        {

            resultView.FitToPage = true;
            resultView.Title = "ALS VET Course Payment Detail";
            resultView.Print();

        }
        private void changeStartFinishDate1st()
        {
            if (StartDate1st.Text.Length == 10 && selectedList.Items.Count != 0)
            {
                //Course course = (Course)this.dHandler.Courses[0];
                int duration = findDuration(selectedList.Items[0].Text);
                if (this.checkDateFormat(StartDate1st.Text) && duration != 0)
                {

                    fisnishBox1st.Text = makeFinishDate(StartDate1st.Text, duration);
                }
            }
        }
        private void changeStartFinishDate2nd()
        {
            if (StartDate2nd.Text.Length == 10 && selectedList.Items.Count > 1)
            {
                int duration = findDuration(selectedList.Items[1].Text);
                if (this.checkDateFormat(StartDate2nd.Text) && duration != 0)
                {

                    finishDate2nd.Text = makeFinishDate(StartDate2nd.Text, duration);
                }
            }

        }
        private void changeStartFinishDate3rd()
        {
            if (StartDate3rd.Text.Length == 10 && selectedList.Items.Count > 2)
            {
                int duration = findDuration(selectedList.Items[2].Text);
                if (this.checkDateFormat(StartDate3rd.Text) && duration != 0)
                {

                    finishDate3rd.Text = makeFinishDate(StartDate3rd.Text, duration);
                }
            }

        }
        private void changeStartFinishDate4th()
        {
            if (StartDate4th.Text.Length == 10 && selectedList.Items.Count > 3)
            {
                int duration = findDuration(selectedList.Items[3].Text);
                if (this.checkDateFormat(StartDate4th.Text) && duration != 0)
                {

                    finishDate4th.Text = makeFinishDate(StartDate4th.Text, duration);
                }
            }

        }
        private void changeStartFinishDate5th()
        {
            if (StartDate5th.Text.Length == 10 && selectedList.Items.Count > 4)
            {
                int duration = findDuration(selectedList.Items[4].Text);
                if (this.checkDateFormat(StartDate5th.Text) && duration != 0)
                {

                    finishDate5th.Text = makeFinishDate(StartDate5th.Text, duration);
                }
            }

        }
        private void changeStartFinishDate6th()
        {
            if (StartDate6th.Text.Length == 10 && selectedList.Items.Count > 5)
            {
                int duration = findDuration(selectedList.Items[5].Text);
                if (this.checkDateFormat(StartDate6th.Text) && duration != 0)
                {

                    finishDate6th.Text = makeFinishDate(StartDate6th.Text, duration);
                }
            }

        }
        
     
        private void button_select_Click(object sender, EventArgs e)
        {
            if (courseLists.SelectedItems.Count > 0 && selectedList.Items.Count < MaxCourse)
            {
                string selectedItem = courseLists.SelectedItems[0].Text;
                selectedList.Items.Add(selectedItem);
                courseLists.Items.Remove(courseLists.SelectedItems[0]);


            }

            this.check_selectedItemStatus();
            changeAllStartDate();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            if (selectedList.SelectedItems.Count > 0)
            {
                courseLists.Items.Add(selectedList.SelectedItems[0].Text);
                selectedList.Items.Remove(selectedList.SelectedItems[0]);

            }
            this.check_selectedItemStatus();
            changeAllStartDate();
        }


        private void button_moveUp_Click(object sender, EventArgs e)
        {
            if (selectedList.SelectedItems.Count > 0)
            {
                var currentIndex = selectedList.SelectedItems[0].Index;
                var item = selectedList.Items[currentIndex];
                if (currentIndex > 0)
                {
                    selectedList.Items.RemoveAt(currentIndex);
                    selectedList.Items.Insert(currentIndex - 1, item);
                }

            }
            this.check_selectedItemStatus();
            changeAllStartDate();


        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            if (selectedList.SelectedItems.Count > 0)
            {

                var currentIndex = selectedList.SelectedItems[0].Index;
                var item = selectedList.Items[currentIndex];
                if (currentIndex < selectedList.Items.Count - 1)
                {
                    selectedList.Items.RemoveAt(currentIndex);
                    selectedList.Items.Insert(currentIndex + 1, item);
                }
            }
            this.check_selectedItemStatus();
            changeAllStartDate();
        }
        private void check_selectedItemStatus()
        {
            // bool flagToChangeCombobox1 =false, flagToChangeCombobox2 =false, flagToChangeCombobox3 =false, flagToChangeCombobox4 =false;
            //flagToChangeCombobox1= flagToChangeCombobox2 = flagToChangeCombobox3 =flagToChangeCombobox4 = false;
            var count = selectedList.Items.Count;
            label_2nd.Visible = label_3rd.Visible = label_4th.Visible = false;
            StartDate2nd.Visible = StartDate3rd.Visible = StartDate4th.Visible = label_2ndFinish.Visible = finishDate2nd.Visible =
                label_3rdFinish.Visible = finishDate3rd.Visible =
                label_4thFinish.Visible = finishDate4th.Visible =
                label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = false;
            label_5th.Visible = StartDate5th.Visible = label_5thFinish.Visible = finishDate5th.Visible = false;
            label_6th.Visible = StartDate6th.Visible = label_6thFinish.Visible = finishDate6th.Visible = false;





            switch (count)
            {

                case 1:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    option3.Visible = false;
                    option3.Checked = false;
                    // 

                    break;

                case 2:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    label_2nd.Visible = StartDate2nd.Visible = label_2ndFinish.Visible = finishDate2nd.Visible = true;
                    option3.Visible = true;


                    break;
                case 3:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    label_2nd.Visible = StartDate2nd.Visible = label_2ndFinish.Visible = finishDate2nd.Visible = true;
                    label_3rd.Visible = StartDate3rd.Visible = label_3rdFinish.Visible = finishDate3rd.Visible = true;
                    option3.Visible = true;


                    break;
                case 4:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    label_2nd.Visible = StartDate2nd.Visible = label_2ndFinish.Visible = finishDate2nd.Visible = true;
                    label_3rd.Visible = StartDate3rd.Visible = label_3rdFinish.Visible = finishDate3rd.Visible = true;
                    label_4th.Visible = StartDate4th.Visible = label_4thFinish.Visible = finishDate4th.Visible = true;
                    option3.Visible = true;
                    break;

                case 5:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    label_2nd.Visible = StartDate2nd.Visible = label_2ndFinish.Visible = finishDate2nd.Visible = true;
                    label_3rd.Visible = StartDate3rd.Visible = label_3rdFinish.Visible = finishDate3rd.Visible = true;
                    label_4th.Visible = StartDate4th.Visible = label_4thFinish.Visible = finishDate4th.Visible = true;
                    label_5th.Visible = StartDate5th.Visible = label_5thFinish.Visible = finishDate5th.Visible = true;
                    option3.Visible = true;
                    break;
                case 6:
                    label_1st.Visible = StartDate1st.Visible = label_1stFinish.Visible = fisnishBox1st.Visible = true;
                    label_2nd.Visible = StartDate2nd.Visible = label_2ndFinish.Visible = finishDate2nd.Visible = true;
                    label_3rd.Visible = StartDate3rd.Visible = label_3rdFinish.Visible = finishDate3rd.Visible = true;
                    label_4th.Visible = StartDate4th.Visible = label_4thFinish.Visible = finishDate4th.Visible = true;
                    label_5th.Visible = StartDate5th.Visible = label_5thFinish.Visible = finishDate5th.Visible = true;
                    label_6th.Visible = StartDate6th.Visible = label_6thFinish.Visible = finishDate6th.Visible = true;
                    option3.Visible = true;
                    break;
                // changeAllStartDate();

            }


            //changeStartDateComboBoxes();

        }
        private void changeStartDateComboBox1()
        {
            List<string> startdates;
            string courseName;

            DateTime finishedDate = now;
            if (StartDate1st.Visible)
            {
                courseName = selectedList.Items[0].Text;
                startdates = findStartDates(courseName);

                foreach (string start in startdates)
                {
                    StartDate1st.Items.Add(start);
                }
            }
        }
        private void changeStartDateComboBox2()
        {
            List<string> startdates;
            string courseName;
            DateTime finishedDate = now;

            if (StartDate2nd.Visible)
            {



                courseName = selectedList.Items[1].Text;
                startdates = findStartDates(courseName);
                if (fisnishBox1st.Text != "" && fisnishBox1st.Text != null)
                {
                    finishedDate = DataHandler.changeStringToDate(fisnishBox1st.Text);
                    startdates = createStartDateSet(finishedDate, startdates);
                }


                foreach (string start in startdates)
                {


                    StartDate2nd.Items.Add(start);
                }

            }
        }
        private void changeStartDateComboBox3()
        {
            List<string> startdates;
            string courseName;
            DateTime finishedDate = now;

            if (StartDate3rd.Visible)
            {


                courseName = selectedList.Items[2].Text;
                startdates = findStartDates(courseName);

                if (finishDate2nd.Text != "" && finishDate2nd.Text != null)
                {


                    finishedDate = DataHandler.changeStringToDate(finishDate2nd.Text);
                    startdates = createStartDateSet(finishedDate, startdates);
                }
                foreach (string start in startdates)
                {
                    StartDate3rd.Items.Add(start);
                }
            }
        }
        private void changeStartDateComboBox4()
        {
            List<string> startdates;
            string courseName;
            DateTime finishedDate = now;

            if (StartDate4th.Visible)
            {
                courseName = selectedList.Items[3].Text;
                startdates = findStartDates(courseName);

                if (finishDate3rd.Text != "" && finishDate3rd.Text != null)
                {

                    finishedDate = DataHandler.changeStringToDate(finishDate3rd.Text);
                    startdates = createStartDateSet(finishedDate, startdates);
                }
                foreach (string start in startdates)
                {
                    StartDate4th.Items.Add(start);
                }
            }

        }
        private void changeStartDateComboBox5()
        {
            List<string> startdates;
            string courseName;
            DateTime finishedDate = now;

            if (StartDate5th.Visible)
            {
                courseName = selectedList.Items[4].Text;
                startdates = findStartDates(courseName);

                if (finishDate4th.Text != "" && finishDate4th.Text != null)
                {

                    finishedDate = DataHandler.changeStringToDate(finishDate4th.Text);
                    startdates = createStartDateSet(finishedDate, startdates);
                }
                foreach (string start in startdates)
                {
                    StartDate5th.Items.Add(start);
                }
            }

        }
        private void changeStartDateComboBox6()
        {
            List<string> startdates;
            string courseName;
            DateTime finishedDate = now;

            if (StartDate6th.Visible)
            {
                courseName = selectedList.Items[5].Text;
                startdates = findStartDates(courseName);

                if (finishDate5th.Text != "" && finishDate5th.Text != null)
                {

                    finishedDate = DataHandler.changeStringToDate(finishDate5th.Text);
                    startdates = createStartDateSet(finishedDate, startdates);
                }
                foreach (string start in startdates)
                {
                    StartDate6th.Items.Add(start);
                }
            }

        }
        private void emptyOptions()
        {

            StartDate1st.Text = StartDate2nd.Text = StartDate3rd.Text = StartDate4th.Text =
            fisnishBox1st.Text = finishDate2nd.Text = finishDate3rd.Text = finishDate4th.Text = string.Empty;
            StartDate5th.Text = StartDate6th.Text = finishDate5th.Text = finishDate6th.Text = string.Empty;
            option1.Checked = true;
            option2.Checked = false;
            option3.Checked = false;
            optionLatin.Checked = false;
            option3.Visible = false;

            chbEnrol.Checked = chbEnrolPercent.Checked = chbFStudent.Checked = chbMat.Checked = chbMatPercent.Checked = chbPayment.Checked = false;
            txbEnrolPercent.Text = txbMatPercent.Text = "";

        }
        private void Reset_Click(object sender, EventArgs e)
        {
            this.addItemToSelectView();
            this.check_selectedItemStatus();
            this.emptyOptions();

            resultView.Items.Clear();

        }


        private void StartDayBox1st_TextChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate1st();
        }

        private void StartDate2nd_TextChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate2nd();

        }

        private void StartDate3rd_TextChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate3rd();
        }

        private void StartDate4th_TextChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate4th();
        }
        private void StartDate1st_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate1st();

        }

        private void StartDate2nd_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate2nd();


        }

        private void StartDate3rd_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate3rd();

        }

        private void StartDate4th_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate4th();

        }

        private void fisnishBox1st_TextChanged(object sender, EventArgs e)
        {

        }

        private void finishDate2nd_TextChanged(object sender, EventArgs e)
        {

        }

        private void finishDate4th_TextChanged(object sender, EventArgs e)
        {

        }

        private void finishDate3rd_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        private int findDuration(string courseName)
        {

            foreach (Course a in DHandler.Courses)
            {
                if (a.CourseName == courseName)
                {
                    return a.Duration;
                }
            }

            return 0;

        }
        private List<string> findStartDates(string courseName)
        {
            List<string> resultList = new List<string>();
            foreach (Course a in DHandler.Courses)
            {
                if (a.CourseName == courseName)
                {
                    foreach (string startdate in a.StartDate)
                    {
                        resultList.Add(startdate);
                    }
                }
            }

            return resultList;
        }

        // need to improve logic..... idiot!!

        private void changeAllStartDate()
        {

            switch (selectedList.Items.Count)
            {
                case 1:

                    // new course added.. this will add start dates
                    //if (oldCourse1 == null)
                    //{
                    //    changeStartDateComboBox1();


                    //}
                    //// need to update start dates in start date list. if it is not the same as old course. will change the start date list.
                    //if (oldCourse1 != selectedList.Items[0].Text && oldCourse1 != null)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;
                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    break;

                case 2:
                    // check the first course and it may be change the order or something happen to added list. this will check all item from the top and change the start list.
                    //if (oldCourse1 != selectedList.Items[0].Text)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;

                    //if (oldCourse2 == null )
                    //{
                    //    changeStartDateComboBox2();


                    //}
                    //if (oldCourse2 != selectedList.Items[1].Text && oldCourse2 != null )
                    //{
                    //    StartDate2nd.Items.Clear();
                    //    changeStartDateComboBox2();
                    //}

                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    StartDate2nd.Items.Clear();
                    changeStartDateComboBox2();
                    // oldCourse2 = selectedList.Items[1].Text;


                    break;
                case 3:

                    //if (oldCourse1 != selectedList.Items[0].Text)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;


                    //if (oldCourse2 != selectedList.Items[1].Text)
                    //{
                    //    StartDate2nd.Items.Clear();
                    //    changeStartDateComboBox2();
                    //}
                    //oldCourse2 = selectedList.Items[1].Text;


                    //if (oldCourse3 == null)
                    //{
                    //    changeStartDateComboBox3();


                    //}
                    //if (oldCourse3 != selectedList.Items[2].Text && oldCourse3 != null)
                    //{
                    //    StartDate3rd.Items.Clear();
                    //    changeStartDateComboBox3();
                    //}
                    //oldCourse3 = selectedList.Items[2].Text;
                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    StartDate2nd.Items.Clear();
                    changeStartDateComboBox2();
                    StartDate3rd.Items.Clear();
                    changeStartDateComboBox3();
                    break;
                case 4:

                    //if (oldCourse1 != selectedList.Items[0].Text)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;


                    //if (oldCourse2 != selectedList.Items[1].Text)
                    //{
                    //    StartDate2nd.Items.Clear();
                    //    changeStartDateComboBox2();
                    //}
                    //oldCourse2 = selectedList.Items[1].Text;


                    //if (oldCourse3 != selectedList.Items[2].Text)
                    //{
                    //    StartDate3rd.Items.Clear();
                    //    changeStartDateComboBox3();
                    //}
                    //oldCourse3 = selectedList.Items[2].Text;
                    //if (oldCourse4 == null)
                    //{
                    //    changeStartDateComboBox4();

                    //}
                    //if (oldCourse4 != selectedList.Items[3].Text && oldCourse4 != null)
                    //{
                    //    StartDate4th.Items.Clear();
                    //    changeStartDateComboBox4();
                    //}
                    //oldCourse4 = selectedList.Items[3].Text;
                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    StartDate2nd.Items.Clear();
                    changeStartDateComboBox2();
                    StartDate3rd.Items.Clear();
                    changeStartDateComboBox3();
                    StartDate4th.Items.Clear();
                    changeStartDateComboBox4();
                    break;
                case 5:
                    //if (oldCourse1 != selectedList.Items[0].Text)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;


                    //if (oldCourse2 != selectedList.Items[1].Text)
                    //{
                    //    StartDate2nd.Items.Clear();
                    //    changeStartDateComboBox2();
                    //}
                    //oldCourse2 = selectedList.Items[1].Text;


                    //if (oldCourse3 != selectedList.Items[2].Text)
                    //{
                    //    StartDate3rd.Items.Clear();
                    //    changeStartDateComboBox3();
                    //}
                    //oldCourse3 = selectedList.Items[2].Text;

                    //if (oldCourse4 != selectedList.Items[3].Text)
                    //{
                    //    StartDate4th.Items.Clear();
                    //    changeStartDateComboBox4();
                    //}
                    //oldCourse4 = selectedList.Items[3].Text;
                    //if (oldCourse5 == null)
                    //{
                    //    changeStartDateComboBox5();

                    //}
                    //if (oldCourse5 != selectedList.Items[4].Text && oldCourse5 != null)
                    //{
                    //    StartDate4th.Items.Clear();
                    //    changeStartDateComboBox5();
                    //}
                    //oldCourse5 = selectedList.Items[4].Text;

                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    StartDate2nd.Items.Clear();
                    changeStartDateComboBox2();
                    StartDate3rd.Items.Clear();
                    changeStartDateComboBox3();
                    StartDate4th.Items.Clear();
                    changeStartDateComboBox4();
                    StartDate5th.Items.Clear();
                    changeStartDateComboBox5();
                    break;
                case 6:
                    //if (oldCourse1 != selectedList.Items[0].Text)
                    //{
                    //    StartDate1st.Items.Clear();
                    //    changeStartDateComboBox1();
                    //}
                    //oldCourse1 = selectedList.Items[0].Text;


                    //if (oldCourse2 != selectedList.Items[1].Text)
                    //{
                    //    StartDate2nd.Items.Clear();
                    //    changeStartDateComboBox2();
                    //}
                    //oldCourse2 = selectedList.Items[1].Text;


                    //if (oldCourse3 != selectedList.Items[2].Text)
                    //{
                    //    StartDate3rd.Items.Clear();
                    //    changeStartDateComboBox3();
                    //}
                    //oldCourse3 = selectedList.Items[2].Text;

                    //if (oldCourse4 != selectedList.Items[3].Text)
                    //{
                    //    StartDate4th.Items.Clear();
                    //    changeStartDateComboBox4();
                    //}
                    //oldCourse4 = selectedList.Items[3].Text;
                    //if (oldCourse5 != selectedList.Items[4].Text)
                    //{
                    //    StartDate5th.Items.Clear();
                    //    changeStartDateComboBox5();
                    //}
                    //oldCourse5 = selectedList.Items[4].Text;

                    //if (oldCourse6 == null)
                    //{
                    //    changeStartDateComboBox6();

                    //}
                    //if (oldCourse6 != selectedList.Items[5].Text && oldCourse6 != null)
                    //{
                    //    StartDate6th.Items.Clear();
                    //    changeStartDateComboBox6();
                    //}
                    //oldCourse6 = selectedList.Items[5].Text;
                    StartDate1st.Items.Clear();
                    changeStartDateComboBox1();
                    StartDate2nd.Items.Clear();
                    changeStartDateComboBox2();
                    StartDate3rd.Items.Clear();
                    changeStartDateComboBox3();
                    StartDate4th.Items.Clear();
                    changeStartDateComboBox4();
                    StartDate5th.Items.Clear();
                    changeStartDateComboBox5();
                    StartDate6th.Items.Clear();
                    changeStartDateComboBox6();

                    break;

            }
            //   bool startDateflag1 = false, startDateflag2 = false, startDateflag3 = false;

            //else
            //{
            //    StartDate1st.Items.Clear();
            //    StartDate2nd.Items.Clear();
            //    StartDate3rd.Items.Clear();
            //    StartDate4th.Items.Clear();
            //}

            //newFinishedDate1 = fisnishBox1st.Text;
            //newFinishedDate2 = finishDate2nd.Text;
            //newFinishedDate3 = finishDate3rd.Text;
            //newFinishedDate4 = finishDate4th.Text;
            //if (oldFinishedDate1 == null || oldFinishedDate1 != newFinishedDate1)
            //{
            //    startDateflag1 = true;

            //}
            //if (oldFinishedDate2 == null || oldFinishedDate2 != newFinishedDate2)
            //{
            //    startDateflag2 = true;
            //}
            //if (oldFinishedDate3 == null || oldFinishedDate3 != newFinishedDate3)
            //{
            //    startDateflag3 = true;
            //}
            




        }



        private List<string> createStartDateSet(DateTime finishDate, List<string> startlist)
        {
            List<string> result = new List<string>();
            DateTime temp;
            foreach (string start in startlist)
            {
                temp = DataHandler.changeStringToDate(start);
                if (temp >= finishDate)
                {
                    result.Add(temp.ToString("dd/MM/yyyy", new CultureInfo("en-AU")));
                }
            }

            return result;

        }



        private bool checkDateFormat(String s)
        {

            DateTime fromDateValue;
            var formats = new[] { "dd/MM/yyyy" };

            bool flag = true;
            if (s == null || s == "")
            {
                MessageBox.Show(this, "Start Date is Empty!!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
            }
            else
            {
                if (!DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                {
                    MessageBox.Show(this, "Date Format is invalid! dd/MM/yyyy", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                    // do for valid date
                }
            }
            return flag;

        }

        private DateTime changeStringToDate(string s)
        {
            var culture = new CultureInfo("en-AU");
            return DateTime.ParseExact(s, "dd/MM/yyyy", culture);
        }

        private List<ListViewItem> makeCourseDetails(List<string> courseList)
        {

            List<ListViewItem> output = new List<ListViewItem>();
            ListViewItem items;
            if (StartDate1st.Visible)
            {
                items = new ListViewItem(courseList[0]);
                items.SubItems.Add(StartDate1st.Text);
                items.SubItems.Add(fisnishBox1st.Text);
                output.Add(items);

            }
            if (StartDate2nd.Visible)
            {
                items = new ListViewItem(courseList[1]);
                items.SubItems.Add(StartDate2nd.Text);
                items.SubItems.Add(finishDate2nd.Text);
                output.Add(items);
            }
            if (StartDate3rd.Visible)
            {
                items = new ListViewItem(courseList[2]);
                items.SubItems.Add(StartDate3rd.Text);
                items.SubItems.Add(finishDate3rd.Text);
                output.Add(items);
            }
            if (StartDate4th.Visible)
            {
                items = new ListViewItem(courseList[3]);
                items.SubItems.Add(StartDate4th.Text);
                items.SubItems.Add(finishDate4th.Text);
                output.Add(items);
            }
            if (StartDate5th.Visible)
            {
                items = new ListViewItem(courseList[4]);
                items.SubItems.Add(StartDate5th.Text);
                items.SubItems.Add(finishDate5th.Text);
                output.Add(items);
            }
            if (StartDate6th.Visible)
            {
                items = new ListViewItem(courseList[5]);
                items.SubItems.Add(StartDate6th.Text);
                items.SubItems.Add(finishDate6th.Text);
                output.Add(items);
            }
            return output;
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            this.coursename = new List<string>();
            this.startDate = new List<DateTime>();
            /// collect all data,
            bool CheckFlag = true;

            resultView.Items.Clear();

            //ListViewItem lvItemCourse1,lvItemCourse2,lvItemCourse3,lvItemCourse4 ;


            if (selectedList.Items.Count == 0)
            {
                MessageBox.Show(this, "Please Select at least one Course", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CheckFlag = false;

            }

            if (StartDate1st.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate1st.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate1st.Text));


                }
                else { StartDate1st.Focus(); return; }
            }
            if (StartDate2nd.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate2nd.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate2nd.Text));
                }
                else { StartDate2nd.Focus(); return; }
            }
            if (StartDate3rd.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate3rd.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate3rd.Text));
                }
                else { StartDate3rd.Focus(); return; }
            }
            if (StartDate4th.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate4th.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate4th.Text));
                }
                else { StartDate4th.Focus(); return; }
            }
            if (StartDate5th.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate5th.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate5th.Text));
                }
                else { StartDate5th.Focus(); return; }
            }
            if (StartDate6th.Visible)
            {
                CheckFlag = this.checkDateFormat(StartDate6th.Text);
                if (CheckFlag)
                {
                    startDate.Add(this.changeStringToDate(StartDate6th.Text));
                }
                else { StartDate6th.Focus(); return; }
            }


            int enrolmentPercent = 100;
            int matPercent = 100; bool enrolFee; bool matFee;
             if(chbEnrolPercent.Checked){
                 if (string.IsNullOrEmpty(txbEnrolPercent.Text))
                 {
                     MessageBox.Show(this, "Please Enter any Number!!!!!!!!!!!!!!!!!!!!!!!!!!!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     txbEnrolPercent.Focus();
                     CheckFlag = false;
                 } else
                     enrolFee = int.TryParse(txbEnrolPercent.Text, out enrolmentPercent);
                        
             }
            if(chbMatPercent.Checked){
            if( string.IsNullOrEmpty(txbMatPercent.Text)){
                 MessageBox.Show(this, "Please Enter any Number!!!!!!!!!!!!!!!!!!!!!!!!!!!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 txbMatPercent.Focus();
                 CheckFlag = false;
            }         else matFee = int.TryParse( txbMatPercent.Text,out matPercent);

             }

             
                    
            
                    
                 
                        

            if (CheckFlag)
            {


                for (int i = 0; i < selectedList.Items.Count; i++)
                    coursename.Add(selectedList.Items[i].Text);

                paymentOption = 0;
                if (option1.Checked)
                {
                    paymentOption = 1;
                }
                else if (option2.Checked)
                {
                    paymentOption = 2;
                }
                else if (option3.Checked)
                {
                    paymentOption = 3;
                }
                else paymentOption = 4;




                bool fStudent = chbFStudent.Checked;
                bool fEnrolmentFee = chbEnrol.Checked;
                bool fMat = chbMat.Checked;
                bool fPaymentPlanFee = chbPayment.Checked;
                Calculator cal;
                if (chbEnrolPercent.Checked || chbMatPercent.Checked)
                {



                    cal = new Calculator(dHandler, coursename, paymentOption, startDate, fStudent, fEnrolmentFee, fMat, fPaymentPlanFee, enrolmentPercent, matPercent);

                }
                else
                {
                    cal = new Calculator(dHandler, coursename, paymentOption, startDate, fStudent, fEnrolmentFee, fMat, fPaymentPlanFee);
                }
                this.calculatedResult = cal.getResult();

                // get all course name to make group list(in order to remove duplicated course names)

                /// display course start and finish date.
                /// 




                List<string> groupList = new List<string>();
                List<string> paymentDate = new List<string>();

                foreach (OutputSet o in this.calculatedResult)
                {
                    groupList.Add(o.CourseName);
                    paymentDate.Add(o.DueDate);
                }

                List<string> courseGroup = groupList.Distinct().ToList();
                List<string> paymentGroup = paymentDate.Distinct().ToList();



                int groupIndex = resultView.Groups.Add(new ListViewGroup("Course Details", HorizontalAlignment.Left));
                //int counter = 0;

                List<ListViewItem> details = this.makeCourseDetails(courseGroup);


                foreach (ListViewItem lvi in details)
                {
                    resultView.Items.Add(lvi);
                    resultView.Groups[groupIndex].Items.Add(lvi);
                }


                //oupIndex = resultView.Groups.Add(new ListViewGroup(s, HorizontalAlignment.Left));
                List<OutputSet> paymentPlan = new List<OutputSet>();
                foreach (OutputSet o in this.calculatedResult)
                {

                    if (o.ItemDescription == Calculator.TUITION && (paymentPlan.Count > 0))
                    {

                        int paymentCount = paymentPlan.Count;
                        for (int i = 0; i < paymentCount; i++)
                        {


                            if (paymentPlan[i].DueDate == o.DueDate && paymentPlan[i].ItemDescription == Calculator.TUITION && o.ItemDescription == Calculator.TUITION)
                            {
                                paymentPlan[i].Amount = (int.Parse(paymentPlan[i].Amount) + int.Parse(o.Amount)).ToString();

                                break;
                            }
                            else if (paymentCount - 1 == i)
                            {
                                paymentPlan.Add(o);

                            }

                        }
                    }
                    else
                    {
                        paymentPlan.Add(o);
                    }
                }


                groupIndex = resultView.Groups.Add(new ListViewGroup("Payment plan", HorizontalAlignment.Left));

                int remain = 0, currentPayment = 0, totalCourseTuition = 0, courseCnt = 0;
                bool courseChangeFlag = false;
                Course currentCourse = new Course();

                
                foreach (OutputSet o in paymentPlan)
                {

                    ListViewItem lvi = new ListViewItem(o.DueDate);
                    int.TryParse(o.Amount, out currentPayment);

                    if (!courseChangeFlag)
                    {
                        currentCourse = this.findCourseByName(courseGroup[courseCnt]);
                        totalCourseTuition = currentCourse.Tuition;
                        courseCnt++;
                        groupIndex = resultView.Groups.Add(new ListViewGroup(currentCourse.CourseName, HorizontalAlignment.Left));
                        courseChangeFlag = true;
                    }

                    if (o.ItemDescription != Calculator.TUITION)
                    {
                        lvi.SubItems.Add(o.ItemDescription);
                        lvi.SubItems.Add("$" + o.Amount);
                        //lvi.SubItems.Add(o.CourseName);
                        resultView.Items.Add(lvi);
                        resultView.Groups[groupIndex].Items.Add(lvi);


                    }
                    else
                    {
                        if (totalCourseTuition < currentPayment)
                        {
                            
                            lvi.SubItems.Add(o.ItemDescription);
                            lvi.SubItems.Add("$" + (totalCourseTuition).ToString() );
                            //lvi.SubItems.Add(o.CourseName);
                            resultView.Items.Add(lvi);
                            resultView.Groups[groupIndex].Items.Add(lvi);
                            
                            remain = totalCourseTuition -= currentPayment;

                            currentCourse = this.findCourseByName(courseGroup[courseCnt]);
                            totalCourseTuition = currentCourse.Tuition;
                            courseCnt++;
                            groupIndex = resultView.Groups.Add(new ListViewGroup(currentCourse.CourseName, HorizontalAlignment.Left));
                            courseChangeFlag = true;
                            
                            totalCourseTuition += remain;
                            ListViewItem lvi1 = new ListViewItem(o.DueDate);
                            lvi1.SubItems.Add(o.ItemDescription);
                            lvi1.SubItems.Add("$" + (Math.Abs(remain) ).ToString());
                            //lvi.SubItems.Add(o.CourseName);
                            resultView.Items.Add(lvi1);
                            resultView.Groups[groupIndex].Items.Add(lvi1);

                        }
                        else
                        {
                            
                            totalCourseTuition -= currentPayment;
                            if (totalCourseTuition >= 0)
                            {
                                lvi.SubItems.Add(o.ItemDescription);
                                lvi.SubItems.Add("$" + o.Amount);
                                //lvi.SubItems.Add(o.CourseName);
                                resultView.Items.Add(lvi);
                                resultView.Groups[groupIndex].Items.Add(lvi);
                            }
                            
                        }
                        if(totalCourseTuition == 0){
                            courseChangeFlag = false;
                        }


                    }


                }
                double total = 0;
                foreach (OutputSet o in paymentPlan)
                {
                    total += double.Parse(o.Amount);
                }
                ListViewItem totalItem = new ListViewItem("");
                totalItem.SubItems.Add("Total");
                totalItem.SubItems.Add("$" + total.ToString());
                resultView.Items.Add(totalItem);
                resultView.Groups[groupIndex].Items.Add(totalItem);




                //////////////// pass data to calculator class.

                /// for Natalie!!!!!!!!!!!!!!!!!!!!!

                //for (int i = 0; i < selectedList.Items.Count; i++)
                //    coursename.Add(selectedList.Items[i].Text);

                //int paymentOption = 0;
                //if (option1.Checked)
                //{
                //    paymentOption = 1;
                //}
                //else if (option2.Checked)
                //{
                //    paymentOption = 2;
                //}
                //else paymentOption = 3;


                //Calculator cal = new Calculator(dHandler, coursename, paymentOption, startDate);
                //ArrayList result = cal.getResult();

                //// get all course name to make group list(in order to remove duplicated course names)

                //List<string> groupList = new List<string>();
                //foreach (OutputSet o in result)
                //{
                //    groupList.Add(o.CourseName);
                //}
                //List<string> courseGroup = groupList.Distinct().ToList();

                //foreach (string s in courseGroup)
                //{
                //    int groupIndex = resultView.Groups.Add(new ListViewGroup(s, HorizontalAlignment.Left));
                //    foreach (OutputSet o in result)
                //    {
                //        if (s == o.CourseName)
                //        {
                //            ListViewItem lviItem = new ListViewItem(o.DueDate);
                //            lviItem.SubItems.Add(o.ItemDescription);
                //            lviItem.SubItems.Add(o.Amount);
                //            resultView.Items.Add(lviItem);
                //            resultView.Groups[groupIndex].Items.Add(lviItem);
                //        }
                //    }
                //}

                //string[] strArrGroups = new string[3] { "FIRST", "SECOND", "THIRD" };
                //string[] strArrItems = new string[4] { "uno", "dos", "twa", "quad" };
                //for (int i = 0; i < strArrGroups.Length; i++)
                //{
                //    int groupIndex = resultView.Groups.Add(new ListViewGroup(strArrGroups[i], HorizontalAlignment.Left));
                //    for (int j = 0; j < strArrItems.Length; j++)
                //    {
                //        ListViewItem lvi = new ListViewItem(strArrItems[j]);
                //        ListViewItem lvi1 = new ListViewItem(strArrItems[j]+"hello");
                //        lvi.SubItems.Add("Hasta la Vista, Mon Cherri!");
                //        resultView.Items.Add(lvi);
                //        resultView.Items.Add(lvi1);
                //        resultView.Groups[i].Items.Add(lvi);
                //    }
                //} 
                //ListViewGroup group1 = new ListViewGroup("hello");

                //lvi.SubItems.Add("hello2123213123123");
                //lvi.SubItems.Add("hlloo11111111111111111111111111");
                //resultView.Groups.Add(lvg);
                //resultView.Groups[0].Items.Insert(0,lvi);

                //resultView.Items.Add(lvi);
            }
        }


        private Course findCourseByIndex(int index)
        {
            return (Course)this.dHandler.Courses[index];
            
        }
        private Course findCourseByName(string courseName)
        {
            foreach (Course c in this.dHandler.Courses)
            {

                if (c.CourseName == courseName)
                {
                    return c;
                }

            }
            return null;
        }



        private string makeFinishDate(string startDate, int duration)
        {
            DateTime finishDate = this.changeStringToDate(startDate);
            finishDate = finishDate.AddDays(7 * duration - 3);
            string output = finishDate.ToString("dd/MM/yyyy", new CultureInfo("en-AU"));
            return output;
        }


        private void button_CreatPDF_Click(object sender, EventArgs e)
        {
            if (resultView.Items.Count > 0)
            {
                PersonalDetailForm studentInformation = new PersonalDetailForm(this.coursename, this.startDate, this.paymentOption);
                studentInformation.ShowDialog();
            }
        }

        private void StartDate5th_SelectedIndexChanged(object sender, EventArgs e)
        { 
            this.changeStartFinishDate5th();

        

        }

        private void StartDate6th_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate6th();
        }

        private void StartDate5th_TextChanged(object sender, EventArgs e)
        {

            this.changeStartFinishDate5th();
        }

        private void StartDate6th_TextChanged(object sender, EventArgs e)
        {
            this.changeStartFinishDate6th();
        }





    }

}

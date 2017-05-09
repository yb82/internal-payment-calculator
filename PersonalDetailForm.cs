
using iTextSharp.text.pdf;
using NatCalculator.DataType;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NatCalculator
{
    public partial class PersonalDetailForm : Form
    {
        //public StudentDetails studentDetail ;

        private StudentDetails _student;
        private bool nameErr = false, dobErr = false, emailErr = false, genderErr = false, titleErr = false, nationErr = false;
        private string dob;
        private const string NOTSPCIFIED = "Not Specified";
        private string _option;
        protected List<string> _coursename;
        protected List<DateTime> _startDate;
        public PersonalDetailForm(List<string> coursename, List<DateTime> startDate, int option)
        {


            InitializeComponent();
            foreach (string title in StudentDetails.TitleType)
            {
                cbTitle.Items.Add(title);
            }
           
            foreach (string month in StudentDetails.Month)
            {
                cbMM.Items.Add(month);
            }
            _student = new StudentDetails();
            //    aa = new StudentDetails();


            this._coursename = coursename;
            this._startDate = startDate;
            this._option = option.ToString();
        }

        private void collectData()
        {
            if (String.IsNullOrEmpty(cbTitle.Text))
                this.titleErr = true;
            else this._student.Title = cbTitle.Text;

            if (String.IsNullOrEmpty(tbFirstName.Text))
                this.nameErr = true;
            else this._student.FName = tbFirstName.Text;

            if (String.IsNullOrEmpty(tbLastName.Text))
                this.nameErr = true;
            else this._student.LName = tbLastName.Text;

            if (String.IsNullOrEmpty(tbDD.Text))
                this.dobErr = true;
            else this._student.DateOfBirth = tbDD.Text + "/";
            if (String.IsNullOrEmpty(cbMM.Text))
                this.dobErr = true;
            else this._student.DateOfBirth += cbMM.Text + "/";
            if (String.IsNullOrEmpty(tbYYYY.Text))
                this.dobErr = true;
            else this._student.DateOfBirth += tbYYYY.Text;
            

            

            if (String.IsNullOrEmpty(tbNation.Text))
                this.nationErr = true;
            else this._student.Nation = tbNation.Text;

           
            if (String.IsNullOrEmpty(tbPassport.Text))
                this._student.PassportNo = NOTSPCIFIED;
            else this._student.PassportNo = tbPassport.Text;

            if (String.IsNullOrEmpty(tbAddress.Text))
                this._student.CurrentAddr = NOTSPCIFIED;
            else this._student.CurrentAddr = tbAddress.Text;

            if (String.IsNullOrEmpty(tbEmail.Text))
                this.emailErr = true;
            else this._student.Email = tbEmail.Text;

            if (rbInsuranceYes.Checked)
            {
                this._student.Insurance = true;
                if (rbSingle.Checked) { this._student.InsuranceType = "single"; }
                else if (rbCouple.Checked) { this._student.InsuranceType = "couple"; }
                else if (rbFamily.Checked) { this._student.InsuranceType = "family"; }

            }
            else this._student.Insurance = false;










            //return true;
        }
        private bool checkError()
        {
            string errorMsg = "";
            if (titleErr)
                errorMsg = "Please Check Title";
            if (nameErr)
                errorMsg += "Please Check Name";
            if (dobErr)
                errorMsg += "Please Check Date of Birth";
            if (genderErr)
                errorMsg += "Please Check Student Gender";
            if (nationErr) errorMsg += "Please Check Student Nationality";
            if (emailErr) errorMsg += "Please Check Email Address";

            if (titleErr || nationErr || dobErr || genderErr || nationErr || emailErr)
            {
                MessageBox.Show(errorMsg);
                return false;
            }
            return true;


        }
        private void createApplication()
        {


        }
        private void btDone_Click(object sender, EventArgs e)
        {

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Save Application file";

            fileDialog.Filter = "Acrobat Reader (*.pdf)|*.pdf";
            string savefilename = "";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                savefilename = fileDialog.FileName;
            }

            this.collectData();

            if (!checkError())
            {
                return;
            }


            string pdfTemplate = "VET Application Form 2016.pdf";

            //pdfFlat = new MemoryStream();
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            FileStream newFile = new FileStream(savefilename, FileMode.Create);
            PdfStamper pdfStamp = new PdfStamper(pdfReader, newFile);
            AcroFields fields = pdfStamp.AcroFields;


            if (!String.IsNullOrEmpty(this._student.Title))
                fields.SetField("title", this._student.Title);

            if (!String.IsNullOrEmpty(this._student.FName))

                fields.SetField("f.name", this._student.FName);

            
            if (!String.IsNullOrEmpty(this._student.LName))

                fields.SetField("l.name", this._student.LName);
            if (rbMale.Checked)
                fields.SetField("gender", "male");
            else fields.SetField("gender", "female");

            if (!String.IsNullOrEmpty(this._student.DateOfBirth))
                fields.SetField("dob", this._student.DateOfBirth);
            if (!String.IsNullOrEmpty(this._student.PassportNo))
                fields.SetField("passport", this._student.PassportNo);
            if (!String.IsNullOrEmpty(this._student.Nation))
                fields.SetField("country", this._student.Nation);
            if (!String.IsNullOrEmpty(this._student.CurrentAddr))
                fields.SetField("address", this._student.CurrentAddr);
            if (!String.IsNullOrEmpty(this._student.Contact))
                fields.SetField("phone", this._student.Contact);

            if (!String.IsNullOrEmpty(this._student.Email))
                fields.SetField("email", this._student.Email);

            if (!String.IsNullOrEmpty(tbContact.Text))
                fields.SetField("phone", tbContact.Text);
            
            if (rbStudentVisa.Checked)
                fields.SetField("visa", "student");
            else fields.SetField("visa", "other");
            

            fields.SetField("paymentoption", "option"+this._option);
            fields.SetField("dibp", tbDIBP.Text);

            if (this._student.Insurance)
            {
                fields.SetField("insurance", "yes");
                fields.SetField("insurance_type", this._student.InsuranceType);

            }
            else fields.SetField("insurance", "no");

            switch (_coursename.Count)
            {
                case 1:
                    fields.SetField("course1", this._coursename[0]);
                    fields.SetField("start1", this._startDate[0].ToString(resultBox.FORMAT));
                    break;
                case 2:
                    fields.SetField("course1", this._coursename[0]);
                    fields.SetField("course2", this._coursename[1]);
                    fields.SetField("start1", this._startDate[0].ToString(resultBox.FORMAT));
                    fields.SetField("start2", this._startDate[1].ToString(resultBox.FORMAT));
                    break;
                case 3:
                    fields.SetField("course1", this._coursename[0]);
                    fields.SetField("course2", this._coursename[1]);
                    fields.SetField("course3", this._coursename[2]);
                    fields.SetField("start1", this._startDate[0].ToString(resultBox.FORMAT));
                    fields.SetField("start2", this._startDate[1].ToString(resultBox.FORMAT));
                    fields.SetField("start3", this._startDate[2].ToString(resultBox.FORMAT));
                    break;
                case 4:
                    fields.SetField("course1", this._coursename[0]);
                    fields.SetField("course2", this._coursename[1]);
                    fields.SetField("course3", this._coursename[2]);
                    fields.SetField("course4", this._coursename[3]);
                    fields.SetField("start1", this._startDate[0].ToString(resultBox.FORMAT));
                    fields.SetField("start2", this._startDate[1].ToString(resultBox.FORMAT));
                    fields.SetField("start3", this._startDate[2].ToString(resultBox.FORMAT));
                    fields.SetField("start4", this._startDate[3].ToString(resultBox.FORMAT));
                    break;
            }

         
            pdfStamp.FormFlattening = true;
            pdfStamp.FreeTextFlattening = true;
            pdfStamp.Writer.CloseStream = false;
            pdfStamp.Close();
            pdfReader.Close();
            newFile.Close();




        }

        private void rbInsuranceYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInsuranceYes.Checked)

                gbInsType.Visible = true;

            else
                gbInsType.Visible = false;
        }

      
    }
}

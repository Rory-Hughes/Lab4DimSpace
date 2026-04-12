using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4DimSpace
{
    public partial class frmDimSpace : Form
    {
        //Global variables to handle context and logged in user

        public frmDimSpace()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContextData();
            SetInitialFormContents();
        }

        private void LoadContextData()
        {
            //load in contexts
        }

        private void SetInitialFormContents()
        {
            //code to setup initial form look for login

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Code to handle user login
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //Code to handle user logout
        }

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            //code here to create a new assignment dropbox **NOTE** only an INSTRUCTOR should be able to do this!
        }

        private void btnSubmitAssignment_Click(object sender, EventArgs e)
        {
            //code here to submit an existing assignment **NOTE** only a STUDENT should be able to do this!

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Search Code
        }

        private void cboActiveCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Active Course changed
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            //Code to create a new user **NOTE** only an Admin should be able to do this
        }

        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            //Code to assign displayed user to active course **NOTE** only an Admin should be able to do this
        }
    }
}

using Lab4DimSpace.Models;
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
        private DimSpaceContext _context = new DimSpaceContext();
        private User _loggedInUser = null;

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
            var courses = _context.Courses.ToList();
            cboActiveCourse.DataSource = courses;
            cboActiveCourse.DisplayMember = "Name";
            cboActiveCourse.ValueMember = "CourseId";
        }

        private void SetInitialFormContents()
        {
            //code to setup initial form look for login
            // Hide all tabs except login
            tabNavigation.TabPages.Remove(tabAssignments);
            tabNavigation.TabPages.Remove(tabUsers);

            // Make login tab the active tab
            if (!tabNavigation.TabPages.Contains(tabLogin))
            {
                tabNavigation.TabPages.Add(tabLogin);
            }

            // hide top group boxes
            grpLoginInfo.Visible = false;
            grpSearch.Visible = false;
            grpActiveCourse.Visible = false;

            // Clear login fields
            txtUsername.Clear();
            txtPassword.Clear();
            lblUserName.Text = "";
            _loggedInUser = null;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Code to handle user login
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            _loggedInUser = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (_loggedInUser == null)
            {
                // Failed login
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // update login info display
            lblUserName.Text = _loggedInUser.Username;
            grpLoginInfo.Visible = true;
            grpSearch.Visible = true;
            grpActiveCourse.Visible = true;

            // Remove login tab and show appropriate tabs based on role
            tabNavigation.TabPages.Remove(tabLogin);


            if (_loggedInUser.UserRoleId == 1) // Student role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                LoadAssignments();
            }
            else if (_loggedInUser.UserRoleId == 2) // Instructor role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                tabNavigation.TabPages.Add(tabUsers);
                LoadAssignments();
                LoadUsers();

            }
            else if (_loggedInUser.UserRoleId == 3) // Admin role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                LoadAssignments();
            }
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //Code to handle user logout
            SetInitialFormContents();
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
            if (tabNavigation.SelectedTab == tabAssignments)
                LoadAssignments(txtSearch.Text);
            else if (tabNavigation.SelectedTab == tabUsers)
                LoadUsers(txtSearch.Text);
        }

        private void cboActiveCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Active Course changed
            if (_loggedInUser == null) return;

            if (tabNavigation.TabPages.Contains(tabAssignments))
                LoadAssignments();
            if (tabNavigation.TabPages.Contains(tabUsers))
                LoadUsers();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            //Code to create a new user **NOTE** only an Admin should be able to do this
        }

        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            //Code to assign displayed user to active course **NOTE** only an Admin should be able to do this
        }


        private void LoadAssignments(string search = "")
        {
            int courseId = (int)cboActiveCourse.SelectedValue;

            if (_loggedInUser.UserRoleId == 1) // Student sees only their own
            {
                var query = _context.DropBoxItems
                    .Where(d => d.DropBox.CourseId == courseId && d.StudentId == _loggedInUser.UserId)
                    .Select(d => new
                    {
                        d.DropBox.DropBoxId,
                        d.DropBox.Name,
                        d.DropBox.Description,
                        d.DropBox.DueDate,
                        Status = d.Status.StatusName
                    });

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(d => d.Name.Contains(search) || d.Description.Contains(search));

                dgvAssignments.DataSource = query.ToList();
            }
            else // Instructor sees all assignments for the course
            {
                var query = _context.DropBoxes
                    .Where(d => d.CourseId == courseId)
                    .Select(d => new
                    {
                        d.DropBoxId,
                        d.Name,
                        d.Description,
                        d.DueDate
                    });

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(d => d.Name.Contains(search) || d.Description.Contains(search));

                dgvAssignments.DataSource = query.ToList();
            }
        }




        private void LoadUsers(string search = "")
        {
            int courseId = (int)cboActiveCourse.SelectedValue;

            var query = _context.CourseAccesses
                .Where(ca => ca.CourseId == courseId)
                .Select(ca => new
                {
                    ca.User.UserId,
                    ca.User.Username,
                    ca.User.Email
                });

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.Username.Contains(search));

            dgvUsers.DataSource = query.ToList();
        }

    }
}



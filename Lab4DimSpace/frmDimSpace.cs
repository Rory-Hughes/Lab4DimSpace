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

            if (_loggedInUser != null)
            {
                // Successful login
                MessageBox.Show($"Welcome, {_loggedInUser.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetupFormForUserRole();
            }
            else
            {
                // Failed login
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // update login info display
            lblUserName.Text = _loggedInUser != null ? $"Logged in as: {_loggedInUser.Username} ({_loggedInUser.UserRole.Name})" : "Not logged in";
            grpLoginInfo.Visible = _loggedInUser != null;
            grpSearch.Visible = _loggedInUser != null;
            grpActiveCourse.Visible = _loggedInUser != null;

            // Remove login tab and show appropriate tabs based on role
            tabNavigation.TabPages.Remove(tabLogin);

            
            if (_loggedInUser.UserRoleId == 1) // Student role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                LoadAssignments();
            } else if (_loggedInUser.UserRoleId == 2) // Instructor role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                LoadAssignments();
            } else if (_loggedInUser.UserRoleId == 3) // Admin role
            {
                tabNavigation.TabPages.Add(tabAssignments);
                tabNavigation.TabPages.Add(tabUsers);
                LoadAssignments();
                LoadUsers();

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

        private void LoadAssignments(string search = "")
        {
            //Code to load assignments for the active course
            int courseId = (int)cboActiveCourse.SelectedValue;

            if (_loggedInUser.UserRoleId == 1) // Student
            {
                var assignments = _context.DropBoxItems
                    .Where(d => d.DropBox.CourseId == courseId && d.StudentId == _loggedInUser.UserId && (d.DropBox.Name.Contains(search) || d.Description.Contains(search)))
                    .Select(d => new
                    {
                        d.DropBoxItemId,
                        d.DropBox.Name,
                        d.Description,
                        Status = d.Status.StatusName
                    })
                    .ToList();
                dgvAssignments.DataSource = assignments;
            }
            else if (_loggedInUser.UserRoleId == 2) // Instructor
            {
                var assignments = _context.DropBoxItems
                    .Where(d => d.DropBox.CourseId == courseId && (d.DropBox.Name.Contains(search) || d.Description.Contains(search)))
                    .Select(d => new
                    {
                        d.DropBoxItemId,
                        d.DropBox.Name,
                        d.Description,
                        Student = d.Student.Username,
                        Status = d.Status.StatusName
                    })
                    .ToList();
                dgvAssignments.DataSource = assignments;
            }
             else if (_loggedInUser.UserRoleId == 3) // Admin
             {
                 var assignments = _context.DropBoxItems
                     .Where(d => d.DropBox.CourseId == courseId && (d.DropBox.Name.Contains(search) || d.Description.Contains(search)))
                     .Select(d => new
                     {
                         d.DropBoxItemId,
                         d.DropBox.Name,
                         d.Description,
                         Student = d.Student.Username,
                         Status = d.Status.StatusName
                     })
                     .ToList();
                 dgvAssignments.DataSource = assignments;
            }
        }
    }
}

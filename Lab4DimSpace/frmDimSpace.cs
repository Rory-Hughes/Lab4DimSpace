using Lab4DimSpace.Models;
using Microsoft.EntityFrameworkCore;
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
                tabNavigation.TabPages.Add(tabUsers);
                LoadUsers();
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
            if (_loggedInUser.UserRoleId != 2)
                return;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtAssignmentName.Text))
            {
                MessageBox.Show("Please enter an assignment name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int courseId = (int)cboActiveCourse.SelectedValue;

            // Create the new DropBox record
            var newDropBox = new DropBox
            {
                CourseId = courseId,
                Name = txtAssignmentName.Text.Trim(),
                Description = txtAssignmentDescription.Text.Trim(),
                DueDate = DateOnly.FromDateTime(dtpDueDate.Value)
            };

            _context.DropBoxes.Add(newDropBox);
            _context.SaveChanges(); // Save first so newDropBox gets its DropBoxId assigned

            // Find all students enrolled in the active course
            var enrolledStudents = _context.CourseAccesses
                .Where(ca => ca.CourseId == courseId && ca.User.UserRoleId == 1)
                .Select(ca => ca.UserId)
                .ToList();

            // Create a DropBoxItem for each student with StatusId = 1 (Released)
            foreach (var studentId in enrolledStudents)
            {
                var item = new DropBoxItem
                {
                    DropBoxId = newDropBox.DropBoxId,
                    StudentId = studentId,
                    StatusId = 1 // Released
                };
                _context.DropBoxItems.Add(item);
            }

            _context.SaveChanges();

            MessageBox.Show($"Assignment created and released to {enrolledStudents.Count} student(s).",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the form fields
            txtAssignmentName.Clear();
            txtAssignmentDescription.Clear();
            dtpDueDate.Value = DateTime.Now;

            // Refresh the assignments grid
            LoadAssignments();
        }

        private void btnSubmitAssignment_Click(object sender, EventArgs e)
        {
            //code here to submit an existing assignment **NOTE** only a STUDENT should be able to do this!
            if (_loggedInUser.UserRoleId != 1) return; // Students only

            // Make sure a row is selected
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an assignment to submit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the DropBoxId from the selected row
            int dropBoxId = (int)dgvAssignments.SelectedRows[0].Cells["DropBoxId"].Value;

            // Find this student's DropBoxItem
            var item = _context.DropBoxItems
                .FirstOrDefault(d => d.DropBoxId == dropBoxId && d.StudentId == _loggedInUser.UserId);

            if (item == null)
            {
                MessageBox.Show("Assignment record not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if already submitted or graded
            if (item.StatusId != 1) // 1 = Released
            {
                MessageBox.Show("This assignment has already been submitted.", "Already Submitted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update status to Submitted (StatusId = 2)
            item.StatusId = 2;
            _context.SaveChanges();

            MessageBox.Show("Assignment submitted successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh the grid to show updated status
            LoadAssignments();

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
            if (_loggedInUser.UserRoleId != 3) return; // Admins only

            // Validate input
            if (string.IsNullOrWhiteSpace(txtUsers.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Determine role from radio buttons
            int roleId;
            if (radStudent.Checked)
                roleId = 1;
            else if (radInstructor.Checked)
                roleId = 2;
            else
            {
                MessageBox.Show("Please select a role (Student or Instructor).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtUsers.Text.Trim();

            // Check if username already exists
            bool userExists = _context.Users.Any(u => u.Username == username);
            if (userExists)
            {
                MessageBox.Show("That username already exists. Please choose another.", "Duplicate User",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Auto-format email based on role (matching the database pattern)
            // Students: username@mymbcc.ca  |  Instructors: username@mbcc.ca
            string email = roleId == 1
                ? $"{username}@mymbcc.ca"
                : $"{username}@mbcc.ca";

            // Create the new user (password defaults to username, matching seed data pattern)
            var newUser = new User
            {
                Username = username,
                Password = username,
                Email = email,
                UserRoleId = roleId
            };

            _context.Users.Add(newUser);
            _context.SaveChanges(); // Save to generate the UserId

            // Enroll the new user in the active course
            int courseId = (int)cboActiveCourse.SelectedValue;

            var courseAccess = new CourseAccess
            {
                UserId = newUser.UserId,
                CourseId = courseId
            };

            _context.CourseAccesses.Add(courseAccess);
            _context.SaveChanges();

            MessageBox.Show($"User '{username}' created and enrolled in the active course.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the input field and refresh the grid
            txtUsers.Clear();
            txtEmail.Text = email; // Show the generated email so admin can see it
            LoadUsers();
        }

        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            //Code to assign displayed user to active course **NOTE** only an Admin should be able to do this
            if (_loggedInUser.UserRoleId != 3) return; // Admins only

            // Make sure a username is populated
            if (string.IsNullOrWhiteSpace(txtUsers.Text))
            {
                MessageBox.Show("Please select a user from the list first.", "No User Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Look up the user by username
            var selectedUser = _context.Users
                .FirstOrDefault(u => u.Username == txtUsers.Text.Trim());

            if (selectedUser == null)
            {
                MessageBox.Show("User not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int courseId = (int)cboActiveCourse.SelectedValue;

            // Check if user is already enrolled in this course
            bool alreadyEnrolled = _context.CourseAccesses
                .Any(ca => ca.UserId == selectedUser.UserId && ca.CourseId == courseId);

            if (alreadyEnrolled)
            {
                MessageBox.Show($"'{selectedUser.Username}' is already enrolled in this course.",
                    "Already Enrolled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Assign user to the active course
            var courseAccess = new CourseAccess
            {
                UserId = selectedUser.UserId,
                CourseId = courseId
            };

            _context.CourseAccesses.Add(courseAccess);
            _context.SaveChanges();

            MessageBox.Show($"'{selectedUser.Username}' has been assigned to the active course.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the selection and refresh
            txtUsers.Clear();
            LoadUsers();
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

                grpCreateAssignment.Visible = false;
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

                btnSubmitAssignment.Visible = false;
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

            if (_loggedInUser.UserRoleId == 2) grpUserManagement.Visible = false;
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            txtUsers.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();

        }
    }
}
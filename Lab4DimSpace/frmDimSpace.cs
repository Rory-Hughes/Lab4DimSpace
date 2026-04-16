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
        // EF Core database context used for all database operations throughout the form
        private DimSpaceContext _context = new DimSpaceContext();

        // Stores the currently logged-in user. Null when no user is logged in.
        // Used throughout to check role-based access and identify the active user.
        private User _loggedInUser = null;

        public frmDimSpace()
        {
            InitializeComponent();
        }

        // Fires when the form first loads.
        // Courses must be loaded before SetInitialFormContents so the combo box
        // is populated even though it's hidden at startup.
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContextData();
            SetInitialFormContents();
        }

        // Loads all courses from the database into the Active Course combo box.
        // CourseId is used as the underlying value; Name is shown to the user.
        private void LoadContextData()
        {
            try
            {
                var courses = _context.Courses.ToList();
                cboActiveCourse.DataSource = courses;
                cboActiveCourse.DisplayMember = "Name";
                cboActiveCourse.ValueMember = "CourseId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load course data. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Resets the form to its pre-login state.
        // Called both on initial load and when the user logs out.
        // Removes all role-specific tabs, hides the top group boxes,
        // clears input fields, and nulls out the logged-in user.
        private void SetInitialFormContents()
        {
            // Remove role-specific tabs, leaving only the Login tab
            tabNavigation.TabPages.Remove(tabAssignments);
            tabNavigation.TabPages.Remove(tabUsers);

            // Re-add the Login tab if it was previously removed (e.g. after logout)
            if (!tabNavigation.TabPages.Contains(tabLogin))
                tabNavigation.TabPages.Add(tabLogin);

            // Hide the top group boxes — these are only relevant when logged in
            grpLoginInfo.Visible = false;
            grpSearch.Visible = false;
            grpActiveCourse.Visible = false;

            // Clear any leftover credentials and reset the user state
            txtUsername.Clear();
            txtPassword.Clear();
            lblUserName.Text = "";
            _loggedInUser = null;
        }

        // Handles user login. Validates input, authenticates against the database,
        // then shows the appropriate tabs and loads data based on the user's role:
        //   Role 1 (Student)       → Assignments tab only
        //   Role 2 (Instructor)    → Assignments tab + Users tab
        //   Role 3 (Administrator) → Users tab only
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate that both fields are filled before querying the database
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both a username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Attempt to find a matching user record in the database
                _loggedInUser = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                // No match found — notify the user and abort
                if (_loggedInUser == null)
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update the login info group box with the authenticated user's name
                lblUserName.Text = _loggedInUser.Username;
                grpLoginInfo.Visible = true;
                grpSearch.Visible = true;
                grpActiveCourse.Visible = true;

                // Remove the login tab now that the user is authenticated
                tabNavigation.TabPages.Remove(tabLogin);

                // Show tabs and load data based on the user's role
                if (_loggedInUser.UserRoleId == 1) // Student
                {
                    tabNavigation.TabPages.Add(tabAssignments);
                    LoadAssignments();
                }
                else if (_loggedInUser.UserRoleId == 2) // Instructor
                {
                    tabNavigation.TabPages.Add(tabAssignments);
                    tabNavigation.TabPages.Add(tabUsers);
                    LoadAssignments();
                    LoadUsers();
                }
                else if (_loggedInUser.UserRoleId == 3) // Administrator
                {
                    tabNavigation.TabPages.Add(tabUsers);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logs the user out by resetting the form back to its initial state.
        // SetInitialFormContents handles clearing all data and nulling _loggedInUser.
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SetInitialFormContents();
        }

        // Creates a new assignment (DropBox) for the active course and automatically
        // generates a DropBoxItem record with status "Released" for every student
        // currently enrolled in that course. Restricted to Instructors only.
        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            // only instructors should reach this logic
            if (_loggedInUser.UserRoleId != 2) return;

            // Validate all input fields before touching the database
            if (string.IsNullOrWhiteSpace(txtAssignmentName.Text))
            {
                MessageBox.Show("Please enter an assignment name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAssignmentDescription.Text))
            {
                MessageBox.Show("Please enter an assignment description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent assignments from being created with a past due date
            if (dtpDueDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int courseId = (int)cboActiveCourse.SelectedValue;

                // Build the new DropBox record from the form inputs
                var newDropBox = new DropBox
                {
                    CourseId = courseId,
                    Name = txtAssignmentName.Text.Trim(),
                    Description = txtAssignmentDescription.Text.Trim(),
                    DueDate = DateOnly.FromDateTime(dtpDueDate.Value)
                };

                // Save the DropBox first — needed to generate DropBoxId for the items below
                _context.DropBoxes.Add(newDropBox);
                _context.SaveChanges();

                // Find all students enrolled in the active course
                var enrolledStudents = _context.CourseAccesses
                    .Where(ca => ca.CourseId == courseId && ca.User.UserRoleId == 1)
                    .Select(ca => ca.UserId)
                    .ToList();

                // Create a DropBoxItem for each enrolled student with StatusId = 1 (Released)
                foreach (var studentId in enrolledStudents)
                {
                    _context.DropBoxItems.Add(new DropBoxItem
                    {
                        DropBoxId = newDropBox.DropBoxId,
                        StudentId = studentId,
                        StatusId = 1 // Released
                    });
                }

                // Save all the DropBoxItems in one batch
                _context.SaveChanges();

                MessageBox.Show($"Assignment created and released to {enrolledStudents.Count} student(s).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the input fields and refresh the grid
                txtAssignmentName.Clear();
                txtAssignmentDescription.Clear();
                dtpDueDate.Value = DateTime.Now;
                LoadAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create assignment. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Allows a student to submit a selected assignment.
        // Finds their DropBoxItem record and updates its status from
        // Released (1) to Submitted (2). Restricted to Students only.
        private void btnSubmitAssignment_Click(object sender, EventArgs e)
        {
            // Role guard — only students should reach this logic
            if (_loggedInUser.UserRoleId != 1) return;

            // Ensure the student has actually selected a row in the grid
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an assignment to submit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Retrieve the DropBoxId from the selected row in the DataGridView
                int dropBoxId = (int)dgvAssignments.SelectedRows[0].Cells["DropBoxId"].Value;

                // Find the student's specific DropBoxItem for this assignment
                var item = _context.DropBoxItems
                    .FirstOrDefault(d => d.DropBoxId == dropBoxId && d.StudentId == _loggedInUser.UserId);

                // Guard against a missing record (shouldn't happen, but just incase)
                if (item == null)
                {
                    MessageBox.Show("Assignment record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Prevent re-submitting an already submitted or graded assignment
                if (item.StatusId != 1) // 1 = Released
                {
                    MessageBox.Show("This assignment has already been submitted.", "Already Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Update the status to Submitted and save
                item.StatusId = 2; // Submitted
                _context.SaveChanges();

                MessageBox.Show("Assignment submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the grid so the updated status is reflected immediately
                LoadAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to submit assignment. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fires as the user types in the search box.
        // Dynamically filters the active tab's DataGridView in real time.
        // Delegates to LoadAssignments or LoadUsers depending on which tab is active.
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (tabNavigation.SelectedTab == tabAssignments)
                LoadAssignments(txtSearch.Text);
            else if (tabNavigation.SelectedTab == tabUsers)
                LoadUsers(txtSearch.Text);
        }

        // Fires when the user selects a different course from the combo box.
        // Reloads the DataGridView to show records for the newly selected course.
        // Guard against firing before login (when _loggedInUser is null).
        private void cboActiveCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loggedInUser == null) return;

            if (tabNavigation.TabPages.Contains(tabAssignments))
                LoadAssignments();
            if (tabNavigation.TabPages.Contains(tabUsers))
                LoadUsers();
        }

        // Creates a new user account and immediately enrolls them in the active course.
        // Email is auto-generated based on role to match the database naming convention:
        //   Students    → username@mymbcc.ca
        //   Instructors → username@mbcc.ca
        // Password defaults to the username, matching the seed data pattern.
        // Restricted to Administrators only.
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            // only admins should reach this logic
            if (_loggedInUser.UserRoleId != 3) return;

            // Validate that a username has been entered
            if (string.IsNullOrWhiteSpace(txtUsers.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Determine the role from the radio buttons before querying the database
            int roleId;
            if (radStudent.Checked)
                roleId = 1;
            else if (radInstructor.Checked)
                roleId = 2;
            else
            {
                MessageBox.Show("Please select a role (Student or Instructor).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string username = txtUsers.Text.Trim();

                // Check for duplicate usernames before attempting to insert
                bool userExists = _context.Users.Any(u => u.Username == username);
                if (userExists)
                {
                    MessageBox.Show("That username already exists. Please choose another.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Auto-generate email based on role to match the database convention
                string email = roleId == 1 ? $"{username}@mymbcc.ca" : $"{username}@mbcc.ca";

                // Build and save the new User record
                var newUser = new User
                {
                    Username = username,
                    Password = username, // Default password matches username per seed data convention
                    Email = email,
                    UserRoleId = roleId
                };

                _context.Users.Add(newUser);
                _context.SaveChanges(); // Save first to generate the UserId for CourseAccess below

                // Immediately enroll the new user in the currently active course
                int courseId = (int)cboActiveCourse.SelectedValue;
                _context.CourseAccesses.Add(new CourseAccess
                {
                    UserId = newUser.UserId,
                    CourseId = courseId
                });

                _context.SaveChanges();

                MessageBox.Show($"User '{username}' created and enrolled in the active course.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Show the generated email and refresh the users grid
                txtUsers.Clear();
                txtEmail.Text = email;
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create user. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Assigns an existing user (selected from the grid) to the active course.
        // Prevents duplicate enrollments by checking CourseAccess before inserting.
        // Restricted to Administrators only.
        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            // only admins should reach this logic
            if (_loggedInUser.UserRoleId != 3) return;

            // txtUsers is populated by clicking a row in dgvUsers — validate it's set
            if (string.IsNullOrWhiteSpace(txtUsers.Text))
            {
                MessageBox.Show("Please select a user from the list first.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Look up the user by the username shown in txtUsers
                var selectedUser = _context.Users
                    .FirstOrDefault(u => u.Username == txtUsers.Text.Trim());

                if (selectedUser == null)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int courseId = (int)cboActiveCourse.SelectedValue;

                // Prevent duplicate enrollments in the same course
                bool alreadyEnrolled = _context.CourseAccesses
                    .Any(ca => ca.UserId == selectedUser.UserId && ca.CourseId == courseId);

                if (alreadyEnrolled)
                {
                    MessageBox.Show($"'{selectedUser.Username}' is already enrolled in this course.", "Already Enrolled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create the CourseAccess record to link the user to the course
                _context.CourseAccesses.Add(new CourseAccess
                {
                    UserId = selectedUser.UserId,
                    CourseId = courseId
                });

                _context.SaveChanges();

                MessageBox.Show($"'{selectedUser.Username}' has been assigned to the active course.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the selection and refresh the grid
                txtUsers.Clear();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to assign user to course. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Loads assignments into dgvAssignments filtered by the active course.
        // Behavior differs by role:
        //   Student    → sees only their own DropBoxItems including submission status
        //   Instructor → sees all DropBox records for the course without status column
        // An optional search string filters by Name and Description.
        private void LoadAssignments(string search = "")
        {
            try
            {
                int courseId = (int)cboActiveCourse.SelectedValue;

                if (_loggedInUser.UserRoleId == 1) // Student view
                {
                    // Join DropBoxItems to DropBox to get assignment details,
                    // filtering to only this student's records for the active course
                    var query = _context.DropBoxItems
                        .Where(d => d.DropBox.CourseId == courseId && d.StudentId == _loggedInUser.UserId)
                        .Select(d => new
                        {
                            d.DropBox.DropBoxId,
                            d.DropBox.Name,
                            d.DropBox.Description,
                            d.DropBox.DueDate,
                            Status = d.Status.StatusName // Show human-readable status
                        });

                    // Apply search filter if the user has entered a search term
                    if (!string.IsNullOrEmpty(search))
                        query = query.Where(d => d.Name.Contains(search) || d.Description.Contains(search));

                    dgvAssignments.DataSource = query.ToList();

                    // Students cannot create assignments — hide the creation group box
                    grpCreateAssignment.Visible = false;
                }
                else // Instructor view
                {
                    // Instructors see all assignments for the course without status
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

                    // Instructors cannot submit assignments — hide the submit button
                    btnSubmitAssignment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load assignments. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Loads all users enrolled in the active course into dgvUsers.
        // Instructors see this tab as read-only (grpUserManagement is hidden for them).
        // Administrators see the full management controls.
        // An optional search string filters by Username.
        private void LoadUsers(string search = "")
        {
            try
            {
                int courseId = (int)cboActiveCourse.SelectedValue;

                // Join CourseAccess to Users to get the enrolled user details
                var query = _context.CourseAccesses
                    .Where(ca => ca.CourseId == courseId)
                    .Select(ca => new
                    {
                        ca.User.UserId,
                        ca.User.Username,
                        ca.User.Email
                    });

                // Apply search filter if the user has entered a search term
                if (!string.IsNullOrEmpty(search))
                    query = query.Where(u => u.Username.Contains(search));

                dgvUsers.DataSource = query.ToList();

                // Instructors can view users but cannot manage them — hide the management controls
                if (_loggedInUser.UserRoleId == 2) grpUserManagement.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load users. Details: { ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Fires when a row is clicked in the Users DataGridView.
        // Populates txtUsers with the selected user's username so it can be
        // used by btnAssignUser to identify who should be enrolled.
        // Ignores clicks on the header row (RowIndex < 0).
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtUsers.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();
        }
    }
}




namespace Lab4DimSpace
{
    partial class frmDimSpace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpLoginInfo = new GroupBox();
            btnLogOut = new Button();
            lblUserName = new Label();
            label4 = new Label();
            grpSearch = new GroupBox();
            txtSearch = new TextBox();
            cboActiveCourse = new ComboBox();
            grpActiveCourse = new GroupBox();
            tabAssignments = new TabPage();
            btnSubmitAssignment = new Button();
            grpCreateAssignment = new GroupBox();
            txtAssignmentDescription = new TextBox();
            txtAssignmentName = new TextBox();
            dtpDueDate = new DateTimePicker();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            btnCreateAssignment = new Button();
            dgvAssignments = new DataGridView();
            tabUsers = new TabPage();
            btnAssignUser = new Button();
            groupBox1 = new GroupBox();
            radStudent = new RadioButton();
            radInstructor = new RadioButton();
            label5 = new Label();
            label3 = new Label();
            txtEmail = new TextBox();
            txtUsers = new TextBox();
            btnCreateUser = new Button();
            dgvUsers = new DataGridView();
            UserId = new DataGridViewTextBoxColumn();
            Username = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            tabLogin = new TabPage();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            tabNavigation = new TabControl();
            grpUserManagement = new GroupBox();
            grpLoginInfo.SuspendLayout();
            grpSearch.SuspendLayout();
            grpActiveCourse.SuspendLayout();
            tabAssignments.SuspendLayout();
            grpCreateAssignment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).BeginInit();
            tabUsers.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            tabLogin.SuspendLayout();
            tabNavigation.SuspendLayout();
            grpUserManagement.SuspendLayout();
            SuspendLayout();
            // 
            // grpLoginInfo
            // 
            grpLoginInfo.Controls.Add(btnLogOut);
            grpLoginInfo.Controls.Add(lblUserName);
            grpLoginInfo.Controls.Add(label4);
            grpLoginInfo.Location = new Point(677, 12);
            grpLoginInfo.Name = "grpLoginInfo";
            grpLoginInfo.Size = new Size(537, 104);
            grpLoginInfo.TabIndex = 17;
            grpLoginInfo.TabStop = false;
            grpLoginInfo.Text = "Login";
            // 
            // btnLogOut
            // 
            btnLogOut.Location = new Point(418, 65);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(113, 33);
            btnLogOut.TabIndex = 2;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(55, 34);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(0, 20);
            lblUserName.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 34);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 0;
            label4.Text = "Hello, ";
            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(txtSearch);
            grpSearch.Location = new Point(344, 12);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(310, 104);
            grpSearch.TabIndex = 18;
            grpSearch.TabStop = false;
            grpSearch.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(0, 37);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Insert search terms here";
            txtSearch.Size = new Size(287, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // cboActiveCourse
            // 
            cboActiveCourse.DisplayMember = "CourseId";
            cboActiveCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboActiveCourse.FormattingEnabled = true;
            cboActiveCourse.Location = new Point(34, 37);
            cboActiveCourse.Name = "cboActiveCourse";
            cboActiveCourse.Size = new Size(248, 28);
            cboActiveCourse.TabIndex = 19;
            cboActiveCourse.ValueMember = "CourseId";
            cboActiveCourse.SelectedIndexChanged += cboActiveCourse_SelectedIndexChanged;
            // 
            // grpActiveCourse
            // 
            grpActiveCourse.Controls.Add(cboActiveCourse);
            grpActiveCourse.Location = new Point(7, 12);
            grpActiveCourse.Name = "grpActiveCourse";
            grpActiveCourse.Size = new Size(317, 98);
            grpActiveCourse.TabIndex = 19;
            grpActiveCourse.TabStop = false;
            grpActiveCourse.Text = "Active Course";
            // 
            // tabAssignments
            // 
            tabAssignments.Controls.Add(btnSubmitAssignment);
            tabAssignments.Controls.Add(grpCreateAssignment);
            tabAssignments.Controls.Add(dgvAssignments);
            tabAssignments.Location = new Point(4, 29);
            tabAssignments.Name = "tabAssignments";
            tabAssignments.Size = new Size(1203, 517);
            tabAssignments.TabIndex = 3;
            tabAssignments.Text = "Assignments";
            tabAssignments.UseVisualStyleBackColor = true;
            // 
            // btnSubmitAssignment
            // 
            btnSubmitAssignment.Location = new Point(967, 425);
            btnSubmitAssignment.Name = "btnSubmitAssignment";
            btnSubmitAssignment.Size = new Size(211, 57);
            btnSubmitAssignment.TabIndex = 1;
            btnSubmitAssignment.Text = "Submit Assignment";
            btnSubmitAssignment.UseVisualStyleBackColor = true;
            btnSubmitAssignment.Click += btnSubmitAssignment_Click;
            // 
            // grpCreateAssignment
            // 
            grpCreateAssignment.Controls.Add(txtAssignmentDescription);
            grpCreateAssignment.Controls.Add(txtAssignmentName);
            grpCreateAssignment.Controls.Add(dtpDueDate);
            grpCreateAssignment.Controls.Add(label10);
            grpCreateAssignment.Controls.Add(label9);
            grpCreateAssignment.Controls.Add(label8);
            grpCreateAssignment.Controls.Add(btnCreateAssignment);
            grpCreateAssignment.Location = new Point(949, 6);
            grpCreateAssignment.Name = "grpCreateAssignment";
            grpCreateAssignment.Size = new Size(251, 319);
            grpCreateAssignment.TabIndex = 2;
            grpCreateAssignment.TabStop = false;
            grpCreateAssignment.Text = "Create Assignment";
            // 
            // txtAssignmentDescription
            // 
            txtAssignmentDescription.Location = new Point(6, 151);
            txtAssignmentDescription.Multiline = true;
            txtAssignmentDescription.Name = "txtAssignmentDescription";
            txtAssignmentDescription.Size = new Size(239, 62);
            txtAssignmentDescription.TabIndex = 22;
            // 
            // txtAssignmentName
            // 
            txtAssignmentName.Location = new Point(64, 30);
            txtAssignmentName.Name = "txtAssignmentName";
            txtAssignmentName.Size = new Size(181, 27);
            txtAssignmentName.TabIndex = 19;
            // 
            // dtpDueDate
            // 
            dtpDueDate.Location = new Point(6, 89);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(239, 27);
            dtpDueDate.TabIndex = 19;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 128);
            label10.Name = "label10";
            label10.Size = new Size(88, 20);
            label10.TabIndex = 21;
            label10.Text = "Description:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 66);
            label9.Name = "label9";
            label9.Size = new Size(75, 20);
            label9.TabIndex = 20;
            label9.Text = "Due Date:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 33);
            label8.Name = "label8";
            label8.Size = new Size(52, 20);
            label8.TabIndex = 19;
            label8.Text = "Name:";
            // 
            // btnCreateAssignment
            // 
            btnCreateAssignment.Location = new Point(18, 241);
            btnCreateAssignment.Name = "btnCreateAssignment";
            btnCreateAssignment.Size = new Size(211, 57);
            btnCreateAssignment.TabIndex = 0;
            btnCreateAssignment.Text = "Create Assignment";
            btnCreateAssignment.UseVisualStyleBackColor = true;
            btnCreateAssignment.Click += btnCreateAssignment_Click;
            // 
            // dgvAssignments
            // 
            dgvAssignments.AllowUserToAddRows = false;
            dgvAssignments.AllowUserToDeleteRows = false;
            dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvAssignments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAssignments.Location = new Point(6, 6);
            dgvAssignments.Name = "dgvAssignments";
            dgvAssignments.ReadOnly = true;
            dgvAssignments.RowHeadersWidth = 51;
            dgvAssignments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAssignments.Size = new Size(937, 505);
            dgvAssignments.TabIndex = 1;
            // 
            // tabUsers
            // 
            tabUsers.Controls.Add(grpUserManagement);
            tabUsers.Controls.Add(dgvUsers);
            tabUsers.Location = new Point(4, 29);
            tabUsers.Name = "tabUsers";
            tabUsers.Size = new Size(1203, 517);
            tabUsers.TabIndex = 2;
            tabUsers.Text = "Users";
            tabUsers.UseVisualStyleBackColor = true;
            // 
            // btnAssignUser
            // 
            btnAssignUser.Location = new Point(8, 370);
            btnAssignUser.Name = "btnAssignUser";
            btnAssignUser.Size = new Size(220, 37);
            btnAssignUser.TabIndex = 10;
            btnAssignUser.Text = "Assign User to Active Course";
            btnAssignUser.UseVisualStyleBackColor = true;
            btnAssignUser.Click += btnAssignUser_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radStudent);
            groupBox1.Controls.Add(radInstructor);
            groupBox1.Location = new Point(8, 200);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(220, 96);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Role:";
            // 
            // radStudent
            // 
            radStudent.AutoSize = true;
            radStudent.Checked = true;
            radStudent.Location = new Point(21, 26);
            radStudent.Name = "radStudent";
            radStudent.Size = new Size(81, 24);
            radStudent.TabIndex = 5;
            radStudent.TabStop = true;
            radStudent.Text = "Student";
            radStudent.UseVisualStyleBackColor = true;
            // 
            // radInstructor
            // 
            radInstructor.AutoSize = true;
            radInstructor.Location = new Point(21, 56);
            radInstructor.Name = "radInstructor";
            radInstructor.Size = new Size(92, 24);
            radInstructor.TabIndex = 6;
            radInstructor.Text = "Instructor";
            radInstructor.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 123);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 8;
            label5.Text = "Email:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 45);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 7;
            label3.Text = "Username:";
            // 
            // txtEmail
            // 
            txtEmail.Enabled = false;
            txtEmail.Location = new Point(8, 146);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(220, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtUsers
            // 
            txtUsers.Location = new Point(8, 68);
            txtUsers.Name = "txtUsers";
            txtUsers.Size = new Size(220, 27);
            txtUsers.TabIndex = 3;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Location = new Point(8, 317);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(220, 37);
            btnCreateUser.TabIndex = 2;
            btnCreateUser.Text = "Create User";
            btnCreateUser.UseVisualStyleBackColor = true;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { UserId, Username, Email });
            dgvUsers.Location = new Point(6, 6);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(937, 505);
            dgvUsers.TabIndex = 1;
            // 
            // UserId
            // 
            UserId.DataPropertyName = "UserId";
            UserId.HeaderText = "UserId";
            UserId.MinimumWidth = 6;
            UserId.Name = "UserId";
            UserId.ReadOnly = true;
            UserId.Width = 80;
            // 
            // Username
            // 
            Username.DataPropertyName = "Username";
            Username.HeaderText = "Username";
            Username.MinimumWidth = 6;
            Username.Name = "Username";
            Username.ReadOnly = true;
            Username.Width = 104;
            // 
            // Email
            // 
            Email.DataPropertyName = "Email";
            Email.HeaderText = "Email";
            Email.MinimumWidth = 6;
            Email.Name = "Email";
            Email.ReadOnly = true;
            Email.Width = 75;
            // 
            // tabLogin
            // 
            tabLogin.Controls.Add(txtPassword);
            tabLogin.Controls.Add(txtUsername);
            tabLogin.Controls.Add(btnLogin);
            tabLogin.Controls.Add(label1);
            tabLogin.Controls.Add(label2);
            tabLogin.Location = new Point(4, 29);
            tabLogin.Name = "tabLogin";
            tabLogin.Padding = new Padding(3);
            tabLogin.Size = new Size(1203, 517);
            tabLogin.TabIndex = 0;
            tabLogin.Text = "Login";
            tabLogin.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(518, 175);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(149, 27);
            txtPassword.TabIndex = 9;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(518, 134);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(149, 27);
            txtUsername.TabIndex = 8;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(518, 237);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(149, 39);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(425, 137);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 6;
            label1.Text = "Username:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(425, 178);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 7;
            label2.Text = "Password:";
            // 
            // tabNavigation
            // 
            tabNavigation.Controls.Add(tabLogin);
            tabNavigation.Controls.Add(tabUsers);
            tabNavigation.Controls.Add(tabAssignments);
            tabNavigation.Location = new Point(3, 122);
            tabNavigation.Name = "tabNavigation";
            tabNavigation.SelectedIndex = 0;
            tabNavigation.Size = new Size(1211, 550);
            tabNavigation.TabIndex = 16;
            // 
            // grpUserManagement
            // 
            grpUserManagement.Controls.Add(label3);
            grpUserManagement.Controls.Add(btnAssignUser);
            grpUserManagement.Controls.Add(txtUsers);
            grpUserManagement.Controls.Add(btnCreateUser);
            grpUserManagement.Controls.Add(groupBox1);
            grpUserManagement.Controls.Add(label5);
            grpUserManagement.Controls.Add(txtEmail);
            grpUserManagement.Location = new Point(949, 23);
            grpUserManagement.Name = "grpUserManagement";
            grpUserManagement.Size = new Size(250, 422);
            grpUserManagement.TabIndex = 11;
            grpUserManagement.TabStop = false;
            grpUserManagement.Text = "User Management";
            // 
            // frmDimSpace
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1219, 671);
            Controls.Add(grpActiveCourse);
            Controls.Add(grpSearch);
            Controls.Add(grpLoginInfo);
            Controls.Add(tabNavigation);
            Name = "frmDimSpace";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DimSpace";
            Load += Form1_Load;
            grpLoginInfo.ResumeLayout(false);
            grpLoginInfo.PerformLayout();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpActiveCourse.ResumeLayout(false);
            tabAssignments.ResumeLayout(false);
            grpCreateAssignment.ResumeLayout(false);
            grpCreateAssignment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).EndInit();
            tabUsers.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            tabLogin.ResumeLayout(false);
            tabLogin.PerformLayout();
            tabNavigation.ResumeLayout(false);
            grpUserManagement.ResumeLayout(false);
            grpUserManagement.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpLoginInfo;
        private Button btnLogOut;
        private Label lblUserName;
        private Label label4;
        private GroupBox grpSearch;
        private TextBox txtSearch;
        private ComboBox cboActiveCourse;
        private GroupBox grpActiveCourse;
        private TabPage tabAssignments;
        private Button btnSubmitAssignment;
        private GroupBox grpCreateAssignment;
        private TextBox txtAssignmentDescription;
        private TextBox txtAssignmentName;
        private DateTimePicker dtpDueDate;
        private Label label10;
        private Label label9;
        private Label label8;
        private Button btnCreateAssignment;
        private DataGridView dgvAssignments;
        private TabPage tabUsers;
        private DataGridView dgvUsers;
        private TabPage tabLogin;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnLogin;
        private Label label1;
        private Label label2;
        private TabControl tabNavigation;
        private DataGridViewTextBoxColumn dropBoxIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dueDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn UserId;
        private DataGridViewTextBoxColumn Username;
        private DataGridViewTextBoxColumn Email;
        private Label label5;
        private Label label3;
        private RadioButton radInstructor;
        private RadioButton radStudent;
        private TextBox txtEmail;
        private TextBox txtUsers;
        private Button btnCreateUser;
        private Button btnAssignUser;
        private GroupBox groupBox1;
        private GroupBox grpUserManagement;
    }
}
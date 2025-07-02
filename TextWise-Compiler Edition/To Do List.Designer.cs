namespace To_Do_List_App
{
    partial class ToDoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToDoList));
            this.panel = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.toDoListView = new System.Windows.Forms.DataGridView();
            this.dgvtitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvdescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.descriptionTextbox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toDoListView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.BackColor = System.Drawing.Color.Navy;
            this.panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel.BackgroundImage")));
            this.panel.Controls.Add(this.searchTextBox);
            this.panel.Controls.Add(this.search);
            this.panel.Controls.Add(this.dateTimePicker1);
            this.panel.Controls.Add(this.toDoListView);
            this.panel.Controls.Add(this.deleteButton);
            this.panel.Controls.Add(this.saveButton);
            this.panel.Controls.Add(this.editButton);
            this.panel.Controls.Add(this.newButton);
            this.panel.Controls.Add(this.descriptionTextbox);
            this.panel.Controls.Add(this.titleTextBox);
            this.panel.ForeColor = System.Drawing.Color.Blue;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1109, 747);
            this.panel.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(157, 263);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // toDoListView
            // 
            this.toDoListView.AllowUserToAddRows = false;
            this.toDoListView.AllowUserToDeleteRows = false;
            this.toDoListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.toDoListView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.toDoListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.toDoListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvtitle,
            this.dgvdescription,
            this.dgvtime});
            this.toDoListView.Location = new System.Drawing.Point(147, 384);
            this.toDoListView.Name = "toDoListView";
            this.toDoListView.ReadOnly = true;
            this.toDoListView.RowHeadersWidth = 51;
            this.toDoListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toDoListView.Size = new System.Drawing.Size(789, 297);
            this.toDoListView.TabIndex = 7;
            this.toDoListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.toDoListView_CellClick);
            // 
            // dgvtitle
            // 
            this.dgvtitle.DataPropertyName = "title";
            this.dgvtitle.HeaderText = "Title";
            this.dgvtitle.Name = "dgvtitle";
            this.dgvtitle.ReadOnly = true;
            // 
            // dgvdescription
            // 
            this.dgvdescription.DataPropertyName = "description";
            this.dgvdescription.HeaderText = "Description";
            this.dgvdescription.Name = "dgvdescription";
            this.dgvdescription.ReadOnly = true;
            // 
            // dgvtime
            // 
            this.dgvtime.DataPropertyName = "time";
            this.dgvtime.HeaderText = "Time";
            this.dgvtime.Name = "dgvtime";
            this.dgvtime.ReadOnly = true;
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.SteelBlue;
            this.deleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deleteButton.BackgroundImage")));
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.SystemColors.Window;
            this.deleteButton.Location = new System.Drawing.Point(536, 305);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.SteelBlue;
            this.saveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveButton.BackgroundImage")));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.SystemColors.Window;
            this.saveButton.Location = new System.Drawing.Point(708, 305);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.SteelBlue;
            this.editButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("editButton.BackgroundImage")));
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.ForeColor = System.Drawing.SystemColors.Window;
            this.editButton.Location = new System.Drawing.Point(343, 305);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "edit";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // newButton
            // 
            this.newButton.BackColor = System.Drawing.Color.SteelBlue;
            this.newButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("newButton.BackgroundImage")));
            this.newButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newButton.ForeColor = System.Drawing.SystemColors.Window;
            this.newButton.Location = new System.Drawing.Point(157, 305);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 3;
            this.newButton.Text = "new";
            this.newButton.UseVisualStyleBackColor = false;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // descriptionTextbox
            // 
            this.descriptionTextbox.Location = new System.Drawing.Point(157, 206);
            this.descriptionTextbox.Name = "descriptionTextbox";
            this.descriptionTextbox.Size = new System.Drawing.Size(789, 20);
            this.descriptionTextbox.TabIndex = 2;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(157, 156);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(789, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // search
            // 
            this.search.BackColor = System.Drawing.Color.SteelBlue;
            this.search.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("search.BackgroundImage")));
            this.search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search.ForeColor = System.Drawing.SystemColors.Window;
            this.search.Location = new System.Drawing.Point(708, 262);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 9;
            this.search.Text = "search";
            this.search.UseVisualStyleBackColor = false;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(389, 263);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(300, 20);
            this.searchTextBox.TabIndex = 10;
            // 
            // ToDoList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1093, 708);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToDoList";
            this.Text = "To Do List";
            this.Load += new System.EventHandler(this.ToDoList_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toDoListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox descriptionTextbox;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.DataGridView toDoListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvdescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtime;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.TextBox searchTextBox;
    }
}
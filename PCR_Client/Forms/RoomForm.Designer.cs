namespace PCR_Client.Forms
{
    partial class RoomForm
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
            roomName_textBox = new TextBox();
            secretKey_textBox = new TextBox();
            Apply_button = new Button();
            label1 = new Label();
            username_textBox = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // roomName_textBox
            // 
            roomName_textBox.Location = new Point(12, 74);
            roomName_textBox.Name = "roomName_textBox";
            roomName_textBox.Size = new Size(194, 21);
            roomName_textBox.TabIndex = 0;
            // 
            // secretKey_textBox
            // 
            secretKey_textBox.Location = new Point(12, 201);
            secretKey_textBox.Name = "secretKey_textBox";
            secretKey_textBox.Size = new Size(194, 21);
            secretKey_textBox.TabIndex = 1;
            // 
            // Apply_button
            // 
            Apply_button.Location = new Point(12, 101);
            Apply_button.Name = "Apply_button";
            Apply_button.Size = new Size(194, 32);
            Apply_button.TabIndex = 2;
            Apply_button.UseVisualStyleBackColor = true;
            Apply_button.Click += Apply_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(149, 13);
            label1.TabIndex = 3;
            label1.Text = "Введите имя пользователя:";
            // 
            // username_textBox
            // 
            username_textBox.Location = new Point(12, 25);
            username_textBox.Name = "username_textBox";
            username_textBox.Size = new Size(194, 21);
            username_textBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(35, 13);
            label2.TabIndex = 5;
            label2.Text = "label2";
            // 
            // RoomForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(220, 143);
            Controls.Add(label2);
            Controls.Add(username_textBox);
            Controls.Add(label1);
            Controls.Add(Apply_button);
            Controls.Add(secretKey_textBox);
            Controls.Add(roomName_textBox);
            Name = "RoomForm";
            Text = "RoomForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox roomName_textBox;
        private TextBox secretKey_textBox;
        private Button Apply_button;
        private Label label1;
        private TextBox username_textBox;
        private Label label2;
    }
}
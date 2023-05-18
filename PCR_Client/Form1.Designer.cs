namespace PCR_Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            message_textBox = new TextBox();
            send_button = new Button();
            messages_listBox = new ListBox();
            SuspendLayout();
            // 
            // message_textBox
            // 
            message_textBox.Location = new Point(12, 12);
            message_textBox.Multiline = true;
            message_textBox.Name = "message_textBox";
            message_textBox.Size = new Size(670, 58);
            message_textBox.TabIndex = 0;
            // 
            // send_button
            // 
            send_button.Font = new Font("Dubai", 8.5F, FontStyle.Bold, GraphicsUnit.Point);
            send_button.Location = new Point(688, 12);
            send_button.Name = "send_button";
            send_button.Size = new Size(100, 58);
            send_button.TabIndex = 2;
            send_button.Text = "Отправить";
            send_button.UseVisualStyleBackColor = true;
            send_button.Click += send_button_Click;
            // 
            // messages_listBox
            // 
            messages_listBox.FormattingEnabled = true;
            messages_listBox.Location = new Point(12, 78);
            messages_listBox.Name = "messages_listBox";
            messages_listBox.SelectionMode = SelectionMode.None;
            messages_listBox.Size = new Size(776, 355);
            messages_listBox.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(messages_listBox);
            Controls.Add(send_button);
            Controls.Add(message_textBox);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox message_textBox;
        private Button send_button;
        private ListBox messages_listBox;
    }
}
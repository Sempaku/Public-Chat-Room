namespace PCR_Client.Forms
{
    partial class StartForm
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
            CreateRoom_button = new Button();
            JoinToRoom_button = new Button();
            SuspendLayout();
            // 
            // CreateRoom_button
            // 
            CreateRoom_button.Location = new Point(12, 12);
            CreateRoom_button.Name = "CreateRoom_button";
            CreateRoom_button.Size = new Size(202, 75);
            CreateRoom_button.TabIndex = 0;
            CreateRoom_button.Text = "Создать комнату";
            CreateRoom_button.UseVisualStyleBackColor = true;
            CreateRoom_button.Click += CreateRoom_button_Click;
            // 
            // JoinToRoom_button
            // 
            JoinToRoom_button.Location = new Point(12, 100);
            JoinToRoom_button.Name = "JoinToRoom_button";
            JoinToRoom_button.Size = new Size(202, 75);
            JoinToRoom_button.TabIndex = 1;
            JoinToRoom_button.Text = "Войти в комнату";
            JoinToRoom_button.UseVisualStyleBackColor = true;
            JoinToRoom_button.Click += JoinToRoom_button_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(224, 187);
            Controls.Add(JoinToRoom_button);
            Controls.Add(CreateRoom_button);
            Name = "StartForm";
            Text = "StartForm";
            ResumeLayout(false);
        }

        #endregion

        private Button CreateRoom_button;
        private Button JoinToRoom_button;
    }
}
namespace LocalDataUpTool
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
            txt2json = new Button();
            String2json = new Button();
            jsonUp = new Button();
            txtUpjson = new Button();
            SuspendLayout();
            // 
            // txt2json
            // 
            txt2json.Location = new Point(12, 137);
            txt2json.Name = "txt2json";
            txt2json.Size = new Size(187, 83);
            txt2json.TabIndex = 0;
            txt2json.Text = "txt2json";
            txt2json.UseVisualStyleBackColor = true;
            txt2json.Click += txt2jsonButton;
            // 
            // String2json
            // 
            String2json.Location = new Point(292, 137);
            String2json.Name = "String2json";
            String2json.Size = new Size(187, 83);
            String2json.TabIndex = 1;
            String2json.Text = "String2json";
            String2json.UseVisualStyleBackColor = true;
            String2json.Click += String2jsonButton;
            // 
            // jsonUp
            // 
            jsonUp.Location = new Point(601, 137);
            jsonUp.Name = "jsonUp";
            jsonUp.Size = new Size(187, 83);
            jsonUp.TabIndex = 2;
            jsonUp.Text = "jsonUp";
            jsonUp.UseVisualStyleBackColor = true;
            jsonUp.Click += jsonUpButton;
            // 
            // txtUpjson
            // 
            txtUpjson.BackColor = SystemColors.Info;
            txtUpjson.Location = new Point(12, 271);
            txtUpjson.Name = "txtUpjson";
            txtUpjson.Size = new Size(776, 59);
            txtUpjson.TabIndex = 3;
            txtUpjson.Text = "txtUpjson";
            txtUpjson.UseVisualStyleBackColor = false;
            txtUpjson.Click += txtUpjsonButton;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtUpjson);
            Controls.Add(jsonUp);
            Controls.Add(String2json);
            Controls.Add(txt2json);
            Name = "Form1";
            Text = "Form";
            ResumeLayout(false);
        }

        #endregion

        private Button txt2json;
        private Button String2json;
        private Button jsonUp;
        private Button txtUpjson;
    }
}

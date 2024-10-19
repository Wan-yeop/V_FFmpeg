
namespace V_FFmpeg
{
    partial class MainForm
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
            this.New_Window = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // New_Window
            // 
            this.New_Window.Location = new System.Drawing.Point(74, 46);
            this.New_Window.Margin = new System.Windows.Forms.Padding(4);
            this.New_Window.Name = "New_Window";
            this.New_Window.Size = new System.Drawing.Size(184, 48);
            this.New_Window.TabIndex = 0;
            this.New_Window.Text = "Capture 영역 지정";
            this.New_Window.UseVisualStyleBackColor = true;
            this.New_Window.Click += new System.EventHandler(this.New_Window_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(74, 152);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(158, 41);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start Recording";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(74, 256);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(158, 49);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop Recording";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get DPI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 539);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.New_Window);
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Recording  Ver0.9";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button New_Window;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button button1;
    }
}


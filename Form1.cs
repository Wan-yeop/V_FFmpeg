using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V_FFmpeg
{
    public partial class MainForm : Form
    {
        private Form childForm;   //동적 생성 자식 폼

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }



        // New Windows-----------------------------------------------
        private void New_Window_Click(object sender, EventArgs e)
        {
            childForm = new Form
            {
                Size = new Size(400, 300),
                BackColor = Color.Red,
                StartPosition = FormStartPosition.CenterScreen,
                Opacity = 0.6
            };

            childForm.Move += ChildForm_MoveResize;     // + move event 
            childForm.Resize += ChildForm_MoveResize;   // + resize event

            childForm.Show();

        }

        // Windows Location & Size
        private void ChildForm_MoveResize(Object sender, EventArgs e)
        {
            string xy;

            xy = " X:" + childForm.Left.ToString()
                + "_Y:" + childForm.Top.ToString()
                + " W:" + childForm.Width.ToString()
                + "_H:" + childForm.Height.ToString();

            childForm.Text = xy;
        }
    }
}

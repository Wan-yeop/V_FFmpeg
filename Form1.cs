using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace V_FFmpeg
{
    public partial class MainForm : Form
    {
        private Form childForm;   //동적 생성 자식 폼
        private Process ffmpegProcess;
        private StreamWriter ffmpegInput;
        private bool ffmpegRunning = false;

        private Rectangle childFormRetangle;
        private String ffmpegArguments;

        public MainForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hWnd);

        public static uint Get_dpiX(Form form)
        {
            IntPtr hWnd = form.Handle;  // 현재 윈도우 핸들
            return GetDpiForWindow(hWnd);
        }


        public static float GetScalingFactor()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = g.DpiX; ;
                float scalingFactor = dpiX / 96.0f;
                return scalingFactor;
            }
        }



        // Start !! button

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (childForm != null)
            {
                //childForm.Close();
                childForm.TransparencyKey = Color.Lime;
            }

            if (ffmpegArguments == null)
            {
                MessageBox.Show("먼저 Capture 영역을 지정해주세요");
                return;
            }

            //string commandLine = " -y -f gdigrab -framerate 30 -offset_x 10 -offset_y 10 -video_size 400x400 -i desktop -vcodec libx264 -pix_fmt yuv420p output.mp4";

            var startInfo = new ProcessStartInfo
            {
                FileName = "C:\\msys64\\ffmpeg_src\\ffmpeg\\ffmpeg2.exe",   //"C:\\msys64\\ffmpeg_src\\ffmpeg\\ffmpeg2.exe", "C:\\msys64\\mingw64\\bin\\ffmpeg.exe"
                Arguments = ffmpegArguments,
                RedirectStandardInput = true,
                RedirectStandardError = false,
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            ffmpegProcess = new Process { StartInfo = startInfo };

            try
            {
                ffmpegProcess.Start();
                ffmpegInput = ffmpegProcess.StandardInput;  //입력 스트림 생성
                ffmpegRunning = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                MessageBox.Show("FFmpeg recording started.");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Faild to start FFmpeg: " + ex.Message);
            }


        }

        //STOP Button Click !!

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!ffmpegRunning)
            {
                MessageBox.Show("FFmpeg is not running.");
                return;
            }

            try
            {
                // 'q' 명령어 전달

                byte[] exitCommand = new byte[] { (byte)'q' };  // 'q'의 ASCII 값
                ffmpegProcess.StandardInput.BaseStream.Write(exitCommand, 0, 1);  // 1바이트 전송
                ffmpegProcess.StandardInput.BaseStream.Flush();  // 버퍼 플러시


                if (!ffmpegProcess.WaitForExit(5000))  //프로세스 종료 대기,.
                {
                    ffmpegProcess.Kill();
                    MessageBox.Show("FFmpeg was forcefully terminated.");
                }
                else
                {
                    MessageBox.Show("FFmpeg recording stopped normally");
                }

                ffmpegProcess.Close();

                ffmpegRunning = false;
                btnStart.Enabled = true;
                btnStop.Enabled = false;

                ffmpegArguments = null;

                if (childForm != null)
                {
                    childForm.Close();
                    childForm = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faild to Stop FFmpeg" + ex.Message);
            }

        }



        // New Windows Button Click !!

        private void New_Window_Click(object sender, EventArgs e)
        {
            
            if (childForm != null)
            {
                return;
            }

            childForm = new Form
            {
                Size = new Size(400, 300),
                // FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Lime,
                // TransparencyKey = Color.Lime,
                TopMost = true,
                ShowInTaskbar = true,
                StartPosition = FormStartPosition.CenterScreen,
                Opacity = 0.6
            };

            // childForm.Paint += TransparentForm_Paint;
            childForm.Move += ChildForm_MoveResize;     // + move event 
            childForm.Resize += ChildForm_MoveResize;   // + resize event
            // childForm.FormClosing += ChildForm_FormClosing;

            childForm.Show();

        }

        private void TransparentForm_Paint(object sender, PaintEventArgs e)
        {
            int borderThickness = 5;
            Pen redPen = new Pen(Color.Red, borderThickness);   // 테두리 색상과 굵기 설정
            Form form = sender as Form;
            e.Graphics.DrawRectangle(redPen, borderThickness / 2, borderThickness / 2, (form.ClientSize.Width-borderThickness), (form.ClientSize.Height-borderThickness));  // 테두리 그리기
        }

        // 캡쳐 영역 창을 닫기 전에 위치 및 사이즈 정보 얻기 (ffmpegArguments)
        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // H264는  width, Height 가 2의 배수여야 한다. -vf "scale="
            Rectangle Rect = new Rectangle(childForm.Left, childForm.Top, (int)(Math.Truncate(childForm.Width/2.0)*2), (int)(Math.Truncate(childForm.Height/2.0)*2) );

            ffmpegArguments = $"-y -f gdigrab -framerate 30 -offset_x {Rect.Left} -offset_y {Rect.Top} -video_size {Rect.Width}x{Rect.Height} -i desktop -vcodec libx264 -pix_fmt yuv420p output.mp4";
        }

        // Windows Location & Size
        private void ChildForm_MoveResize(Object sender, EventArgs e)
        {
            if ( childForm.Left < 0 )
            {
                childForm.Left = 0;  // 창 Left 값이 0 이하가 되지 않도록
            }

            Rectangle Rect = new Rectangle((childForm.Left+10), (childForm.Top+60), (int)(Math.Truncate((childForm.Width-20) / 2.0) * 2), (int)(Math.Truncate((childForm.Height-70) / 2.0) * 2));
            ffmpegArguments = $"-y -f gdigrab -framerate 30 -offset_x {Rect.Left} -offset_y {Rect.Top} -video_size {Rect.Width}x{Rect.Height} -i desktop -vcodec libx264 -pix_fmt yuv420p output.mp4";

            String xy;

            xy = " X:" + Rect.Left.ToString() + "_Y:" + Rect.Top.ToString() + " W:" + Rect.Width.ToString() + "_H:" + Rect.Height.ToString();
            childForm.Text = xy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float dpiX = GetScalingFactor();

            MessageBox.Show(String.Format("{0:F2}", dpiX));

        }
    }
}
   

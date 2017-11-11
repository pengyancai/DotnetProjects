using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
/*        Load custom font by filename*/
        public void setFontByFileName()
        {
            string AppPath = Application.StartupPath;
            string fontPath = AppPath + @"\汉仪刚艺体-35W.ttf";
            if (System.IO.File.Exists(fontPath))
            {
                System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
                pfc.AddFontFile(fontPath);
                Font myFont = new Font(pfc.Families[0], 20f);

                this.label1.Font = myFont;
            }
        }

/*        Load custom font by font file stream*/
        public void setFontByStream()
        {
            string AppPath = Application.StartupPath;
            string fontPath = AppPath + @"\汉仪刚艺体-35W.ttf";
            if (System.IO.File.Exists(fontPath))
            {
                System.IO.Stream fileStream = new System.IO.StreamReader(fontPath).BaseStream;

                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                fileStream.Seek(0, System.IO.SeekOrigin.Begin);
                System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
                unsafe
                {
                    fixed (byte* pFontData = &bytes[0])
                    {
                        pfc.AddMemoryFont((System.IntPtr)pFontData, bytes.Length);
                    }
                }

                Font myFont = new Font(pfc.Families[0], 20f);
                this.label3.Font = myFont;
            }
        }

/*        load custom font from application resource*/
        public void setFontFromResource()
        {
            byte[] bytes = LoadCustomFont.Properties.Resources.汉仪刚艺体_35W;

             System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
            IntPtr MeAdd = System.Runtime.InteropServices.Marshal.AllocHGlobal(bytes.Length);
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, MeAdd, bytes.Length);
            pfc.AddMemoryFont(MeAdd, bytes.Length);

//             unsafe //打开"允许不安全代码编译"开关，此句才不报错  
//             {
//                 fixed (byte* pFontData = bytes)
//                 {
//                     pfc.AddMemoryFont((System.IntPtr)pFontData, bytes.Length);
//                 }
//             }
            Font myFont = new Font(pfc.Families[0], 20f);
            this.label4.Font = myFont;
        }

        public Form1()
        {
            InitializeComponent();

            setFontByFileName();

            setFontByStream();

            setFontFromResource();
        }
    }
}

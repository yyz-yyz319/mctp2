using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalDataUpTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void txt2jsonButton(object sender, EventArgs e)
        {
            // 调用TxtToJsonConverter类的ConvertTxtToJson方法
            string result = TxtToJsonConverter.ConvertTxtToJson();
            // 显示转换结果
            MessageBox.Show(result, "转换结果");
        }

        private void String2jsonButton(object sender, EventArgs e)
        {

        }

        private async void jsonUpButton(object sender, EventArgs e)
        {
            // 调用DataUploader.UploadData方法上传数据
            string result = await DataUploader.UploadData();

            // 显示上传结果
            if (result.StartsWith("上传成功"))
            {
                MessageBox.Show(result, "成功");
            }
            else
            {
                MessageBox.Show(result, "错误");
            }
        }

        private async void txtUpjsonButton(object sender, EventArgs e)
        {
             // 调用TxtUpJsonProcessor.ProcessTxtUpJson方法执行txt转json并上传
                        string result = await TxtUpJsonProcessor.ProcessTxtUpJson();

                        // 显示处理结果
                        if (result.StartsWith("上传成功") || result.StartsWith("成功转换"))
                        {
                            MessageBox.Show(result, "成功");
                        }
                        else
                        {
                            MessageBox.Show(result, "错误");
                        }
        }
    }


}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotNet.Utilities;

namespace KanColleJehad
{
    public partial class Form1 : Form
    {
        int total_num = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (ckb_agreement.Checked == false)
            {
                MessageBox.Show("未同意协议，不能使用");
            }
            else
            {
                ckb_agreement.Enabled = false;
                tmr_send.Interval = (int)numericUpDown1.Value;
                tmr_send.Enabled = true;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            ckb_agreement.Enabled = true;
            tmr_send.Enabled = false;
        }

        private void tmr_send_Tick(object sender, EventArgs e)
        {
            Random ran = new Random();
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "http://gc.moejn.com/login.html",
                Method = "POST",
                Timeout = 100000,
                ReadWriteTimeout = 30000,
                IsToLower = false,
                Cookie = txt_cookie.Text,
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",
                Accept = "text/html, application/xhtml+xml, */*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://gc.moejn.com/login.html",
                Postdata = "name="+ran.Next()+"&password="+ran.Next()+"&world=1&seccode="+txt_scd.Text+"&registersubmit=%E6%8F%90%E4%BA%A4",
                //ResultType = ResultType.String,//返回数据类型，是Byte还是String
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            string cookie = result.Cookie;
            byte[] b = result.ResultByte;
            total_num++;
            lbl_num.Text = "已发送：" +total_num+ "次"; 
        }
    }
}

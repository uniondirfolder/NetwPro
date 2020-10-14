using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dn_002_AsyncDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Action<int> dlg;
            //Func<int> dlg = compute;
        }
        private int i;
        Random rnd = new Random();

        //int compute()
        //{
        //    Thread.Sleep(rnd.Next(500));
        //    return ++i;
        //}

        //syncronous version
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    for (int j = 0; j < 100; j++)
        //    {
        //        int item = compute();
        //        progressBar1.Value = item;
        //    }
        //}

        //asyncronous version using delegats
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.i = 0;
        //    Action runner = () =>
        //    {
        //        for (int j = 0; j < 100; j++)
        //        {
        //            int item = compute();
        //            Invoke(new Action(() => //UI Thread
        //                {
        //                    progressBar1.Value = item;
        //                    label1.Text = item.ToString();
        //                }
        //            ));
        //        }
        //    };
        //    runner.BeginInvoke(null, null);
        //}

        //asyncronous version using threads
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.i = 0;
        //    new Thread( () =>
        //    {
        //        for (int j = 0; j < 100; j++)
        //        {
        //            int item = compute();
        //            Invoke(new Action(() => //UI Thread
        //                {
        //                    progressBar1.Value = item;
        //                    label1.Text = item.ToString();
        //                }
        //            ));
        //        }
        //    }).Start();

        //}

        //asyncronous version using Task 4.0
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.i = 0;
        //    Task.Run(() =>
        //    {
        //        for (int j = 0; j < 100; j++)
        //        {
        //            int item = compute();
        //            Invoke(new Action(() => //UI Thread
        //                {
        //                    progressBar1.Value = item;
        //                    label1.Text = item.ToString();
        //                }
        //            ));
        //        }
        //    });

        //}


        Task<int> compute()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(rnd.Next(500));
                return ++i;
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.i = 0;

            for (int j = 0; j < 100; j++)
            {
                int item = await compute();

                progressBar1.Value = item;
                label1.Text = item.ToString();
            }

        }
    }
}

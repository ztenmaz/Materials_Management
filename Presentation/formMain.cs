﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void txbHome_CheckedChanged(object sender, EventArgs e)
        {
            if(txbHome.Checked)
            {
                UC_Home.BringToFront();
            }    
        }

        private void txbSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (txbSupplier.Checked)
            {
                UC_Suplier.BringToFront();
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void UC_Home_Load(object sender, EventArgs e)
        {

        }
    }
}

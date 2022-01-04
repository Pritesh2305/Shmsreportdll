using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SmartCRM
{
    public partial class frmmdi : Form
    {
        public frmmdi()
        {
            InitializeComponent();
        }

        private void promationalSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmanualsms frmmanualsms = new frmmanualsms();           
            frmmanualsms.Instance.ShowDialog();
        }      

    }
}

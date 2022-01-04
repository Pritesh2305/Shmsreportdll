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
    public partial class frmmanualsms : Form
    {
        private static frmmanualsms _instance;

        public frmmanualsms()
        {
            InitializeComponent();
        }

        public frmmanualsms Instance
        {
            get
            {
                if (frmmanualsms._instance == null)
                {
                    frmmanualsms._instance = new frmmanualsms();
                }

                return frmmanualsms._instance;
            }
        }
    }
}

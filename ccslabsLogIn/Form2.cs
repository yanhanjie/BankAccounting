using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ccslabsLogIn
{
    public partial class Form2 : Form 
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void custmerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.custmerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.applicationDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO:  这行代码将数据加载到表“applicationDataSet.custmer”中。您可以根据需要移动或删除它。
            this.custmerTableAdapter.Fill(this.applicationDataSet.custmer);

        }


        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }


    }
}

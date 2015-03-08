using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using ccslabsLogIn.forms;



namespace ccslabsLogIn
{
    public partial class frmMain : Form
    {
        frmLogin _login = new frmLogin();

        public int _x;
        public int x
        {
            get { return _x; }
            set { _x = value; }
        }


        public string username;
        SqlCeConnection mySqlConnection;

        public frmMain()
        {
            InitializeComponent();
            _login.ShowDialog();
            if (_login.Authenticated)
            {
                MessageBox.Show("You have logged in successfully, " + _login.Username);
            }
            else
            {
                MessageBox.Show("You failed to login or register - bye bye", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            Welcome();
            Customer_Details();
            populateListBox();
        }

        public void Welcome()
        {
            username = this._login.Username;
            this.label1.Text = "Welcome, "+ this.username;
        }

        public void Connection()
        {
            mySqlConnection = new SqlCeConnection(@"Data Source=C:\Users\YAM HON GIT\Documents\assignment2\C#\ccslabsLogIn\MyDatabase.sdf");

        }

        public void populateListBox()
        {
            Connection();

            x = 1;

            String selcmd = "SELECT * FROM Customer where CustomerId = 1";

            SqlCeCommand mySqlCommand = new SqlCeCommand(selcmd, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                listBox1.Items.Clear();

                while (mySqlDataReader.Read())
                {

                    FirstNameBox.Text = mySqlDataReader["FirstName"].ToString();
                    LastNameBox.Text = mySqlDataReader["LastName"].ToString();
                    BalanceBox.Text = mySqlDataReader["Balance"].ToString();
                    CustomerId.Text = mySqlDataReader["CustomerId"].ToString();


                }

                 mySqlConnection.Close();
            }

            catch (SqlCeException ex)
            {

                MessageBox.Show(" .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void Customer_Details()
        {

            Connection();

            String select = "SELECT * FROM Customer where CustomerId = " + x;

            SqlCeCommand SelectCommand = new SqlCeCommand(select, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader SelectDataReader = SelectCommand.ExecuteReader();

                FirstNameBox.Clear();
                LastNameBox.Clear();
                BalanceBox.Clear();

                while (SelectDataReader.Read())
                {
                    FirstNameBox.Text = SelectDataReader["FirstName"].ToString();
                    LastNameBox.Text = SelectDataReader["LastName"].ToString();
                    BalanceBox.Text = SelectDataReader["Balance"].ToString();
                    CustomerId.Text = SelectDataReader["CustomerId"].ToString();
                }
            }

            catch (SqlCeException ex)
            {

                MessageBox.Show(" .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void showdetails_Click(object sender, EventArgs e)
        {   
            Form2 pass = new Form2();
            pass.Show();
        }

        private void first_Click(object sender, EventArgs e)
        {
            x=1;
            Customer_Details();
            MessageBox.Show("This is the first Record");

        }

        private void next_Click(object sender, EventArgs e)
        {   
           
            start:
            x++;
            Connection();
            int max = int.Parse(Num_Max());

            if (x <= max)
            {

                String next = "SELECT * FROM Customer where CustomerId = " + x;

                SqlCeCommand nextCommand = new SqlCeCommand(next, mySqlConnection);

                mySqlConnection.Open();

                SqlCeDataReader nextDataReader = nextCommand.ExecuteReader();

                if (nextDataReader.Read())
                {
                        FirstNameBox.Text = nextDataReader["FirstName"].ToString();
                        LastNameBox.Text = nextDataReader["LastName"].ToString();
                        BalanceBox.Text = nextDataReader["Balance"].ToString();
                        CustomerId.Text = nextDataReader["CustomerId"].ToString();
                }

                else
                {
                    goto start;
                }


            }
            else if (x > max)
            {
                x--;
                MessageBox.Show("This is the last record");
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            x--;

            if (x<1)
            {
                MessageBox.Show("This the first record");
                x++;
            }

            else
            {
                Customer_Details();

            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            FirstNameBox.Clear();
            LastNameBox.Clear();
            BalanceBox.Clear();
            CustomerId.Text = "";
        }

        private string Num_Max()
        {
            string max = string.Format("select Max(CustomerId)  from Customer");

            Connection();

            SqlCeCommand maxCommand = new SqlCeCommand(max, mySqlConnection);

                mySqlConnection.Open();

                SqlCeDataReader Max = maxCommand.ExecuteReader();

                Max.Read();
                
                string _y = Max[0].ToString();

                mySqlConnection.Close();

                return _y;
        }

        private void add_Click(object sender, EventArgs e)
        {
            string firstname = this.FirstNameBox.Text;
            string lastname = this.LastNameBox.Text;
            string balance = this.BalanceBox.Text;

            int _next = int.Parse(Num_Max()) +1;
            string next = _next.ToString();

            string _add = string.Format("insert into Customer values('{0}','{1}','{2}','{3}','{4}')", next, firstname, lastname, balance, next);
            
            Connection();

            SqlCeCommand addCommand = new SqlCeCommand(_add, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader add = addCommand.ExecuteReader();
                    
                MessageBox.Show("Added");
             }


            catch (SqlCeException ex)
            {
                MessageBox.Show(" .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void update_Click(object sender, EventArgs e)
        {
            string firstname = this.FirstNameBox.Text;
            string lastname = this.LastNameBox.Text;
            string balance = this.BalanceBox.Text;
            string customerid = this.CustomerId.Text;

            string _update = string.Format("update Customer SET FirstName = '"+firstname+"' , LastName = '"+ lastname+ "' , Balance = '"+balance+"' where  CustomerId = '"+customerid+"'");

            Connection();

            SqlCeCommand updateCommand = new SqlCeCommand(_update, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader update = updateCommand.ExecuteReader();
                    
                MessageBox.Show("Updated");
             }

            catch (SqlCeException ex)
            {
                MessageBox.Show(" .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void last_Click(object sender, EventArgs e)
        {
            x = int.Parse(Num_Max());
            Customer_Details();
            MessageBox.Show("This is the last record");
        }

        private void delete_Click(object sender, EventArgs e)
        {
            string customerid = this.CustomerId.Text;

            string _delete = string.Format("delete from Customer where CustomerId='" +customerid+"'");

            DialogResult Result = MessageBox.Show("Are you sure to delte this record?", "Delete", MessageBoxButtons.YesNo);

            if (Result == DialogResult.Yes)
            {
                Connection();

                SqlCeCommand deleteCommand = new SqlCeCommand(_delete, mySqlConnection);

                try
                {
                    mySqlConnection.Open();

                    SqlCeDataReader update = deleteCommand.ExecuteReader();

                    MessageBox.Show("Deleted");

                    x = int.Parse(customerid)+1;

                    Customer_Details();

                }

                catch (SqlCeException ex)
                {
                    MessageBox.Show(" .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Result == DialogResult.No)
            {
            }
        }

        
    }
}

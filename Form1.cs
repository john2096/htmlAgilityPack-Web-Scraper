using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Xml;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace _462Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();  
            doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //DOM object
            
            string text = "";  //empty string for textbox

            HtmlNode[] full = doc.DocumentNode.SelectNodes("//a[@class='title'] | //h4[@class='pull-right price'] | //p[@class='description']").ToArray();
            //node array

            foreach (var nodes in full)  
            {
                text += nodes.InnerText + "\r\n";  //populationg the text string
            }

            textBox1.Text = text; //display text
        }

        // THE NEXT 3 FUNCTIONS ARE THE SAME AS ABOVE BUT CUT OUT 2 OF THE STATEMENTS IN DocumentNode.SelectNodes()

        private void button2_Click(object sender, EventArgs e)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //DOM object
            HtmlNode[] prices = doc.DocumentNode.SelectNodes("//h4[@class='pull-right price']").ToArray();  //node array
            string text = "";

            foreach (var nodes in prices)
            {
                text += nodes.InnerText + "\r\n";
            }

            textBox1.Text = text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //DOM object
            string text = "";

            HtmlNode[] title = doc.DocumentNode.SelectNodes("//a[@class='title']").ToArray(); //node array

            foreach (var nodes in title)
            {
                text += nodes.InnerText + "\r\n";
            }

            textBox1.Text = text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //DOM object
            string text = "";

            HtmlNode[] dec = doc.DocumentNode.SelectNodes("//p[@class='description']").ToArray();  //node array

            foreach (var nodes in dec)
            {
                text += nodes.InnerText + "\r\n";
            }

            textBox1.Text = text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //DAVINA CODE STARTS HERE - Add all nodes to the database

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();  //DOM
            doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //Pulling DOM from site


            HtmlNode[] full = doc.DocumentNode.SelectNodes("//a[@class='title'] | //h4[@class='pull-right price'] | //p[@class='description']").ToArray();
            //Selecting all nodes for titles, price, and description.  Xpath finds price first, then title, and description.

            HtmlNode[] dec = doc.DocumentNode.SelectNodes("//p[@class='description']").ToArray();
            HtmlNode[] prices = doc.DocumentNode.SelectNodes("//h4[@class='pull-right price']").ToArray();
            HtmlNode[] title = doc.DocumentNode.SelectNodes("//a[@class='title']").ToArray();


            //Database Code

            var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");

            string statement = "";
            var cmd = new SQLiteCommand();
            connection.Open();

            var Name = "";
            var Price = "";
            var Desc = "";
            int x = 0;



            foreach (var nodes in title)
            {
                Name = title[x].InnerText;
                Price = prices[x].InnerText;
                Desc = dec[x].InnerText;

                statement = "Insert into Electronics (Name,Price,Description)" +
                "Values (" + "'" + Name + "'," + "'" + Price + "'," + "'" + Desc + "'" + ")";
                cmd = new SQLiteCommand(statement, connection);
                cmd.ExecuteNonQuery();

                x++;
            }
            connection.Close();



            MessageBox.Show("All rows have been added!");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DELETED&&BROKEN
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DELETED&&BROKEN
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                //General Queries 

                //Title
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT Name FROM Electronics";



                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);

                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();



                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetString(0), "NULL","NULL");
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                // Price
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT Price FROM Electronics";



                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);

                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();



                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add("NULL",dr.GetString(0), "NULL");
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                //Description
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT Description FROM Electronics";



                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);

                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();



                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add("NULL", "NULL",dr.GetString(0));
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                //All
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT * FROM Electronics";



                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);

                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();



                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetString(1), dr.GetString(2), dr.GetString(3));
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Specific Queries

            var x = dataGridView1.SelectedCells[0].Value.ToString();
            if (comboBox1.SelectedIndex == 0)
            {
                //Title
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT * FROM Electronics WHERE Name = @x";

                var args = new Dictionary<string, object>
                {
                    {"@x", x }
                };

                // create parameters - need to check if makerid is not an integer
                SQLiteParameter paramMakerId = new SQLiteParameter();
                paramMakerId.ParameterName = "@x";
                paramMakerId.DbType = DbType.String;
                paramMakerId.Value = x;  

                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);
                cmd.Parameters.Add(paramMakerId);
                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();



                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetString(1), dr.GetString(2), dr.GetString(3));
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                // Price
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT * FROM Electronics WHERE Price = @x";


                var args = new Dictionary<string, object>
                {
                    {"@x", x }
                };

                // create parameters - need to check if makerid is not an integer
                SQLiteParameter paramMakerId = new SQLiteParameter();
                paramMakerId.ParameterName = "@x";
                paramMakerId.DbType = DbType.String;
                paramMakerId.Value = x;

                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);
                cmd.Parameters.Add(paramMakerId);
                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();


                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetString(1), dr.GetString(2), dr.GetString(3));
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                //Description
                dataGridView1.Rows.Clear();
                var connection = new SQLiteConnection(@"Data Source=C:\Users\Michael\Desktop\FinalProj\ITS462FinalProject.db;New=False;");
                string statement = "SELECT * FROM Electronics WHERE Description = @x";



                var args = new Dictionary<string, object>
                {
                    {"@x", x }
                };

                // create parameters - need to check if makerid is not an integer
                SQLiteParameter paramMakerId = new SQLiteParameter();
                paramMakerId.ParameterName = "@x";
                paramMakerId.DbType = DbType.String;
                paramMakerId.Value = x;

                // open the connection
                connection.Open();
                var cmd = new SQLiteCommand(statement, connection);
                cmd.Parameters.Add(paramMakerId);
                // retrieve data to a data reader object
                SQLiteDataReader dr = cmd.ExecuteReader();


                // loop through all the rows
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr.GetString(1), dr.GetString(2), dr.GetString(3));
                    }
                }
                else
                {
                    MessageBox.Show("Help");
                }
                dr.Close();

                // close the connection
                connection.Close();
                //dataGridView1.Rows.Clear();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                MessageBox.Show("Detailed View Already Selected");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");  //DOM object


                HtmlNode[] title = doc.DocumentNode.SelectNodes("//a[@title='" + textBox2.Text + "'] | //*[a='" + textBox2.Text + "']/preceding-sibling::h4 | //*[a='" + textBox2.Text + "']/following-sibling::p").ToArray();
                //finds node based off title, then the next two nodes are searching for h4 tags before first node and p tags after it

                string text = "";
                string name = "";
                string price = "";
                string desc = "";
                name = title[0].InnerText;
                price = title[1].InnerText;
                desc = title[2].InnerText;
                text = price + ", " + name + ", " + desc;  //assigns string values based of array postion to control output

                textBox1.Text = text;
            }
            catch (Exception ex) //exception handler
            {
                MessageBox.Show("Value isn't a product", "Title Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)  //GREATER THAN FUNCTION
        {
            try
            {

                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");
                HtmlNode[] prices = doc.DocumentNode.SelectNodes("//h4[@class='pull-right price']").ToArray();
                string text = "";
                string test1 = textBox3.Text;   //pulls string from textbox
                int test2 = test1.Length;  //finds length of string from textbox

                foreach (var nodes in prices)
                {

                    string arrayString = nodes.InnerText; //pull current string from array
                    int arrayInt = arrayString.Length; //finds length of that string

                    if (arrayInt < test2)  //strings with unequal length don't work right with String.Compare(), here values less than $100.00 are add to the textbox
                    {
                        text += nodes.InnerText + "\r\n";
                    }
                    else if (arrayInt == test2)
                    {
                        int test = String.Compare(nodes.InnerText, textBox3.Text);  //values less than textbox string are added to display
                        if (test < 0)
                        {
                            text += nodes.InnerText + "\r\n";
                        }
                    }

                }
                textBox1.Text = text;
            }
            catch (Exception ex) //exception handler
            {
                MessageBox.Show("Try entering a value like $100.00", "Price Exeception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)  //Less than function
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = web.Load("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");
                HtmlNode[] prices = doc.DocumentNode.SelectNodes("//h4[@class='pull-right price']").ToArray();
                string text = "";
                string test1 = textBox3.Text;  //pulls string from textbox
                int test2 = test1.Length;  //finds length of string from textbox

                foreach (var nodes in prices)
                {

                    string arrayString = nodes.InnerText;  //pull current string from array
                    int arrayInt = arrayString.Length;  //finds length of that string

                    if (arrayInt > test2) //if the value is lower than $100.00 this will add all values greater than $100.00 to the display
                    {
                        text += nodes.InnerText + "\r\n";
                    }
                    else if (arrayInt == test2)
                    {
                        int test = String.Compare(nodes.InnerText, textBox3.Text);  //
                        if (test > 0)
                        {
                            text += nodes.InnerText + "\r\n";
                        }
                    }

                }
                textBox1.Text = text;
            }
            catch (Exception ex)  //exception handler
            {
                MessageBox.Show("Try entering a value like $100.00", "Price Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

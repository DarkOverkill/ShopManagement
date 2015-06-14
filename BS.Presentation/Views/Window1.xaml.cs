using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BS.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<object> obj = new List<object>();
        public Window1()
        {
            InitializeComponent();
            obj.Add(CB_Categoty);
            obj.Add(CB_SubCategoty);
            obj.Add(CB_Producer);
            obj.Add(CB_Product);
            //obj.Add(TB_Volume);
            //obj.Add(TB_Price);
            obj.Add(CB_Measure);
            obj.Add(CB_VolumeMeasure);
            Info();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();          
        }

        private void Info()
        {
            try
            {
                string cs = "Data Source=(local)\\SQLEXPRESS;initial catalog=TestShop;integrated security=True";
                var conn = new SqlConnection(cs);
                conn.Open();

                //добавляем категории
                var cmd = new SqlCommand("select * from Categoryes where ParentId is null ", conn);
                var rdr = cmd.ExecuteReader();
                List<string> temp_str = new List<string>();
                while (rdr.Read())
                {
                    temp_str.Add(rdr["Name"].ToString());               
                }
                CB_Categoty.ItemsSource = temp_str;
                rdr.Close();
               
                //добовляем производителей
                cmd = new SqlCommand("select * from Producers", conn);
                rdr = cmd.ExecuteReader();
                temp_str = new List<string>();
                while(rdr.Read())
                {
                    temp_str.Add(rdr["Name"].ToString());
                }
                CB_Producer.ItemsSource = temp_str;
                rdr.Close();

                // добовляем товары
                cmd = new SqlCommand("select * from Products", conn);
                rdr = cmd.ExecuteReader();
                temp_str = new List<string>();
                while(rdr.Read())
                {
                    temp_str.Add(rdr["Name"].ToString());
                }
                CB_Product.ItemsSource = temp_str;
                rdr.Close();

                // добавляем измерение товаров
                cmd = new SqlCommand("select * from Measures", conn);
                rdr = cmd.ExecuteReader();
                temp_str = new List<string>();
                while(rdr.Read())
                {
                    temp_str.Add(rdr["ShortName"].ToString());
                }
                CB_Measure.ItemsSource = temp_str;
                CB_VolumeMeasure.ItemsSource = temp_str;
                conn.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int _prodId, _measure, _volMeasure, _vol, _pacId, _price;
            string _addDate, cmdStr;

            if (TB_Price.Text != "" && TB_Volume.Text != "")
            {
                foreach (var item in obj)
                {
                    if ((item as ComboBox).Text == "")
                    {
                        MessageBox.Show("not fill all field!");
                        return;
                    }
                }
                // если заполнены все поля
                try
                {
                    string cs = "Data Source=(local)\\SQLEXPRESS;initial catalog=TestShop;integrated security=True";
                    var conn = new SqlConnection(cs);
                    conn.Open();
                    cmdStr = "DECLARE @prodId int SELECT @prodId = Id FROM Products WHERE Name = \'" + CB_Product.Text + "\' select @prodId";
                    var cmd = new SqlCommand(cmdStr, conn);
                    var rdr = cmd.ExecuteScalar();
                    _prodId = Convert.ToInt32(rdr.ToString());
                        //MessageBox.Show(_prodId.ToString());

                    cmdStr = "DECLARE @measure int SELECT @measure = Id FROM Measures WHERE ShortName = \'" + CB_Measure.Text + "\' select @measure";
                    cmd.CommandText = cmdStr;
                    rdr = cmd.ExecuteScalar();
                    _measure = Convert.ToInt32(rdr.ToString());
                    //MessageBox.Show(_measure.ToString());

                    cmdStr = "DECLARE @volMeasure int SELECT @volMeasure = id FROM Measures WHERE ShortName = \'" + CB_VolumeMeasure.Text + "\' select @volMeasure";
                    cmd.CommandText = cmdStr;
                    rdr = cmd.ExecuteScalar();
                    _volMeasure = Convert.ToInt32(rdr.ToString());
                    //MessageBox.Show(_volMeasure.ToString());

                    _vol = Convert.ToInt32(TB_Volume.Text);
                    //MessageBox.Show(_vol.ToString());
                    cmdStr = "INSERT INTO Packeges Values (" + _prodId + ", " + _volMeasure + ", " + _measure + ", " + _vol + ") select @@IDENTITY";
                    cmd.CommandText = cmdStr;
                    rdr = cmd.ExecuteScalar();
                    _pacId = Convert.ToInt32(rdr.ToString());
                   // MessageBox.Show(_pacId.ToString());


                    _price = Convert.ToInt32(TB_Price.Text);

                    _addDate = DateTime.Today.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
                    //MessageBox.Show(_addDate);

                    cmdStr = "INSERT INTO Price Values (" + _pacId + ", " + _price + ", \'" + _addDate + "\')";
                    cmd.CommandText = cmdStr;
                    //MessageBox.Show(cmdStr);
                    cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("not fill all field!");
            }
         
        }

        private void CB_Categoty_SelectionChanged(object sender, SelectionChangedEventArgs e)//добовляем подгрупы товара по выброной категории
        {
            string category = CB_Categoty.SelectedValue.ToString();
            string cs = "Data Source=(local)\\SQLEXPRESS;initial catalog=TestShop;integrated security=True";
            var conn = new SqlConnection(cs);
            conn.Open();
            string str = "declare @id int select @id = Id from Categoryes where Name = \'";
            str += category;
            str +=  "\' select * from Categoryes where ParentId = @id";
            var cmd = new SqlCommand(str, conn);
            var rdr = cmd.ExecuteReader();
            List<string> temp_str = new List<string>();
            while (rdr.Read())
            {
                temp_str.Add(rdr["Name"].ToString());
                // MessageBox.Show(rdr["Name"].ToString());
            }
            CB_SubCategoty.ItemsSource = temp_str;
            conn.Close();
        }
       
    }
}

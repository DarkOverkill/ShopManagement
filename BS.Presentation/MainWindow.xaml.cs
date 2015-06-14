using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BS.Bussnies.Managers.Abstract;
using BS.Bussnies.Managers.Concreate;
using BS.Presentation.ViewModels;
using System.Configuration;
using BSProject;
using System.Data.SqlClient;



namespace BS.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
        } 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TestShopEntities"].ConnectionString;
        
                ICategoryManager categoryManager = new CategoryManager(cs);
                IMeasureManager measureManager = new MeasureManager(cs);
                IPackegeManager packageManager = new PackegeManager(cs);
                IPriceManager priceManager = new PriceManager(cs);
                IProducerManager producerManager = new ProducerManager(cs);
                IProductManager productManager = new ProductManager(cs);

                CatalogViewModel catalogViewModel =
                    new CatalogViewModel(
                         categoryManager
                        , measureManager
                        , packageManager
                        , priceManager
                        , producerManager
                        , productManager
                        );
                
                vwCatalog.dgCatalogUC.DataContext = catalogViewModel.Catalog;

                cbByCategory.ItemsSource = catalogViewModel.Categoryes;
    
               // MessageBox.Show(cbByCategory.Items.Count.GetType().ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TestShopEntities"].ConnectionString;

                ICategoryManager categoryManager = new CategoryManager(cs);
                IMeasureManager measureManager = new MeasureManager(cs);
                IPackegeManager packageManager = new PackegeManager(cs);
                IPriceManager priceManager = new PriceManager(cs);
                IProducerManager producerManager = new ProducerManager(cs);
                IProductManager productManager = new ProductManager(cs);
                CatalogViewModel catalogViewModel =
                    new CatalogViewModel(
                         categoryManager
                        , measureManager
                        , packageManager
                        , priceManager
                        , producerManager
                        , productManager
                        );
                if (cbByCategory.SelectedIndex != cbByCategory.Items.Count - 1) 
                {
                    vwCatalog.dgCatalogUC.DataContext = catalogViewModel.CatalogFilterByCategory(cbByCategory.SelectedItem.ToString());
                }
                else
                {
                    vwCatalog.dgCatalogUC.DataContext = catalogViewModel.Catalog;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.Window1  w1 = new Views.Window1();
            w1.Top = this.Top+40;
            w1.Left = this.Left+40;
            w1.ShowDialog();         
        }
    }
}

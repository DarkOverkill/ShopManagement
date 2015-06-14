using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using BS.Presentation.Models;

namespace BS.Presentation.ViewModels
{
    class CatalogViewModel
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMeasureManager _measureMaanager;
        private readonly IPackegeManager _packageManager;
        private readonly IPriceManager _priceManager;
        private readonly IProducerManager _producerManager;
        private readonly IProductManager _productManager;

        List<CatalogModel> _items = null;

        public CatalogViewModel(
            ICategoryManager categoryManager,
            IMeasureManager measureMaanager,
            IPackegeManager packageManager,
            IPriceManager priceManager,
            IProducerManager producerManager,
            IProductManager productManager)
        {
            _categoryManager = categoryManager;
            _measureMaanager = measureMaanager;
            _packageManager = packageManager;
            _priceManager = priceManager;
            _producerManager = producerManager;
            _productManager = productManager;
        }

        static public List<CatalogModel> GreateCatalogModel(
            IEnumerable<Categoryes> categoryes,
            IEnumerable<Measures> measures,
            IEnumerable<Packeges> packeges,
            IEnumerable<Price> prices,
            IEnumerable<Producers> producers,
            IEnumerable<Products> products
            )
        {
            List<CatalogModel> listCm = new List<CatalogModel>();
            
            var res =
                from pg in packeges
                join p in products
                    on pg.ProductId equals p.Id
                join c in categoryes
                    on p.CategoryId equals c.Id
                join cc in categoryes
                    on c.ParentId equals cc.Id
                join prd in producers
                    on p.ProducerId equals prd.Id
                join m in measures
                    on pg.MeasureId equals m.Id
                join vm in measures
                    on pg.VolumeMeasureId equals vm.Id
                join pr in prices
                    on pg.Id equals pr.PackegeId
                select new
                {
                    Id = pg.Id,
                    Name = p.Name,
                    Producer = prd.Name,
                    Category = cc.Name,
                    SubCategory = c.Name,
                    Volume = pg.Volume,
                    Measure = m.ShortName,
                    VolMeasure = vm.ShortName,
                    Price = pr.Cost
                };

            foreach (var c in res)
            {
                listCm.Add(
                    new CatalogModel(c.Id, c.Name, c.Producer, c.Category, c.SubCategory, (decimal)c.Volume, (decimal)c.Price, c.Measure, c.VolMeasure)
                    );
            }
            return listCm;
        }
        
        static public List<CatalogModel> GreateCatalogModelFilterByCategory(
           IEnumerable<Categoryes> categoryes,
           IEnumerable<Measures> measures,
           IEnumerable<Packeges> packeges,
           IEnumerable<Price> prices,
           IEnumerable<Producers> producers,
           IEnumerable<Products> products,
            string nameOfCategory
           )
        {
            List<CatalogModel> listCm = new List<CatalogModel>();
            
            var res =
                from pg in packeges
                join p in products
                    on pg.ProductId equals p.Id
                join c in categoryes
                    on p.CategoryId equals c.Id
                join cc in categoryes                
                    on c.ParentId equals cc.Id
                    where cc.Name == nameOfCategory
                join prd in producers
                    on p.ProducerId equals prd.Id
                join m in measures
                    on pg.MeasureId equals m.Id
                join vm in measures
                    on pg.VolumeMeasureId equals vm.Id
                join pr in prices
                    on pg.Id equals pr.PackegeId
                select new
                {
                    Id = pg.Id,
                    Name = p.Name,
                    Producer = prd.Name,
                    Category = cc.Name,
                    SubCategory = c.Name,
                    Volume = pg.Volume,
                    Measure = m.ShortName,
                    VolMeasure = vm.ShortName,
                    Price = pr.Cost
                };

            foreach (var c in res)
            {
                listCm.Add(
                    new CatalogModel(c.Id, c.Name, c.Producer, c.Category, c.SubCategory, (decimal)c.Volume, (decimal)c.Price, c.Measure, c.VolMeasure)
                    );
            }
            return listCm;
        }
        public IEnumerable<CatalogModel> CatalogFilterByCategory(string name)
        {
            if (_items == null)
            {
                _items = new List<CatalogModel>();
                _items = GreateCatalogModelFilterByCategory (
                    _categoryManager.GetAll(),
                    _measureMaanager.GetAll(),
                    _packageManager.GetAll(),
                    _priceManager.GetAll(),
                    _producerManager.GetAll(),
                    _productManager.GetAll(),
                    name
                    );
            }
            return _items;
        }

        public IEnumerable<CatalogModel> Catalog
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<CatalogModel>();
                    _items = GreateCatalogModel(
                        _categoryManager.GetAll(),
                        _measureMaanager.GetAll(),
                        _packageManager.GetAll(),
                        _priceManager.GetAll(),
                        _producerManager.GetAll(),
                        _productManager.GetAll()
                        );    
                }
                return _items;
            }
        }

        public List<string> Categoryes
        {       
            get 
            {
                List<string> tmp = new List<string>();
                bool addFlag = false;
                for (int i = 0; i < _items.Count; i++)
                {
                    addFlag = true;
                    foreach (var CategoryName in tmp)
                    {
                        if ( _items[i].Category == CategoryName)
                        {
                            addFlag = false;
                            break;
                        }
                    }
                    if (addFlag)
                    {
                        tmp.Add(_items[i].Category);
                    }
                }
                tmp.Add("Все");
                    return tmp;
            }
        }

    }
}
namespace BS.Presentation.Models
{
    public class CatalogModel
    {
        #region Data Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public string Measure { get; set; }
        public string VolMeasure { get; set; }

        #endregion

        public CatalogModel(
            int id,
            string name,
            string producer,
            string category,
            string subCategory,
            decimal volume,
            decimal price,
            string measure,
            string volMeasure
            )
        {
            Id = id;
            Name = name;
            Producer = producer;
            Category = category;
            SubCategory = subCategory;
            Volume = volume;
            Price = price;
            Measure = measure;
            VolMeasure = volMeasure;
        }
    }
}

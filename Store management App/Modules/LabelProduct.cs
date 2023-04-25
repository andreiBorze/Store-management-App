namespace Store_management_App.Modules
{
    public class LabelProduct
    {
        public int Id { get; private set; }
        public string? Barcode { get; set; }
        public decimal Price { get; set; }

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
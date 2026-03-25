namespace ECommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public UnitOfWork(AppContext context, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

namespace ECommerce.Common;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public PaginationMetaData MetaData { get; set; } = new PaginationMetaData();
}

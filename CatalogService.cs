using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog
{
    public class CatalogService
    {
        private readonly Catalog _catalog;

        public CatalogService(Catalog catalog)
        {
            _catalog = catalog;
        }

        public List<Book> SearchBooks(string query)
        {
            return _catalog.Books.Where(b =>
                b.Tytul.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Autorzy.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void AddBook(Book book)
        {
            _catalog.Books.Add(book);
        }
    }
}

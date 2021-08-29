using PaginacaoSQL.Models;
using System.Collections.Generic;

namespace PaginacaoSQL.Repositories
{
    public interface IValorRepository
    {
        int ObterTotalPaginas(int rows);
        IEnumerable<Valor> ObterValores(int page, int rows);
    }
}

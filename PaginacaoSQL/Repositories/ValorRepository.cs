using Dapper;
using Microsoft.Extensions.Configuration;
using PaginacaoSQL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PaginacaoSQL.Repositories
{
    public class ValorRepository : IValorRepository
    {
        private readonly string connectionString;

        public ValorRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Default");
        }

        public int ObterTotalPaginas(int rows)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "SELECT COUNT(*) / @RowsPage FROM Valores; SELECT COUNT(*) % @RowsPage FROM Valores;";

                conn.Open();

                using (var multi = conn.QueryMultiple(sql, new { RowsPage = rows }))
                {
                    var pages = multi.Read<int>().First();
                    var resto = multi.Read<int>().First();

                    if (resto != 0)
                        pages++;

                    return pages;
                }
            }
        }

        public IEnumerable<Valor> ObterValores(int page, int rows)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"SELECT Id, Conteudo, [Data]
                            FROM Valores
                            ORDER BY Id
                            OFFSET((@CurrentPage - 1) * @RowsPage) ROWS
                            FETCH NEXT @RowsPage ROWS ONLY; ";

                conn.Open();

                var result = conn.Query<Valor>(sql, new { CurrentPage = page, RowsPage = rows });

                return result;
            }
        }
    }
}

using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.ApiEndpoints
{
    public static class ProdutoEndpoints
    {
        public static void MapProdudoEndpoints(this WebApplication app)
        {
            /////////// Rota Protudos /////////

            // Incluir na tabela
            app.MapPost("/produtos", async (Produto produto, AppDpContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            });

            // Lista total
            app.MapGet("/produtos", async (AppDpContext db) =>
                            await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

            // Lista por ID
            app.MapGet("/produtos/{id:int}", async (int id, AppDpContext db) =>
            {
                return await db.Produtos.FindAsync(id)
                    is Produto produtos
                        ? Results.Ok(produtos)
                        : Results.NotFound();
            });


            //Atualizar a tabela por ID
            app.MapPost("/produtos/{id:int}", async (int id, Produto produtos, AppDpContext db) =>
            {
                if (produtos.ProdutoId != id)
                {
                    return Results.BadRequest();
                }

                var produtosDb = await db.Produtos.FindAsync(id);

                if (produtosDb is null) return Results.NotFound();

                produtosDb.Nome = produtos.Nome;
                produtosDb.Descricao = produtos.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(produtosDb);
            });

            //Apagando Infornações da tabela
            app.MapDelete("/produtos/{id:int}", async (int id, AppDpContext db) =>
            {
                var produtos = await db.Produtos.FindAsync(id);

                if (produtos is null)
                {
                    return Results.NotFound();
                }

                db.Produtos.Remove(produtos);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

        }
    }
}

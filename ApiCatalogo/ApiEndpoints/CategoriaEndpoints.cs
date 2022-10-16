using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.ApiEndpoints
{
    public static class CategoriaEndpoints
    {
        public static void MapCategoriaEndpoints(this WebApplication app)
        {
            // Incluir na tabela
            app.MapPost("/categoria", async (Categoria categoria, AppDpContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categoria/{categoria.CategoriaId}", categoria);
            });

            // Lista total
            app.MapGet("/categoria", async (AppDpContext db) =>
                        await db.Categorias.ToListAsync()).WithTags("Categoria").RequireAuthorization();

            // Lista por ID
            app.MapGet("/categoria/{id:int}", async (int id, AppDpContext db) =>
            {
                return await db.Categorias.FindAsync(id)
                    is Categoria categoria
                        ? Results.Ok(categoria)
                        : Results.NotFound();
            });


            //Atualizar a tabela por ID
            app.MapPost("/categoria/{id:int}", async (int id, Categoria categoria, AppDpContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                var categoriaDb = await db.Categorias.FindAsync(id);

                if (categoriaDb is null) return Results.NotFound();

                categoriaDb.Nome = categoria.Nome;
                categoriaDb.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(categoriaDb);
            });

            //Apagando Infornações da tabela
            app.MapDelete("/categoria/{id:int}", async (int id, AppDpContext db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                {
                    return Results.NotFound();
                }

                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

        }
    }
}

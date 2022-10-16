using ApiCatalogo.ApiEndpoints;
using ApiCatalogo.AppServicesExtensions;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();
                    
var app = builder.Build();

app.MapAutenticacaoEndpoints();

app.MapCategoriaEndpoints();

app.MapProdudoEndpoints();


var environment = app.Environment;

app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();


app.UseAuthentication();
app.UseAuthorization();
app.Run();
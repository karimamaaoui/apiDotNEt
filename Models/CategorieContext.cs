using System;
using Microsoft.EntityFrameworkCore;
using CoolApi.Models;

namespace CoolApi.Models
{
    public class CategorieContext : DbContext


{
    public CategorieContext(DbContextOptions<CategorieContext> options) : base(options)
    {
    }

    public DbSet<Categorie> Categories { get; set; }


}}
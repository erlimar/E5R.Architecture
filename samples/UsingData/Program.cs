﻿using System;
using E5R.Architecture.Core;

namespace UsingData
{
    using static Console;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var fs = new DefaultFileSystem();
            var uow = new MemoryUnitOfWork(fs);
            var storage = new BlogStorage();
            var module = new BlogDataModule(uow, storage);

            WriteLine("Creating a blogs...");
            module.Blog.Create(new BlogDataModel
            {
                BlogUrl = "https://erlimar.wordpress.com",
                BlogTitle = "Erlimar Silva Campos"
            });

            module.Blog.Create(new BlogDataModel
            {
                BlogUrl = "https://blog.jetbrains.com/dotnet",
                BlogTitle = "JetBrains .NET TOOLS BLOG"
            });

            var db = uow.Session.Get<MemoryDatabase>();

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }

            WriteLine();
            WriteLine("Updating a blog name...");
            module.Blog.Replace(new BlogDataModel
            {
                BlogUrl = "https://erlimar.wordpress.com",
                BlogTitle = "Erlimar Blog's"
            });

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }

            WriteLine();
            WriteLine("Removing a blog...");
            module.Blog.RemoveByUrl("https://blog.jetbrains.com/dotnet");

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }

            uow.SaveWork();
        }
    }
}

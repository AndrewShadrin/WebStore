using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : DbContext
    {
        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
        {

        }
    }
}

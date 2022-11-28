using Asp.NETCoreAPI.Models;
using System;
using System.Collections.Generic;

namespace Asp.NETCoreAPI.Services
{
    public interface IShoppingCartServices
    {
        ShoppingItem Add(ShoppingItem newItem);
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem GetById(Guid id);

    void Remove(Guid id);
    }
}
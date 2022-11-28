using Asp.NETCoreAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Asp.NETCoreAPI.Services
{
    public class ShoppingCartServices: IShoppingCartServices
    {
        public ShoppingItem Add(ShoppingItem newItem) => throw new NotImplementedException();
        public IEnumerable<ShoppingItem> GetAllItems() => throw new NotImplementedException();
        public ShoppingItem GetById(Guid id) => throw new NotImplementedException();
        public void Remove(Guid id) => throw new NotImplementedException();
    }
}

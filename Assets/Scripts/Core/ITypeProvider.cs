using System;
using System.Collections.Generic;

namespace Core
{
    public interface ITypeProvider
    {
        public IEnumerable<Type> GetTypes();
    }
}
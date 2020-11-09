using System;
using System.Collections.Generic;
using System.Text;

namespace SBDataLibrary.Models
{
    public abstract class Entity
    {
        public abstract void Set(params object[] data);
        public abstract string[] GetFields(); 
    }
}

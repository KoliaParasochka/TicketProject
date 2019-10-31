using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IModel<T>
    {
        /// <summary>
        /// Getting model with all properties.
        /// </summary>
        /// <param name="id">model id</param>
        /// <returns>model</returns>
        T GetModel(int id);
    }
}

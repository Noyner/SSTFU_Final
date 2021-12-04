using System;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface ITechBLO
    {
         public Task PrefillDB(int districts_count);
         public Task HandleQueue();
    }
}

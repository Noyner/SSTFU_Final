using System.Threading.Tasks;
using Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SSTFU_LOD_2021.Models;

namespace SSTFU_LOD_2021.Controllers
{
    public class Statistics : Controller
    {

        private IStatsBLO _blo;

        public Statistics(IStatsBLO statsBlo)
        {
            this._blo = statsBlo;
        }
        // GET
        public async Task<IActionResult> Tables()
        {
            var model = await _blo.GetStats();
            return View(model);
        }

        public async Task<IActionResult> Dashboard()
        {
            var model = new DashboardViewModel()
            {
                VerticalDashboardDataset = await _blo.GetVerticalDashboard()
            };
            return View(model);
        }
    }
}
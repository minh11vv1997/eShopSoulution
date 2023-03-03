using eShopSolution.ViewModels.SlideModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Slides
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAllSile();
    }
}
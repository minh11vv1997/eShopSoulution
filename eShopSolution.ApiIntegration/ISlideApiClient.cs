using eShopSolution.ViewModels.SlideModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<List<SlideViewModel>> GetAllSlide();
    }
}
using eShopSolution.Application.Catalog.Services.Common;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.SlideModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Slides
{
    public class SlideService : ISlideService
    {
        private readonly EShopDbContext _context;
        private readonly IStoregeService _storegeService;

        public SlideService(EShopDbContext context, IStoregeService storegeService)
        {
            _context = context;
            _storegeService = storegeService;
        }

        public async Task<List<SlideViewModel>> GetAllSile()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                    .Select(x => new SlideViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Url = x.Url,
                        Image = x.Image
                    }).ToListAsync();

            return slides;
        }
    }
}
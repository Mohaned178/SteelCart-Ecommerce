using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class RatingRepositry : IRating
    {
        private readonly AppDbContext _context;

        public RatingRepositry(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRatingAsync(RatingDTO ratingDTO, string? email = null)
        {
           
            var rating = new Rating
            {
                ProductId = ratingDTO.productId,
                Stars = ratingDTO.stars,
                Content = ratingDTO.content
            };

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == ratingDTO.productId);

            if (product != null)
            {
                var ratings = await _context.Ratings
                    .AsNoTracking()
                    .Where(m => m.ProductId == product.Id)
                    .ToListAsync();

                if (ratings.Count > 0)
                {
                    double average = ratings.Average(m => m.Stars);
                    double roundedReview = Math.Round(average * 2, MidpointRounding.AwayFromZero) / 2;
                    product.rating = roundedReview;
                }
                else
                {
                    product.rating = ratingDTO.stars;
                }

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(int productId)
        {
            var ratings = await _context.Ratings
                .AsNoTracking()
                .Where(m => m.ProductId == productId)
                .ToListAsync();

            return ratings.Select(m => new ReturnRatingDTO
            {
                content = m.Content,
                ReviewTime = m.Review,
                stars = m.Stars,
                userName = "Anonymous"
            }).ToList();
        }
    }
}

﻿namespace WebAPI.ViewModels
{
    public class RatingVM
    {
        public string CustomerId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public int? Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public string? RatingImage { get; set; }
    }
}

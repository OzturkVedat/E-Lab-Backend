using System;
using System.Collections.Generic;

namespace E_Lab_Backend.Services
{
    public static class IgReferenceRanges
    {
        private static readonly Dictionary<string, Dictionary<(int MinAge, int MaxAge), (int MinRef, int MaxRef)>> AgeBasedReferenceRanges = new()
        {
            {
                "IgG", new Dictionary<(int, int), (int, int)>
                {
                    { (0, 1), (700, 1300) },  // 0-1 months of age, new born
                    { (1, 4), (280, 750) },    // 1-4 months
                    { (4, 7), (200, 1200) },
                    { (7, 13), (300, 1500) },
                    { (13, 36), (400, 1300) },
                    { (36, 72), (600, 1500) },
                    { (72, int.MaxValue), (639, 1344) }
                }
            },
            {
                "IgA", new Dictionary<(int, int), (int, int)>
                {
                    { (0, 1), (0, 11) },
                    { (1, 4), (6, 50) },
                    { (4, 7), (8, 90) },
                    { (7, 13), (16, 100) },
                    { (13, 36), (20, 230) },
                    { (36, 72), (50, 150) },
                    { (72, int.MaxValue), (70, 312) }
                }
            },
             {
                "IgM", new Dictionary<(int, int), (int, int)>
                {
                    { (0, 1), (5, 30) },
                    { (1, 4), (15, 70) },
                    { (4, 7), (10, 90) },
                    { (7, 13), (25, 115) },
                    { (13, 36), (30, 120) },
                    { (36, 72), (22, 100) },
                    { (72, int.MaxValue), (56, 352) }
                }
            },
              {
                "IgG1", new Dictionary<(int, int), (int, int)>
                {
                    { (-9, 0), (435, 1084) },   // Cord, baby not born
                    { (0, 3), (218, 496) },
                    { (3, 6), (143, 394) },
                    { (6, 9), (190, 388) },
                    { (9, 24), (286, 680) },
                    { (24, 48), (381, 884) },
                    { (48, 72), (292, 816) },
                    { (72, 96), (422, 802) },
                    { (96, 120), (456, 938) },
                    { (120, 144), (456, 952) },
                    { (144, 168), (347, 993) },
                    { (168, int.MaxValue), (422, 1292) }
                }
            },
              {
                "IgG2", new Dictionary<(int, int), (int, int)>
                {
                    { (-9, 0), (143, 453) },   
                    { (0, 3), (40, 167) },
                    { (3, 6), (23, 147) },
                    { (6, 9), (37, 60) },
                    { (9, 24), (30, 327) },
                    { (24, 48), (70, 443) },
                    { (48, 72), (83, 513) },
                    { (72, 96), (113, 480) },
                    { (96, 120), (163, 513) },
                    { (120, 144), (147, 493) },
                    { (144, 168), (140, 440) },
                    { (168, int.MaxValue), (117, 747) }
                }
            },
              {
                "IgG3", new Dictionary<(int, int), (int, int)>
                {
                    { (-9, 0), (27, 146) },
                    { (0, 3), (4, 23) },
                    { (3, 6), (4, 100) },
                    { (6, 9), (12, 62) },
                    { (9, 24), (13, 82) },
                    { (24, 48), (17, 90) },
                    { (48, 72), (8, 111) },
                    { (72, 96), (15, 133) },
                    { (96, 120), (26, 113) },
                    { (120, 144), (12, 179) },
                    { (144, 168), (23, 117) },
                    { (168, int.MaxValue), (41, 129) }
                }
            },
              {
                "IgG4", new Dictionary<(int, int), (int, int)>
                {
                    { (-9, 0), (1, 47) },
                    { (0, 3), (1, 120) },
                    { (3, 6), (1, 120) },
                    { (6, 9), (1 , 120) },
                    { (9, 24), (1 , 120) },
                    { (24, 48), (1 , 120) },
                    { (48, 72), (2, 112) },
                    { (72, 96), (1, 138) },
                    { (96, 120), (1, 95) },
                    { (120, 144), (1, 153) },
                    { (144, 168), (1, 143) },
                    { (168, int.MaxValue), (10, 67) }
                }
            },
        };

        public static (float Min, float Max)? GetReferenceRange(string parameter, int ageInMonths)
        {
            if (ageInMonths < -9 || ageInMonths > 1400) 
                throw new ArgumentOutOfRangeException(nameof(ageInMonths), "Age in months is out of valid range.");

            if (AgeBasedReferenceRanges.TryGetValue(parameter, out var ranges))
            {
                foreach (var range in ranges)
                {
                    if (ageInMonths >= range.Key.MinAge && ageInMonths <= range.Key.MaxAge)
                    {
                        return range.Value;
                    }
                }
            }
            return null;
        }

        public static string DetermineResult(string parameter, float value, int ageInMonths)
        {
            var range = GetReferenceRange(parameter, ageInMonths);

            if (range == null)
                throw new ArgumentException($"Gecersiz parametre, ya da bu yaş icin referans bulunmamakta: {parameter}");

            if (value < range.Value.Min)
                return "Düşük";
            if (value > range.Value.Max)
                return "Yüksek";
            return "Normal";
        }
    }
}

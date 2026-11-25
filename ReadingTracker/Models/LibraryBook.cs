using System.Text.Json.Serialization;

namespace ReadingTracker.Models
{
    internal class LibraryBook
    {
        public string OpenLibraryKey { get; set; } = "";
        public string Title { get; set; } = "";
        public string Authors { get; set; } = "";
        public int FirstPublishYear { get; set; }
        public int CoverId { get; set; }
        public string Status { get; set; } = "Want to Read";
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int Rating { get; set; }               // 0 = not rated, otherwise 1 to 5
        public string Review { get; set; } = "";
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string CustomImagePath { get; set; } = "";

        [JsonIgnore]
        public string CoverUrl => CoverId > 0 ? $"https://covers.openlibrary.org/b/id/{CoverId}-M.jpg" : "";

        [JsonIgnore]
        public string DisplayCover
        {
            get
            {
                if (CustomImagePath != "" && File.Exists(CustomImagePath))
                    return CustomImagePath;
                return CoverUrl;
            }
        }

        [JsonIgnore]
        public double ProgressFraction
        {
            get
            {
                if (TotalPages <= 0)
                    return 0;
                return Math.Min(1.0, (double)CurrentPage / TotalPages);
            }
        }

        [JsonIgnore]
        public bool ShowProgress => Status == "Currently Reading" && TotalPages > 0;

        [JsonIgnore]
        public string StatusLine
        {
            get
            {
                if (ShowProgress)
                    return $"Reading: page {CurrentPage} of {TotalPages} ({ProgressFraction * 100:F0}%)";
                return Status;
            }
        }

        [JsonIgnore]
        public string RatingText => Rating > 0 ? $"Rating: {Rating}/5" : "Not rated";

        // Cover size comes from the Cover size setting
       /* [JsonIgnore]
        public double CoverWidth
        {
            get
            {
                if (App.Settings.CoverSize == "Small")
                    return 48;
                if (App.Settings.CoverSize == "Large")
                    return 96;
                return 64;
            }
        }*/

     /*   [JsonIgnore]
        public double CoverHeight => CoverWidth * 1.5;*/

        // Changes the status and keeps the dates consistent
        public void SetStatus(string newStatus)
        {
            Status = newStatus;

            if (newStatus == "Want to Read")
            {
                DateStarted = null;
                DateCompleted = null;
            }
            else if (newStatus == "Currently Reading")
            {
                if (DateStarted == null)
                    DateStarted = DateTime.Now;
                DateCompleted = null;
            }
            else if (newStatus == "Completed")
            {
                if (DateStarted == null)
                    DateStarted = DateTime.Now;
                if (DateCompleted == null)
                    DateCompleted = DateTime.Now;
                if (TotalPages > 0)
                    CurrentPage = TotalPages;
            }
        }

        public LibraryBook Clone()
        {
            return new LibraryBook
            {
                OpenLibraryKey = OpenLibraryKey,
                Title = Title,
                Authors = Authors,
                FirstPublishYear = FirstPublishYear,
                CoverId = CoverId,
                Status = Status,
                CurrentPage = CurrentPage,
                TotalPages = TotalPages,
                Rating = Rating,
                Review = Review,
                DateAdded = DateAdded,
                DateStarted = DateStarted,
                DateCompleted = DateCompleted,
                CustomImagePath = CustomImagePath
            };
        }

        public void CopyFrom(LibraryBook other)
        {
            Title = other.Title;
            Authors = other.Authors;
            FirstPublishYear = other.FirstPublishYear;
            CoverId = other.CoverId;
            Status = other.Status;
            CurrentPage = other.CurrentPage;
            TotalPages = other.TotalPages;
            Rating = other.Rating;
            Review = other.Review;
            DateAdded = other.DateAdded;
            DateStarted = other.DateStarted;
            DateCompleted = other.DateCompleted;
            CustomImagePath = other.CustomImagePath;
        }
    }
}

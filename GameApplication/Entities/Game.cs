using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;


namespace GameApplication.Entities;

public class Game : Entity
{
    //public string Id { get; set; }
    public string Title { get; private  set; }
    public string Developer { get; private  set; }
    public string Editor { get; private  set; }
    public string Date { get; private  set; }
    public int PositiveReviews { get; private set; } = 0;
    public List<string> PositiveReviewsList { get; private set; } = new List<string>();
    public int NegativeReviews { get; private set; } = 0;
    public List<string> NegativeReviewsList { get; private set; } = new List<string>();
    public string TimeItWasEdited { get; private  set; }

    public static bool DateCheck(string Date)
    {
        bool status = false;
        DateTime dateValue;
        DateTime startDate = new DateTime(2000, 1, 1);
        DateTime endDate = new DateTime(2100, 12, 31);
        string[] formats=
        {
            "M/d/yyyy", "M/d/yyyy", "MM/dd/yyyy", "M/d/yyyy", "M/d/yyyy", "M/d/yyyy", "M/d/yyyy", "M/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy",
            "M.d.yyyy", "M.d.yyyy", "MM.dd.yyyy", "M.d.yyyy", "M.d.yyyy", "M.d.yyyy", "M.d.yyyy", "M.d.yyyy", "MM.dd.yyyy", "M.dd.yyyy",
            "M d yyyy", "M d yyyy", "MM dd yyyy", "M d yyyy", "M d yyyy", "M d yyyy", "M d yyyy", "M d yyyy", "MM dd yyyy", "M dd yyyy",
            "M-d-yyyy", "M-d-yyyy", "MM-dd-yyyy", "M-d-yyyy", "M-d-yyyy", "M-d-yyyy", "M-d-yyyy", "M-d-yyyy", "MM-dd-yyyy", "M-dd-yyyy"
        };

        DateTime.TryParseExact(Date, formats,
            new CultureInfo("ro-RO"),
            DateTimeStyles.None,
            out dateValue);
        if (dateValue < startDate || dateValue > endDate)
        {
            return false;
        }
        if (DateTime.IsLeapYear(dateValue.Year))  return dateValue.Month != 2 || (dateValue.Day >= 1 && dateValue.Day <= 29);
        return dateValue.Month != 2 || (dateValue.Day >= 1 && dateValue.Day <= 28);
    }
    
    private Game(){}

    public static Game Create(string title, string developer, string editor, string date)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title can't be empty");
        if (string.IsNullOrWhiteSpace(developer))
            throw new Exception("Developer Name can't be empty");
        if (string.IsNullOrWhiteSpace(editor))
            throw new Exception("Editor Name can't be empty");
        if (DateCheck(date))
            throw new Exception("Date is incorrect, Date must be of type : dd/mm/yyyy");
        
        return new Game
        {
            //Id = Guid.NewGuid().ToString(),
            Title = title,
            Developer = developer,
            Editor = editor,
            Date = date,
            TimeItWasEdited = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")
        };
    }
    
    public void SetTitleName(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title can't be empty");

        Title = title;
    }

    public void SetDeveloperName(string developer)
    {
        if (string.IsNullOrWhiteSpace(developer))
            throw new Exception("Developer Name can't be empty");

        Developer = developer;
    }
    
    public void SetEditorName(string editor)
    {
        if (string.IsNullOrWhiteSpace(editor))
            throw new Exception("Editor Name can't be empty");

        Editor = editor;
    }
    
    public void SetDate(string date)
    {
        if (DateCheck(date))
            throw new Exception("Date is incorrect");

        Date = date;
    }

    public void AddPositiveReview(string review)
    {
        if (string.IsNullOrWhiteSpace(review))
            throw new Exception("Positive review can't be empty");
        
        PositiveReviewsList.Add(review);
        PositiveReviews++;
    }
    
    public void AddNegativeReview(string review)
    {
        if (string.IsNullOrWhiteSpace(review))
            throw new Exception("Negative review can't be empty");
        
        NegativeReviewsList.Add(review);
        NegativeReviews++;
    }
}
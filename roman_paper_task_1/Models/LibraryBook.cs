using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace roman_paper_task_1.Models
{
    public partial class LibraryBook : ObservableObject
    {
        [ObservableProperty] private string inventoryCode;

        public string Title { get; }
        public IReadOnlyList<string> Authors { get; }
        public int PageCount { get; }
        public string Category { get; }

        [ObservableProperty] private BookState state;

        public LibraryBook(string inventoryCode, string title, List<string> authors,
            int pageCount, string category, BookState state)
        {
            InventoryCode = inventoryCode;
            Title = title;
            Authors = new ReadOnlyCollection<string>(authors);
            PageCount = pageCount;
            Category = category;
            State = state;
        }

        public void ChangeState(BookState newState)
        {
            State = newState;
        }

        public void ChangeInventoryCode(string newCode)
        {
            InventoryCode = newCode;
        }

        public override string ToString()
        {
            return $"{Title} - {string.Join(", ", Authors)}";
        }
    }
}
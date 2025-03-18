using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using roman_paper_task_1.Converters;
using roman_paper_task_1.Models;

namespace roman_paper_task_1.ViewModels;

public partial class LibraryBookViewModel : ViewModelBase
{
    [ObservableProperty] private LibraryBook _book;

    [ObservableProperty] private ObservableCollection<LibraryBook> _books = new();

    [ObservableProperty] private LibraryBook _selectedBook;

    [ObservableProperty] private string _newInventoryCode;

    [ObservableProperty] private BookState _newState;

    [ObservableProperty] private string _newTitle;

    [ObservableProperty] private string _newAuthors;

    [ObservableProperty] private int _newPageCount;

    [ObservableProperty] private string _newCategory;

    [ObservableProperty] private ObservableCollection<EnumItem> _bookStates;

    
    [ObservableProperty]
    private EnumItem _selectedEnumItem;

    partial void OnSelectedEnumItemChanged(EnumItem value)
    {
        if (value != null)
            NewState = (BookState)value.Value;
    }

    
    
    public LibraryBookViewModel()
    {
        BookStates = new ObservableCollection<EnumItem>(
            Enum.GetValues<BookState>().Select(state => new EnumItem
            {
                Value = state,
                Description = GetEnumDescription(state)
            }));

        // Установка первого значения по умолчанию
        if (BookStates.Count > 0)
        {
            NewState = (BookState)BookStates[0].Value;
        }

        // Инициализация примерными данными
        Books.Add(new LibraryBook("A001", "Война и мир", new List<string> { "Лев Толстой" }, 1225,
            "Классическая литература", BookState.InStock));
        Books.Add(new LibraryBook("B002", "Преступление и наказание", new List<string> { "Фёдор Достоевский" }, 576,
            "Классическая литература", BookState.CheckedOut));
        Books.Add(new LibraryBook("C003", "Мастер и Маргарита", new List<string> { "Михаил Булгаков" }, 480,
            "Классическая литература", BookState.UnderRestoration));
    }

    [RelayCommand]
    private void ChangeBookState()
    {
        SelectedBook.ChangeState(NewState);
        OnPropertyChanged(nameof(SelectedBook));
    }

    [RelayCommand]
    private void ChangeBookInventoryCode()
    {
        if (!string.IsNullOrWhiteSpace(NewInventoryCode))
        {
            SelectedBook.ChangeInventoryCode(NewInventoryCode);
            OnPropertyChanged(nameof(SelectedBook));
            NewInventoryCode = string.Empty;
        }
    }

    [RelayCommand]
    private void AddNewBook()
    {
        if (!string.IsNullOrWhiteSpace(NewTitle) && !string.IsNullOrWhiteSpace(NewAuthors) &&
            !string.IsNullOrWhiteSpace(NewCategory) && !string.IsNullOrWhiteSpace(NewInventoryCode))
        {
            var authors = NewAuthors.Split(',').Select(a => a.Trim()).ToList();
            var newBook = new LibraryBook(
                NewInventoryCode,
                NewTitle,
                authors,
                NewPageCount,
                NewCategory,
                BookState.InStock
            );

            Books.Add(newBook);

            // Очистка полей
            NewTitle = string.Empty;
            NewAuthors = string.Empty;
            NewCategory = string.Empty;
            NewInventoryCode = string.Empty;
            NewPageCount = 0;
        }
    }

    private string GetEnumDescription(Enum value)
    {
        if (value == null) return string.Empty;

        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute != null ? attribute.Description : value.ToString();
    }
}
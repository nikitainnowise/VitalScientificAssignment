using System.Windows.Input;
using Assignment.Application.Common.Exceptions;
using Assignment.Application.Common.Interfaces;
using Assignment.Application.TodoItems.Commands.DoneTodoItem;
using Assignment.Application.TodoLists.Queries.GetTodos;
using Assignment.Domain.Interfaces;
using Caliburn.Micro;
using MediatR;
using CacheKeys = Assignment.Domain.Constants.CacheKeys;

namespace Assignment.UI;
internal class TodoManagmentViewModel : Screen
{
    private readonly ISender _sender;
    private readonly IWindowManager _windowManager;
    private readonly IExceptionViewer _exceptionViewer;
    private readonly ICacheService _cacheService;

    private IList<TodoListDto> todoLists;
    public IList<TodoListDto> TodoLists
    {
        get
        {
            return todoLists;
        }
        set
        {
            todoLists = value;
            NotifyOfPropertyChange(() => TodoLists);
        }
    }

    private TodoListDto _selectedTodoList;
    public TodoListDto SelectedTodoList
    {
        get => _selectedTodoList;
        set
        {
            _selectedTodoList = value;
            NotifyOfPropertyChange(() => SelectedTodoList);
        }
    }

    private TodoItemDto _selectedItem;
    public TodoItemDto SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            NotifyOfPropertyChange(() => SelectedItem);
        }
    }

    public ICommand AddTodoListCommand { get; private set; }
    public ICommand AddTodoItemCommand { get; private set; }
    public ICommand DoneTodoItemCommand { get; private set; }

    public TodoManagmentViewModel(ISender sender,
        IWindowManager windowManager,
        IExceptionViewer exceptionViewer,
        ICacheService cacheService)
    {
        _sender = sender;
        _windowManager = windowManager;
        _exceptionViewer = exceptionViewer;
        _cacheService = cacheService;

        Initialize();
    }

    private async void Initialize()
    {
        await RefereshTodoLists();

        AddTodoListCommand = new RelayCommand(AddTodoList);
        AddTodoItemCommand = new RelayCommand(AddTodoItem);
        DoneTodoItemCommand = new RelayCommand(DoneTodoItem);
    }

    private async Task RefereshTodoLists()
    {
        var cachedToDoLists = await _cacheService.GetAsync<List<TodoListDto>>(CacheKeys.TodoListsKey);
        var selectedListId = SelectedTodoList?.Id;

        if (cachedToDoLists == null)
        {
            TodoLists = await _sender.Send(new GetTodosQuery());
            await _cacheService.SetAsync(CacheKeys.TodoListsKey, TodoLists.ToList());
        }
        else
        {
            TodoLists = cachedToDoLists;
        }

        if (selectedListId.HasValue && selectedListId.Value > 0)
        {
            SelectedTodoList = TodoLists.FirstOrDefault(list => list.Id == selectedListId.Value);
        }
    }

    private async void AddTodoList(object obj)
    {
        try
        {
            var todoList = new TodoListViewModel(_sender, _exceptionViewer);
            var isSuccess = await _windowManager.ShowDialogAsync(todoList);

            if (isSuccess.HasValue && isSuccess.Value)
            {
                await _cacheService.RemoveAsync(CacheKeys.TodoListsKey);
                await RefereshTodoLists();
            }
        }
        catch (ValidationException ex)
        {
            await _exceptionViewer.ShowErrors(ex);
        }
    }

    private async void AddTodoItem(object obj)
    {
        try
        {
            var todoItem = new TodoItemViewModel(_exceptionViewer, _sender, SelectedTodoList.Id);
            var isSuccess = await _windowManager.ShowDialogAsync(todoItem);

            if (isSuccess.HasValue && isSuccess.Value)
            {
                await _cacheService.RemoveAsync(CacheKeys.TodoListsKey);
                await RefereshTodoLists();
            }
        }
        catch (ValidationException ex)
        {
            await _exceptionViewer.ShowErrors(ex);
        }
    }

    private async void DoneTodoItem(object obj)
    {
        try
        {
            await _sender.Send(new DoneTodoItemCommand(SelectedItem.Id));
            await _cacheService.RemoveAsync(CacheKeys.TodoListsKey);
            await RefereshTodoLists();
        }
        catch (ValidationException ex)
        {
            await _exceptionViewer.ShowErrors(ex);
        }
    }
}
